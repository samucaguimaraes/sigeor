using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucNoticias : System.Web.UI.UserControl
{
    bool _projetos;
    public bool projetos
    {
        get { return _projetos; }
        set { _projetos = value; }
    }
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (projetos)
            {
                lblTitle.Text = "Últimas Notícias";
            }
            else
            {
                lblTitle.Text = "Notícias";
                GridView1.Columns[2].Visible = false;
            }
            this.Panel1.Visible = false;
            cod.Value = "0";
            FormBind();
            ViewState["sentido"] = "ASC";
            ViewState["campo"] = "dt_data";
            GridBind();
            ViewState["pesquisa"] = "";
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Ascending);
        }
        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
        }
    }
    private void Exibir()
    {
        this.Panel1.Visible = true;
        this.GridView1.Visible = false;
        this.PanelAdd.Visible = false;
        txtds_noticia.Text = "";
        txtdt_data.Text = "";
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true;
        txtds_noticia.Text = "";
        txtdt_data.Text = "";
        GridBind();
    }

    private void GridBind()
    {
        t23_noticia t23 = new t23_noticia();
        {
            
            string query = "";
            if (pb.fl_admin() || pb.fl_visitante() || pb.fl_estrategico())
            {
                //query = " where t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + " and fl_ativa=1";
            }
            else //se usuário sem perfil e diferente de visitante
            {
                if (pb.cd_parceiro() != 0) //parceiro
                {
                    query = " and t03_cd_projeto in " +
                        "(select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade in " +
                        "(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")) ";
                }
                else //administrador parceiro
                {
                    query = " and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=" + pb.cd_entidade() + ")";
                }

            }
            if (projetos)
            {
                t23.order = query + "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
                GridView1.DataSource = t23.ListProjetos();
                PanelAdd.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }
            else
            {
                t23.t03_cd_projeto = pb.cd_projeto();
                t23.order = "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
                GridView1.DataSource = t23.List();
            }
            GridView1.DataBind();
        }
    }
    private void Retrieve()
    {
        PanelArquivo.Visible = false;
        PanelOpcao.Visible = true;

        t23_noticia t23 = new t23_noticia();
        {
            t23.t23_cd_noticia = Int32.Parse(cod.Value);
            t23.Retrieve();
            if (t23.Found)
            {
                txtds_noticia.Text = t23.ds_noticia;
                txtdt_data.Text = t23.dt_data.ToShortDateString();
            }
        }
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t23_noticia t23 = new t23_noticia();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            uploadArquivo up = new uploadArquivo();
            {
                if (funm_arquivo.Visible)
                {
                    up.pasta = "Documentos";
                    up.nomeinicial = "noticia";
                    up.fu = funm_arquivo;
                    result = up.Save();
                    msg = pb.Message(up.msg, "erro");

                    if (!result)
                    {
                        up.nomearquivo = "";
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
                
                if (result)
                {
                    t23.t03_cd_projeto = pb.cd_projeto();
                    t23.nm_noticia = "";
                    t23.ds_noticia = pb.ReplaceAspas(txtds_noticia.Text);
                    t23.dt_data = DateTime.Parse(txtdt_data.Text);
                    t23.dt_cadastro = DateTime.Now;
                    t23.dt_alterado = DateTime.Now;
                    
                    if (!(erro))
                    {
                        if (cod.Value != "0")
                        {
                            t23.t23_cd_noticia = Int32.Parse(cod.Value);
                            if (funm_arquivo.Visible)
                            {
                                t23.order = ", nm_arquivo='" + up.nomearquivo + "' ";
                            }
                            result = t23.Update();
                            msg = pb.Message("Alteração realizada com sucesso!", "ok");
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t23_noticia", "update", cod.Value);
                        }
                        else
                        {
                            t23.nm_arquivo = up.nomearquivo;
                            result = t23.Save();
                            msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t23_noticia", "insert", t23.ds_noticia);
                        }

                        if (result)
                        {
                            Ocultar();
                            GridBind();
                            cod.Value = "0";
                        }
                    }
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                int cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
                switch (e.CommandName)
                {
                    case "Selecionar":
                        t23_noticia t23 = new t23_noticia();
                        {
                            t23.t23_cd_noticia = cd;
                            t23.Retrieve();
                            if (t23.Found)
                            {
                                Session["cd_projeto"] = t23.t03_cd_projeto;
                                Response.Redirect("Arvore.aspx");
                            }
                        }
                        break;
                    case "Editar":
                        Exibir();
                        this.lblHeader.Text = "Alteração";
                        this.btnAcao.Text = "Alterar";
                        cod.Value = cd.ToString();
                        Retrieve();

                        break;
                    case "Deletar":
                        t23 = new t23_noticia();
                        {
                            t23.t23_cd_noticia = cd;
                            t23.Delete();
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t23_noticia", "delete", t23.t23_cd_noticia.ToString());
                        }
                        GridBind();
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        break;
                }
            }
        }
        catch { }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = ((DataRowView)e.Row.DataItem);

            //Adicionar mensagem de alterta antes da exclusão
            ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
            if (btn != null)
            {
                btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
            }

            if (drv["nm_arquivo"].ToString().Length > 1)
            {
                e.Row.Cells[4].Text = "<a href='Documentos/" + drv["nm_arquivo"] + "' title='download do arquivo' target='_blank' ><img src='images/ico_download.gif'/></a>";
            }

            e.Row.Cells[3].Text = pb.ReplaceNewLines(drv["ds_noticia"].ToString());

        }
    }



    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";
        PanelArquivo.Visible = true;
        PanelOpcao.Visible = false;
        Exibir();
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }


    protected void FormBind()
    {
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection sd;
        if (ViewState["sentido"].ToString() == "ASC")
        {
            ViewState["sentido"] = "DESC";
            sd = SortDirection.Descending;
        }
        else
        {
            ViewState["sentido"] = "ASC";
            sd = SortDirection.Ascending;
        }


        pb.AppendSortOrderImageToGridHeader(sd, e.SortExpression, this.GridView1);
        ViewState["campo"] = e.SortExpression;
        GridBind();
    }

    protected void rblArquivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblArquivo.SelectedValue == "N")
        {
            PanelArquivo.Visible = false;
        }
        else
        {
            PanelArquivo.Visible = true;
        }
    }
}

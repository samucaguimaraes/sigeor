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

public partial class ucRestricoes : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTitle.Text = "Restrições";
            this.Panel1.Visible = false;
            cod.Value = "0";
            FormBind();
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "dt_limite";
            GridBind();
            ViewState["pesquisa"] = "";
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
        }

        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
        }
    }
    private void Exibir()
    {
        this.Panel1.Visible = true;
        this.GridView1.Visible = false;
        this.PanelAdd.Visible = false;
        txtds_restricao.Text = "";
        txtds_medida.Text = "";
        txtdt_limite.Text = "";
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true;
        txtds_restricao.Text = "";
        txtds_medida.Text = "";
        txtdt_limite.Text = "";
        GridBind();
    }

    private void GridBind()
    {
        t07_restricao t07 = new t07_restricao();
        {
            t07.t03_cd_projeto = pb.cd_projeto();
            t07.order = "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
            GridView1.DataSource = t07.List();
            GridView1.DataBind();
        }
    }
    private void Retrieve()
    {
        t07_restricao t07 = new t07_restricao();
        {
            t07.t07_cd_restricao = Int32.Parse(cod.Value);
            t07.Retrieve();
            if (t07.Found)
            {
                
                txtds_restricao.Text = t07.ds_restricao;
                txtds_medida.Text = t07.ds_medida;
                txtdt_limite.Text = t07.dt_limite.ToShortDateString();
                ddlt08_cd_acao.ClearSelection();
                t29_acaorestricao t29 = new t29_acaorestricao();
                {
                    t29.t07_cd_restricao = t07.t07_cd_restricao;
                    t29.Retrieve();
                    if (t29.Found)
                    {
                        ListItem li = ddlt08_cd_acao.Items.FindByValue(t29.t08_cd_acao.ToString());
                        if (li != null)
                            li.Selected = true;
                    }
                    else
                    {
                        cbProjeto.Checked = true;
                    }
                }
            }
        }
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t07_restricao t07 = new t07_restricao();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            t07.t03_cd_projeto = pb.cd_projeto();
            t07.ds_restricao = pb.ReplaceAspas(txtds_restricao.Text);
            t07.ds_medida = pb.ReplaceAspas(txtds_medida.Text);
            t07.dt_limite = DateTime.Parse(txtdt_limite.Text);
            t07.dt_cadastro = DateTime.Now;
            t07.dt_alterado = DateTime.Now;

            if ((ddlt08_cd_acao.SelectedValue == "") && (!cbProjeto.Checked))
            {
                erro = true;
                msg = pb.Message("É necessário selecionar uma ação ou marcar a opção Restrição vinculada ao projeto, antes de continuar.", "erro");
                btnAcao.Focus();
            }
            else if ((ddlt08_cd_acao.SelectedValue != "") && (cbProjeto.Checked))
            {
                erro = true;
                msg = pb.Message("Favor escolher somente uma das opções: Ação relacionada ou Restrição vinculada ao projeto, antes de continuar.", "erro");
                btnAcao.Focus();
            }
            
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t07.t07_cd_restricao = Int32.Parse(cod.Value);
                    result = t07.Update();
                    msg = "Alteração realizada com sucesso!";
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t07_restricao", "update", cod.Value);
                }
                else
                {
                    result = t07.Save();
                    msg = "Cadastro realizado com sucesso!";
                    pb.criarEmail(pb.cd_projeto(), txtds_restricao.Text);
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t07_restricao", "insert", t07.ds_restricao);
                }

                if (result)
                {
                    if (cod.Value == "0")
                    {
                        t07.RetrieveCod();
                        if (t07.Found) cod.Value = t07.t07_cd_restricao.ToString();
                    }
                    if (!cbProjeto.Checked)
                    {
                        t29_acaorestricao t29 = new t29_acaorestricao();
                        {
                            t29.t07_cd_restricao = Int32.Parse(cod.Value);
                            t29.t08_cd_acao = Int32.Parse(ddlt08_cd_acao.SelectedValue);
                            t29.Delete();
                            t29.Save();
                        }
                    }
                }

                Ocultar();
                GridBind();
                cod.Value = "0";
                Response.Redirect("Gerenciamento.aspx?msg=" + msg);

            }
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        bool redirect = false;
        string msg = "";
        try
        {
            GridView gv = (GridView)sender;
            int cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
            switch (e.CommandName.Trim())
            {
                case "Superada":
                    t07_restricao t07 = new t07_restricao();
                    {
                        t07.t07_cd_restricao = cd;
                        t07.dt_superada = DateTime.Now;
                        t07.dt_alterado = DateTime.Now;
                        t07.UpdateSuperar();
                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "superou a restrição", "t07_restricao", "update", t07.t07_cd_restricao.ToString());
                    }
                    t29_acaorestricao t29 = new t29_acaorestricao();
                    {
                        t29.t07_cd_restricao = cd;
                        t29.Delete();
                    }
                    GridBind();
                    msg = "Restrição superada com sucesso!";

                    break;

                case "Selecionar":
                    Context.Items["t07_cd_restricao"] = cd.ToString();
                    Server.Transfer("~/Restricao.aspx", false);
                    break;

                case "Editar":
                    Exibir();
                    this.lblHeader.Text = "Alteração";
                    this.btnAcao.Text = "Alterar";
                    btnAcao.Focus();
                    cod.Value = cd.ToString();
                    Retrieve();

                    break;
                case "Deletar":
                    t07 = new t07_restricao();
                    {
                        t07.t07_cd_restricao = cd;
                        t07.Delete();
                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t07_restricao", "delete", t07.t07_cd_restricao.ToString());
                    }
                    t29 = new t29_acaorestricao();
                    {
                        t29.t07_cd_restricao = cd;
                        t29.Delete();
                    }
                    GridBind();
                    msg = "Exclusão realizada com sucesso!";
                    
                    redirect = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message(ex.Message, "erro");
            lblMsg.Visible = true;
        }

        if (msg.Length > 1)
        {
            lblMsg.Text = pb.Message(msg, "ok");
            lblMsg.Visible = true;
        }
        if (redirect)
        {
            Response.Redirect("Gerenciamento.aspx?msg=" + msg);
        }

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
            btn = (ImageButton)e.Row.Cells[2].Controls[0];
            if (btn != null)
            {
                btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja superar?') == false) return false;");
            }
        }
    }

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";
        btnAcao.Focus();
        FormBind();
        Exibir();
    }


    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }


    protected void FormBind()
    {
        //FormBind()
        t08_acao t08 = new t08_acao();
        {
            t08.t03_cd_projeto = pb.cd_projeto();
            t08.order = "order by nm_acao";
            ddlt08_cd_acao.DataSource = t08.List();
            ddlt08_cd_acao.DataTextField = "nm_acao";
            ddlt08_cd_acao.DataValueField = "t08_cd_acao";
            ddlt08_cd_acao.DataBind();
            pb.AddEmptyItem(ddlt08_cd_acao, "Selecione");
        }
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



}

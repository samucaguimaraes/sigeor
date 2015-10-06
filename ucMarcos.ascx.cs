using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucMarcos : System.Web.UI.UserControl
{
    
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        //txtnu_esforco.Attributes.Add("onkeydown", "return numeros(event)");
        DATA_ATUAL.ValueToCompare = DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
            lblTitle.Text = "Marcos Críticos";
            this.Panel1.Visible = false;
            cod.Value = "0";
            FormBind();
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "dt_prevista";
            GridBind();
            ViewState["pesquisa"] = "";
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
        }

        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
            btnDes.Visible = false;
        }
    }
    private void Exibir()
    {
        this.Panel1.Visible = true;
        this.GridView1.Visible = false;
        lblmsg_mc.Visible = false;
        this.PanelAdd.Visible = false;
        txtnu_esforco.Text = "";
        txtds_marco.Text = "";
        txtdt_prevista.Text = "";
        txtdt_realizada.Text = "";
        txtds_comentario.Text = "";
        txtds_marco.ReadOnly = false;
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        lblmsg_mc.Visible = true;
        this.PanelAdd.Visible = true;
        txtnu_esforco.Text = "";
        txtds_marco.Text = "";
        txtdt_prevista.Text = "";
        txtdt_realizada.Text = "";
        txtds_comentario.Text = "";
        txtds_marco.ReadOnly = false;
        GridBind();
    }

    private void GridBind()
    {
        t09_marco t09 = new t09_marco();
        {
            int nu_esforco = 0;
            int i = 0;
            t09.t08_cd_acao  = pb.cd_acao();
            t09.order = "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
            GridView1.DataSource = t09.List();
            GridView1.DataBind();
            if (pb.fl_gerente())
            {
                foreach (DataRow dr in t09.List().Tables[0].Rows)
                {
                    i++;
                    nu_esforco += Int32.Parse(dr["nu_esforco"].ToString());

                    if (dr["dt_realizada"] != null)
                    {
                        btnDes.Visible = true;
                    }
                    else
                    {
                        btnDes.Visible = false;
                    }
                }
                if (i > 0)
                {
                    if (nu_esforco != 100)
                    {
                      //LEVI  lblmsg_mc.Text = "Atenção: Percentual de esforço do(s) Marco(s) Crítico(s) é de: " + nu_esforco.ToString() + "% e deveria ser 100%.";
                        //lblmsg_mc.Font.Bold = true;
                        lblmsg_mc.ForeColor = System.Drawing.Color.Brown;
                    }
                    else
                    {
                        lblmsg_mc.Text = "";
                    }
                }
            }
        }
    }
    private void Retrieve()
    {
        trReal.Visible = true;
        t09_marco t09 = new t09_marco();
        {
            t09.t09_cd_marco = Int32.Parse(cod.Value);
            t09.Retrieve();
            if (t09.Found)
            {
                txtnu_esforco.Text = t09.nu_esforco.ToString();
                txtds_marco.Text = t09.ds_marco;
                txtdt_prevista.Text = t09.dt_prevista.ToShortDateString();
                if (t09.dt_realizada.ToShortDateString() != "1/1/0001")
                {
                txtdt_realizada.Text = t09.dt_realizada.ToShortDateString();
                }
                txtds_comentario.Text = t09.ds_comentario;

                if (t09.fl_original)
                {
                    txtds_marco.ReadOnly = true;
                }
                else
                {
                    txtds_marco.ReadOnly = false;
                }
            }
        }
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t09_marco t09 = new t09_marco();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            t09.t08_cd_acao = pb.cd_acao();
            //t09.nu_esforco = Int32.Parse(txtnu_esforco.Text);
            t09.ds_marco = pb.ReplaceAspas(txtds_marco.Text);
            t09.dt_prevista = DateTime.Parse(txtdt_prevista.Text);
            //t09.dt_original = DateTime.Parse(txtdt_prevista.Text);
            t09.ds_comentario = txtds_comentario.Text;
            t09.fl_status = "G";
            t09.dt_cadastro = DateTime.Now;
            t09.dt_alterado = DateTime.Now;
            t09.fl_original = false;
            t08_acao t08 = new t08_acao();
            {
                t08.t08_cd_acao = pb.cd_acao();
                t08.Retrieve();
                if (t08.Found)
                {
                    if ((t09.dt_prevista < t08.dt_inicio) || (t09.dt_prevista > t08.dt_fim))
                    {
                        erro = true;
                        msg = pb.Message("As Data Prevista do Marco Crítico deve estar entre as " +
                            "datas de início (" + t08.dt_inicio.ToShortDateString() + ") " +
                            "e término (" + t08.dt_fim.ToShortDateString() + ") da Ação!", "erro");
                    }
                }
            }


            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t09.t09_cd_marco = Int32.Parse(cod.Value);
                    if (txtdt_realizada.Text != "")
                    {
                        t09.order = ", dt_realizada='" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtdt_realizada.Text)) + "', fl_status = 'B'";
                        
                    }
                    else
                    {
                        if (t09.dt_prevista >= DateTime.Today)
                        {
                            //t09.fl_status = "G";
                            t09.order = ", fl_status = 'G'";
                        }
                        else
                        {
                            //t09.fl_status = "R";
                            t09.order = ", fl_status = 'R'";
                        }
                    }
                    result = t09.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t09_marco", "update", cod.Value);
                }
                else
                {
                    if (t09.dt_prevista < DateTime.Today)
                    {
                        t09.fl_status = "R";
                    }

                    result = t09.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t09_marco", "insert", t09.ds_marco);
                }

                if (result)
                {
                    Ocultar();
                    GridBind();
                    cod.Value = "0";
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.CommandName != "Sort")
        {
            int cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    Exibir();
                    this.lblHeader.Text = "Alteração";
                    this.btnAcao.Text = "Alterar";
                    cod.Value = cd.ToString();
                    Retrieve();

                    break;
                case "Deletar":
                    t09_marco t09 = new t09_marco();
                    {
                        t09.t09_cd_marco = cd;
                        t09.Delete();
                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t09_marco", "delete", t09.t09_cd_marco.ToString());
                    }
                    GridBind();
                    lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                    lblMsg.Visible = true;
                    break;
            }
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

            if (drv["fl_status"].ToString() == "R")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }

            if ((bool)drv["fl_original"])
            {
                e.Row.Cells[1].Controls[0].Visible = false;
            }

            if (drv["dt_original"] != DBNull.Value)
            {
                Image img = (Image)e.Row.FindControl("imgOriginal");
                if (img != null)
                {
                    string dt_ori = String.Format("{0:dd/MM/yyyy}", (DateTime)drv["dt_original"]);
                    string dt_pre = String.Format("{0:dd/MM/yyyy}", (DateTime)drv["dt_prevista"]);
                    if (dt_ori != dt_pre)
                    {
                        img.Visible = true;
                        img.ToolTip = "Data Original: " + dt_ori;
                    }
                }
            }
            //
       }
    }

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";
        trReal.Visible = false;
        btnDes.Visible = false;
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

    protected void Desrealizar_Click(object sender, System.EventArgs e)
    {
        bool result = false;
        string msg = "";
        t09_marco t09 = new t09_marco();
        {
            t09.t09_cd_marco = Int32.Parse(cod.Value);
            //t09.UpdateDesrealizar();
            result = t09.UpdateDesrealizar();
            txtdt_realizada.Text = "";
            msg = pb.Message("Alteração realizada com sucesso!", "ok");
            lblMsg.Visible = true;
        }

        if (result)
        {
            pb.UpdateCorBarra();
            //Ocultar();
            GridBind();
        }
    }


}

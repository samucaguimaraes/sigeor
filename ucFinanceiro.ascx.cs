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

public partial class ucFinanceiro : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTitle.Text = "Orgão Responsável";
            this.Panel1.Visible = false;
            cod.Value = "0";
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "dt_limite";
            GridBind();
            ViewState["pesquisa"] = "";
            //GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
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
        ddlt05_cd_parceiro.ClearSelection();
        rblfl_economico.ClearSelection();
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true;
        ddlt05_cd_parceiro.ClearSelection();
        rblfl_economico.ClearSelection();
        GridBind();
    }

    private void GridBind()
    {
        t11_financeiro t11 = new t11_financeiro();
        {
            t11.t08_cd_acao = pb.cd_acao();
            GridView1.DataSource = t11.List();
            GridView1.DataBind();
            
        }
    }
    private void Retrieve()
    {
        trReal.Visible = true;
        t11_financeiro t11 = new t11_financeiro();
        {
            t11.t11_cd_financeiro = Int32.Parse(cod.Value);
            t11.Retrieve();
            if (t11.Found)
            {
                //FormBind(" and t05_cd_parceiro not in (select t05_cd_parceiro from t11_financeiro where t08_cd_acao=" + pb.cd_acao() + " and t05_cd_parceiro<>" + t11.t05_cd_parceiro + ")");
                FormBind("");
                ListItem li = ddlt05_cd_parceiro.Items.FindByValue(t11.t05_cd_parceiro.ToString());
                if (li != null) li.Selected = true;
                li = rblfl_economico.Items.FindByValue(t11.fl_economico.ToString());
                if (li != null) li.Selected = true;

                t08_acao t08 = new t08_acao();
                {
                    t08.t08_cd_acao = pb.cd_acao();
                    t08.Retrieve();
                    if (t08.Found)
                    {
                        t28_vlfinanceiro t28 = new t28_vlfinanceiro();
                        {
                            for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                            {
                                t28.t11_cd_financeiro = t11.t11_cd_financeiro;
                                t28.nu_ano = i;
                                t28.Retrieve();

                                TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                                TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                                TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                                TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());

                                TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                                TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                                TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                                TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());

                                if (t28.Found)
                                {
                                    if (txtvl_p1 != null)
                                    {
                                        txtvl_p1.Text = t28.vl_p1.ToString("N2");
                                        txtvl_p4.Text = t28.vl_p4.ToString("N2");
                                        txtvl_p8.Text = t28.vl_p8.ToString("N2");
                                        txtvl_p12.Text = t28.vl_p12.ToString("N2");
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        txtvl_r1.Text = t28.vl_r1.ToString("N2");
                                        txtvl_r4.Text = t28.vl_r4.ToString("N2");
                                        txtvl_r8.Text = t28.vl_r8.ToString("N2");
                                        txtvl_r12.Text = t28.vl_r12.ToString("N2");
                                    }
                                }
                                else
                                {
                                    if (txtvl_p1 != null)
                                    {
                                        txtvl_p1.Text = "0";
                                        txtvl_p4.Text = "0";
                                        txtvl_p8.Text = "0";
                                        txtvl_p12.Text = "0";
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        txtvl_r1.Text = "0";
                                        txtvl_r4.Text = "0";
                                        txtvl_r8.Text = "0";
                                        txtvl_r12.Text = "0";
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t11_financeiro t11 = new t11_financeiro();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            t11.t08_cd_acao = pb.cd_acao();
            t11.t05_cd_parceiro = Int32.Parse(ddlt05_cd_parceiro.SelectedValue);
            t11.dt_cadastro = DateTime.Now;
            t11.dt_alterado = DateTime.Now;
          // LEVI  t11.fl_economico = bool.Parse(rblfl_economico.SelectedValue);
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t11.t11_cd_financeiro = Int32.Parse(cod.Value);
                    result = t11.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t11_financeiro", "update", cod.Value);
                }
                else
                {
                    result = t11.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t11_financeiro", "insert", t11.nu_ano.ToString());
                }

                if (result)
                {
                    t28_vlfinanceiro t28 = new t28_vlfinanceiro();
                    {
                        t08_acao t08 = new t08_acao();
                        {
                            t08.t08_cd_acao = pb.cd_acao();
                            t08.Retrieve();
                            if (t08.Found)
                            {
                                if (trReal.Visible)
                                {
                                    t28.t11_cd_financeiro = Int32.Parse(cod.Value.ToString());
                                }
                                else
                                {
                                    t11.RetrieveCod();
                                    if (t11.Found) t28.t11_cd_financeiro = t11.t11_cd_financeiro;
                                }
                                t28.Delete();
                                for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                                {
                                    TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                                    TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                                    TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                                    TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());

                                    TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                                    TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                                    TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                                    TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());

                                    if (txtvl_p1 != null)
                                    {
                                        if (txtvl_p1.Text == "") txtvl_p1.Text = "0";
                                        if (txtvl_p4.Text == "") txtvl_p4.Text = "0";
                                        if (txtvl_p8.Text == "") txtvl_p8.Text = "0";
                                        if (txtvl_p12.Text == "") txtvl_p12.Text = "0";
                                        
                                        t28.nu_ano = i;
                                        t28.vl_p1 = Decimal.Parse(txtvl_p1.Text);
                                        t28.vl_p4 = Decimal.Parse(txtvl_p4.Text);
                                        t28.vl_p8 = Decimal.Parse(txtvl_p8.Text);
                                        t28.vl_p12 = Decimal.Parse(txtvl_p12.Text);
                                        if (trReal.Visible)
                                        {
                                            if (txtvl_r1.Text == "") txtvl_r1.Text = "0";
                                            if (txtvl_r4.Text == "") txtvl_r4.Text = "0";
                                            if (txtvl_r8.Text == "") txtvl_r8.Text = "0";
                                            if (txtvl_r12.Text == "") txtvl_r12.Text = "0";
                                            t28.vl_r1 = Decimal.Parse(txtvl_r1.Text);
                                            t28.vl_r4 = Decimal.Parse(txtvl_r4.Text);
                                            t28.vl_r8 = Decimal.Parse(txtvl_r8.Text);
                                            t28.vl_r12 = Decimal.Parse(txtvl_r12.Text);
                                        }
                                        t28.Save();
                                        if (txtvl_p1 != null)
                                        {
                                            txtvl_p1.Text = "0";
                                            txtvl_p4.Text = "0";
                                            txtvl_p8.Text = "0";
                                            txtvl_p12.Text = "0";
                                        }
                                        if (txtvl_r1 != null)
                                        {
                                            txtvl_r1.Text = "0";
                                            txtvl_r4.Text = "0";
                                            txtvl_r8.Text = "0";
                                            txtvl_r12.Text = "0";
                                        }
                                    }
                                }
                            }
                        }
                    }
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
        bool redirect = false;
        int cd=0;
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
                switch (e.CommandName.Trim())
                {
                    case "Selecionar":
                        redirect = true;
                        break;
                    case "Editar":
                        Exibir();
                        this.lblHeader.Text = "Alteração";
                        this.btnAcao.Text = "Alterar";
                        cod.Value = cd.ToString();
                        Retrieve();

                        break;
                    case "Deletar":
                        t11_financeiro t11 = new t11_financeiro();
                        {
                            t11.t11_cd_financeiro = cd;
                            t11.Delete();
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t11_financeiro", "delete", t11.t11_cd_financeiro.ToString());
                        }
                        t28_vlfinanceiro t28 = new t28_vlfinanceiro();
                        {
                            t28.t11_cd_financeiro = cd;
                            t28.Delete();
                        }
                        GridBind();
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        if (redirect)
        {
            Context.Items["t11_cd_financeiro"] = cd.ToString();
            Server.Transfer("Financeiro.aspx", false);
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

            if ((bool)drv["fl_economico"])
            {
                e.Row.Cells[4].Controls.Add(pb.GetLiteral("Econômico"));
            }
            else
            {
                e.Row.Cells[4].Controls.Add(pb.GetLiteral("Financeiro"));
            }
        }
    }

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";
        FormBind("");
        Exibir();
        trReal.Visible = false;
        
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }


    protected void FormBind(string str_t05)
    {
        t08_acao t08 = new t08_acao();
        {
            t08.t08_cd_acao = pb.cd_acao();
            t08.Retrieve();
            if (t08.Found)
            {
                for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                {
                    TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                    TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                    TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                    TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());

                    TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                    TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                    TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                    TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());

                    if (txtvl_p1 != null)
                    {
                        txtvl_p1.Text = "";
                        txtvl_p4.Text = "";
                        txtvl_p8.Text = "";
                        txtvl_p12.Text = "";
                    }
                    if (txtvl_r1 != null)
                    {
                        txtvl_r1.Text = "";
                        txtvl_r4.Text = "";
                        txtvl_r8.Text = "";
                        txtvl_r12.Text = "";
                    }
                }
            }
        }

        t05_parceiro t05 = new t05_parceiro();
        {
            t03_projeto t03 = new t03_projeto();
            {
                t03.t03_cd_projeto = pb.cd_projeto();
                t03.Retrieve();
                if (t03.Found)
                {
                    t05.t01_cd_entidade = t03.t01_cd_entidade;
                    t05.order = "order by nm_parceiro";
                    ddlt05_cd_parceiro.DataSource = t05.List();
                    ddlt05_cd_parceiro.DataTextField = "nm_parceiro";
                    ddlt05_cd_parceiro.DataValueField = "t05_cd_parceiro";
                    ddlt05_cd_parceiro.DataBind();
                    pb.AddEmptyItem(ddlt05_cd_parceiro, "Selecione");
                }
            }
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

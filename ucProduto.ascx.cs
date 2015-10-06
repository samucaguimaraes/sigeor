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

public partial class ucProduto : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTitle.Text = "Produto";
            this.Panel1.Visible = false;
            cod.Value = "0";
            FormBind();
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
        txtds_produto.Text = "";
        txtnm_medida.Text = "";
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true;
        txtds_produto.Text = "";
        txtnm_medida.Text = "";
        GridBind();
    }

    private void GridBind()
    {
        t10_produto t10 = new t10_produto();
        {
            t10.t08_cd_acao = pb.cd_acao();
            //t10.order = "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
            GridView1.DataSource = t10.List();
            GridView1.DataBind();
        }
    }
    private void Retrieve()
    {
        trReal.Visible = true;
        t10_produto t10 = new t10_produto();
        {
            t10.t10_cd_produto = Int32.Parse(cod.Value);
            t10.Retrieve();
            if (t10.Found)
            {
                txtds_produto.Text = t10.ds_produto;
                txtnm_medida.Text = t10.nm_medida;
                t08_acao t08 = new t08_acao();
                {
                    t08.t08_cd_acao = pb.cd_acao();
                    t08.Retrieve();
                    if (t08.Found)
                    {
                        t27_vlproduto t27 = new t27_vlproduto();
                        {
                            for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                            {
                                t27.t10_cd_produto = t10.t10_cd_produto;
                                t27.nu_ano = i;
                                t27.Retrieve();

                                TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                                TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                                TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                                TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());

                                TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                                TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                                TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                                TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());

                                if (t27.Found)
                                {
                                    char c = char.Parse(",");
                                    int casadecimal = 0;
                                    if (txtvl_p1 != null)
                                    {
                                        casadecimal = Int32.Parse(t27.vl_p1.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p1.Text = t27.vl_p1.ToString("N2");
                                        else
                                            txtvl_p1.Text = t27.vl_p1.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p4.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p4.Text = t27.vl_p4.ToString("N2");
                                        else
                                            txtvl_p4.Text = t27.vl_p4.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p8.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p8.Text = t27.vl_p8.ToString("N2");
                                        else
                                            txtvl_p8.Text = t27.vl_p8.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p12.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p12.Text = t27.vl_p12.ToString("N2");
                                        else
                                            txtvl_p12.Text = t27.vl_p12.ToString("N0");
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        casadecimal = Int32.Parse(t27.vl_r1.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r1.Text = t27.vl_r1.ToString("N2");
                                        else
                                            txtvl_r1.Text = t27.vl_r1.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r4.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r4.Text = t27.vl_r4.ToString("N2");
                                        else
                                            txtvl_r4.Text = t27.vl_r4.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r8.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r8.Text = t27.vl_r8.ToString("N2");
                                        else
                                            txtvl_r8.Text = t27.vl_r8.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r12.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r12.Text = t27.vl_r12.ToString("N2");
                                        else
                                            txtvl_r12.Text = t27.vl_r12.ToString("N0");
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
        t10_produto t10 = new t10_produto();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            t10.t08_cd_acao = pb.cd_acao();
            t10.ds_produto = pb.ReplaceAspas(txtds_produto.Text);
            t10.nm_medida = pb.ReplaceAspas(txtnm_medida.Text);
            if (trReal.Visible)
            {
            }
            else
            {
            }
            t10.dt_cadastro = DateTime.Now;
            t10.dt_alterado = DateTime.Now;

            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t10.t10_cd_produto = Int32.Parse(cod.Value);
                    result = t10.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t10_produto", "update", cod.Value);
                }
                else
                {
                    result = t10.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t10_produto", "insert", t10.ds_produto);
                }

                if (result)
                {
                    t27_vlproduto t27 = new t27_vlproduto();
                    {
                        t08_acao t08 = new t08_acao();
                        {
                            t08.t08_cd_acao = pb.cd_acao();
                            t08.Retrieve();
                            if (t08.Found)
                            {
                                if (trReal.Visible)
                                {
                                    t27.t10_cd_produto = Int32.Parse(cod.Value.ToString());
                                }
                                else
                                {
                                    t10.RetrieveCod();
                                    if (t10.Found) t27.t10_cd_produto = t10.t10_cd_produto;
                                }
                                t27.Delete();
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

                                        t27.nu_ano = i;
                                        t27.vl_p1 = Decimal.Parse(txtvl_p1.Text);
                                        t27.vl_p4 = Decimal.Parse(txtvl_p4.Text);
                                        t27.vl_p8 = Decimal.Parse(txtvl_p8.Text);
                                        t27.vl_p12 = Decimal.Parse(txtvl_p12.Text);
                                        if (trReal.Visible)
                                        {
                                            if (txtvl_r1.Text == "") txtvl_r1.Text = "0";
                                            if (txtvl_r4.Text == "") txtvl_r4.Text = "0";
                                            if (txtvl_r8.Text == "") txtvl_r8.Text = "0";
                                            if (txtvl_r12.Text == "") txtvl_r12.Text = "0";
                                            t27.vl_r1 = Decimal.Parse(txtvl_r1.Text);
                                            t27.vl_r4 = Decimal.Parse(txtvl_r4.Text);
                                            t27.vl_r8 = Decimal.Parse(txtvl_r8.Text);
                                            t27.vl_r12 = Decimal.Parse(txtvl_r12.Text);
                                        }
                                        t27.Save();
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
        int cd = 0;
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
                switch (e.CommandName)
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
                        t10_produto t10 = new t10_produto();
                        {
                            t10.t10_cd_produto = cd;
                            t10.Delete();
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t10_produto", "delete", t10.t10_cd_produto.ToString());
                        }
                        t27_vlproduto t27 = new t27_vlproduto();
                        {
                            t27.t10_cd_produto = cd;
                            t27.Delete();
                        }
                        GridBind();
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        break;
                }
            }
        }
        catch
        {
        }
        if (redirect)
        {
            Context.Items["t10_cd_produto"] = cd.ToString();
            Server.Transfer("Produto.aspx", false);
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = ((DataRowView)e.Row.DataItem);

            char c = char.Parse(",");
            int casadecimal = 0;

            if (drv["vl_p"] != DBNull.Value)
            {
                decimal p = (decimal)drv["vl_p"];
                casadecimal = Int32.Parse(p.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    e.Row.Cells[5].Text = p.ToString("N2");
            }
            if (drv["vl_r"] != DBNull.Value)
            {
                decimal r = (decimal)drv["vl_r"];
                casadecimal = Int32.Parse(r.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    e.Row.Cells[6].Text = r.ToString("N2");
            }
            

            //Adicionar mensagem de alterta antes da exclusão
            ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
            if (btn != null)
            {
                btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
            }
        }
    }

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";
        trReal.Visible = false;
        Exibir();
        FormBind();
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }


    protected void FormBind()
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

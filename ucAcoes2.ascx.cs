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

public partial class ucAcoes : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
        }
        //Sem perfil
        if (pb.fl_semperfil(pb.cd_entidade_projeto()))
        {
            GridView1.Columns[2].Visible = false;
            GridView1.Columns[7].Visible = false;
        }

        if (!IsPostBack)
        {
            lblTitle.Text = "Ações";
            this.Panel1.Visible = false;
            cod.Value = "0";
            FormBind();
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "dt_inicio";
        }
        GridBind();
        if (!IsPostBack)
        {

            ViewState["pesquisa"] = "";
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);

        }
    }
    private void Exibir()
    {
        this.Panel1.Visible = true;
        this.GridView1.Visible = false;
        this.PanelAdd.Visible = false;
        txtnm_acao.Text = "";
        txtds_acao.Text = "";
        txtdt_inicio.Text = "";
        txtdt_fim.Text = "";
        ddlt02_cd_usuario.ClearSelection();
        txtds_palvo.Text = "";
		txtds_andamento.Text = "";
		txtds_latuacao.Text = "";
		txtds_parceiro.Text = "";
    }
    private void Ocultar()
    {
        this.Panel1.Visible = false;
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true;
        txtnm_acao.Text = "";
        txtds_acao.Text = "";
        txtdt_inicio.Text = "";
        txtdt_fim.Text = "";
        ddlt02_cd_usuario.ClearSelection();
        GridBind();
        txtds_palvo.Text = "";
		txtds_andamento.Text = "";
		txtds_latuacao.Text = "";
		txtds_parceiro.Text = "";
    }

    private void GridBind()
    {
        t08_acao t08 = new t08_acao();
        {
            t08.t03_cd_projeto = pb.cd_projeto();
            t08.order = "order by " + ViewState["campo"].ToString() +" "+ ViewState["sentido"].ToString();
            GridView1.DataSource = t08.List();
            GridView1.DataBind();
        }
        if (t08.List().Tables[0].Rows.Count >= 1) 
            linkGraf.Visible = true; 
        else 
            linkGraf.Visible = false; 
    }
    private void Retrieve()
    {
        t08_acao t08 = new t08_acao();
        {
            t08.t08_cd_acao = Int32.Parse(cod.Value);
            t08.Retrieve();
            if (t08.Found)
            {
                txtnm_acao.Text = t08.nm_acao;
                txtds_acao.Text = t08.ds_acao;
                txtdt_inicio.Text = t08.dt_inicio.ToShortDateString();
                txtdt_fim.Text = t08.dt_fim.ToShortDateString();
                ListItem li = ddlt02_cd_usuario.Items.FindByValue(t08.t02_cd_usuario);
                txtds_palvo.Text = t08.ds_palvo;
				txtds_andamento.Text = t08.ds_andamento;
				txtds_latuacao.Text = t08.ds_latuacao;
				txtds_parceiro.Text = t08.ds_parceiro;
                if (li != null) li.Selected = true;
            }
        }
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        //Label lblmsg = lblme.Text;
        t08_acao t08 = new t08_acao();
        {
            bool result = false;
            bool erro = false;
            string msg = "";
            t08.t03_cd_projeto = pb.cd_projeto();
            t08.nm_acao = pb.ReplaceAspas(txtnm_acao.Text);
            t08.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
            t08.ds_acao = pb.ReplaceAspas(txtds_acao.Text);
            t08.dt_inicio = DateTime.Parse(txtdt_inicio.Text);
            t08.dt_fim = DateTime.Parse(txtdt_fim.Text);
            t08.dt_original = DateTime.Parse(txtdt_fim.Text);
            t08.dt_cadastro = DateTime.Now;
            t08.dt_alterado = DateTime.Now;
            t08.ds_palvo = pb.ReplaceAspas(txtds_palvo.Text);
			t08.ds_andamento = pb.ReplaceAspas(txtds_andamento.Text);
			t08.ds_latuacao = pb.ReplaceAspas(txtds_latuacao.Text);
			t08.ds_parceiro = pb.ReplaceAspas(txtds_parceiro.Text);

            t03_projeto t03 = new t03_projeto();
            {
                t03.t03_cd_projeto = t08.t03_cd_projeto;
                t03.Retrieve();
                if (t03.Found)
                {
                    if ((t08.dt_inicio < t03.dt_inicio)||(t08.dt_fim > t03.dt_fim))
                    {
                        erro = true;
                        msg = pb.Message("As datas de início e término da Ação deve estar entre as "+
                            "datas de início (" + t03.dt_inicio .ToShortDateString() + ") "+
                            "e término ("+t03.dt_fim.ToShortDateString()+") do Projeto!", "erro");
                    }

                }
            }
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                                t08.t08_cd_acao = Int32.Parse(cod.Value);

                                foreach (DataRow dr in t08.ListAlt().Tables[0].Rows)
                                {
                                    if (dr["menor"] != DBNull.Value)
                                    {
                                        //Response.Write(dr["menor"]);
                                        if ((DateTime.Parse(txtdt_inicio.Text).Year > (int)dr["menor"]) || (DateTime.Parse(txtdt_fim.Text).Year < (int)dr["maior"]))
                                        {
                                            if (btnAcao.CommandArgument != "1")
                                            {
                                                btnAcao.Text = "Confirmar";
                                                lblme.Text = "<b>Atenção:</b> Serão excluídos eventuais valores de Financeiros cadastrados em anos não contidos no novo prazo, clique em Confirmar para prosseguir com a alteração.<br />";
                                                btnAcao.CommandArgument = "1";
                                            }
                                            else
                                            {
                                                t08.order = " nu_ano not between '" + DateTime.Parse(txtdt_inicio.Text).Year + "' And '" + DateTime.Parse(txtdt_fim.Text).Year + "' ";
                                                t08.DeleteAlt();

                                                result = t08.Update();
                                                msg = pb.Message("Alteração realizada com sucesso!", "ok");
                                                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t08_acao", "update", cod.Value);

                                            }
                                        }
                                        else
                                        {
                                            result = t08.Update();
                                            msg = pb.Message("Alteração realizada com sucesso!", "ok");
                                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t08_acao", "update", cod.Value);
                                        }
                                    }
                                    else
                                    {
                                        result = t08.Update();
                                        msg = pb.Message("Alteração realizada com sucesso!", "ok");
                                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t08_acao", "update", cod.Value);
                                    }
                                }
                        


                    
                }
                else
                {
                    result = t08.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t08_acao", "insert", t08.nm_acao);

                    if (result)
                    {
                        t08.RetrieveCod();
                        if (t08.Found)
                        {
                            t09_marco t09 = new t09_marco();
                            {
                                t09.t08_cd_acao = t08.t08_cd_acao;
                                t09.nu_esforco = 1;
                                t09.ds_marco = "Ação encerrada";
                                t09.dt_prevista = t08.dt_fim;
                                t09.dt_original = t08.dt_fim;
                                t09.ds_comentario = "";
                                t09.fl_status = "G";
                                t09.dt_cadastro = DateTime.Now;
                                t09.dt_alterado = DateTime.Now;
                                t09.fl_original = true;
                                t09.Save();
                            }
                        }
                    }
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
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                int cd = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
                switch (e.CommandName)
                {
                    case "Selecionar":
                        Session["cd_acao"] = cd.ToString();
                        Response.Redirect("~/Acao.aspx");
                        break;

                    case "Editar":
                        Exibir();
                        this.lblHeader.Text = "Alteração";
                        this.btnAcao.Text = "Alterar";
                        cod.Value = cd.ToString();
                        Retrieve();

                        break;
                    case "Deletar":
                        t08_acao t08 = new t08_acao();
                        {
                            t08.t08_cd_acao = cd;
                            t08.Delete();
                            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t08_acao", "delete", t08.t08_cd_acao.ToString());
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
            e.Row.Cells[6].Controls.Add(pb.GetLiteral(statusAcao(Int32.Parse(drv["t08_cd_acao"].ToString()))));

        }
    }

    protected string statusAcao(int cd_acao)
    {
        string cor = "";
        bool r = false;
        bool g = false;
        bool b = false;
        t09_marco t09 = new t09_marco();
        {
            t09.t08_cd_acao = cd_acao;
            foreach (DataRow dr in t09.List().Tables[0].Rows)
            {
                switch (dr["fl_status"].ToString())
                {
                    case "R":
                        r = true;
                        break;
                    case "G":
                        g = true;
                        break;
                    case "B":
                        b = true;
                        break;
                }
            }
        }
        if (r)
        {
            cor = "<img src=\"images/R.gif\" />";
        }
        else if (g)
        {
            cor = "<img src=\"images/G.gif\" />";
        }
        else if (b)
        {
            cor = "<img src=\"images/B.gif\" />";
        }
        else
        {
            cor = "";
        }
        if (!r)
        {
            t29_acaorestricao t29 = new t29_acaorestricao();
            {
                t29.t08_cd_acao = cd_acao;
                t29.RetrieveAcao();
                if (t29.Found)
                {
                    cor = "<img src=\"images/Y.gif\" />";
                }
            }
        }

        return cor;
    }
    protected string restricaoAcao(int cd_acao)
    {
        string str = "";
        t29_acaorestricao t29 = new t29_acaorestricao();
        {
            t29.t08_cd_acao = cd_acao;
            t29.RetrieveAcao();
            if (t29.Found)
            {
                str = "<div style='font-weight:bold;text-align:center'>R</div>";
            }
        }

        return str;
    }
    protected string graficoAcao(int cd_acao)
    {
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int i;
        double fisico=0;
        double financeiro = 0;
        double difdias = 0;
        double difhoje=0;
        double crono = 0;
        t08_acao t08 = new t08_acao();
        {
            t08.t08_cd_acao = cd_acao;
            t08.Retrieve();
            if (t08.Found)
            {
                //TEMPO
                difdias = t08.dt_fim.Subtract(t08.dt_inicio).Days;
                difhoje = t08.dt_fim.Subtract(DateTime.Now).Days;

                if (DateTime.Now.Date > t08.dt_inicio.Date)
                {
                    crono = (((difhoje / difdias) * 100) - 100) * -1;
                    if (crono < 0)
                    {
                        crono = 0;
                    }
                    else if (crono > 100)
                    {
                        crono = 100;
                    }
                }
                else
                {
                    crono = 0;
                }

                //FÍSICO (Marcos Críticos)
                t09_marco t09 = new t09_marco();
                {
                    t09.t08_cd_acao = t08.t08_cd_acao;
                    //double mcprev = 0; double mcreal = 0;
                    foreach (DataRow dr in t09.List().Tables[0].Rows)
                    {
                        if ((string)dr["fl_status"] == "B")
                        {
                            fisico += (int)dr["nu_esforco"];
                        }
                        //mcprev += (int)dr["nu_esforco"];
                        //Response.Write(dr["fl_status"] + " - " + dr["nu_esforco"] + "<br>");
                    }
                    //if (mcprev>0)
                      //  fisico = ((mcreal * 100) / mcprev);
                }
                //FÍSICO (Produto)
                //t10_produto t10 = new t10_produto();
                //{
                //    i = 0;
                //    t10.t08_cd_acao = t08.t08_cd_acao;
                //    foreach (DataRow dr in t10.List().Tables[0].Rows)
                //    {
                //        double prev, real;
                //        i++;
                //        if (dr["vl_r"] == DBNull.Value)
                //        {
                //            real = 0;
                //        }
                //        else
                //        {
                //            real = double.Parse(dr["vl_r"].ToString());
                //        }
                //        if (dr["vl_p"] == DBNull.Value)
                //        {
                //            prev = 0;
                //        }
                //        else
                //        {
                //            prev = double.Parse(dr["vl_p"].ToString());
                //        }

                //        if (prev > 0)
                //        {
                //            fisico += ((real * 100) / prev);
                //        }
                //        else
                //        {
                //            fisico += 0;
                //        }
                //    }

                //    if (i>0) fisico = fisico / i;

                //    if (fisico > 100)
                //    {
                //        fisico = 100;
                //    }
                //    else if (fisico < 0)
                //    {
                //        fisico = 0;
                //    }

                //}

                //FINANCEIRO
                t11_financeiro t11 = new t11_financeiro();
                {
                    i = 0;
                    t11.t08_cd_acao = t08.t08_cd_acao;
                    foreach (DataRow dr in t11.ListCalc().Tables[0].Rows)
                    {
                        double prev, real;
                        i++;
                        if (dr["realizado"] == DBNull.Value)
                        {
                            real = 0;
                        }
                        else
                        {
                            real = double.Parse(dr["realizado"].ToString());
                        }
                        if (dr["previsto"] == DBNull.Value)
                        {
                            prev = 0;
                        }
                        else
                        {
                            prev = double.Parse(dr["previsto"].ToString());
                        }
                        if (prev > 0)
                        {
                            financeiro = ((real * 100) / prev);
                        }
                        else
                        {
                            financeiro = 0;
                        }
                    }


                    if (financeiro > 100)
                    {
                        financeiro = 100;
                    }
                    else if (financeiro < 0)
                    {
                        financeiro = 0;
                    }

                }
            }
        }
        //bgColor='" + linha + "'
        sb1.Append("<graph chartRightMargin='23' numberSuffix='%25' chartBottomMargin='30' yAxisMaxValue='100'  showAlternateVGridColor='1' alternateVGridAlpha='10' alternateVGridColor='AFD8F8'  numDivLines='4' decimalPrecision='0' canvasBorderThickness='1' canvasBorderColor='114B78' baseFontColor='114B78' hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>");
        sb1.Append("<set name='Tempo' value='" + crono.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("<set name='Físico' value='" + fisico.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("<set name='Financeiro' value='" + financeiro.ToString().Replace(",",".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("</graph>");

        sb2.Append("<object id=\"FC2Column\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"");
        sb2.Append("height=\"80\" width=\"250\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">");
        sb2.Append("<param name=\"Movie\" value=\"Charts/FC_2_3_Bar2D.swf\">");
        sb2.Append("<param name=\"FlashVars\" value=\"&chartWidth=250&chartHeight=100&dataXML=" + sb1.ToString() + "\">");
        sb2.Append("<embed src=\"Charts/FC_2_3_Bar2D.swf\" flashvars=\"&chartWidth=250&chartHeight=100&dataXML=" + sb1.ToString() + "\"");
        sb2.Append("quality=\"high\" width=\"250\" height=\"80\" name=\"FC2Column\" type=\"application/x-shockwave-flash\" pluginspace=\"http://www.macromedia.com/go/getflashplayer\"> </embed></object>");
        
        return sb2.ToString();
    }

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Cadastro";
        this.btnAcao.Text = "Cadastrar";

        Exibir();
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }


    protected void FormBind()
    {
        t02_usuario t02 = new t02_usuario();
        {
            t02.order = "order by nm_nome";
            t02.t01_cd_entidade = pb.cd_entidade_projeto();
            t02.fl_ativa = true;
            ddlt02_cd_usuario.DataSource = t02.ListComboProjeto();
            ddlt02_cd_usuario.DataTextField = "nm_nome";
            ddlt02_cd_usuario.DataValueField = "t02_cd_usuario";
            ddlt02_cd_usuario.DataBind();
            pb.AddEmptyItem(ddlt02_cd_usuario, "Selecione");
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


    protected void txtnm_acao_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

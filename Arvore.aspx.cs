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

public partial class Arvore : System.Web.UI.Page
{
    double valorInvestimento;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            Session["fl_gerente"] = false;
            ArvoreBind();
        }
        IsPostBackBind();

        if (Session["cd_projeto"] == null)
        {
            Response.Redirect("Projetos.aspx");
        }
    }
    protected void IsPostBackBind()
    {
        t11_financeiro t11 = new t11_financeiro();
        {
            t11.order = " where t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto=" + pb.cd_projeto().ToString() + ") ";
            GridInvestimento.DataSource = t11.ListInvestimento();
            GridInvestimento.DataBind();
        }
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                t02_usuario t02 = new t02_usuario();
                {
                    t02.t02_cd_usuario = t03.t02_cd_usuario;
                    t02.Retrieve();
                    if (t02.Found)
                    {
                        StringBuilder sb = new StringBuilder();
                        {
                            sb.Append("<div class='userInfo'>");
                            sb.Append("<div class='userTitle'>Gestor</div>");
                            sb.Append("<div class='userName'><b>" + t02.nm_nome + "</b>");
                            if (t02.nm_cargo != "") sb.Append("<br /> " + t02.nm_cargo);
                            sb.Append("</div>");
                            sb.Append("<div id='userID1'>");
                            if (t02.nu_telefone.ToString().Length == 10)
                            {
                                sb.Append("<div class='userFone'> (" + t02.nu_telefone.ToString().Substring(0, 2) + ") " + t02.nu_telefone.ToString().Substring(2, 8) + "</div>");
                            }
                            if (t02.nu_celular.ToString().Length == 10)
                            {
                                sb.Append("<div class='userMobile'> (" + t02.nu_celular.ToString().Substring(0, 2) + ") " + t02.nu_celular.ToString().Substring(2, 8) + "</div>");
                            }
                            sb.Append("</div>"); //userID
                            sb.Append("</div>"); //userInfo
                        }
                        PanelUsuario.Controls.Clear();
                        PanelUsuario.Controls.Add(pb.GetLiteral(sb.ToString()));
                    }
                }
            }
        }
       

        if (!pb.fl_gerente()) //se não for gerente
        {
            linkds_objetivo.Visible = false;
            linkds_publico.Visible = false;
            linkds_situacao.Visible = false;
            linkdt_acordo.Visible = false;
            linkdt_inicio.Visible = false;
            linkds_usuariofinal.Visible = false;

            if (dlResultados.Items.Count == 0)
            {
                linkResultados.Visible = false;
            }

        }
    }
    
    protected void ArvoreBind()
    {
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                //Perfil Gerente
                if (t03.t02_cd_usuario == pb.cd_usuario()) Session["fl_gerente"] = true;

                //Entidade do Projeto
                Session["cd_entidade_projeto"] = t03.t01_cd_entidade;

                //Sem perfil
                if (pb.fl_semperfil(pb.cd_entidade_projeto()))
                {
                    PanelInvestimentos.Visible = false;
                    tdDocumentos.Visible = false;
                }


                //Perfil Monitoramento
                if (t03.t02_cd_usuario_monitoramento == pb.cd_usuario())
                {
                    linkt19_cd_fase.Visible = true;
                }
                else
                {
                    linkt19_cd_fase.Visible = false;
                }

                Session["nm_projeto"] = t03.nm_projeto;

                lblds_objetivo.Text = pb.ReplaceNewLines(t03.ds_objetivo); txtds_objetivo.Text = t03.ds_objetivo;
                lblds_publico.Text = pb.ReplaceNewLines(t03.ds_publico); txtds_publico.Text = t03.ds_publico;
                if (t03.dt_inicio.Year > 1)
                {
                    lbldt_inicio.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_inicio);
                    txtdt_inicio.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_inicio);

                    lbldt_fim.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_fim);
                    txtdt_fim.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_fim);
                    btnGerenciamento.OnClientClick = "";
                    lblstatus.Visible = true;
                    lblgrafico.Visible = true;
                }
                else
                {
                    lblstatus.Visible = false;
                    lblgrafico.Visible = false;
                    btnGerenciamento.OnClientClick = "javascript:alert('Desculpe, mas é necessário definir as datas de início é término do projeto antes de continuar!')";
                    if (!pb.fl_gerente()) btnGerenciamento.Visible = false;
                }
                if (t03.dt_alterado.Year > 1) { lbldt_alterado.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_alterado); }

                if (t03.dt_acordo.Year > 1) { lbldt_acordo.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_acordo); txtdt_acordo.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_acordo); }


                t19_fase t19 = new t19_fase();
                {
                    t19.t19_cd_fase = t03.t03_cd_projeto;
                    t19.RetrieveFaseProjeto();
                    if (t19.Found)
                    {
                        ddlt19_cd_fase.DataSource = t19.List();
                        ddlt19_cd_fase.DataTextField = "nm_fase";
                        ddlt19_cd_fase.DataValueField = "t19_cd_fase";
                        ddlt19_cd_fase.DataBind();
                        ddlt19_cd_fase.ClearSelection();
                        ddlt19_cd_fase.SelectedValue = t19.t19_cd_fase.ToString();
                        lblnm_fase.Text = t19.nm_fase;
                    }
                }

                t16_situacao t16 = new t16_situacao();
                {
                    t16.t03_cd_projeto = t03.t03_cd_projeto;
                    t16.Retrieve();
                    if (t16.Found) { lblds_situacao.Text = pb.ReplaceNewLines(t16.ds_situacao); txtds_situacao.Text = t16.ds_situacao; }
                }

                t14_resultado t14 = new t14_resultado();
                {
                    t14.t03_cd_projeto = pb.cd_projeto();
                    dlResultados.DataSource = t14.List();
                    dlResultados.DataBind();
                }
                t18_documento t18 = new t18_documento();
                {
                    t18.t03_cd_projeto = pb.cd_projeto();
                    //crono
                    t18.fl_cronograma = true; t18.fl_foto = false; t18.fl_outros = false; t18.fl_video = false;
                    lblCronograma.Text = t18.List().Tables[0].Rows.Count.ToString();
                    //foto
                    t18.fl_cronograma = false; t18.fl_foto = true; t18.fl_outros = false; t18.fl_video = false;
                    lblFoto.Text = t18.List().Tables[0].Rows.Count.ToString();
                    //outros
                    t18.fl_cronograma = false; t18.fl_foto = false; t18.fl_outros = true; t18.fl_video = false;
                    lblOutros.Text = t18.List().Tables[0].Rows.Count.ToString();
                    //video
                    t18.fl_cronograma = false; t18.fl_foto = false; t18.fl_outros = false; t18.fl_video = true;
                    lblVideo.Text = t18.List().Tables[0].Rows.Count.ToString();
                }
                t23_noticia t23 = new t23_noticia();
                {
                    t23.t03_cd_projeto = t03.t03_cd_projeto;
                    lblNoticias.Text = t23.List().Tables[0].Rows.Count.ToString();
                }
                t24_agenda t24 = new t24_agenda();
                {
                    t24.t03_cd_projeto = t03.t03_cd_projeto;
                    lblAgenda.Text = t24.List().Tables[0].Rows.Count.ToString();
                }

                string barras = pb.Status(t03.t03_cd_projeto);
                if (barras != "&nbsp;")
                {
                    lblstatus.Text = "<table><tr><td style='color:#114B78; text-align:right;'><small>Ações</small></td><td style='width:157px'>" + pb.Status(t03.t03_cd_projeto) + "</td></tr></table>";
                }
                lblgrafico.Text = graficoProjeto();


                t04_tipologia t04 = new t04_tipologia();
                {
                    t04.t04_cd_tipologia = t03.t04_cd_tipologia;
                    t04.Retrieve();
                    if (t04.Found)
                    {
                        Session["nm_tipologia"] = t04.nm_tipologia;
                        if (t04.nm_tipologia == "Gestão Interna")
                        {
                            Session["gestaoInterna"] = true;
                            lblh_publico.Text = "Demandante";
                            lblh_foco.Text = "Demanda";
                            tdUsuarioFinal.Visible = true;
                            lblds_usuariofinal.Text = pb.ReplaceNewLines(t03.ds_usuariofinal); txtds_usuariofinal.Text = t03.ds_usuariofinal;
                            linkPremissas.Visible = false;
                        }
                        else
                        {
                            tdUsuarioFinal.Visible = false;
                            Session["gestaoInterna"] = false;
                            lblh_publico.Text = "Público-alvo";
                            lblh_foco.Text = "Foco estratégico";
                        }
                    }
                }

            }

        }

    }

    protected void dlResultado_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView drv = ((DataRowView)e.Item.DataItem);

        Label lbl;
        lbl = (Label)e.Item.FindControl("lblds_resultado");
        if (lbl != null) lbl.Text = drv["ds_resultado"].ToString();

    }

    protected void link_Click(object sender, System.EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Panel panel = (Panel)PanelArvore.FindControl("Panel" + link.CommandArgument);
        Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + link.CommandArgument);
        Button btn = (Button)PanelArvore.FindControl("btnSalvar" + link.CommandArgument);
        if (PanelEdit != null)
        {
            PanelEdit.Visible = true;
            if (btn != null)
            {
                //this.RegisterClientScriptBlock("clientScript",
                //      "<script>document.getElementById('" + txt.ClientID +
                //      "').focus();</script>");
                RegisterStartupScript("clientScript",
                      "<script>document.getElementById('" + btn.ClientID +
                      "').focus();</script>");
            }
        }
        if (panel != null)
        {
            panel.Visible = false;
        }

    }

    protected void btnSalvar_Click(object sender, System.EventArgs e)
    {
        bool erro = false;
        string msg = "";
        Button btnSalvar = (Button)sender;

        string log="";
        t03_projeto t03 = new t03_projeto();
        {
            switch (btnSalvar.CommandArgument)
            {
                case "ds_publico":
                    t03.order = ", ds_publico = '" +
                       pb.ReplaceAspas(txtds_publico.Text) + "'";
                    log = txtds_publico.Text;
                    break;
                case "ds_objetivo":
                    t03.order = ", ds_objetivo = '" +
                       pb.ReplaceAspas(txtds_objetivo.Text) + "'";
                    log = txtds_objetivo.Text;
                    break;
                case "ds_usuariofinal":
                    t03.order = ", ds_usuariofinal = '" +
                       pb.ReplaceAspas(txtds_usuariofinal.Text) + "'";
                    log = txtds_usuariofinal.Text;
                    break;
                case "dt_inicio":
                    if (!verDatasAcao(DateTime.Parse(txtdt_inicio.Text), DateTime.Parse(txtdt_fim.Text)))
                    {
                        erro = true;
                        msg = pb.Message("É necessário ajustar as datas das Ações antes de continuar, pois existem datas de ações entre as datas do Eixo.", "erro");
                    }
                    if (!verDatasResultado(DateTime.Parse(txtdt_inicio.Text), DateTime.Parse(txtdt_fim.Text)))
                    {
                        erro = true;
                        msg += pb.Message("É necessário ajustar valores de Resultados antes de continuar, pois existem lançamentos nos anos do Eixo.", "erro");
                    }
                    t03.order = ", dt_inicio = '" +
                       String.Format("{0:yyyy/MM/dd}", DateTime.Parse(txtdt_inicio.Text)) + "'"+
                       ", dt_fim = '" +
                       String.Format("{0:yyyy/MM/dd}", DateTime.Parse(txtdt_fim.Text)) + "' ";
                    break;
                case "dt_acordo":
                    t03.order = ", dt_acordo = '" +
                        String.Format("{0:yyyy/MM/dd}", DateTime.Parse(txtdt_acordo.Text)) + "'";
                    break;
                case "ds_situacao":
                    t16_situacao t16 = new t16_situacao();
                    {
                        t16.ds_situacao = pb.ReplaceAspas(txtds_situacao.Text);
                        t16.t03_cd_projeto = pb.cd_projeto();
                        t16.dt_alterado = System.DateTime.Now;
                        t16.dt_cadastro = System.DateTime.Now;
                        t16.Save();
                    }
                    break;
                case "t19_cd_fase":
                    t20_faseprojeto t20 = new t20_faseprojeto();
                    {
                        t20.t19_cd_fase = Int32.Parse(ddlt19_cd_fase.SelectedValue);
                        t20.t03_cd_projeto = pb.cd_projeto();
                        t20.fl_ativa = true;
                        t20.dt_alterado = System.DateTime.Now;
                        t20.dt_cadastro = System.DateTime.Now;
                        t20.Delete(); // altera fl_ativa p/ false
                        t20.Save();

                        mudaFase(t20.t19_cd_fase); // mudança de fase
                    }
                    break;
                    
            }
            if (!erro)
            {
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), log, "t03_cd_projeto", "update", "alteração na arvore:" + btnSalvar.CommandArgument);
                t03.dt_alterado = System.DateTime.Now;
                t03.t03_cd_projeto = pb.cd_projeto();
                t03.UpdateArvore();

                Panel panel = (Panel)PanelArvore.FindControl("Panel" + btnSalvar.CommandArgument);
                Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + btnSalvar.CommandArgument);
                if (PanelEdit != null)
                {
                    PanelEdit.Visible = false;
                }
                if (panel != null)
                {
                    panel.Visible = true;
                }
                
                ArvoreBind();
                msg = pb.Message("Alteração realizada com sucesso!", "ok");
            }
        }
        
        lblMsg.Visible = true;
        lblMsg.Text = msg;

    }

    protected void mudaFase(int cd_fase)
    {
        t19_fase t19 = new t19_fase();
        {
            t19.t19_cd_fase = cd_fase;
            t19.RetrieveFase();
            if (t19.Found)
            {
                if (t19.fl_fase == "EX")
                {
                    t30_projetofase t30 = new t30_projetofase();
                    {
                        t30.t03_cd_projeto = pb.cd_projeto();
                        t30.RetrieveCod();
                        if (!t30.Found)
                        {
                            t03_projeto t03 = new t03_projeto();
                            {
                                t03.t03_cd_projeto = pb.cd_projeto();
                                t03.Retrieve();
                                if (t03.Found)
                                {
                                    t30.t03_cd_projeto = t03.t03_cd_projeto;
                                    t30.t02_cd_usuario = t03.t02_cd_usuario;
                                    t30.t02_cd_usuario_monitoramento = t03.t02_cd_usuario_monitoramento;
                                    t30.t04_cd_tipologia = t03.t04_cd_tipologia;
                                    t30.t01_cd_entidade = t03.t01_cd_entidade;
                                    t30.nm_projeto = t03.nm_projeto;
                                    t30.ds_publico = t03.ds_publico;
                                    t30.ds_objetivo = t03.ds_objetivo;
                                    t30.ds_usuariofinal = t03.ds_usuariofinal;
                                    
                                    //t30.dt_inicio = t03.dt_inicio;
                                    //t30.dt_fim = t03.dt_fim;
                                    //t30.dt_acordo = t03.dt_acordo;
                                    //Response.Write(t03.dt_acordo);
                                    if (t03.dt_inicio.Year > 1)
                                    {                                        
                                        t30.dt_inicio = t03.dt_inicio;                                        
                                    }
                                    if (t03.dt_fim.Year > 1)
                                    {                                        
                                        t30.dt_fim = t03.dt_fim;
                                    }
                                 
                                    if (t03.dt_acordo.Year > 1)
                                    {
                                        t30.dt_acordo = t03.dt_acordo;
                                     }
                                    
                                    //Response.Write(t30.Save());
                                    t30.Save();

                                    t30.RetrieveCod();
                                    if (t30.Found)
                                    {
                                        t31_acaofase t31 = new t31_acaofase();
                                        {
                                            t31.t03_cd_projeto = pb.cd_projeto();
                                            foreach (DataRow dr in t31.ListAcoes().Tables[0].Rows)
                                            {
                                                t31.t30_cd_projetofase = t30.t30_cd_projetofase;
                                                t31.t02_cd_usuario = (string)dr["t02_cd_usuario"];
                                                t31.t03_cd_projeto = pb.cd_projeto();
                                                t31.nm_acao = (string)dr["nm_acao"];
                                                t31.ds_acao = (string)dr["ds_acao"];
                                                t31.dt_inicio = (DateTime)dr["dt_inicio"];
                                                t31.dt_fim = (DateTime)dr["dt_fim"];
                                                t31.dt_original = (DateTime)dr["dt_original"];

                                                if (dr["vl_previsto"] != DBNull.Value)
                                                { t31.vl_previsto = (decimal)dr["vl_previsto"]; }
                                                else { t31.vl_previsto = 0; }

                                                if (dr["vl_realizado"] != DBNull.Value) { t31.vl_realizado = (decimal)dr["vl_realizado"]; }
                                                else { t31.vl_realizado = 0; }

                                                t31.Save();
                                            } //foreach
                                        } //t31_acaofase
                                    } //t30.Found
                                } //t03.Found
                            } //t03_projeto
                        } //!t30.Found
                    } //t30_projetofase

                    t31_acaofase t31b = new t31_acaofase();
                    {
                        t31b.t03_cd_projeto = pb.cd_projeto();
                        t31b.UpdateMarcos();
                    }

                } //t19.fl_fase
            } //t19.Found
        }
    }

    protected bool verDatasAcao(DateTime dti, DateTime dtf)
    {
        bool altera = true;
        t08_acao t08 = new t08_acao();
        {
            t08.order = "order by dt_inicio";
            t08.t03_cd_projeto = pb.cd_projeto();
            foreach (DataRow dr in t08.List().Tables[0].Rows)
            {
                DateTime acaoInicio = DateTime.Parse(dr["dt_inicio"].ToString());
                DateTime acaoFim = DateTime.Parse(dr["dt_fim"].ToString());
                //Response.Write("(" + acaoInicio + " < " + dti + ") && (" + acaoFim + " > " + dtf + ")<br>");
                if ((acaoInicio < dti) || (acaoFim > dtf))
                {
                   altera = false;
                }
            }
        }
        return altera;
    }

    protected bool verDatasResultado(DateTime dti, DateTime dtf)
    {
        bool altera = true;
        t14_resultado t14 = new t14_resultado();
        {
            t14.t03_cd_projeto = pb.cd_projeto();
            foreach (DataRow dr in t14.List().Tables[0].Rows)
            {
                t15_vlresultado t15 = new t15_vlresultado();
                {
                    t15.t14_cd_resultado = Int32.Parse(dr["t14_cd_resultado"].ToString());
                    foreach (DataRow drv in t15.List().Tables[0].Rows)
                    {
                        int ano = Int32.Parse(drv["nu_ano"].ToString());
                        if ((ano < dti.Year) || (ano > dtf.Year))
                        {
                            double vl_previsto = double.Parse(drv["vl_previsto"].ToString());
                            double vl_realizado = double.Parse(drv["vl_realizado"].ToString());
                            if (vl_previsto>0 || vl_realizado>0) altera = false;
                        }
                    }
                }
            }
        }
        return altera;
    }
    protected void btnCancelar_Click(object sender, System.EventArgs e)
    {
        Button btnCancelar = (Button)sender;
        Panel panel = (Panel)PanelArvore.FindControl("Panel" + btnCancelar.CommandArgument);
        Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + btnCancelar.CommandArgument);
        if (PanelEdit != null)
        {
            PanelEdit.Visible = false;
        }
        if (panel != null)
        {
            panel.Visible = true;
        }
        ArvoreBind();
    }
    protected void btnGerenciamento_Click(object sender, EventArgs e)
    {
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                if (t03.dt_inicio.Year > 1)
                {
                    Response.Redirect("Gerenciamento3.aspx");
                }
                else
                {
                    Response.Redirect("Arvore.aspx");
                }
            }
        }
    }
    protected void GridInvestimento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = ((DataRowView)e.Row.DataItem);
           // valorInvestimento += double.Parse(drv["valor"].ToString()); {ERRO QUANDO DEIXO ESSA LINHA DESCOMENTADA}
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Controls.Add(pb.GetLiteral("Total"));
            e.Row.Cells[1].Controls.Add(pb.GetLiteral(valorInvestimento.ToString("N")));
        }
    }

    protected string graficoProjeto()
    {
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int i=0;
        int ifin=0;
        double fisico = 0;
        double acfisico = 0;
        double financeiro = 0;
        double difdias = 0;
        double difhoje = 0;
        double crono = 0;

        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                //TEMPO
                difdias = t03.dt_fim.Subtract(t03.dt_inicio).Days;
                difhoje = t03.dt_fim.Subtract(DateTime.Now).Days;
                crono = (((difhoje / difdias) * 100) - 100) * -1;
                if (DateTime.Now.Date > t03.dt_inicio.Date)
                {
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

                t08_acao t08 = new t08_acao();
                {
                    t08.t03_cd_projeto = t03.t03_cd_projeto;
                    foreach (DataRow dra in t08.List().Tables[0].Rows)
                    {
                        i++;
                        int p = 0;
                        t08.t08_cd_acao = Int32.Parse(dra["t08_cd_acao"].ToString());
                        t08.Retrieve();
                        if (t08.Found)
                        {
                            //FÍSICO (Marcos Críticos)
                            t09_marco t09 = new t09_marco();
                            {
                                t09.t08_cd_acao = t08.t08_cd_acao;
                                foreach (DataRow dr in t09.List().Tables[0].Rows)
                                {
                                    if ((string)dr["fl_status"] == "B")
                                    {
                                        fisico += (int)dr["nu_esforco"];
                                    }
                                }
                                
                            }
                            

                            //FINANCEIRO
                            t11_financeiro t11 = new t11_financeiro();
                            {
                                t11.t08_cd_acao = t08.t08_cd_acao;
                                foreach (DataRow dr in t11.ListCalc().Tables[0].Rows)
                                {
                                    double prev, real;
                                    //ifin++;
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
                                        financeiro += ((real * 100) / prev);
                                    }
                                    else
                                    {
                                        financeiro += 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (fisico > 0) fisico = (fisico / i);
            //if (financeiro > 0) financeiro = (financeiro / i);
            
            if (fisico > 100)
            {
                fisico = 100;
            }
            else if (fisico < 0)
            {
                fisico = 0;
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
        //bgColor='" + linha + "'
        sb1.Append("<graph chartRightMargin='23' numberSuffix='%25' chartBottomMargin='30' yAxisMaxValue='100'  showAlternateVGridColor='1' alternateVGridAlpha='10' alternateVGridColor='AFD8F8'  numDivLines='4' decimalPrecision='0' canvasBorderThickness='1' canvasBorderColor='114B78' baseFontColor='114B78' hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>");
        sb1.Append("<set name='Tempo' value='" + crono.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("<set name='Físico' value='" + fisico.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("<set name='Financeiro' value='" + financeiro.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        sb1.Append("</graph>");

        sb2.Append("<object id=\"FC2Column\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"");
        sb2.Append("height=\"80\" width=\"250\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">");
        sb2.Append("<param name=\"Movie\" value=\"Charts/FC_2_3_Bar2D.swf\">");
        sb2.Append("<param name=\"FlashVars\" value=\"&chartWidth=250&chartHeight=100&dataXML=" + sb1.ToString() + "\">");
        sb2.Append("<embed src=\"Charts/FC_2_3_Bar2D.swf\" flashvars=\"&chartWidth=250&chartHeight=100&dataXML=" + sb1.ToString() + "\"");
        sb2.Append("quality=\"high\" width=\"250\" height=\"80\" name=\"FC2Column\" type=\"application/x-shockwave-flash\" pluginspace=\"http://www.macromedia.com/go/getflashplayer\"> </embed></object>");

        return sb2.ToString();
    }
}

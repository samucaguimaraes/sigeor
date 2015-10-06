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

public partial class MonPainel : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    double fin_prev; double fin_real;
    double fis_prev; double fis_real;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Buffer = true;
        Response.AddHeader("pragma", "no-cache");
        Response.Expires = 0;
        if (!(IsPostBack))
        {
            PanelMon.Visible = true;
            //Gráfico de Atualização
            GrafAtualizacao(pnAtualizacao);

            //Barra de Status das Ações
            AcaoStatus(pnAcaoStatus);

            // Gráfico Acompanhamento Físico - Financeiro
            GrafFisicoFinanceiro(pnFisicoFinanceiro);

            // Indicadores
            IndicadoresCarteira();

            //Detalhes do Filtro
            lblfiltrogerado.Text = DateTime.Now.ToShortDateString();
            if (Session["monparceiro"] != null) lblfiltroparceiro.Text = Session["monparceiro"].ToString();
            if (Session["monquantproj"] != null) linkfiltroprojetos.Text=Session["monquantproj"].ToString();
            if (Session["monprojeto"] != null) lblfiltroprojeto.Text = Session["monprojeto"].ToString();
            if (Session["montipologia"] != null) lblfiltrotipologia.Text = Session["montipologia"].ToString();
            if (Session["monfase"] != null) lblfiltrofase.Text = Session["monfase"].ToString();
            if ((Session["mondti"] != null) && (Session["mondtf"] != null)) lblfiltroperiodo.Text = Session["mondti"].ToString() + " à " + Session["mondtf"].ToString();
        }
    }

    public void GrafAtualizacao(Panel pn)
    {
        try
        {
            int var1 = 0;
            int var2 = 0;
            int var3 = 0;

            int i = 0;
            double totaldias = 0;
            int dias = 0;

            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "select dt_alterado from t03_projeto where (fl_ativa=1) and (dt_alterado is not null) " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    DateTime dt = (DateTime)dr["dt_alterado"];
                    dias = (DateTime.Now - dt).Days + 1;

                    if (dias <= 14)
                        var1++;
                    if (dias > 14 && dias <= 28)
                        var2++;
                    if (dias > 28)
                        var3++;

                    totaldias += dias;
                    i++;
                }
            }

            if ((totaldias / i) == 1)
            {
                linkAtualiza.Text = "Média de dias sem atualizar: " + (totaldias / i).ToString("N0") + " dia";
            }
            else if (double.IsNaN(totaldias / i))
            {
                if (i == 1)
                {
                    linkAtualiza.Text = "Projeto não foi atualizado.";
                }
                else
                {
                    linkAtualiza.Text = "Projetos não foram atualizados.";
                }

                linkAtualiza.Enabled = false;
            }
            else
            {
                linkAtualiza.Text = "Média de dias sem atualizar: " + (totaldias / i).ToString("N0") + " dias";
            }
            linkAtualiza.ToolTip = linkAtualiza.Text;

            string str = "";
            string strXMLData;
            strXMLData = "<graph caption='' bgColor='' shownames='1' showvalues='1' formatNumber='1' decimalSeparator=',' decimalPrecision='0' numberSuffix=''>";

            int total = var1;
            string periodostr = "0 a 2 semanas";
            string cormen = "#008000";

            if (var1 == 0)
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' color='" + cormen + "'/>";
            }
            else
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' value='" + total.ToString().Replace(",", ".") + "' color='" + cormen + "'/>";
            }


            total = var2;
            periodostr = "2 a 4 semanas";
            cormen = "#7A00A8";

            if (var2 == 0)
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' color='" + cormen + "'/>";
            }
            else
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' value='" + total.ToString().Replace(",", ".") + "' color='" + cormen + "'/>";
            }

            total = var3;
            periodostr = "mais de 4 semanas";
            cormen = "#FF0000";
            if (var3 == 0)
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' color='" + cormen + "'/>";
            }
            else
            {
                strXMLData = strXMLData + "<set name='" + periodostr + "' value='" + total.ToString().Replace(",", ".") + "' color='" + cormen + "'/>";
            }



            strXMLData = strXMLData + "</graph>";

            //-------------------------------------------------------------------- 
            //Destroy objects 

            string largura = "340";
            string altura = "150";

            str = "<OBJECT classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" WIDTH=" + largura + " HEIGHT=\"" + altura + "\" ALIGN=\"\">";
            str += "<param name=\"wmode\" value=\"transparent\">";
            str += "<PARAM NAME=\"FlashVars\" value=\"&dataXML=" + strXMLData + "\">";
            str += "<PARAM NAME=movie VALUE=\"charts/FC_2_3_Column3D.swf?chartWidth=" + largura + "&amp;chartHeight=" + altura + "\">";
            str += "<PARAM NAME=quality VALUE=high>";
            str += "<PARAM NAME=bgcolor VALUE=#FFFFFF>";
            str += "<EMBED src=\"charts/FC_2_3_Column3D.swf?chartWidth=" + largura + "&&chartHeight=" + altura + "\" FlashVars=\"&dataXML=" + strXMLData + "\" quality=high bgcolor=#FFFFFF WIDTH=" + largura + " HEIGHT=\"" + altura + "\" ALIGN=\"\" TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\"></EMBED>";
            str += "</OBJECT>";

            pn.Controls.Add(pb.GetLiteral(str));
        }
        catch (Exception ex)
        {
            Response.Write("GrafAtualizacao: " + ex.Message);
        }
    }

    public void AcaoStatus(Panel pn)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int azul = 0;
            int amarelo = 0;
            int vermelho = 0;
            int verde = 0;
            int total = 0;

            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "select * from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by t03_cd_projeto";
                foreach (DataRow drp in t03.ListQuery().Tables[0].Rows)
                {
                    t08_acao t08 = new t08_acao();
                    {
                        t08.t03_cd_projeto = (int)drp["t03_cd_projeto"];
                        foreach (DataRow dra in t08.List().Tables[0].Rows)
                        {

                            bool r = false;
                            bool g = false;
                            bool b = false;
                            t09_marco t09 = new t09_marco();
                            {
                                t09.order = " and t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao where t08_cd_acao not in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='R')) ";
                                t09.t08_cd_acao = Int32.Parse(dra["t08_cd_acao"].ToString());
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
                                vermelho++;
                            }
                            else if (g)
                            {
                                verde++;
                            }
                            else if (b)
                            {
                                azul++;
                            }

                            if (!r)
                            {
                                t29_acaorestricao t29 = new t29_acaorestricao();
                                {
                                    t29.t08_cd_acao = Int32.Parse(dra["t08_cd_acao"].ToString());
                                    t29.RetrieveAcao();
                                    if (t29.Found)
                                    {
                                        amarelo++;
                                    }

                                }
                            }
                        }


                    }
                }


                total = verde + vermelho + amarelo + azul;
                if (total > 0)
                {

                    lblAzul.Text = azul.ToString();
                    lblVerde.Text = verde.ToString();
                    lblAmarela.Text = amarelo.ToString();
                    lblVermelha.Text = vermelho.ToString();

                    lblFatiaAzul.Text = ((azul * 100) / total).ToString();
                    lblFatiaVerde.Text = ((verde * 100) / total).ToString();
                    lblFatiaAmarela.Text = ((amarelo * 100) / total).ToString();
                    lblFatiaVermelha.Text = ((vermelho * 100) / total).ToString();

                    if (azul == 0) linkConcluidos.NavigateUrl = "";
                    if (verde == 0) linkPrazos.NavigateUrl = "";
                    if (amarelo == 0) linkComRestricoes.NavigateUrl = "";
                    if (vermelho == 0) linkAtraso.NavigateUrl = "";



                    sb.Append("<table width=100% height=20 border=0 cellpadding=0 cellspacing=0><tr>");
                    if (azul != 0)
                    {
                        sb.Append("<td style=\"border:none;background:url('images/B.gif');width:" + (azul * 100) / total + "%\" title='" + (azul * 100) / total + "%'>&nbsp;</td>");
                    }
                    if (verde != 0)
                    {
                        sb.Append("<td style=\"border:none;background:url('images/G.gif');width:" + (verde * 100) / total + "%\" title='" + (verde * 100) / total + "%'>&nbsp;</td>");
                    }
                    if (amarelo != 0)
                    {
                        sb.Append("<td style=\"border:none;background:url('images/Y.gif');width:" + (amarelo * 100) / total + "%\" title='" + (amarelo * 100) / total + "%'>&nbsp;</td>");
                    }
                    if (vermelho != 0)
                    {
                        sb.Append("<td style=\"border:none;background:url('images/R.gif');width:" + (vermelho * 100) / total + "%\" title='" + (vermelho * 100) / total + "%'>&nbsp;</td>");
                    }
                    sb.Append("</tr></table>");
                }
                else
                {

                    lblAzul.Text = "0";
                    lblVerde.Text = "0";
                    lblAmarela.Text = "0";
                    lblVermelha.Text = "0";

                    lblFatiaAzul.Text = "0";
                    lblFatiaVerde.Text = "0";
                    lblFatiaAmarela.Text = "0";
                    lblFatiaVermelha.Text = "0";

                    linkConcluidos.NavigateUrl = "";
                    linkPrazos.NavigateUrl = "";
                    linkComRestricoes.NavigateUrl = "";
                    linkAtraso.NavigateUrl = "";
                }
            }
            pn.Controls.Add(pb.GetLiteral(sb.ToString()));
        }
        catch (Exception ex)
        {
            Response.Write("AcaoStatus: " + ex.Message);
        }
    }

    public void GrafFisicoFinanceiro(Panel pn)
    {
        try
        {
            fis_prev = 0; fin_prev = 0;
            fis_real = 0; fin_real = 0;
            detalhamentoFisico();
            detalhamentoFinanceiro();
            lblifisica.Text = fis_real.ToString("N2");
            lblifinanceira.Text = fin_real.ToString("N2");

            string strxml = "<graph caption='' subCaption='' yaxisname='' xaxisname='' showAlternateVGridColor='1' showvalues='1' yAxisMaxValue='100' numberSuffix='%25' ";
            strxml += "shownames='1' alternateVGridAlpha='30' alternateVGridColor='AFD8F8' numDivLines='4' ";
            strxml += "decimalPrecision='2' canvasBorderThickness='1' canvasBorderColor='114B78' baseFontColor='114B78' ";
            strxml += "hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>";
            strxml += "<set name='Físico previsto' hoverText='Físico previsto' value='" + fis_prev.ToString().Replace(",", ".") + "' color='00A900' alpha='70'/> ";
            strxml += "<set name='Físico realizado' hoverText='Físico realizado' value='" + fis_real.ToString().Replace(",", ".") + "' color='0000FF' alpha='70'/> ";
            strxml += "<set name='Financeiro previsto' hoverText='Financeiro previsto' value='" + fin_prev.ToString().Replace(",", ".") + "' color='00A900' alpha='70'/> ";
            strxml += "<set name='Financeiro realizado' hoverText='Financeiro realizado' value='" + fin_real.ToString().Replace(",", ".") + "' color='0000FF' alpha='70'/> ";
            strxml += "</graph>";

            int altura = 130;
            int largura = 370;
            string html = "";
            html += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" ";
            html += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" ";
            html += "width=\"" + (largura + 40) + "\" height=\"" + (altura - 10) + "\" align=\"center\">";
            html += "<param name=\"movie\" value=\"charts/FC_2_3_Bar2D.swf\" />";
            html += "<param name=\"FlashVars\" VALUE=\"&dataXML=" + strxml + "&chartWidth=" + largura + "&chartHeight=" + altura + "\" />";
            html += "</object>";

            pn.Controls.Add(pb.GetLiteral(html));

            
        }
        catch (Exception ex)
        {
            Response.Write("GrafFisFin: " + ex.Message);
        }
    }

    protected void IndicadoresCarteira()
    {
        try
        {
            double xcont = 0;
            //Projetos 
            double ycont = 0;
            //Financeiro 
            double xtotal = 0;
            double parceiro = 0;
            t03_projeto t03 = new t03_projeto();
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbP = new System.Text.StringBuilder();
                sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
                sb.Append("<tr class=\"headerGrid\">");
                sb.Append("<td>Projeto</td>");
                sb.Append("<td  style=text-align:center>Valor Alavancado para cada R$ 1,00</td>");
                sb.Append("</tr>");

                sbP.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
                sbP.Append("<tr class=\"headerGrid\">");
                sbP.Append("<td>Projeto</td>");
                sbP.Append("<td style=text-align:center>Parcerias</td>");
                sbP.Append("</tr>");

                t03.order = "select * from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    double xtotal_proprio = 0;
                    double xtotal_externo = 0;
                    xcont++;
                    string str = "";
                    str = "select sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8+ vl_p9+ vl_p10+ vl_p11+ vl_p12) as total, ";
                    str += "t11_cd_financeiro, t05.fl_entidade from t28_vlfinanceiro t28 ";
                    str += "left join t05_parceiro t05 on t05.t05_cd_parceiro in (select t05_cd_parceiro from t11_financeiro where t11_cd_financeiro = t28.t11_cd_financeiro)";
                    str += "where t11_cd_financeiro in ";
                    str += "(select t11_cd_financeiro  from t11_financeiro where ";
                    str += "t08_cd_acao in ";
                    str += "(select t08_cd_acao from t08_acao where t03_cd_projeto = " + dr["t03_cd_projeto"] + " ";
                    str += "and fl_ativa=1)) ";
                    str += "group by t11_cd_financeiro, t05.fl_entidade ";
                    str += "order by t11_cd_financeiro";

                    t03.order = str;
                    foreach (DataRow dr2 in t03.ListQuery().Tables[0].Rows)
                    {
                        if (bool.Parse(dr2["fl_entidade"].ToString()))
                        {
                            xtotal_proprio += double.Parse(dr2["total"].ToString());
                        }
                        else
                        {
                            xtotal_externo += double.Parse(dr2["total"].ToString());
                        }


                    }
                    double totalprojeto = 0;
                    if (xtotal_proprio > 0)
                    {
                        xtotal += (xtotal_externo / xtotal_proprio);
                        totalprojeto = (xtotal_externo / xtotal_proprio);
                    }

                    sb.Append("<tr>");
                    sb.Append("<td>" + dr["NM_PROJETO"] + "</td>");
                    sb.Append("<td style=text-align:center>" + totalprojeto.ToString("N2") + "</td>");
                    sb.Append("</tr>");


                    //parceiros
                    double parcProjeto = 0;
                    ycont++;
                    t03.order = "select count(distinct(t05_cd_parceiro)) as parceiro from t06_parceiroprojeto where t03_cd_projeto=" + dr["t03_cd_projeto"];
                    foreach (DataRow dr2 in t03.ListQuery().Tables[0].Rows)
                    {
                        parcProjeto = double.Parse(dr2["parceiro"].ToString());
                        parceiro += double.Parse(dr2["parceiro"].ToString());
                    }
                    sbP.Append("<tr>");
                    sbP.Append("<td>" + dr["NM_PROJETO"] + "</td>");
                    sbP.Append("<td style=text-align:center>" + parcProjeto + "</td>");
                    sbP.Append("</tr>");

                }





                if (xcont > 0)
                {
                    lblialavancagem.Text = (xtotal / xcont).ToString("N2");
                    //if ((xtotal / xcont) == 0)
                    //    linkAlavancagem.NavigateUrl = "";
                }
                else
                {
                    //linkAlavancagem.NavigateUrl = "";
                }
                if (ycont > 0)
                {
                    lblimobilizacao.Text = (parceiro / ycont).ToString("N2");
                    //if ((parceiro / ycont) == 0)
                        //linkParceiros.NavigateUrl = "";
                }
                else
                {
                    //linkParceiros.NavigateUrl = "";
                }

                sb.Append("<tr class=\"headerGrid\">");
                sb.Append("<td>Média </td>");
                sb.Append("<td style=text-align:center>" + (xtotal / xcont).ToString("N2") + "</td>");
                sb.Append("</tr>");
                sb.Append("</table><br>");

                sbP.Append("<tr class=\"headerGrid\">");
                sbP.Append("<td>Média</td>");
                sbP.Append("<td style=text-align:center>" + (parceiro / ycont).ToString("N0") + "</td>");
                sbP.Append("</tr>");
                sbP.Append("</table><br>");

                Session["MonAlavancagem"] = sb.ToString();
                Session["MomParceiros"] = sbP.ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("Indicadores: " + ex.Message);
        }
    }

    protected void detalhamentoFisico()
    {
        try
        {
            System.Text.StringBuilder sbFis = new System.Text.StringBuilder();
            sbFis.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
            sbFis.Append("<tr class=\"headerGrid\">");
            sbFis.Append("<td>Projeto</td>");
            sbFis.Append("<td style=\"text-align:right\">Previsto (%)</td>");
            sbFis.Append("<td style=\"text-align:right\">Realizado (%)</td>");
            sbFis.Append("<td style=\"text-align:right\">Realizado / Previsto (%)</td>");
            sbFis.Append("</tr>");

            int ano = DateTime.Now.Year;
            int mes = DateTime.Now.Month;

            double mc_p1 = 0; double mc_p2 = 0; double mc_p3 = 0; double mc_p4 = 0;
            double mc_r1 = 0; double mc_r2 = 0; double mc_r3 = 0; double mc_r4 = 0;
            int contproj = 0;
            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "select * from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow drp in t03.ListQuery().Tables[0].Rows)
                {
                    int cont = 0;
                    double projprev = 0;
                    double projreal = 0;

                    contproj++;
                    double amc_p1 = 0; double amc_p2 = 0; double amc_p3 = 0; double amc_p4 = 0;
                    double amc_r1 = 0; double amc_r2 = 0; double amc_r3 = 0; double amc_r4 = 0;
                    double previsto = 0;
                    double realizado = 0;
                    double previsto_total = 0;

                    //########## PREVISTO ###########
                    t03.order = "select * from t09_marco where fl_ativa=1 and t08_cd_acao in (select t08_cd_acao from t08_acao where t03_cd_projeto=" + drp["t03_cd_projeto"] + ")";
                    foreach (DataRow drm in t03.ListQuery().Tables[0].Rows)
                    {
                        cont++;
                        DateTime dt_original = (DateTime)drm["dt_original"];
                        if (dt_original.Year < ano)
                        {
                            previsto += double.Parse(drm["nu_esforco"].ToString());
                        }
                        else if (dt_original.Year == ano)
                        {
                            if ((dt_original.Month >= 1) && (dt_original.Month <= 3))
                            {
                                amc_p1 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 4) && (dt_original.Month <= 6))
                            {
                                amc_p2 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 7) && (dt_original.Month <= 9))
                            {
                                amc_p3 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 10) && (dt_original.Month <= 12))
                            {
                                amc_p4 += double.Parse(drm["nu_esforco"].ToString());
                            }
                        }
                        previsto_total += double.Parse(drm["nu_esforco"].ToString());

                    }

                    //########## REALIZADO ###########
                    t03.order = "select * from t09_marco where fl_ativa=1 and fl_status='B' and t08_cd_acao in (select t08_cd_acao from t08_acao where t03_cd_projeto=" + drp["t03_cd_projeto"] + ")";
                    foreach (DataRow drm in t03.ListQuery().Tables[0].Rows)
                    {
                        DateTime dt_original = (DateTime)drm["dt_original"];
                        if (dt_original.Year < ano)
                        {
                            realizado += double.Parse(drm["nu_esforco"].ToString());
                        }
                        else if (dt_original.Year == ano)
                        {
                            if ((dt_original.Month >= 1) && (dt_original.Month <= 3))
                            {
                                amc_r1 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 4) && (dt_original.Month <= 6))
                            {
                                amc_r2 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 7) && (dt_original.Month <= 9))
                            {
                                amc_r3 += double.Parse(drm["nu_esforco"].ToString());
                            }
                            else if ((dt_original.Month >= 10) && (dt_original.Month <= 12))
                            {
                                amc_r4 += double.Parse(drm["nu_esforco"].ToString());
                            }
                        }
                    }
                    if (previsto_total > 0)
                    {
                        mc_p1 += ((previsto + amc_p1) * 100) / previsto_total;
                        mc_p2 += ((previsto + amc_p2) * 100) / previsto_total;
                        mc_p3 += ((previsto + amc_p3) * 100) / previsto_total;
                        mc_p4 += ((previsto + amc_p4) * 100) / previsto_total;

                        mc_r1 += ((realizado + amc_r1) * 100) / previsto_total;
                        mc_r2 += ((realizado + amc_r2) * 100) / previsto_total;
                        mc_r3 += ((realizado + amc_r3) * 100) / previsto_total;
                        mc_r4 += ((realizado + amc_r4) * 100) / previsto_total;

                        if ((mes >= 1) && (mes <= 3))
                        {
                            projprev = ((previsto + amc_p1) * 100) / previsto_total;
                            projreal = ((previsto + amc_r1) * 100) / previsto_total;
                        }
                        else if ((mes >= 4) && (mes <= 6))
                        {
                            projprev = ((previsto + amc_p1) * 100) / previsto_total;
                            projprev += ((previsto + amc_p2) * 100) / previsto_total;

                            projreal = ((previsto + amc_r1) * 100) / previsto_total;
                            projreal += ((previsto + amc_r2) * 100) / previsto_total;
                        }
                        else if ((mes >= 7) && (mes <= 9))
                        {
                            projprev = ((previsto + amc_p1) * 100) / previsto_total;
                            projprev += ((previsto + amc_p2) * 100) / previsto_total;
                            projprev += ((previsto + amc_p3) * 100) / previsto_total;

                            projreal = ((previsto + amc_r1) * 100) / previsto_total;
                            projreal += ((previsto + amc_r2) * 100) / previsto_total;
                            projreal += ((previsto + amc_r3) * 100) / previsto_total;
                        }
                        else if ((mes >= 10) && (mes <= 12))
                        {
                            projprev = ((previsto + amc_p1) * 100) / previsto_total;
                            projprev += ((previsto + amc_p2) * 100) / previsto_total;
                            projprev += ((previsto + amc_p3) * 100) / previsto_total;
                            projprev += ((previsto + amc_p4) * 100) / previsto_total;

                            projreal = ((previsto + amc_r1) * 100) / previsto_total;
                            projreal += ((previsto + amc_r2) * 100) / previsto_total;
                            projreal += ((previsto + amc_r3) * 100) / previsto_total;
                            projreal += ((previsto + amc_r4) * 100) / previsto_total;
                        }
                    }
                    sbFis.Append("<tr>");
                    sbFis.Append("<td>" + drp["nm_projeto"] + "</td>");
                    if (cont != 0)
                    {
                        sbFis.Append("<td style=text-align:right>" + projprev.ToString("N2") + "</td>");
                        sbFis.Append("<td style=text-align:right>" + projreal.ToString("N2") + "</td>");
                        if (projprev != 0)
                        {
                            sbFis.Append("<td style=text-align:right>" + (projreal / projprev).ToString("N2") + "</td>");
                        }
                        else
                        {
                            sbFis.Append("<td style=text-align:right>0,00</td>");
                        }
                    }
                    else
                    {
                        sbFis.Append("<td style=text-align:right>0,00</td>");
                        sbFis.Append("<td style=text-align:right>0,00</td>");
                        sbFis.Append("<td style=text-align:right>0,00</td>");
                    }
                    sbFis.Append("</tr>");

                }
                sbFis.Append("</table>");

                string strxml = "";
                strxml = "<graph caption='(Ano Referência " + ano + ")' formatNumberScale='0' ";
                strxml += "yAxisMaxValue='100' decimalPrecision='2' xAxisName='' yAxisName='' numberPrefix='' ";
                strxml += "numberSuffix='%25' showNames='1' showvalues='0'>";
                strxml += "<categories>";
                //=== lista as datas das medições 
                strxml += "<category name='1º Trim' showName='1'/>";
                strxml += "<category name='2º Trim' showName='1'/>";
                strxml += "<category name='3º Trim' showName='1'/>";
                strxml += "<category name='4º Trim' showName='1'/>";
                strxml += "</categories>";

                strxml += "<dataset seriesName='Previsto' color='00A900' >";
                if (contproj > 0)
                {
                    strxml += "<set value='" + (mc_p1 / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + ((mc_p1 + mc_p2) / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + ((mc_p1 + mc_p2 + mc_p3) / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + ((mc_p1 + mc_p2 + mc_p3 + mc_p4) / contproj).ToString().Replace(",", ".") + "' />";
                }
                else
                {
                    strxml += "<set /><set /><set /><set />";
                }
                strxml += "</dataset>";

                strxml += "<dataset seriesName='Realizado' color='0000FF' >";
                if (contproj > 0)
                {
                    if ((mes >= 1))
                    {
                        strxml += "<set value='" + (mc_r1 / contproj).ToString().Replace(",", ".") + "' />";
                        fis_prev = (mc_p1 / contproj);
                        fis_real = (mc_r1 / contproj);
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 4))
                    {
                        strxml += "<set value='" + ((mc_r1 + mc_r2) / contproj).ToString().Replace(",", ".") + "' />";
                        fis_prev = ((mc_p1 + mc_p2) / contproj);
                        fis_real = ((mc_r1 + mc_r2) / contproj);
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 7))
                    {
                        strxml += "<set value='" + ((mc_r1 + mc_r2 + mc_r3) / contproj).ToString().Replace(",", ".") + "' />";
                        fis_prev = ((mc_p1 + mc_p2 + mc_p3) / contproj);
                        fis_real = ((mc_r1 + mc_r2 + mc_r3) / contproj);
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 10))
                    {
                        strxml += "<set value='" + ((mc_r1 + mc_r2 + mc_r3 + mc_r4) / contproj).ToString().Replace(",", ".") + "' />";
                        fis_prev = ((mc_p1 + mc_p2 + mc_p3 + mc_p4) / contproj);
                        fis_real = ((mc_r1 + mc_r2 + mc_r3 + mc_r4) / contproj);
                    }
                    else
                    {
                        strxml += "<set />";
                    }


                }
                else
                {
                    strxml += "<set /><set /><set /><set />";
                }
                strxml += "</dataset>";
                strxml += "<trendlines>";
                strxml += "<line startValue='22000' color='00cc00' displayValue='Average' isTrendZone='0'/>";
                strxml += "</trendlines>";
                strxml += "</graph>";

                int altura = 350;
                int largura = 500;
                string html = "";
                html += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" ";
                html += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" ";
                html += "width=\"" + (largura + 10) + "\" height=\"" + altura + "\" align=\"center\">";
                html += "<param name=\"movie\" value=\"charts/FC_2_3_MSColumn3D.swf\" /><param name=\"wmode\" value=\"transparent\">";
                html += "<param name=\"FlashVars\" VALUE=\"&dataXML=" + strxml + "&chartWidth=" + largura + "&chartHeight=" + altura + "\" />";
                html += "</object>";



                Session["MonFisicoGraf"] = html;
                Session["MonFisicoInd"] = sbFis.ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

    protected void detalhamentoFinanceiro()
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;text-align:left\">");
            sb.Append("<tr class=\"headerGrid\">");
            sb.Append("<td>Projeto</td>");
            sb.Append("<td style=\"text-align:right\">Previsto</td>");
            sb.Append("<td style=\"text-align:right\">Realizado</td>");
            sb.Append("<td style=\"text-align:right\">% de execução</td>");
            sb.Append("<td style=\"text-align:right\">Previsto (%)</td>");
            sb.Append("<td style=\"text-align:right\">Realizado (%)</td>");
            sb.Append("<td style=\"text-align:right\">Realizado / Previsto (%)</td>");
            sb.Append("</tr>");

            int ano = DateTime.Now.Year;
            int ano_ini = ano; int ano_fim = ano;
            int mes = DateTime.Now.Month;

            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "select min(year(dt_inicio)) as ano_ini, max(year(dt_fim)) as ano_fim from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    ano_ini = (int)dr["ano_ini"];
                    ano_fim = (int)dr["ano_fim"];
                }
            }

            int contproj = 0;
            double[] vl_p = new double[ano_fim - ano_ini];
            double[] vl_r = new double[ano_fim - ano_ini];

            double acprev = 0;
            double acreal = 0;

            //double[] vl_p1 = new double[ano_fim - ano_ini];
            //double[] vl_p4 = new double[ano_fim - ano_ini];
            //double[] vl_p8 = new double[ano_fim - ano_ini];
            //double[] vl_p12 = new double[ano_fim - ano_ini];
            //double[] vl_r1 = new double[ano_fim - ano_ini];
            //double[] vl_r4 = new double[ano_fim - ano_ini];
            //double[] vl_r8 = new double[ano_fim - ano_ini];
            //double[] vl_r12 = new double[ano_fim - ano_ini];
            //double[] vl_acp1 = new double[ano_fim - ano_ini];
            //double[] vl_acp4 = new double[ano_fim - ano_ini];
            //double[] vl_acp8 = new double[ano_fim - ano_ini];
            //double[] vl_acp12 = new double[ano_fim - ano_ini];
            //double[] vl_acr1 = new double[ano_fim - ano_ini];
            //double[] vl_acr4 = new double[ano_fim - ano_ini];
            //double[] vl_acr8 = new double[ano_fim - ano_ini];
            //double[] vl_acr12 = new double[ano_fim - ano_ini];

            t03 = new t03_projeto();
            {
                t03.order = "select min(year(dt_inicio)) as ano_ini, max(year(dt_fim)) as ano_fim from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro();
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    ano_ini = (int)dr["ano_ini"];
                    ano_fim = (int)dr["ano_fim"];
                }

                for (int i = ano_ini; ano_ini >= ano_fim; i++)
                {
                    vl_p[i] = 0; vl_r[i] = 0; vl_acp4[i] = 0; vl_acr4[i] = 0; vl_acp8[i] = 0; vl_acr8[i] = 0; vl_acp12[i] = 0; vl_acr12[i] = 0;
                    vl_p1[i] = 0; vl_r1[i] = 0; vl_p4[i] = 0; vl_r4[i] = 0; vl_p8[i] = 0; vl_r8[i] = 0; vl_p12[i] = 0; vl_r12[i] = 0;
                }

                t03.order = "select * from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    int cont = 0;
                    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        FINANCEIRO
                    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
                    contproj++;
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    string str2 = "";
                    double previsto = 0;
                    double realizado = 0;
                    double projprev = 0;
                    double projreal = 0;
                    double previsto_total = 0;
                    str = new System.Text.StringBuilder();
                    str.Append("select nu_ano,  ");
                    str.Append("sum(vl_p1) as vl_p1, sum(vl_r1) as vl_r1,");
                    str.Append("sum(vl_p1 + vl_p2+ vl_p3+ vl_p4) as vl_p4,");
                    str.Append("sum(vl_r1+ vl_r2+ vl_r3+ vl_r4) as vl_r4,");
                    str.Append("sum(vl_p5+ vl_p6+ vl_p7+ vl_p8) as vl_p8,");
                    str.Append("sum(vl_r5+ vl_r6+ vl_r7+ vl_r8) as vl_r8,");
                    str.Append("sum(vl_p9+ vl_p10+ vl_p11+ vl_p12) as vl_p12,");
                    str.Append("sum(vl_r9+ vl_r10+ vl_r11+ vl_r12) as vl_r12 ");
                    str.Append("from t28_vlfinanceiro where t11_cd_financeiro in  ");
                    str.Append("(select t11_cd_financeiro from t11_financeiro where t08_cd_acao in  ");
                    str.Append("(select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto=" + dr["t03_cd_projeto"].ToString() + ")) group by nu_ano ");
                    //Response.Write(str.ToString()+"<br><br>");
                    t03.order = str.ToString();
                    foreach (DataRow dr2 in t03.ListQuery().Tables[0].Rows)
                    {
                        cont++;
                        //if ((int)dr2["nu_ano"] < ano)
                        //{
                        //    previsto += double.Parse(dr2["vl_p12"].ToString());
                        //    realizado += double.Parse(dr2["vl_r12"].ToString());
                        //}
                        //else if ((int)dr2["nu_ano"] == ano)
                        //{
                        vl_acp1[(int)dr2["nu_ano"]] += previsto + double.Parse(dr2["vl_p1"].ToString());
                        vl_acr1[(int)dr2["nu_ano"]] += realizado + double.Parse(dr2["vl_r1"].ToString());
                        vl_acp4[(int)dr2["nu_ano"]] += previsto + double.Parse(dr2["vl_p4"].ToString());
                        vl_acr4[(int)dr2["nu_ano"]] += realizado + double.Parse(dr2["vl_r4"].ToString());
                        vl_acp8[(int)dr2["nu_ano"]] += previsto + double.Parse(dr2["vl_p8"].ToString());
                        vl_acr8[(int)dr2["nu_ano"]] += realizado + double.Parse(dr2["vl_r8"].ToString());
                        vl_acp12[(int)dr2["nu_ano"]] += previsto + double.Parse(dr2["vl_p12"].ToString());
                        vl_acr12[(int)dr2["nu_ano"]] += realizado + double.Parse(dr2["vl_r12"].ToString());

                        if ((mes >= 1) && (mes <= 3))
                        {
                            projprev += previsto + double.Parse(dr2["vl_p1"].ToString());
                            projreal += realizado + double.Parse(dr2["vl_r1"].ToString());
                        }
                        else if ((mes >= 4) && (mes <= 6))
                        {
                            projprev += previsto + double.Parse(dr2["vl_p4"].ToString());
                            projreal += realizado + double.Parse(dr2["vl_r4"].ToString());
                        }
                        else if ((mes >= 7) && (mes <= 9))
                        {
                            projprev += previsto + double.Parse(dr2["vl_p8"].ToString());
                            projreal += realizado + double.Parse(dr2["vl_r8"].ToString());
                        }
                        else if ((mes >= 10) && (mes <= 12))
                        {
                            projprev += previsto + double.Parse(dr2["vl_p12"].ToString());
                            projreal += realizado + double.Parse(dr2["vl_r12"].ToString());
                        }

                        //}
                        previsto_total += double.Parse(dr2["vl_p12"].ToString());
                        //Response.Write(previsto_total + "<br>");
                    }


                    sb.Append("<tr>");
                    sb.Append("<td>" + dr["nm_projeto"] + "</td>");
                    if (cont != 0)
                    {
                        sb.Append("<td style=text-align:right>" + projprev.ToString("N2") + "</td>");
                        sb.Append("<td style=text-align:right>" + projreal.ToString("N2") + "</td>");
                        if (projprev != 0)
                        {
                            sb.Append("<td style=text-align:right>" + (projreal / projprev).ToString("N2") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style=text-align:right>0,00</td>");
                        }
                        
                    }
                    else
                    {
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                    }

                    //Índice de realização financeira 
                    if (cont != 0)
                    {
                        sb.Append("<td style=text-align:right>" + ((projprev * 100) / previsto_total).ToString("N2") + "</td>");
                        sb.Append("<td style=text-align:right>" + ((projreal * 100) / previsto_total).ToString("N2") + "</td>");
                        if (projprev != 0)
                        {
                            sb.Append("<td style=text-align:right>" + (((projreal * 100) / previsto_total) / ((projprev * 100) / previsto_total)).ToString("N2") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style=text-align:right>0,00</td>");
                        }

                        acprev += (projprev * 100) / previsto_total;
                        acreal += (projreal * 100) / previsto_total;
                    }
                    else
                    {
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                    }
                    sb.Append("</tr>");

                }//FIM DR (PROJETOS) 

                sb.Append("<tr  class=\"headerGrid\">");
                sb.Append("<td colspan=3>Média</td>");
                sb.Append("<td style=text-align:right>" + (acprev / contproj).ToString("N2") + "</td>");
                sb.Append("<td style=text-align:right>" + (acreal / contproj).ToString("N2") + "</td>");
                if (fin_prev > 0)
                {
                    sb.Append("<td style=text-align:right>" + ((acreal / contproj) / (acprev / contproj)).ToString("N2") + "</td>");
                }
                else
                {
                    sb.Append("<td style=text-align:right>0,00</td>");
                }
                sb.Append("</tr>");
                sb.Append("</table>");

                Session["MonFinanceiro"] = sb.ToString();

                string strxml = "";
                strxml = "<graph caption='(Ano Referência " + ano + ")' formatNumberScale='0' ";
                strxml += "yAxisMaxValue='100' decimalPrecision='2' xAxisName='' yAxisName='' numberPrefix='' ";
                strxml += "numberSuffix='%25' showNames='1' showvalues='0'>";
                strxml += "<categories>";
                //=== lista as datas das medições 
                for (int i = ano_ini; ano_ini >= ano_fim; i++)
                {
                    strxml += "<category name='"+ i +"' showName='1'/>";
                }
                strxml += "</categories>";

                strxml += "<dataset seriesName='Previsto' color='00A900' >";
                if (contproj > 0)
                {
                    for (int i = ano_ini; ano_ini >= ano_fim; i++)
                    {
                        strxml += "<set value='" + ((vl_p1[i] + vl_p4[i] + vl_p8[i] + vl_p12[i]) / contproj).ToString().Replace(",", ".") + "' />";
                    }
                }
                else
                {
                    for (int i = ano_ini; ano_ini >= ano_fim; i++)
                    {
                        strxml += "<set />";
                    }
                }
                strxml += "</dataset>";

                strxml += "<dataset seriesName='Realizado' color='0000FF' >";
                if (contproj > 0)
                {
                        for (int i = ano_ini; ano_ini >= ano_fim; i++)
                        {
                            strxml += "<set value='" + ((vl_r1[i] + vl_r4[i] + vl_r8[i] + vl_r12[i]) / contproj).ToString().Replace(",", ".") + "' />";
                            fin_prev += ((vl_p1[i] + vl_p4[i] + vl_p8[i] + vl_p12[i]) / contproj);
                            fin_real += ((vl_r1[i] + vl_r4[i] + vl_r8[i] + vl_r12[i]) / contproj);
                        }
                }
                else
                {
                    for (int i = ano_ini; ano_ini >= ano_fim; i++)
                    {
                        strxml += "<set />";
                    }
                }
                strxml += "</dataset>";
                strxml += "<trendlines>";
                strxml += "<line startValue='22000' color='00cc00' displayValue='Average' isTrendZone='0'/>";
                strxml += "</trendlines>";
                strxml += "</graph>";

                int altura = 350;
                int largura = 500;
                string html = "";
                html += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" ";
                html += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" ";
                html += "width=\"" + (largura + 10) + "\" height=\"" + altura + "\" align=\"center\">";
                html += "<param name=\"movie\" value=\"charts/FC_2_3_MSColumn3D.swf\" /><param name=\"wmode\" value=\"transparent\">";
                html += "<param name=\"FlashVars\" VALUE=\"&dataXML=" + strxml + "&chartWidth=" + largura + "&chartHeight=" + altura + "\" />";
                html += "</object>";

                Response.Write(pb.GetLiteral(html + sb.ToString()));
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

}

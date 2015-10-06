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

public partial class MonFinanceiroGraf : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
       // GrafBind(Panel1);
        if (Session["MonFinanceiroGraf"] != null)
            Panel1.Controls.Add(pb.GetLiteral(Session["MonFinanceiroGraf"].ToString()));
        if (Session["MonFinanceiroInd"] != null)
            Panel1.Controls.Add(pb.GetLiteral(Session["MonFinanceiroInd"].ToString()));
        if (Session["MonFinanceiroIndParceiro"] != null)
            Panel1.Controls.Add(pb.GetLiteral(Session["MonFinanceiroIndParceiro"].ToString()));
         if (Session["MonFinanceiroPeriodo"] != null)
            Panel1.Controls.Add(pb.GetLiteral(Session["MonFinanceiroPeriodo"].ToString()));
    }

    protected void GrafBind(Panel pn)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;text-align:left\">");
            sb.Append("<tr class=\"headerGrid\">");
            sb.Append("<td>Projeto2</td>");
            sb.Append("<td style=\"text-align:right\">Previsto</td>");
            sb.Append("<td style=\"text-align:right\">Realizado</td>");
            sb.Append("<td style=\"text-align:right\">% de execução</td>");
            sb.Append("</tr>");

            int ano = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            
            int contproj = 0;
            double vl_prev = 0;
            double vl_real = 0;

            double vl_p1 = 0;double vl_p4 = 0;double vl_p8 = 0; double vl_p12 = 0;double vl_r1 = 0;double vl_r4 = 0;double vl_r8 = 0;double vl_r12 = 0;
            double vl_acp1 = 0;double vl_acp4 = 0;double vl_acp8 = 0;double vl_acp12 = 0;double vl_acr1 = 0;double vl_acr4 = 0;double vl_acr8 = 0;double vl_acr12 = 0;

            t03_projeto t03 = new t03_projeto();
            {
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
                    str.Append("sum(vl_p1) as vl_p1, sum(vl_r1) as vl_r1,  ");
                    str.Append("sum(vl_p1 + vl_p2+ vl_p3+ vl_p4) as vl_p4, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4) as vl_r4, ");
                    str.Append("sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8) as vl_p8, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6+ vl_r7+ vl_r8) as vl_r8, ");
                    str.Append("sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8+ vl_p9+ vl_p10+ vl_p11+ vl_p12) as vl_p12, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6+ vl_r7+ vl_r8+ vl_r9+ vl_r10+ vl_r11+ vl_r12) as vl_r12 ");
                    str.Append("from t28_vlfinanceiro where t11_cd_financeiro in  ");
                    str.Append("(select t11_cd_financeiro from t11_financeiro where t08_cd_acao in  ");
                    str.Append("(select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto=" + dr["t03_cd_projeto"].ToString() + ")) group by nu_ano ");
                    //Response.Write(str.ToString()+"<br><br>");
                    t03.order = str.ToString();
                    foreach (DataRow dr2 in t03.ListQuery().Tables[0].Rows)
                    {
                        cont++;
                        if ((int)dr2["nu_ano"] < ano)
                        {
                            if (dr2["vl_p12"] != DBNull.Value)
                            {
                                previsto += double.Parse(dr2["vl_p12"].ToString());
                            }
                            if (dr2["vl_r12"] != DBNull.Value)
                            {
                                realizado += double.Parse(dr2["vl_r12"].ToString());
                            }
                        }
                        else if ((int)dr2["nu_ano"] == ano)
                        {
                            vl_acp1 += previsto + double.Parse(dr2["vl_p1"].ToString());
                            vl_acr1 += realizado + double.Parse(dr2["vl_r1"].ToString());
                            vl_acp4 += previsto + double.Parse(dr2["vl_p4"].ToString());
                            vl_acr4 += realizado + double.Parse(dr2["vl_r4"].ToString());
                            vl_acp8 += previsto + double.Parse(dr2["vl_p8"].ToString());
                            vl_acr8 += realizado + double.Parse(dr2["vl_r8"].ToString());
                            vl_acp12 += previsto + double.Parse(dr2["vl_p12"].ToString());
                            vl_acr12 += realizado + double.Parse(dr2["vl_r12"].ToString());

                            if ((mes >= 1) && (mes <= 3))
                            {
                                if (dr2["vl_p1"] != DBNull.Value)
                                {
                                    projprev += previsto + double.Parse(dr2["vl_p1"].ToString());
                                }
                                if (dr2["vl_r1"] != DBNull.Value)
                                {
                                    projreal += realizado + double.Parse(dr2["vl_r1"].ToString());
                                }
                            }
                            else if ((mes >= 4) && (mes <= 6))
                            {
                                if (dr2["vl_p4"] != DBNull.Value)
                                {
                                    projprev += previsto + double.Parse(dr2["vl_p4"].ToString());
                                }
                                if (dr2["vl_r4"] != DBNull.Value)
                                {
                                    projreal += realizado + double.Parse(dr2["vl_r4"].ToString());
                                }
                            }
                            else if ((mes >= 7) && (mes <= 9))
                            {
                                if (dr2["vl_p8"] != DBNull.Value)
                                {
                                    projprev += previsto + double.Parse(dr2["vl_p8"].ToString());
                                }
                                if (dr2["vl_r8"] != DBNull.Value)
                                {
                                    projreal += realizado + double.Parse(dr2["vl_r8"].ToString());
                                }
                            }
                            else if ((mes >= 10) && (mes <= 12))
                            {
                                if (dr2["vl_p12"] != DBNull.Value)
                                {
                                    projprev += previsto + double.Parse(dr2["vl_p12"].ToString());
                                }
                                if (dr2["vl_r12"] != DBNull.Value)
                                {
                                    projreal += realizado + double.Parse(dr2["vl_r12"].ToString());
                                }
                            }
                            
                        }
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
                        vl_p1 += (vl_acp1 * 100) / previsto_total;
                        vl_p4 += (vl_acp4 * 100) / previsto_total;
                        vl_p8 += (vl_acp8 * 100) / previsto_total;
                        vl_p12 += (vl_acp12 * 100) / previsto_total;

                        vl_r1 += (vl_acr1 * 100) / previsto_total;
                        vl_r4 += (vl_acr4 * 100) / previsto_total;
                        vl_r8 += (vl_acr8 * 100) / previsto_total;
                        vl_r12 += (vl_acr12 * 100) / previsto_total;
                    }
                    else
                    {
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                    }
                    sb.Append("</tr>");


                    
                    
                }//FIM DR (PROJETOS) 
                sb.Append("</table>");
                

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
                    strxml += "<set value='" + (vl_p1 / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + (vl_p4 / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + (vl_p8 / contproj).ToString().Replace(",", ".") + "' />";
                    strxml += "<set value='" + (vl_p12 / contproj).ToString().Replace(",", ".") + "' />";
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
                        strxml += "<set value='" + (vl_r1 / contproj).ToString().Replace(",", ".") + "' />";
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 4))
                    {
                        strxml += "<set value='" + (vl_r4 / contproj).ToString().Replace(",", ".") + "' />";
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 7))
                    {
                        strxml += "<set value='" + (vl_r8 / contproj).ToString().Replace(",", ".") + "' />";
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 10))
                    {
                        strxml += "<set value='" + (vl_r12 / contproj).ToString().Replace(",", ".") + "' />";
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


                pn.Controls.Add(pb.GetLiteral(html + sb.ToString()));

            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }


}

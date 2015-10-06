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

public partial class MonFisicoGraf : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        //GrafBind(Panel1);
        if (Session["MonFisicoGraf"] != null)
        {
            Panel1.Controls.Add(pb.GetLiteral(Session["MonFisicoGraf"].ToString()));
        }
    }

    protected void GrafBind(Panel pn)
    {
        try
        {
            System.Text.StringBuilder sbFis = new System.Text.StringBuilder();
            sbFis.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
            sbFis.Append("<tr class=\"headerGrid\">");
            sbFis.Append("<td>Projeto</td>");
            sbFis.Append("<td style=\"text-align:right\">Previsto (%)</td>");
            sbFis.Append("<td style=\"text-align:right\">Realizado (%)</td>");
            sbFis.Append("<td style=\"text-align:right\">Índice de realização</td>");
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
                    t03.order = "select * from t09_marco where fl_ativa=1 and t08_cd_acao in (select t08_cd_acao from t08_acao where t03_cd_projeto="+ drp["t03_cd_projeto"] +")";
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
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 4))
                    {
                        strxml += "<set value='" + ((mc_r1+mc_r2) / contproj).ToString().Replace(",", ".") + "' />";
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 7))
                    {
                        strxml += "<set value='" + ((mc_r1 + mc_r2 + mc_r3) / contproj).ToString().Replace(",", ".") + "' />";
                    }
                    else
                    {
                        strxml += "<set />";
                    }

                    if ((mes >= 10))
                    {
                        strxml += "<set value='" + ((mc_r1 + mc_r2 + mc_r3 + mc_r4) / contproj).ToString().Replace(",", ".") + "' />";
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


                pn.Controls.Add(pb.GetLiteral(html));
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
}

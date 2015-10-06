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

public partial class MonFinanceiroGraf2 : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        GrafBind(Panel1);
    }

    protected void GrafBind(Panel pn)
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
            sb.Append("</tr>");

            int ano = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int contac = 0;
            int cont = 0;
            int contproj = 0;
            double vl_prev = 0;
            double vl_real = 0;

            //Acumulador Previsto 
            double acreal = 0;
            //Acumulador Realizado 
            double acprev = 0;

            double projprev = 0;
            double projreal = 0;

            double vl_p1 = 0;
            double vl_p4 = 0;
            double vl_p8 = 0;
            double vl_p12 = 0;

            double vl_r1 = 0;
            double vl_r4 = 0;
            double vl_r8 = 0;
            double vl_r12 = 0;

            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "select * from t03_projeto where (fl_ativa=1) and (dt_alterado is not null) " + pb.sqlfiltro() + " order by dt_alterado desc";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        FÍSICO
                    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
                    double vl_fp1 = 0;
                    double vl_fp4 = 0;
                    double vl_fp8 = 0;
                    double vl_fp12 = 0;

                    double vl_fr1 = 0;
                    double vl_fr4 = 0;
                    double vl_fr8 = 0;
                    double vl_fr12 = 0;

                    contproj++;
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    string str2 = "";
                    double previsto_total = 0;
                    double previsto = 0;
                    double realizado = 0;

                    str = new System.Text.StringBuilder();
                    str.Append("select * from t11_financeiro where t08_cd_acao in ");
                    str.Append("(select t08_cd_acao from t08_acao where t03_cd_projeto = " + dr["t03_cd_projeto"].ToString());
                    str.Append(" and fl_ativa=1)");


                    t03.order = str.ToString();
                    foreach (DataRow dr2 in t03.ListQuery().Tables[0].Rows) //INÍCIO DR2 (FINANCEIRO) 
                    {
                        double vl_acp1 = 0;
                        double vl_acp4 = 0;
                        double vl_acp8 = 0;
                        double vl_acp12 = 0;

                        double vl_acr1 = 0;
                        double vl_acr4 = 0;
                        double vl_acr8 = 0;
                        double vl_acr12 = 0;
                        contac++;
                        cont++;
                        str = new System.Text.StringBuilder();
                        {
                            str.Append("select t11_cd_financeiro, nu_ano, ");
                            str.Append("sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8+ vl_p9+ vl_p10+ vl_p11+ vl_p12) as previsto, ");
                            str.Append("sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6+ vl_r7+ vl_r8+ vl_r9+ vl_r10+ vl_r11+ vl_r12) as realizado ");
                            str.Append("from t28_vlfinanceiro where t11_cd_financeiro = " + dr2["t11_cd_financeiro"].ToString() + " group by t11_cd_financeiro, nu_ano");
                        }

                        t03.order = str.ToString();
                        foreach (DataRow dr3 in t03.ListQuery().Tables[0].Rows) //INÍCIO DR3 (VALOR DE FINANCEIRO)
                        {
                            if ((int)dr3["nu_ano"] < ano)
                            {
                                previsto += double.Parse(dr3["previsto"].ToString());
                                realizado += double.Parse(dr3["realizado"].ToString());
                            }
                            else if ((int)dr3["nu_ano"] == ano)
                            {
                                //if ((mes >= 1) && (mes <= 3))
                                //{
                                    str2 = "select sum(vl_p1 + vl_p2+ vl_p3) as previsto, sum(vl_r1+ vl_r2+ vl_r3) as realizado from t28_vlfinanceiro where t11_cd_financeiro  = " + dr3["t11_cd_financeiro"].ToString();
                                    str2 += " and nu_ano=" + ano;
                                    t03.order = str2;
                                    foreach (DataRow dr4 in t03.ListQuery().Tables[0].Rows)
                                    {
                                        vl_acp1 += double.Parse(dr4["previsto"].ToString());
                                        vl_acr1 += double.Parse(dr4["realizado"].ToString());
                                    }
                                //}
                                //else if ((mes >= 4) && (mes <= 6))
                                //{
                                    str2 = "select sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6) as previsto, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6) as realizado from t28_vlfinanceiro where t11_cd_financeiro  = " + dr3["t11_cd_financeiro"].ToString();
                                    str2 += " and nu_ano=" + ano;
                                    t03.order = str2;
                                    foreach (DataRow dr4 in t03.ListQuery().Tables[0].Rows)
                                    {
                                        vl_acp4 += double.Parse(dr4["previsto"].ToString());
                                        vl_acr4 += double.Parse(dr4["realizado"].ToString());
                                    }
                                //}
                                //else if ((mes >= 7) && (mes <= 9))
                                //{
                                    str2 = "select sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8+ vl_p9) as previsto, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6+ vl_r7+ vl_r8+ vl_r9) as realizado from t28_vlfinanceiro where t11_cd_financeiro  = " + dr3["t11_cd_financeiro"].ToString();
                                    str2 += " and nu_ano=" + ano;
                                    t03.order = str2;
                                    foreach (DataRow dr4 in t03.ListQuery().Tables[0].Rows)
                                    {
                                        vl_acp8 += double.Parse(dr4["previsto"].ToString());
                                        vl_acr8 += double.Parse(dr4["realizado"].ToString());
                                    }
                                //}
                                //else if ((mes >= 10) && (mes <= 12))
                                //{
                                    str2 = "select sum(vl_p1 + vl_p2+ vl_p3+ vl_p4+ vl_p5+ vl_p6+ vl_p7+ vl_p8+ vl_p9+ vl_p10+ vl_p11+ vl_p12) as previsto, sum(vl_r1+ vl_r2+ vl_r3+ vl_r4+ vl_r5+ vl_r6+ vl_r7+ vl_r8+ vl_r9+ vl_r10+ vl_r11+ vl_r12) as realizado from t28_vlfinanceiro where t11_cd_financeiro = " + dr3["t11_cd_financeiro"].ToString();
                                    str2 += " and nu_ano=" + ano;
                                    t03.order = str2;
                                    foreach (DataRow dr4 in t03.ListQuery().Tables[0].Rows)
                                    {
                                        vl_acp12 += double.Parse(dr4["previsto"].ToString());
                                        vl_acr12 += double.Parse(dr4["realizado"].ToString());
                                    }
                                //}
                                //Response.Write(String.Format("Previsto 1º={0}, 2º={1}, 3º={2}, 4º={3} <br>", vl_acp1, vl_acp4, vl_acp8, vl_acp12));
                                //Response.Write(String.Format("Realizado 1º={0}, 2º={1}, 3º={2}, 4º={3} <br><br>", vl_acr1, vl_acr4, vl_acr8, vl_acr12));


                            }
                            previsto_total += double.Parse(dr3["previsto"].ToString());
                        }//FIM DR3 


                        if (previsto_total != 0)
                        {
                            //projprev += (previsto * 100) / previsto_total;
                            //projreal += (realizado * 100) / previsto_total;

                            vl_fp1 += ((vl_acp1 + previsto) * 100) / previsto_total;
                            vl_fp4 += ((vl_acp4 + previsto) * 100) / previsto_total;
                            vl_fp8 += ((vl_acp8 + previsto) * 100) / previsto_total;
                            vl_fp12 += ((vl_acp12 + previsto) * 100) / previsto_total;

                            vl_fr1 += ((vl_acr1 + previsto) * 100) / previsto_total;
                            vl_fr4 += ((vl_acr4 + previsto) * 100) / previsto_total;
                            vl_fr8 += ((vl_acr8 + previsto) * 100) / previsto_total;
                            vl_fr12 += ((vl_acr12 + previsto) * 100) / previsto_total;

                            //RELATÓRIO FINANCEIRO 
                            if ((mes >= 1) && (mes <= 3))
                            {
                                projprev += vl_fp1;
                                projreal += vl_fr1;
                            }
                            else if ((mes >= 4) && (mes <= 6))
                            {
                                projprev += vl_fp4;
                                projreal += vl_fr4;
                            }
                            else if ((mes >= 7) && (mes <= 9))
                            {
                                projprev += vl_fp8;
                                projreal += vl_fr8;
                            }
                            else if ((mes >= 10) && (mes <= 12))
                            {
                                projprev += vl_fp12;
                                projreal += vl_fr12;
                            }
                        }
                        else
                        {
                            projprev += 0;
                            projreal += 0;
                        } 
                        vl_acp1 = 0;vl_acp4 = 0;vl_acp8 = 0;vl_acp12 = 0;
                        vl_acr1 = 0;vl_acr4 = 0;vl_acr8 = 0;vl_acr12 = 0;
                        previsto = 0;
                        realizado = 0;
                        previsto_total = 0;
                        //Response.Write(String.Format("Previsto 1º={0}, 2º={1}, 3º={2}, 4º={3} <br>", vl_p1, vl_p4, vl_p8, vl_p12));
                        //Response.Write(String.Format("Realizado 1º={0}, 2º={1}, 3º={2}, 4º={3} <br><br>", vl_r1, vl_r4, vl_r8, vl_r12));

                    } //FIM DR2 


                    sb.Append("<tr>");
                    sb.Append("<td>" + dr["nm_projeto"] + "</td>");
                    if (cont != 0)
                    {
                        sb.Append("<td style=text-align:right>" + projprev.ToString("N2") + "</td>");
                        sb.Append("<td style=text-align:right>" + projreal.ToString("N2") + "</td>");
                        if (projprev != 0)
                        {
                            sb.Append("<td style=text-align:right>" +(projreal / projprev).ToString("N2") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style=text-align:right>0,00</td>");
                        }

                        acprev += projprev;
                        acreal += projreal;

                        vl_p1 += vl_fp1 / cont;
                        vl_p4 += vl_fp4 / cont;
                        vl_p8 += vl_fp8 / cont;
                        vl_p12 += vl_fp12 / cont;

                        vl_r1 += vl_fr1 / cont;
                        vl_r4 += vl_fr4 / cont;
                        vl_r8 += vl_fr8 / cont;
                        vl_r12 += vl_fr12 / cont;

                    }

                    else
                    {
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                        sb.Append("<td style=text-align:right>0,00</td>");
                    }
                    sb.Append("</tr>");
                    projprev = 0;
                    projreal = 0;
                    cont = 0;
                    vl_fp1 = 0; vl_fp4 = 0; vl_fp8 = 0; vl_fp12 = 0;
                    vl_fr1 = 0; vl_fr4 = 0; vl_fr8 = 0; vl_fr12 = 0;
                }//FIM DR (PROJETOS) 

                

                //Response.Write(String.Format("Previsto 1º={0}, 2º={1}, 3º={2}, 4º={3} <br>", vl_p1, vl_p4, vl_p8, vl_p12));
                //Response.Write(String.Format("Realizado 1º={0}, 2º={1}, 3º={2}, 4º={3} <br><br>", vl_r1, vl_r4, vl_r8, vl_r12));
                
                if (contac != 0)
                {


                    vl_prev = (acprev / contproj);
                    vl_real = (acreal / contproj);

                    sb.Append("<tr  class=\"headerGrid\">");
                    sb.Append("<td>Média</td>");
                    sb.Append("<td style=text-align:right>" + vl_prev.ToString("N2") + "</td>");
                    sb.Append("<td style=text-align:right>" + vl_real.ToString("N2") + "</td>");
                    if (vl_prev > 0)
                    {
                        sb.Append("<td style=text-align:right>" + (vl_real / vl_prev).ToString("N2") + "</td>");
                    }
                    else
                    {
                        sb.Append("<td style=text-align:right>0,00</td>");
                    }
                    sb.Append("</tr>");
                }


                sb.Append("</table>");
                Session["MonFinanceiroRel"] = sb.ToString();

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
                html += "width=\"" + (largura+10) + "\" height=\"" + altura + "\" align=\"center\">";
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

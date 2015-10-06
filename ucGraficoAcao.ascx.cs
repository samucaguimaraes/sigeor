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

public partial class ucGraficoAcao : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StringBuilder sb = new StringBuilder();
            string[] ar = {"Jan","Fev","Mar","Abr","Mai","Jun","Jul","Ago","Set","Out","Nov","Dez"};

            t03_projeto t03 = new t03_projeto();
            {

                t03.t03_cd_projeto = pb.cd_projeto();
                t03.Retrieve();
                if (t03.Found)
                {
                    string strclass = "";
                    int anoPinicio = t03.dt_inicio.Year;
                    int anoPfim = t03.dt_fim.Year;
                    sb.Append("<table class='grafAcao' cellpadding=0 cellspacing=0><tr>");
                    int cont = 0;
                    for (int i = anoPinicio; i <= anoPfim; i++)
                    {
                        if (i % 2 == 0) { strclass = "class='anos'"; } else { strclass = "class='anos2'"; }
                        sb.Append("<td colspan=12 " + strclass + ">" + i.ToString() + "</td>");
                        cont++;
                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    int j = 0;
                    int ctemp=cont;
                    for (int i = 1; i <= (cont * 12); i++)
                    {
                        if (ctemp % 2 == 0) { strclass = "class='meses'"; } else { strclass = "class='meses2'"; }
                        sb.Append("<td " + strclass + ">" + ar[j] + "</td>");
                        j++;
                        if (j == 12)
                        {
                            j = 0; 
                            ctemp--;
                        }

                    }
                    sb.Append("</tr>");

                    t08_acao t08 = new t08_acao();
                    {
                        t08.order = "order by dt_inicio";
                        t08.t03_cd_projeto = pb.cd_projeto();
                        
                        int h = 50 + (t08.List().Tables[0].Rows.Count * 50);
                        Panel1.Height = h;

                        foreach (DataRow dr in t08.List().Tables[0].Rows)
                        {
                            
                            DateTime acaoInicio = DateTime.Parse(dr["dt_inicio"].ToString());
                            DateTime acaoFim = DateTime.Parse(dr["dt_fim"].ToString());

                            sb.Append("<tr>");
                            sb.Append("<td colspan='" + (cont * 12) + "' class='nmAcao'><b>Ação:</b> " + dr["nm_acao"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            j = 1;
                            int diff = monthDifference(acaoInicio, acaoFim);
                            int anoP = anoPinicio;
                            
                            bool inicia = false;
                            for (int i = 1; i <= (cont * 12); i++)
                            {
                                if (j == 13)
                                {
                                    j = 1;
                                    anoP++;
                                }
                                string str = "";
                                //Response.Write("if (" + anoP.ToString() + ">= " + acaoInicio.Year.ToString() + ")<br>");
                                if (anoP >= acaoInicio.Year)
                                {
                                    if ((acaoInicio.Month == j))
                                    {
                                        inicia = true;
                                    }

                                    
                                   

                                    if (inicia && diff > 0)
                                    {
                                        int a = (j - 1);
                                        str = "class='barraAcao' title='" + ar[a] + "/" + anoP + "'";
                                        diff--;
                                    }
                                }

                                j++;
                                sb.Append("<td " + str + ">&nbsp;</td>");
                                
                                
                                
                            }
                            sb.Append("</tr>");
                        }
                    }
                }
                sb.Append("</table>");
            }
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));

        }
        
    }
    private static int monthDifference(DateTime startDate, DateTime endDate)
    {
        int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
        return Math.Abs(monthsApart);
    }
}

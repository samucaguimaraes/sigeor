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

public partial class MonProjetos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        t03_projeto t03 = new t03_projeto();
        {
            pageBase pb = new pageBase();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
            sb.Append("<tr class=\"headerGrid\">");
            sb.Append("<td style='width:1%'></td>");
            sb.Append("<td>Projeto</td>");
            sb.Append("<td style=\"text-align:right\">Início</td>");
            sb.Append("<td style=\"text-align:right\">Término</td>");
            sb.Append("</tr>");
            t03.order = "select * from t03_projeto where (fl_ativa=1)  " + pb.sqlfiltro() + " order by nm_projeto";
            foreach (DataRow drp in t03.ListQuery().Tables[0].Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td><a href='redirectArvore.aspx?cd_projeto=" + drp["t03_cd_projeto"] + "'><img title='Ir para arvore do projeto' src='images/lupa.gif' /></a></td>");
                sb.Append("<td>"+drp["nm_projeto"]+"</td>");
                if (drp["dt_inicio"] != DBNull.Value)
                {
                    sb.Append("<td style=\"text-align:right\">" + DateTime.Parse(drp["dt_inicio"].ToString()).ToShortDateString() + "</td>");
                    sb.Append("<td style=\"text-align:right\">" + DateTime.Parse(drp["dt_fim"].ToString()).ToShortDateString() + "</td>");
                }
                else
                {
                    sb.Append("<td style=\"text-align:right\">-</td>");
                    sb.Append("<td style=\"text-align:right\">-</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
        }
        
    }
}

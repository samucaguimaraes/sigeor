using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class MonRestricoes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["fl_status"] != null)
        {
            switch (Request["fl_status"].ToString())
            {
                case "B":
                    lblHeader.Text = "Restrições concluídas";
                    break;
                case "G":
                    lblHeader.Text = "Restrições dentro dos prazos previstos";
                    break;
                case "R":
                    lblHeader.Text = "Restrições com atraso";
                    break;
                   
            }
            GridBind(PanelRel, Request["fl_status"].ToString());
        }
        else
        {
            lblHeader.Text = "Restrições";
            GrafBind(PanelGraf);
        }

    }
    protected void GrafBind(Panel pn)
    {
        PanelGraf.Visible = true;
        PanelOpcao.Visible = true;
        PanelRel.Visible = false;
        linkVoltar.Visible = false;
        t03_projeto t03 = new t03_projeto();
        {
            //R
            t03.order = "select * from t07_restricao t07 where fl_ativa=1 and dt_limite < getdate() and dt_superada is null " +
            "and t03_cd_projeto in (select t03_cd_projeto from t03_projeto " +
            "where (fl_ativa=1)  " + pb.sqlfiltro() + ")";
            int r = t03.ListQuery().Tables[0].Rows.Count;
            //G
            t03.order = "select * from t07_restricao t07 where fl_ativa=1 and dt_limite > getdate() " +
            "and t03_cd_projeto in (select t03_cd_projeto from t03_projeto " +
            "where (fl_ativa=1)  " + pb.sqlfiltro() + ")";
            int g = t03.ListQuery().Tables[0].Rows.Count;
            //B
            t03.order = "select * from t07_restricao t07 where fl_ativa=1 and dt_superada is not null " +
            "and t03_cd_projeto in (select t03_cd_projeto from t03_projeto " +
            "where (fl_ativa=1)  " + pb.sqlfiltro() + ")";
            int b = t03.ListQuery().Tables[0].Rows.Count;
            int total = r + g + b;

            lblAzul.Text = b.ToString();
            lblVerde.Text = g.ToString();
            lblVermelha.Text = r.ToString();

            if (b == 0) linkConcluidos.NavigateUrl = "";
            if (g == 0) linkPrazos.NavigateUrl = "";
            if (r == 0) linkAtraso.NavigateUrl = "";

            if (total > 0)
            {
                lblFatiaAzul.Text = ((b * 100) / total).ToString();
                lblFatiaVerde.Text = ((g * 100) / total).ToString();
                lblFatiaVermelha.Text = ((r * 100) / total).ToString();
            }
            else
            {
                lblFatiaAzul.Text = "0";
                lblFatiaVerde.Text = "0";
                lblFatiaVermelha.Text = "0";
            }
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=100% height=20 border=0 cellpadding=0 cellspacing=0><tr>");
            if (b != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/B.gif');width:" + (b * 100) / total + "%\" title='" + (b * 100) / total + "%'>&nbsp;</td>");
            }
            if (g != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/G.gif');width:" + (g * 100) / total + "%\" title='" + (g * 100) / total + "%'>&nbsp;</td>");
            }
            if (r != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/R.gif');width:" + (r * 100) / total + "%\" title='" + (r * 100) / total + "%'>&nbsp;</td>");
            }
            sb.Append("</tr></table>");
            pn.Controls.Add(pb.GetLiteral(sb.ToString()));
        }
    }

    protected void GridBind(Panel pn, string fl_status)
    {
        PanelGraf.Visible = false;
        PanelOpcao.Visible = false;
        PanelRel.Visible = true;
        linkVoltar.Visible = true;
        try
        {
            t03_projeto t03 = new t03_projeto();
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
                sb.Append("<tr style=\"background:#F0EDEB;font-weight:bold\">");
                sb.Append("<td colspan=\"3\">Projetos</td>");
                sb.Append("</tr>");
                string restricao = "";
                if (fl_status == "R")
                {
                    restricao = "and dt_limite < getdate() and dt_superada is null";
                }
                else if (fl_status == "G")
                {
                    restricao = "and dt_limite > getdate()";
                }
                else if (fl_status == "B")
                {
                    restricao = "and dt_superada is not null";
                }

                t03.order = "select * from t03_projeto t03 where (fl_ativa=1)  " +
                            "and t03_cd_projeto in (select t03_cd_projeto from t07_restricao where fl_ativa=1 " +
                            restricao + ")" + pb.sqlfiltro() + " order by nm_projeto";
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    sb.Append("<tr style=\"background:#FAF9F8;font-weight:bold\">");
                    sb.Append("<td colspan=\"3\">" + dr["nm_projeto"] + "</td>");
                    sb.Append("</tr>");
                    t03.order = "select * from t07_restricao t07 where fl_ativa=1 and t03_cd_projeto=" + dr["t03_cd_projeto"] +
                                restricao + " order by dt_limite desc";
                    int index = 0;
                    foreach (DataRow dre in t03.ListQuery().Tables[0].Rows)
                    {

                        if (index == 0)
                        {
                            sb.Append("<tr class=\"headerGrid\">");
                            sb.Append("<td>Restrição</td>");
                            sb.Append("<td>Medida de gestão</td>");
                            sb.Append("<td>Data limite</td>");
                            sb.Append("</tr>");
                            index = 1;
                        }
                        sb.Append("<tr>");
                        sb.Append("<td>" + dre["ds_restricao"] + "</td>");
                        sb.Append("<td>" + dre["ds_medida"] + "</td>");
                        sb.Append("<td>" + ((DateTime)dre["dt_limite"]).ToShortDateString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
                pn.Controls.Add(pb.GetLiteral(sb.ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

    
}

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

public partial class MonImpressao : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["fl_status"] != null)
        {
            switch (Request["fl_status"].ToString())
            {
                case "B":
                    lblHeader.Text = "Ações concluídas";
                    break;
                case "G":
                    lblHeader.Text = "Ações dentro dos prazos previstos";
                    break;
                case "Y":
                    lblHeader.Text = "Ações com restrições";
                    break;
                case "A":
                    lblHeader.Text = "Ações próximas do vencimento";
                    break;					
                case "R":
                    lblHeader.Text = "Ações com atraso";
                    break;
				case "T":
                    lblHeader.Text = "Todas as Ações";
                    break;	
            }
            GridBind(Request["fl_status"].ToString());
        }
        else
        {
            Response.Redirect("MonImpressao.aspx"); 
        } 
    }
    protected void GridBind(string fl_status)
    {
        try
        {
            t03_projeto t03 = new t03_projeto();
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string restricao = "";
				string restricao2 = "";
				
				restricao2 = "t08.t08_cd_acao=t09.t08_cd_acao and";
                if (fl_status == "Y")
                {
                    //restricao = " and t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status<>'R') or t08_cd_acao in (select t08_cd_acao from t29_acaorestricao)";
                    restricao = " and (t08_cd_acao in (select t08_cd_acao from t29_acaorestricao where t07_cd_restricao in (select t07_cd_restricao from t07_restricao where t03_cd_projeto=t08.t03_cd_projeto) and t08_cd_acao not in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='R'))) ";
                }
				else if (fl_status == "A")
                {
						DateTime dtatual = DateTime.Now.Date;

					restricao = " and t08.t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='G') and t08.t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao) "+
                        "and (select count(*) from t09_marco where fl_status='R' and t08_cd_acao=t08.t08_cd_acao) = 0";
                }
                else if (fl_status == "R")
                {
                   restricao = " and t08.t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='R')";
                }
                else if (fl_status == "G")
                {
                    restricao = " and t08.t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='G') and t08.t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao) "+
                        "and (select count(*) from t09_marco where fl_status='R' and t08_cd_acao=t08.t08_cd_acao) = 0";
                }
                else if (fl_status == "B")
                {
                    restricao = " and t08.t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='B' ) and t08.t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao) "+
                        "and (select count(*) from t09_marco where fl_status='R' and t08_cd_acao=t08.t08_cd_acao) = 0 " +
                        "and (select count(*) from t09_marco where fl_status='G' and t08_cd_acao=t08.t08_cd_acao) = 0 "; 
                }
				else if (fl_status =="T")   //Exibe todos os projetos com todas as ações
                {
                    restricao = " and t08.t08_cd_acao in (select t08_cd_acao from t09_marco where fl_ativa=1)"; 
					
                }
                sb.Append("<center><tr>");
                sb.Append("<td><img src=\"images/logoAgenda.jpg\" /></td> ");
                sb.Append("</tr><br><br></center>");
                sb.Append("<table cellspacing=\"0\" cellpadding=\"11\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
                sb.Append("<tr style=\"background:#F0EDEB;font-weight:bold\">");
                sb.Append("<td colspan=\"11\">Projetos</td>");
                sb.Append("</tr>");
                t03.order = "select * from t03_projeto t03 where (fl_ativa=1)  " +
                            "and t03_cd_projeto in (select t03_cd_projeto from t08_acao t08 where fl_ativa=1 " +
                            restricao + ")" + pb.sqlfiltro() + " order by nm_projeto";

                //Response.Write("Projeto:" + t03.order + "<br><br>");
                foreach (DataRow dr in t03.ListQuery().Tables[0].Rows)
                {
                    sb.Append("<tr style=\"background:#A8C7E3;font-weight:bold\">");
                    sb.Append("<td colspan=\"11\">" + dr["nm_projeto"] + "</td>");
                    sb.Append("</tr>");

                    string responsavel = "";
                    responsavel = Request.Form["pesquisar"]; 


																//ACRESCIMO A SER FEITO DEPOIS DO WHERE QUANDO O REPONSAVEL ESTIVER PRONTO: t08.ds_parceiro LIKE '%teste%' and 
                    t03.order = "select * from t08_acao t08, t09_marco t09 where " + restricao2 + " t08.fl_ativa=1 and ds_parceiro LIKE '%" + responsavel + "%' and t03_cd_projeto=" + dr["t03_cd_projeto"] +
                                restricao +" order by nm_acao";
								
                    //Response.Write("Ação:" + t03.order + "<br><br>");
                    int index = 0;
					int n = 0;
					string imagem = "";
					
				if (Request["fl_status"]=="A"){
						
					foreach (DataRow dra in t03.ListQuery().Tables[0].Rows)
                    {
					
					DateTime dtatual = DateTime.Now.Date;
					string dt = dra["dt_aviso"].ToString();
					DateTime dtaviso = Convert.ToDateTime(dt);
						if (dtatual >= dtaviso)
						{
							if (imagem != "B")
							{
					
					
					
								if (index == 0)
									{		
							
									sb.Append("<tr class=\"headerGrid\">");
									sb.Append("<td width='1%'>Nº</td>");
									sb.Append("<td width='25%'>Ação</td>");
									sb.Append("<td width='10%'>Área</td>");
									sb.Append("<td width='10%'>Responsável</td>");
									sb.Append("<td width='1%'>Situação</td>");					
									sb.Append("<td width='9%'>Andamento</td>");
									sb.Append("<td width='9%'>Público Alvo</td>");
									sb.Append("<td width='10%'>Local de Atuação</td>");							
									sb.Append("<td width='1%'>Início</td>");
									sb.Append("<td width='1%'>Término</td>");
									sb.Append("<td width='1%'>Atualizado</td>");
									sb.Append("</tr>");
									index = 1;
									}
									sb.Append("<tr>");
									imagem = dra["fl_status"].ToString();
									n = n+1;
									sb.Append("<td>" + n + "</td>");
									sb.Append("<td>" + dra["ds_acao"] + "</td>");
									sb.Append("<td>" + dra["nm_acao"] + "</td>");
									sb.Append("<td>" + dra["ds_parceiro"] + "</td>");
									
									sb.Append("<td><img src=\"images/Y.gif\" /></td>");
									
									//sb.Append("<td>" + dra["fl_status"] + "</td>");
									sb.Append("<td>" + dra["ds_andamento"] + "</td>");
									sb.Append("<td>" + dra["ds_palvo"] + "</td>");
									sb.Append("<td>" + dra["ds_latuacao"] + "</td>");						
									sb.Append("<td>" + ((DateTime)dra["dt_inicio"]).ToShortDateString() + "</td>");
									sb.Append("<td>" + ((DateTime)dra["dt_fim"]).ToShortDateString() + "</td>");
									sb.Append("<td>" + ((DateTime)dra["dt_alterado"]).ToShortDateString() + "</td>");
						
									sb.Append("</tr>");
							} 
	
						}
						
						
						
						
                    }
						
				}	else {
					
					
                    foreach (DataRow dra in t03.ListQuery().Tables[0].Rows)
                    {
					

					
					
					
                        if (index == 0)
                        {		
							
                            sb.Append("<tr class=\"headerGrid\">");
							sb.Append("<td width='1%'>Nº</td>");
							sb.Append("<td width='25%'>Ação</td>");
                            sb.Append("<td width='10%'>Área</td>");
							sb.Append("<td width='10%'>Responsável</td>");
							sb.Append("<td width='1%'>Situação</td>");					
							sb.Append("<td width='9%'>Andamento</td>");
							sb.Append("<td width='9%'>Público Alvo</td>");
							sb.Append("<td width='10%'>Local de Atuação</td>");							
                            sb.Append("<td width='1%'>Início</td>");
                            sb.Append("<td width='1%'>Término</td>");
                            sb.Append("<td width='1%'>Atualizado</td>");
                            sb.Append("</tr>");
                            index = 1;
                        }
                        sb.Append("<tr>");
						imagem = dra["fl_status"].ToString();
						n = n+1;
						sb.Append("<td>" + n + "</td>");
						sb.Append("<td>" + dra["ds_acao"] + "</td>");
                        sb.Append("<td>" + dra["nm_acao"] + "</td>");
						sb.Append("<td>" + dra["ds_parceiro"] + "</td>");

						DateTime dtatual = DateTime.Now.Date;
						string dt = dra["dt_aviso"].ToString();
						DateTime dtaviso = Convert.ToDateTime(dt);
						
						
						

						
						if (imagem == "R")
						{
							sb.Append("<td><img src=\"images/R.gif\" /></td>");
						}
						else if (dtatual >= dtaviso)
						{
							if (imagem != "B")
							{
								sb.Append("<td><img src=\"images/Y.gif\" /></td>");
							} else
							if (imagem == "G")
							{
								sb.Append("<td><img src=\"images/G.gif\" /></td>");
							}						
							else if (imagem == "B")
							{
								sb.Append("<td><img src=\"images/B.gif\" /></td>");
							}
	
						}						
						else if (imagem == "G")
						{
							sb.Append("<td><img src=\"images/G.gif\" /></td>");
						}						
						else if (imagem == "B")
						{
							sb.Append("<td><img src=\"images/B.gif\" /></td>");
						}
						

						


						//sb.Append("<td>" + dra["fl_status"] + "</td>");
                        sb.Append("<td>" + dra["ds_andamento"] + "</td>");
						sb.Append("<td>" + dra["ds_palvo"] + "</td>");
                        sb.Append("<td>" + dra["ds_latuacao"] + "</td>");						
                        sb.Append("<td>" + ((DateTime)dra["dt_inicio"]).ToShortDateString() + "</td>");
                        sb.Append("<td>" + ((DateTime)dra["dt_fim"]).ToShortDateString() + "</td>");
                        sb.Append("<td>" + ((DateTime)dra["dt_alterado"]).ToShortDateString() + "</td>");
						
                        sb.Append("</tr>");

                    }
					
				  }

                }
                sb.Append("</table>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    
}

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

public partial class Default2 : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["vr"] = "";
        //Response.Write(Session["vr"]);
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        pageBind();
        if (!IsPostBack)
        {
        if (pb.fl_admin())
        {
            PanelFiltro.Visible = true;
            DropDownList ddl = this.ddlt01_cd_entidade;
            t01_entidade t01 = new t01_entidade();
            {
                t01.fl_ativa = true;
                t01.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where fl_ativa = 1)";
                ddl.DataSource = t01.List();
                ddl.DataTextField = "nm_entidade";
                ddl.DataValueField = "t01_cd_entidade";
                ddl.DataBind();
                pb.AddEmptyItem(ddl, "Todos");
            }
        }
        }
    }

    protected void pageBind()
    {

        Panel1.Controls.Clear();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        {
            if (pb.fl_admin())
            {
                if (this.ddlt01_cd_entidade.SelectedValue != "")
                {
                    t01_entidade t01 = new t01_entidade();
                    {
                        t01.t01_cd_entidade = Int32.Parse(ddlt01_cd_entidade.SelectedValue.ToString());
                        t01.Retrieve();
                        if (t01.Found)
                        {
                            sb.Append("<p style='padding-left:100px;'><b>Parceiro: " + t01.nm_entidade + "</b></p>");
                        }
                    }
                }
                else
                {
                    sb.Append("<p style='padding-left:100px;'><b>Parceiro: Todos</b></p>");
                }
            }
            sb.Append("<p style='padding-left:100px;'><b>Filtro de Eixos</b></p>");
            sb.Append("<table cellspacing=\"2\" cellpadding=\"4\" class=\"tbfases\">");
            sb.Append("<tr class=\"tipo\">");
            sb.Append("<td width=\"20%\" class='hrtipo_projeto' rowspan=\"3\">Tipo de Eixos</td>");
            sb.Append("<td width=\"10%\" class='hrtotal' rowspan=\"3\">Total</td>");
            sb.Append("<td colspan=\"4\" class=\"hrfases\" style=\"background-color:#FDF9E1\">Fases</td>");
            sb.Append("</tr>");
            sb.Append("<tr class=\"tipo\">");
            sb.Append("<td style=\"width:18%\" class=\"hrfases\">");
            sb.Append("Estrutura&ccedil;&atilde;o</td>");
            sb.Append("<td colspan=\"2\" style=\"width:34%\" class=\"hrfases\">Gest&atilde;o</td>");
            sb.Append("<td style=\"width:18%\" class=\"hrfases\">Encerrado</td>");
            sb.Append("</tr>");
            sb.Append("<tr style=\"text-align:center;\">");
            sb.Append("<td class=\"ico_fase\"><div class=\"estruturacao\">&nbsp;</div><span>&nbsp;</span></td>");
            sb.Append("<td class=\"ico_fase\"><div class=\"execucao\">&nbsp;</div><span>Em Execu&ccedil;&atilde;o</span></td>");
            sb.Append("<td class=\"ico_fase\"><div class=\"revisao\">&nbsp;</div><span>Em Revis&atilde;o</span></td>");
            sb.Append("<td class=\"ico_fase\"><div class=\"encerrado\">&nbsp;</div><span>&nbsp;</span></td>");
            sb.Append("</tr>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));

            int num = 0; int numT = 0; int numES = 0; int numEX = 0; int numRE = 0; int numCOEN = 0;
            t04_tipologia t04 = new t04_tipologia();
            {
                t04.fl_ativa = true;
                t04.order = "order by nm_tipologia";
                foreach (DataRow dr in t04.List().Tables[0].Rows)
                {
                    Panel1.Controls.Add(pb.GetLiteral("<tr>"));
                    Panel1.Controls.Add(pb.GetLiteral("<td style=\"text-align:left\">"));
                    LinkButton linkTipologia = new LinkButton();
                    linkTipologia.ID = "link" + dr["t04_cd_tipologia"].ToString();
                    linkTipologia.CommandArgument = dr["t04_cd_tipologia"].ToString();
                    linkTipologia.Text = dr["nm_tipologia"].ToString();
                    linkTipologia.Click += new EventHandler(linkTipologia_Click);

                    t03_projeto t03 = new t03_projeto();
                    {
                        string query = "";
                        string subquery = "";

                        if (pb.fl_admin() || pb.fl_visitante() || pb.fl_estrategico())
                        {
                            if (this.ddlt01_cd_entidade.SelectedValue == "")
                            {
                                query = " where t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + " and fl_ativa=1";
                                Session["vr"] = null;
                            }
                            else
                            {
                                query = " where t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + " and fl_ativa=1" +
                                " and t01_cd_entidade = " + this.ddlt01_cd_entidade.SelectedValue + " ";
                                //"(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + this.ddlt05_cd_parceiro.SelectedValue + " )";

                                subquery = " and fl_ativa=1 and t01_cd_entidade = " + this.ddlt01_cd_entidade.SelectedValue + "";
                                //"(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + this.ddlt05_cd_parceiro.SelectedValue + ")";
                                Session["vr"] = this.ddlt01_cd_entidade.SelectedValue;
                            }
                        }
                        else //se usuário sem perfil e diferente de visitante
                        {
                            if (pb.cd_parceiro() != 0) //parceiro
                            {
                                query = " where t03_cd_projeto in " +
                                    "(select t03_cd_projeto from t03_projeto where t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + " and fl_ativa=1 and t01_cd_entidade in " +
                                    "(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")) ";

                                subquery = " and fl_ativa=1 and t01_cd_entidade in " +
                                    "(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")";
                            }
                            else //administrador parceiro
                            {
                                query = " where t03_cd_projeto in (select t03_cd_projeto from t03_projeto  where t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + " and  fl_ativa=1 and t01_cd_entidade=" + pb.cd_entidade() + ")";

                                subquery = " and fl_ativa=1 and t01_cd_entidade=" + pb.cd_entidade() + " ";
                            }

                        }
                        t03.order = query;
                        num = t03.List().Tables[0].Rows.Count;
                        numT += num; //acumulador
                        if (num == 0) linkTipologia.Enabled = false;
                        Panel1.Controls.Add(linkTipologia);
                        Panel1.Controls.Add(pb.GetLiteral("</td>"));

                        Panel1.Controls.Add(pb.GetLiteral("<td>" + num.ToString() + "</td>"));

                        t20_faseprojeto t20 = new t20_faseprojeto();
                        //Estruturaçao
                        t20.order = "where fl_ativa=1 and t19_cd_fase in (select t19_cd_fase from t19_fase where fl_fase='ES') and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa = 1 and t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + subquery + ")";
                        num = t20.List().Tables[0].Rows.Count;
                        numES += num; //acumulador
                        Panel1.Controls.Add(pb.GetLiteral("<td>" + num.ToString() + "</td>"));

                        //Execução
                        t20.order = "where fl_ativa=1 and t19_cd_fase in (select t19_cd_fase from t19_fase where fl_fase='EX') and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa = 1 and t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + subquery + ")";
                        num = t20.List().Tables[0].Rows.Count;
                        numEX += num; //acumulador
                        Panel1.Controls.Add(pb.GetLiteral("<td>" + num.ToString() + "</td>"));

                        //Revisão
                        t20.order = "where fl_ativa=1 and t19_cd_fase in (select t19_cd_fase from t19_fase where fl_fase='RE') and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa = 1 and t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + subquery + ")";
                        num = t20.List().Tables[0].Rows.Count;
                        numRE += num; //acumulador
                        Panel1.Controls.Add(pb.GetLiteral("<td>" + num.ToString() + "</td>"));

                        //Concluído e Encerrado
                        t20.order = "where fl_ativa=1 and t19_cd_fase in (select t19_cd_fase from t19_fase where fl_fase in('CO', 'EN')) and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa = 1 and t04_cd_tipologia=" + dr["t04_cd_tipologia"].ToString() + subquery + ")";
                        num = t20.List().Tables[0].Rows.Count;
                        numCOEN += num; //acumulador
                        Panel1.Controls.Add(pb.GetLiteral("<td>" + num.ToString() + "</td>"));
                    }
                    Panel1.Controls.Add(pb.GetLiteral("</tr>"));

                }
            }

            sb = new System.Text.StringBuilder();
            sb.Append("<tr bgcolor=\"#ececec\" >");
            sb.Append("<td style=\"text-align:left;\">");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            LinkButton link = new LinkButton();
            link.ID = "linkTodos";
            link.CommandArgument = "0";
            link.Text = "Todos";
            link.Click += new EventHandler(linkTipologia_Click);
            Panel1.Controls.Add(link);
            sb = new System.Text.StringBuilder();
            sb.Append("</td>");
            sb.Append("<td>" + numT.ToString() + "</td>");
            sb.Append("<td>" + numES.ToString() + "</td>");
            sb.Append("<td>" + numEX.ToString() + "</td>");
            sb.Append("<td>" + numRE.ToString() + "</td>");
            sb.Append("<td>" + numCOEN.ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
        }

        
    }
    protected void linkTipologia_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Session["cd_tipologia"] = btn.CommandArgument;
        Response.Redirect("~/Projetos.aspx");
    }

    protected void btnFiltro_Click(object sender, System.EventArgs e)
    {
        
        if (this.ddlt01_cd_entidade.SelectedValue != "")
        {
            Session["vr"] = this.ddlt01_cd_entidade.SelectedValue;
        }
        else
        {
            Session["vr"] = "";
        }
        //Response.Write(this.ddlt01_cd_entidade.SelectedValue + "-" + Session["vr"].ToString());
    }


}

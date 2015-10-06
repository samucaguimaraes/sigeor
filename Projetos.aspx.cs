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

public partial class Projetos : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cd_projeto"] = 0;
        GridBind("order by nm_projeto");
        ucNav.projetos = true;
    }
    protected void link_Click(object sender, System.EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Session["cd_projeto"] = link.CommandArgument;
        Response.Redirect("Arvore.aspx");
    }
    private void GridBind(string order)
    {
        string tipologia="";
        if (pb.cd_tipologia().ToString() != "0")
        {
            tipologia = " t04_cd_tipologia = " + pb.cd_tipologia().ToString() +" and ";
        }
        
        string colspan = "5";
        PanelGrid.Controls.Clear();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table  cellspacing=\"2\" cellpadding=\"4\" class=\"tblist\">");

        t03_projeto t03 = new t03_projeto();
        {
            //t03.t01_cd_entidade = cd_entidade; //somente a entidade do parceiro
            if (pb.fl_admin() || pb.fl_visitante() || pb.fl_estrategico())
            {
                
                //entidade do usuário
                //t03.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1 and t01_cd_entidade="+pb.cd_entidade()+" ) order by nm_entidade";
                if (pb.fl_visitante())
                {
                    t03.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t20_faseprojeto where t19_cd_fase = 2 or t19_cd_fase = 1)) order by nm_entidade";                  
			   }
                else
                {
                    t03.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1) order by nm_entidade";
                }

                if (Session["vr"] != null)
                {
                    t03.order = " and t01_cd_entidade = " + Session["vr"].ToString() + " "+
                            //"(select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1 and t01_cd_entidade = " + Session["vr"].ToString() + " " +
                            "order by nm_entidade";
                }
                    
                    foreach (DataRow dre in t03.ListEntidade().Tables[0].Rows)
                {
                    sb.Append("<tr class=\"hr_white\">");
                    sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                    sb.Append("</tr>");

                    t03.order = tipologia;
                    t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                    foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                    {
                        sb.Append("<tr  class=\"hr_yellow\">");
                        sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr class=\"hr_orange\">");
                        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                        {
                            sb.Append("<td>Eixos</td>");
                        }
                        else { sb.Append("<td colspan=2>Eixos</td>"); }

         //LEVI               sb.Append("<td>Fase</td>");
         //LEVI 11-02-2015                  sb.Append("<td>Atualizado</td>");
                        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"])) { sb.Append("<td>Restri&ccedil;&atilde;o</td>"); }
                        sb.Append("<td style='width:200px'>Evolu&ccedil;&atilde;o</td>");
                        sb.Append("</tr>");

                        if (pb.fl_visitante())
                        {
                            t03.order = "where fl_ativa = 1" +
                                " and t03_cd_projeto in " +
                                " (select t03_cd_projeto from t20_faseprojeto where t19_cd_fase = 2 or t19_cd_fase = 1)" +
                                " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                                " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString();
                        }
                        else
                        {
                            t03.order = "where fl_ativa=1" +
                                " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                                " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                                " order by nm_projeto";

                        }
                        foreach (DataRow drp in t03.List().Tables[0].Rows)
                        {
                            string nm_projeto = drp["nm_projeto"].ToString();
                            string nm_fase = "";
                            string data = "-";

                            if (drp["dt_alterado"] != DBNull.Value)
                            {
                                data = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(drp["dt_alterado"].ToString()));
                            }

                            t19_fase t19 = new t19_fase();
                            {
                                t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                                t19.RetrieveFaseProjeto();
                                if (t19.Found)
                                {
                                    nm_fase = t19.nm_fase;
                                }
                            }

                            sb.Append("<tr>");
                            PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                            {
                                PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                            }
                            else { PanelGrid.Controls.Add(pb.GetLiteral("<td colspan=2>")); }
                            LinkButton link = new LinkButton();
                            {
                                link.ID = "linkP" + drp["t03_cd_projeto"].ToString();
                                link.CommandArgument = drp["t03_cd_projeto"].ToString();
								link.Text = nm_projeto;
								link.Click += new EventHandler(link_Click);
                            }
                            PanelGrid.Controls.Add(link);
                            PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Projeto

                            sb = new StringBuilder();
                 //LEVI           sb.Append("<td>" + nm_fase + "</td>"); //Fase
                 //LEVI 11-02-2015        sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado
                            
                            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                            {
                                t07_restricao t07 = new t07_restricao();
                                {
                                    t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                                    if (t07.List().Tables[0].Rows.Count == 0)
                                    {
                                        sb.Append("<td>&nbsp;</td>"); //Restrição
                                    }
                                    else
                                    {
                                        sb.Append("<td style='text-align:center'><b>R</b></td>"); //Restrição
                                    }
                                }
                            }

                            sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>"); //Evolução
                            sb.Append("</tr>");
                        }
                    }
                }
                //restante das entidades
                //t03.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1  and t01_cd_entidade<>" + pb.cd_entidade() + ") order by nm_entidade";
                //foreach (DataRow dre in t03.ListEntidade().Tables[0].Rows)
                //{
                //    sb.Append("<tr class=\"hr_white\">");
                //    sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                //    sb.Append("</tr>");

                //    t03.order = tipologia;
                //    t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                //    foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                //    {
                //        sb.Append("<tr  class=\"hr_yellow\">");
                //        sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                //        sb.Append("</tr>");

                //        sb.Append("<tr class=\"hr_orange\">");
                //        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //        {
                //            sb.Append("<td>Projeto</td>");
                //        }
                //        else { sb.Append("<td colspan=2>Projeto</td>"); }

                //        sb.Append("<td>Fase</td>");
                //        sb.Append("<td>Atualizado</td>");
                //        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"])) { sb.Append("<td>Restri&ccedil;&atilde;o</td>"); }
                //        sb.Append("<td style='width:120px'>Evolu&ccedil;&atilde;o</td>");
                //        sb.Append("</tr>");

                //        t03.order = "where fl_ativa=1" +
                //            " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                //            " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                //            " order by nm_projeto";

                //        foreach (DataRow drp in t03.List().Tables[0].Rows)
                //        {
                //            string nm_projeto = drp["nm_projeto"].ToString();
                //            string nm_fase = "";
                //            string data = "-";

                //            if (drp["dt_alterado"] != DBNull.Value)
                //            {
                //                data = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(drp["dt_alterado"].ToString()));
                //            }

                //            t19_fase t19 = new t19_fase();
                //            {
                //                t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                //                t19.RetrieveFaseProjeto();
                //                if (t19.Found)
                //                {
                //                    nm_fase = t19.nm_fase;
                //                }
                //            }

                //            sb.Append("<tr>");
                //            PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                //            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //            {
                //                PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                //            }
                //            else { PanelGrid.Controls.Add(pb.GetLiteral("<td colspan=2>")); }

                            
                //            LinkButton link = new LinkButton();
                //            {
                //                link.ID = "linkP" + drp["t03_cd_projeto"].ToString();
                //                link.CommandArgument = drp["t03_cd_projeto"].ToString();
                //                link.Text = nm_projeto;
                //                link.Click += new EventHandler(link_Click);
                //            }
                //            PanelGrid.Controls.Add(link);
                //            PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Projeto

                //            sb = new StringBuilder();
                //            sb.Append("<td>" + nm_fase + "</td>"); //Fase
                //            sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado
                //            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //            {
                //                t07_restricao t07 = new t07_restricao();
                //                {
                //                    t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                //                    if (t07.List().Tables[0].Rows.Count == 0)
                //                    {
                //                        sb.Append("<td>&nbsp;</td>"); //Restrição
                //                    }
                //                    else
                //                    {
                //                        sb.Append("<td style='text-align:center'><b>R</b></td>"); //Restrição
                //                    }
                //                }
                //            }

                //            sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>"); //Evolução
                //            sb.Append("</tr>");
                //        }
                //    }
                //}
            }
            else //se usuário sem perfil e diferente de visitante
            {
                if (pb.cd_parceiro() != 0) //parceiro
                {
                    t03.order = " and t01_cd_entidade in " +
                        "(select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1 and t01_cd_entidade in " +
                        "(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")) " +
                        "order by nm_entidade";
                    //Response.Write(t03.order+"<br>");
                    foreach (DataRow dre in t03.ListEntidade().Tables[0].Rows)
                    {
                        sb.Append("<tr class=\"hr_white\">");
                        sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                        sb.Append("</tr>");

                        t03.order = tipologia;
                        t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                        foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                        {
                            sb.Append("<tr  class=\"hr_yellow\">");
                            sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr class=\"hr_orange\">");
                            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                            {
                                sb.Append("<td>Eixos</td>");
                            }
                            else { sb.Append("<td colspan=2>Eixos</td>"); }
                      //LEVI       sb.Append("<td>Fase</td>");
					//LEVI 11-02-2015		sb.Append("<td>Atualizado</td>");
                            
                    //LEVI         if (!pb.fl_semperfil((int)dre["t01_cd_entidade"])) { sb.Append("<td>Restri&ccedil;&atilde;o</td>"); }
                            sb.Append("<td style='width:200px'>Evolu&ccedil;&atilde;o</td>");
                            sb.Append("</tr>");

                            t03.order = "where fl_ativa=1" +
                                " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                                " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                                " order by nm_projeto";

                            foreach (DataRow drp in t03.List().Tables[0].Rows)
                            {
                                string nm_projeto = drp["nm_projeto"].ToString();
                                string nm_fase = "";
                                string data = "-";

                                if (drp["dt_alterado"] != DBNull.Value)
                                {
                                    data = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(drp["dt_alterado"].ToString()));
                                }

                                t19_fase t19 = new t19_fase();
                                {
                                    t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                                    t19.RetrieveFaseProjeto();
                                    if (t19.Found)
                                    {
                                        nm_fase = t19.nm_fase;
                                    }
                                }

                                sb.Append("<tr>");
                                PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                                if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                                {
                                    PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                                }
                                else { PanelGrid.Controls.Add(pb.GetLiteral("<td colspan=2>")); }

                                LinkButton link = new LinkButton();
                                {
                                    link.ID = "linkP2" + drp["t03_cd_projeto"].ToString();
                                    link.CommandArgument = drp["t03_cd_projeto"].ToString();
                                    link.Text = nm_projeto;
                                    link.Click += new EventHandler(link_Click);
                                }
                                PanelGrid.Controls.Add(link);
                                PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Programas

                                sb = new StringBuilder();
                         //LEVI        sb.Append("<td>" + nm_fase + "</td>"); //Fase
                         //LEVI 11-02-2015       sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado
                                if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                                {
                                    t07_restricao t07 = new t07_restricao();
                                    {
                                        t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                                        if (t07.List().Tables[0].Rows.Count == 0)
                                        {
                                     //LEVI        sb.Append("<td>&nbsp;</td>"); //Restrição
                                        }
                                        else
                                        {
                                     //LEVI       sb.Append("<td style='text-align:center'><b>R</b></td>"); //Restrição
                                        }
                                    }
                                }

                                sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>"); //Evolução
                                sb.Append("</tr>");
                            }
                        }
                    }
                }
                else //administrador parceiro
                {
                    t03.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where " + tipologia + " fl_ativa=1 and t01_cd_entidade=" + pb.cd_entidade() + " ) order by nm_entidade";
                    foreach (DataRow dre in t03.ListEntidade().Tables[0].Rows)
                    {
                        sb.Append("<tr class=\"hr_white\">");
                        sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                        sb.Append("</tr>");

                        t03.order = tipologia;
                        t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                        foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                        {
                            sb.Append("<tr  class=\"hr_yellow\">");
                            sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr class=\"hr_orange\">");
                            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                            {
                                sb.Append("<td>Eixos</td>");
                            }
                            else { sb.Append("<td colspan=2>Eixos</td>"); }

                           // sb.Append("<td>Fase</td>");
                            sb.Append("<td>Atualizado</td>");
                            //if (!pb.fl_semperfil((int)dre["t01_cd_entidade"])) { sb.Append("<td>Restri&ccedil;&atilde;o</td>"); }
                            //sb.Append("<td style='width:120px'>Evolu&ccedil;&atilde;o</td>");
                            sb.Append("</tr>");

                            t03.order = "where fl_ativa=1" +
                                " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                                " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                                " order by nm_projeto";

                            foreach (DataRow drp in t03.List().Tables[0].Rows)
                            {
                                string nm_projeto = drp["nm_projeto"].ToString();
                                string nm_fase = "";
                                string data = "-";

                                if (drp["dt_alterado"] != DBNull.Value)
                                {
                                    data = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(drp["dt_alterado"].ToString()));
                                }

                                t19_fase t19 = new t19_fase();
                                {
                                    t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                                    t19.RetrieveFaseProjeto();
                                    if (t19.Found)
                                    {
                                        nm_fase = t19.nm_fase;
                                    }
                                }

                                sb.Append("<tr>");
                                PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                                if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                                {
                                    PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                                }
                                else { PanelGrid.Controls.Add(pb.GetLiteral("<td colspan=2>")); }
                                LinkButton link = new LinkButton();
                                {
                                    link.ID = "linkP" + drp["t03_cd_projeto"].ToString();
                                    link.CommandArgument = drp["t03_cd_projeto"].ToString();
                                    link.Text = nm_projeto;
                                    link.Click += new EventHandler(link_Click);
                                }
                                PanelGrid.Controls.Add(link);
                                PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Programas

                                sb = new StringBuilder();
                               // sb.Append("<td>" + nm_fase + "</td>"); //Fase
                                sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado

                                //if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                                //{
                                   // t07_restricao t07 = new t07_restricao();
                                   // {
                                      //  t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                                      //  if (t07.List().Tables[0].Rows.Count == 0)
                                      //  {
                                      //      sb.Append("<td>&nbsp;</td>");
                                     //   }
                                    //    else
                                       // {
                                       //     sb.Append("<td style='text-align:center'><b>R</b></td>");
                                      //  }
                                   // }
                              //  }

                                //sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>");
                                sb.Append("</tr>");
                            }
                        }
                    }
                }
                //t03.order = " and t01_cd_entidade in " +
                //    "(select t01_cd_entidade from t03_projeto where "+ tipologia +" fl_ativa=1 and t01_cd_entidade not in " +
                //    "(select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")) " +
                //    "order by nm_entidade";
                ////Response.Write(t03.order + "<br>");
                //foreach (DataRow dre in t03.ListEntidade().Tables[0].Rows)
                //{
                //    sb.Append("<tr class=\"hr_white\">");
                //    sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                //    sb.Append("</tr>");

                //    t03.order = tipologia;
                //    t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                //    foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                //    {
                //        sb.Append("<tr  class=\"hr_yellow\">");
                //        sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                //        sb.Append("</tr>");

                //        sb.Append("<tr class=\"hr_orange\">");
                //        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //        {
                //            sb.Append("<td>Projeto</td>");
                //        }
                //        else { sb.Append("<td colspan=2>Projeto</td>"); }
                //        sb.Append("<td>Fase</td>");
                //        sb.Append("<td>Atualizado</td>");
                //        if (!pb.fl_semperfil((int)dre["t01_cd_entidade"])) { sb.Append("<td>Restri&ccedil;&atilde;o</td>"); }
                //        sb.Append("<td style='width:120px'>Evolu&ccedil;&atilde;o</td>");
                //        sb.Append("</tr>");

                //        t03.order = "where fl_ativa=1" +
                //            " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() +
                //            " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                //            " order by nm_projeto";

                //        foreach (DataRow drp in t03.List().Tables[0].Rows)
                //        {
                //            string nm_projeto = drp["nm_projeto"].ToString();
                //            string nm_fase = "";
                //            string data = "-";

                //            if (drp["dt_alterado"] != DBNull.Value)
                //            {
                //                data = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(drp["dt_alterado"].ToString()));
                //            }

                //            t19_fase t19 = new t19_fase();
                //            {
                //                t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                //                t19.RetrieveFaseProjeto();
                //                if (t19.Found)
                //                {
                //                    nm_fase = t19.nm_fase;
                //                }
                //            }

                //            sb.Append("<tr>");
                //            PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                //            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //            {
                //                PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                //            }
                //            else { PanelGrid.Controls.Add(pb.GetLiteral("<td colspan=2>")); }
                //            LinkButton link = new LinkButton();
                //            {
                //                link.ID = "linkP3" + drp["t03_cd_projeto"].ToString();
                //                link.CommandArgument = drp["t03_cd_projeto"].ToString();
                //                link.Text = nm_projeto;
                //                link.Click += new EventHandler(link_Click);
                //            }
                //            PanelGrid.Controls.Add(link);
                //            PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Projeto

                //            sb = new StringBuilder();
                //            sb.Append("<td>" + nm_fase + "</td>"); //Fase
                //            sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado
                //            if (!pb.fl_semperfil((int)dre["t01_cd_entidade"]))
                //            {
                //                t07_restricao t07 = new t07_restricao();
                //                {
                //                    t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                //                    if (t07.List().Tables[0].Rows.Count == 0)
                //                    {
                //                        sb.Append("<td>&nbsp;</td>"); //Restrição
                //                    }
                //                    else
                //                    {
                //                        sb.Append("<td style='text-align:center'><b>R</b></td>"); //Restrição
                //                    }
                //                }
                //            }

                //            sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>"); //Evolução
                //            sb.Append("</tr>");
                //        }
                //    }
                //}
            }

            sb.Append("</table>");
        }
        PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));
    }
}

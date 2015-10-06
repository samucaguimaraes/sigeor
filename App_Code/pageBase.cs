using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;


/// <summary>
/// Summary description for pageBase
/// </summary>
public class pageBase
{
    
    public string msgerro = "Não foi possível efetuar a operação, caso a dificuldade persista, contate o administrador.";

    #region Sessions & Perfis
    
    private string _cd_usuario;
    private string _nm_nome;
    private int _cd_projeto;
    private int _cd_tipologia;
    private int _cd_entidade;
    private int _cd_parceiro;
    private int _cd_acao;
    private int _cd_entidade_projeto;

    private bool _fl_gerente;
    private bool _fl_admin;
    private bool _fl_adminparceiro;
    private bool _fl_estrategico;
    private bool _fl_visitante;
    private bool _fl_semperfil; //usuário sem perfil

    private bool _gestaoInterna;//tipologia

    private string _sqlfiltro; //monitoramento

    public string cd_usuario() { if (HttpContext.Current.Session["cd_usuario"] != null) { _cd_usuario = HttpContext.Current.Session["cd_usuario"].ToString(); } return _cd_usuario; }
    public string nm_nome() { if (HttpContext.Current.Session["nm_nome"] != null) { _nm_nome = HttpContext.Current.Session["nm_nome"].ToString(); } return _nm_nome; }
    public int cd_tipologia() { if (HttpContext.Current.Session["cd_tipologia"] != null) { _cd_tipologia = Int32.Parse(HttpContext.Current.Session["cd_tipologia"].ToString()); } return _cd_tipologia; }
    public int cd_projeto() { if (HttpContext.Current.Session["cd_projeto"] != null) { _cd_projeto = Int32.Parse(HttpContext.Current.Session["cd_projeto"].ToString()); } return _cd_projeto; }
    public int cd_acao() { if (HttpContext.Current.Session["cd_acao"] != null) { _cd_acao = Int32.Parse(HttpContext.Current.Session["cd_acao"].ToString()); } return _cd_acao; }
    public int cd_entidade() { if (HttpContext.Current.Session["cd_entidade"] != null) { _cd_entidade = Int32.Parse(HttpContext.Current.Session["cd_entidade"].ToString()); } return _cd_entidade; }
    public int cd_entidade_projeto() { if (HttpContext.Current.Session["cd_entidade_projeto"] != null) { _cd_entidade_projeto = Int32.Parse(HttpContext.Current.Session["cd_entidade_projeto"].ToString()); } return _cd_entidade_projeto; }
    public int cd_parceiro() { if (HttpContext.Current.Session["cd_parceiro"] != null) { _cd_parceiro = Int32.Parse(HttpContext.Current.Session["cd_parceiro"].ToString()); } return _cd_parceiro; }

    public bool fl_gerente() { if (HttpContext.Current.Session["fl_gerente"] != null) { _fl_gerente = bool.Parse(HttpContext.Current.Session["fl_gerente"].ToString()); } return _fl_gerente; }
    public bool fl_admin() { if (HttpContext.Current.Session["fl_admin"] != null) { _fl_admin = bool.Parse(HttpContext.Current.Session["fl_admin"].ToString()); } return _fl_admin; }
    public bool fl_adminparceiro() { if (HttpContext.Current.Session["fl_adminparceiro"] != null) { _fl_adminparceiro = bool.Parse(HttpContext.Current.Session["fl_adminparceiro"].ToString()); } return _fl_adminparceiro; }
    public bool fl_estrategico() { if (HttpContext.Current.Session["fl_estrategico"] != null) { _fl_estrategico = bool.Parse(HttpContext.Current.Session["fl_estrategico"].ToString()); } return _fl_estrategico; }
    public bool fl_visitante() { if (HttpContext.Current.Session["fl_visitante"] != null) { _fl_visitante = bool.Parse(HttpContext.Current.Session["fl_visitante"].ToString()); } return _fl_visitante; }
    
    public bool fl_semperfil(int cd_entidade_atual) 
    {
        bool result = true;
        if (fl_visitante())
        {
            result = true;
        }
        else
        {
            if (fl_estrategico() || fl_adminparceiro() || fl_admin())
            {
                result = false;
            }
            else
            {
                if (cd_entidade() == 0)
                {
                    t05_parceiro t05 = new t05_parceiro();
                    {
                        t05.t05_cd_parceiro = cd_parceiro();
                        t05.Retrieve();
                        if (t05.Found)
                        {
                            if (t05.t01_cd_entidade == cd_entidade_atual)
                            {
                                result = false;
                            }
                            else
                            {
                                result = true;
                            }
                        }
                    }
                }
                else
                {
                    if (cd_entidade() == cd_entidade_atual)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
        }
        _fl_semperfil= result;
        return _fl_semperfil; 
    }
    
    public bool gestaoInterna() { if (HttpContext.Current.Session["gestaoInterna"] != null) { _gestaoInterna = bool.Parse(HttpContext.Current.Session["gestaoInterna"].ToString()); } return _gestaoInterna; }

    public string sqlfiltro() { if (HttpContext.Current.Session["sqlfiltro"] != null) { _sqlfiltro = HttpContext.Current.Session["sqlfiltro"].ToString(); } return _sqlfiltro; }
    #endregion

    public string strConn()
    {
        Conn cn = new Conn();
        return cn.conexao;
    }

    public string Message(string str, string img)
    {
        //img=ok or erro
        return "<span class=msg><img src=\"images/" + img + ".gif\" />&nbsp;" + str + "</span>";
    }
    public void AddEmptyItem(DropDownList ddl, string str)
    {
        ListItem li = new ListItem(str, "");
        ddl.Items.Insert(0, li);
    }
    public void AddColorLines(DropDownList ddl, string hexcolor)
    {
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            if (i % 2 != 0)
            {
                ddl.Items[i].Attributes.Add("Style", "Background-Color:" + hexcolor);
            }
        }

    }
    public void AppendSortOrderImageToGridHeader(System.Web.UI.WebControls.SortDirection sortDir, string sortExpr, System.Web.UI.WebControls.GridView grid)
    {

        // looping variable 
        int i;
        // did we find the column header that's being sorted? 
        int foundColumnIndex = -1;

        // constants for sort orders 
        const string SORT_ASC = "<span> <img src='images/tridown.gif' /></span>";
        const string SORT_DESC = "<span> <img src='images/triup.gif' /></span>";

        // get which column we're sorting on 
        for (i = 0; i <= grid.Columns.Count - 1; i++)
        {
            // remove the current sort 
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_ASC, string.Empty);
            grid.Columns[i].HeaderText = grid.Columns[i].HeaderText.Replace(SORT_DESC, string.Empty);
            // if the sort expression of this column matches the passed sort expression, 
            // keep the column number and mark that we've found a match for further processing 
            if (sortExpr == grid.Columns[i].SortExpression)
            {
                // store the column number, but we need to keep going through the loop 
                // to remove all the previous sorts 
                foundColumnIndex = i;
            }
        }

        // if we found the sort column, append the sort direction 
        if (foundColumnIndex > -1)
        {
            // append either ascending or descending string 
            if (sortDir == SortDirection.Ascending)
            {
                grid.Columns[foundColumnIndex].HeaderText += SORT_ASC;
            }
            else
            {
                grid.Columns[foundColumnIndex].HeaderText += SORT_DESC;
            }

        }

    }
    public string ReplaceNewLines(string text)
    {
        if (text == null) return null;

        int length;
        StringReader reader;
        StringWriter writer;
        StringBuilder builder;
        string line;

        length = (int)((double)text.Length * 1.2); //apply some padding to avoid array resizing, you probably want to
        //tweak this value for the size of the strings you're using
        reader = new StringReader(text);
        builder = new StringBuilder(length);
        writer = new StringWriter(builder);

        line = reader.ReadLine();
        if (line != null)
        {
            /*this if then while loop avoids adding an extra blank line at the end of the conversion
            * as opposed to just using:
            * while (line != null) {
            * writer.Write(line);
            * writer.WriteLine("<br/>");
            */

            writer.Write(line);
            line = reader.ReadLine();

            while (line != null)
            {
                writer.WriteLine("<br/>");
                writer.Write(line);
                line = reader.ReadLine();
            }
        }

        return writer.ToString();

    }
    public Literal GetLiteral(string text)
    {
        Literal rv;
        rv = new Literal();
        rv.Text = text;
        return rv;
    }
    public string ReplaceAspas(object input)
    {
        string functionReturnValue = null;
        functionReturnValue = "";
        if ((!object.ReferenceEquals(input, DBNull.Value)))
        {
            functionReturnValue = (string)input.ToString().Replace("'", "");
        }
        return functionReturnValue;
    }
    public void MakeAccessible(GridView grid)
    {
        if (grid.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute 
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements 
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. Remove if you don't have a footer row 
            grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    public void saveLog(string cd_usuario, int cd_projeto, 
        string log, string tabela, string comando, string valor)
    {
        t22_log t22 = new t22_log();
        {
            t22.t02_cd_usuario = cd_usuario;
            t22.t03_cd_projeto = cd_projeto;
            t22.ds_log = log;
            t22.nm_tabela = tabela;
            t22.nm_valor = valor;
            t22.nm_comando = comando;
            t22.dt_data = DateTime.Now;
            t22.Save();
        }
    }

    public string Status(int cd_projeto)
    {
        StringBuilder sb = new StringBuilder();
        string fl_status;
        int azul = 0;
        int amarelo = 0;
        int vermelho = 0;
        int verde = 0;
        int total = 0;

        t08_acao t08 = new t08_acao();
        {
            //t08.order = " and t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao)";
            t08.t03_cd_projeto = cd_projeto;
            foreach (DataRow dra in t08.List().Tables[0].Rows)
            {
                
                bool r = false;
                bool g = false;
                bool b = false;
                bool nenhum = false;
                t09_marco t09 = new t09_marco();
                {
                    t09.order = " and t08_cd_acao not in (select t08_cd_acao from t29_acaorestricao where t08_cd_acao not in (select t08_cd_acao from t09_marco where fl_ativa=1 and fl_status='R'))";
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
                else
                {
                    nenhum = true;
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
                            nenhum = false;
                        }
                        else
                        {
                            nenhum = true;
                        }
                    }
                }
                //if (!nenhum)
                //{
                //    total++;
                //}
            }

            total = verde + vermelho + amarelo + azul;

            
            //t09_marco t09 = new t09_marco();
            //{
            //    t09.order = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto=" + cd_projeto.ToString() + ")";
            //    foreach (DataRow dr in t09.ListStatus().Tables[0].Rows)
            //    {
            //        fl_status = dr["fl_status"].ToString().Trim();
            //        switch (fl_status)
            //        {
            //            case ("Y"):
            //                amarelo += Int32.Parse(dr["nu_esforco"].ToString());
            //                break;

            //            case ("B"):
            //                azul += Int32.Parse(dr["nu_esforco"].ToString());
            //                break;

            //            case ("G"):
            //                verde += Int32.Parse(dr["nu_esforco"].ToString());
            //                break;

            //            case ("R"):
            //                vermelho += Int32.Parse(dr["nu_esforco"].ToString());
            //                break;
            //        }
            //        total += Int32.Parse(dr["nu_esforco"].ToString());
            //    }
            //}
            if (total > 0)
            {
                sb.Append("<table width=100% height=17 border=0 cellpadding=0 cellspacing=0><tr>");
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
                sb.Append("&nbsp;");
            }
            
        }


        return sb.ToString();
    }
    
    public void UpdateCorBarra()
    {
        t09_marco t09 = new t09_marco();
        {
            t09.fl_status = "B";
            t09.order = "where dt_realizada is not null";
            t09.UpdateCor();

            t09.fl_status = "G";
            t09.order = "where dt_prevista >= getdate() and dt_realizada is null";
            t09.UpdateCor();

            t09.fl_status = "R";
            t09.order = "where dt_prevista < getdate()-1 and dt_realizada is null";
            t09.UpdateCor();
        }
    }
    public void MaxLength(TextBox txt)
    {
        txt.Attributes.Add("onkeypress", "doKeypress(this);");
        txt.Attributes.Add("onbeforepaste", "doBeforePaste(this);");
        txt.Attributes.Add("onpaste", "doPaste(this);");
        txt.Attributes.Add("maxLength", txt.MaxLength.ToString());
    }


    public void criarEmail(int cod, string msg)
    {
        string bcc = "levi.miranda@setre.ba.gov.br";
        string projeto = "";
        

               MailMessage mail = new MailMessage();
               {
                   t03_projeto t03 = new t03_projeto();
                   {
                       t03.order = "where t03_cd_projeto =" + cod;
                       foreach (DataRow dr2 in t03.List().Tables[0].Rows)
                       {
                           projeto = dr2["nm_projeto"].ToString();
                       }
                   }

                   t02_usuario t02 = new t02_usuario();
                   {
                       t02.t01_cd_entidade = cd_entidade();
                       foreach (DataRow dr in t02.ListLinha().Tables[0].Rows)
                       {
                           string email = dr["nm_email"].ToString();
                           mail.From = new MailAddress("Agenda Bahia - Nova Restrição <levi.miranda@setre.ba.gov.br>");
                           mail.To.Add(new MailAddress(email));
                           //mail.Bcc.Add(new MailAddress(bcc));
                           mail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                           mail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                           mail.Subject = "Agenda Bahia";

                           System.Text.StringBuilder sb = new System.Text.StringBuilder();
                           {
                               sb.Append("<body style=margin:0px;>");
                               sb.Append("<div style=text-align:left><img hspace=0 src=\"http://www.sigeorparceiros.com.br/images/interna_04_06_2008.jpg\" align=baseline border=0></div><br />");
                               sb.Append("<font face=Verdana, Arial, Helvetica, sans-serif size=2>");
                               sb.Append("<div style=padding:10px; >Prezado(a) <b>" + dr["nm_nome"].ToString() + "</b>,<br /><br /></div>");
                               sb.Append("<div style=padding:10px; >Há uma nova restrição para o projeto: <b>" + projeto + ".</b><br /><br /></div>");
                               sb.Append("<div style=padding:10px; ><b>Restrição:</b><br>" + ReplaceNewLines(msg) + "</div>");
                               sb.Append("<div style=padding:10px; ><p style=color:#999999>Sigeor Parceiros - Restrição</p>");
                               sb.Append("<p><a href='http://www.sigeorparceiros.com.br'>www.sigeorparceiros.com.br</a></p></div>");
                               sb.Append("</body>");

                           }
                           AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), System.Text.Encoding.GetEncoding("ISO-8859-1"), "text/html");
                           mail.AlternateViews.Add(htmlView);
                           enviarEmail(mail);
                       }


                       //Response.Write(cod, msg);
                   }
               }

    }

    protected void enviarEmail(System.Net.Mail.MailMessage mail)
    {
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("127.0.0.1");
        smtp.Send(mail);
    }




}

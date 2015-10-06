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

public partial class frmLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["msgerro"] != null)
        {
            Login1.InstructionText = Request["msgerro"].ToString();
        }
       if (Login1.FindControl("UserName")!=null)Login1.FindControl("UserName").Focus();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Session.Clear();
        bool logado;
        logado = LogarUsuario(Login1.UserName, Login1.Password);
        e.Authenticated = logado;

        if (e.Authenticated)
        {
            pageBase pb = new pageBase();
            pb.UpdateCorBarra();
            t21_acesso t21 = new t21_acesso();
            {
                t21.t02_cd_usuario = pb.cd_usuario();
                t21.nm_ip = Request.UserHostAddress;
                t21.dt_data = DateTime.Now;
                t21.Save();
            }

            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
            Response.Redirect("Projetos.aspx");

        }
    }

    protected bool LogarUsuario(string usuario, string senha)
    {
        bool boolReturn = false;

        t02_usuario t02 = new t02_usuario();
        {
            t02.t02_cd_usuario = usuario;
            t02.pw_senha = senha;
            t02.RetrieveLogin();
            boolReturn = t02.Found;

            if (t02.Found)
            {
                
                Session["cd_usuario"] = t02.t02_cd_usuario;
                Session["nome"] = t02.nm_nome;
                if (t02.t01_cd_entidade > 0)
                {
                    Session["cd_entidade"] = t02.t01_cd_entidade;
                }
                else
                {
                    Session["cd_parceiro"] = t02.t05_cd_parceiro;
                }
				
                t26_usuarioperfil t26 = new t26_usuarioperfil();
                {
                    t26.t02_cd_usuario = t02.t02_cd_usuario;
                    foreach (DataRow dr in t26.List().Tables[0].Rows)
                    {
                        string flag = dr["ds_perfil"].ToString();
                        switch (flag.Trim())
                        {
                            case "fl_admin":
                                Session["fl_admin"] = true;
                                break;
                            case "fl_adminparceiro":
                                Session["fl_adminparceiro"] = true;
                                break;
                            case "fl_estrategico":
                                Session["fl_estrategico"] = true;
                                break;
                        }
                    }
                }
            }
        }

        return boolReturn;
    }

    protected void Login1_LoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
    {

    }

    protected void Login1_LoginError(object sender, EventArgs e)
    {
        //Login1.HelpPageText = "Help with logging in...";
        //Login1.PasswordRecoveryText = "Forgot your password?";
        Login1.FailureText = "usuário ou senha incorreto";
    }

    protected void btnVisitante_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["cd_usuario"] = "visitante";
        Session["nome"] = "Visitante";
        Session["fl_visitante"] = true;
        pageBase pb = new pageBase();
        pb.UpdateCorBarra();
        t21_acesso t21 = new t21_acesso();
        {
            t21.t02_cd_usuario = pb.cd_usuario();
            t21.nm_ip = Request.UserHostAddress;
            t21.dt_data = DateTime.Now;
            t21.Save();
        }

        FormsAuthentication.RedirectFromLoginPage("Visitante", true);
        Response.Redirect("Projetos.aspx");
    }
}

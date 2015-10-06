using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pageBase pb = new pageBase();
        // disable page caching
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        if (Session["nome"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage("msgerro=Sessão expirou!");
        }
        else
        {
           lblnm_usuario.Text = (string)Session["nome"].ToString();
        }
        if (pb.fl_visitante()) topoUsuario.Visible = false;

    }
    protected void btnSair_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage("msgerro=Sessão finalizada!");
    }
}

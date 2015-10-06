using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class frmSenha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["cd_usuario"] == null)
        {
            ucSenha.Usuario = Session["cd_usuario"].ToString();
        }
        else
        {
            ucSenha.Usuario = Request["cd_usuario"].ToString();
            ucSenha.SenhaAtual = false;
            ucSenha.Destino = "frmUsuario.aspx";
        }
    } 
}

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

public partial class Gerenciamento : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["msg"] != null)
            {
                Label lblmsg = (Label)ucRestricao.FindControl("lblMsg");
                lblmsg.Visible = true;
                lblmsg.Text = pb.Message(Request["msg"].ToString(), "ok");
            }
            //Sem perfil
            if (pb.fl_semperfil(pb.cd_entidade_projeto()))
            {
                ucRestricao.Visible = false;
            }
        }
    }
}

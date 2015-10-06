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

public partial class Foco : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucFoco.FindControl("lblHeader").Visible = false;
        if (pb.gestaoInterna())
        {
            lbltitulo.Text = "Demanda";
        }
        else
        {
            lbltitulo.Text = "Foco estratégico";
        }
    }
}

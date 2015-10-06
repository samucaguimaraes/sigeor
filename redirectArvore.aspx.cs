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

public partial class redirectArvore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["cd_projeto"] != null)
        {
            Session["cd_projeto"] = Request["cd_projeto"];
            Response.Redirect("Arvore.aspx");
        }
    }
}

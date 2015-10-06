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

public partial class ucNomeProjeto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["nm_projeto"] != null) lblnm_projeto.Text = Session["nm_projeto"].ToString();
        if (Session["nm_tipologia"] != null) lblnm_tipologia.Text = Session["nm_tipologia"].ToString();
    }
}

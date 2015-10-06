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

public partial class ucLegenda : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pageBase pb = new pageBase();
        if (pb.fl_visitante())
        {
            tdRestricao.Visible = false;
            tdY.ColSpan = 2;

            tdR.Width = "25%";
            tdY.Width = "25%";
            tdB.Width = "25%";
            tdG.Width = "25%";
        }
        else
        {
            tdR.Width = "20%";
            tdY.Width = "20%";
            tdB.Width = "20%";
            tdG.Width = "20%";
            tdRestricao.Width = "20%";
        }
    }
}

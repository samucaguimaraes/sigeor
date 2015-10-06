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

public partial class teste : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        uploadArquivo up = new uploadArquivo();
        {
            up.pasta = "Documentos";
            up.nomeinicial = "documento_";
            up.fu = FileUpload1;
            up.Save();
            
        }
    }
}

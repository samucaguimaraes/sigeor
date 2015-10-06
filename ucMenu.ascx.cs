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
public partial class ucMenu : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (pb.fl_admin() || pb.fl_adminparceiro())
        {
            umAdministrador.Visible = true;
            if (pb.fl_adminparceiro())
            {
                umProjeto.Visible = true;
                umParceiros.Visible = true;
                umEntidade.Visible = false;
                //umTipologia.Visible = false;
                umUsuario.Visible = true;
                umLinha.Visible = true;
            }
            else
            {
                umProjeto.Visible = false;
                umParceiros.Visible = false;
                umLinha.Visible = false;
                umEntidade.Visible = true;
                //umTipologia.Visible = true;
                umUsuario.Visible = true;
            }
            if (pb.fl_admin() && pb.fl_adminparceiro())
            {
                umProjeto.Visible = true;
                umParceiros.Visible = true;
                umEntidade.Visible = true;
                //umTipologia.Visible = true;
                umUsuario.Visible = true;

            }
    
        }
        else
        {
            umAdministrador.Visible = false;

            t03_projeto t03 = new t03_projeto();
            {
                t03.order = " where t02_cd_usuario='" + pb.cd_usuario() + "' or t02_cd_usuario_monitoramento='" + pb.cd_usuario() + "'";
                if (t03.List().Tables[0].Rows.Count >= 1)
                {
                    mMonitora.Visible = true;
                }
                else
                {
                    mMonitora.Visible = false;
                }
            }
            
        }
        
        if (pb.fl_visitante())
        {
            mMonitora.Visible = false;
        }

        if (pb.cd_projeto() != 0)
        {
            umDocArvore.Visible = true;
            umDocProjeto.Visible = true;
            umDocGerente.Visible = true;
            umDocMatriz.Visible = true;
        }
        else
        {
            umDocArvore.Visible = false;
            umDocProjeto.Visible = false;
            umDocGerente.Visible = false;
            umDocMatriz.Visible = false;
        }
    }
}

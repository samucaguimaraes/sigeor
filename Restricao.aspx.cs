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

public partial class Restricao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Context.Items["t07_cd_restricao"] != null)
        {
            Retrieve(Int32.Parse(Context.Items["t07_cd_restricao"].ToString()));
        }
        else
        {
            Response.Redirect("~/Gerenciamento.aspx");
        }
    }
    private void Retrieve(int cod)
    {
        t07_restricao t07 = new t07_restricao();
        {
            t07.t07_cd_restricao = cod;
            t07.Retrieve();
            if (t07.Found)
            {
                lblds_restricao.Text = t07.ds_restricao;
                lblds_medida.Text = t07.ds_medida;
                lbldt_limite.Text = t07.dt_limite.ToShortDateString();
                lbldt_cadastro.Text = t07.dt_cadastro.ToShortDateString();

                
                t29_acaorestricao t29 = new t29_acaorestricao();
                {
                    t29.t07_cd_restricao = t07.t07_cd_restricao;
                    t29.Retrieve();
                    if (t29.Found)
                    {
                        t08_acao t08 = new t08_acao();
                        {
                            t08.t08_cd_acao = t29.t08_cd_acao;
                            t08.Retrieve();
                            if (t08.Found)
                            {
                                trAcao.Visible = true;
                                lblnm_acao.Text = t08.nm_acao;
                            }
                          
                        }
                    }
                    else
                    {
                        trProjeto.Visible = true;
                    }
                }
            }
        }
        
        
    }

}

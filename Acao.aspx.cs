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

public partial class Acao : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        FormBind();
        
    }

    protected void FormBind()
    {
        t08_acao t08 = new t08_acao();
        {
            t08.t08_cd_acao = pb.cd_acao();
            t08.Retrieve();
            if (t08.Found)
            {
                lblds_acao.Text = t08.ds_acao;
                lblnm_acao.Text = t08.nm_acao;
                lbldt_inicio.Text = t08.dt_inicio.ToShortDateString();
                lbldt_fim.Text = t08.dt_fim.ToShortDateString();
				lblds_palvo.Text = t08.ds_palvo;
				lblds_latuacao.Text = t08.ds_latuacao;
				lblds_andamento.Text = t08.ds_andamento;

                t02_usuario t02 = new t02_usuario();
                {
                    t02.fl_ativa = true;
                    t02.order = " and t02.t02_cd_usuario = '" + t08.t02_cd_usuario + "'";
                    foreach (DataRow dr in t02.ListParceiro().Tables[0].Rows)
                    {
                        lblnm_nome.Text = dr["nm_nome"].ToString();
                        if ((int)dr["t05_cd_parceiro"] == 0)
                        {
                            lblnm_parceiro.Text = dr["nm_entidade"].ToString();
                        }
                        else
                        {
                            lblnm_parceiro.Text = dr["nm_parceiro"].ToString();
                        }
                    }
                }
            }
            t11_financeiro t11 = new t11_financeiro();
            {
                t11.order = "where t08_cd_acao="+ t08.t08_cd_acao;
                string financiadores = "";
                foreach (DataRow dr in t11.ListInvestimento().Tables[0].Rows)
                {
                    financiadores += dr["nm_parceiro"] + ", ";
                }
                if (financiadores.Length > 1)
                {
                    lblfinanciadores.Text = financiadores.Substring(0, financiadores.Length - 2) + ".";
                }
            }
        }
    }

    protected void ucFinanceiro_PreRender(object sender, EventArgs e)
    {
        FormBind();
    }
}

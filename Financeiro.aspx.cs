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

public partial class Financeiro : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Context.Items["t11_cd_financeiro"] != null)
            //{
            //    Retrieve(Int32.Parse(Context.Items["t11_cd_financeiro"].ToString()));
            //}
            //else
            //{
            //    Response.Redirect("~/Acao.aspx");
            //}
        }
    }
    protected void ucRealizado_Load(object sender, EventArgs e)
    {
        if (Context.Items["t11_cd_financeiro"] != null)
        {
            Retrieve(Int32.Parse(Context.Items["t11_cd_financeiro"].ToString()));
        }
        else
        {
            Response.Redirect("~/Acao.aspx");
        }
    }

    private void Retrieve(int cod)
    {
        t11_financeiro t11 = new t11_financeiro();
        {
            t11.t11_cd_financeiro = cod;
            t11.Retrieve();
            if (t11.Found)
            {
                if (t11.fl_economico)
                {
                    lbltipo.Text = "Econômico";
                }
                else
                {
                    lbltipo.Text = "Financeiro";
                }
                t05_parceiro t05 = new t05_parceiro();
                {
                    t05.t05_cd_parceiro = t11.t05_cd_parceiro;
                    t05.Retrieve();
                    if (t05.Found)
                    { lblnm_parceiro.Text = t05.nm_parceiro; }
                }
                t08_acao t08 = new t08_acao();
                {
                    t08.t08_cd_acao = pb.cd_acao();
                    t08.Retrieve();
                    if (t08.Found)
                    {
                        lblnm_acao.Text = t08.nm_acao;
                        t28_vlfinanceiro t28 = new t28_vlfinanceiro();
                        {
                            for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                            {
                                t28.t11_cd_financeiro = t11.t11_cd_financeiro;
                                t28.nu_ano = i;
                                t28.Retrieve();

                                TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                                TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                                TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                                TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());
                                TextBox txtvl_ptotal = (TextBox)ucPrevisto.FindControl("txtvl_ptotal5" + i.ToString());

                                TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                                TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                                TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                                TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());
                                TextBox txtvl_rtotal = (TextBox)ucRealizado.FindControl("txtvl_rtotal5" + i.ToString());

                                if (t28.Found)
                                {
                                    if (txtvl_p1 != null)
                                    {
                                        txtvl_p1.Text = t28.vl_p1.ToString("N2");
                                        txtvl_p4.Text = t28.vl_p4.ToString("N2");
                                        txtvl_p8.Text = t28.vl_p8.ToString("N2");
                                        txtvl_p12.Text = t28.vl_p12.ToString("N2");
                                        txtvl_ptotal.Text = (t28.vl_p1 + t28.vl_p4 + t28.vl_p8 + t28.vl_p12).ToString("N2");
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        txtvl_r1.Text = t28.vl_r1.ToString("N2");
                                        txtvl_r4.Text = t28.vl_r4.ToString("N2");
                                        txtvl_r8.Text = t28.vl_r8.ToString("N2");
                                        txtvl_r12.Text = t28.vl_r12.ToString("N2");
                                        txtvl_rtotal.Text = (t28.vl_r1 + t28.vl_r4 + t28.vl_r8 + t28.vl_r12).ToString("N2");
                                    }
                                }
                                else
                                {
                                    if (txtvl_p1 != null)
                                    {
                                        txtvl_p1.Text = "0,00";
                                        txtvl_p4.Text = "0,00";
                                        txtvl_p8.Text = "0,00";
                                        txtvl_p12.Text = "0,00";
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        txtvl_r1.Text = "0,00";
                                        txtvl_r4.Text = "0,00";
                                        txtvl_r8.Text = "0,00";
                                        txtvl_r12.Text = "0,00";
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    }
   
}

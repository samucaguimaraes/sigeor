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

public partial class Produto : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void ucRealizado_Load(object sender, EventArgs e)
    {
        if (Context.Items["t10_cd_produto"] != null)
        {
            Retrieve(Int32.Parse(Context.Items["t10_cd_produto"].ToString()));
        }
        else
        {
            Response.Redirect("~/Acao.aspx");
        }
    }

    private void Retrieve(int cod)
    {
        t10_produto t10 = new t10_produto();
        {
            t10.t10_cd_produto = cod;
            t10.Retrieve();
            if (t10.Found)
            {
                lblds_produto.Text = pb.ReplaceNewLines(t10.ds_produto);
                lblnm_medida.Text = t10.nm_medida;
                t08_acao t08 = new t08_acao();
                {
                    t08.t08_cd_acao = pb.cd_acao();
                    t08.Retrieve();
                    if (t08.Found)
                    {
                        lblnm_acao.Text = t08.nm_acao;
                        t27_vlproduto t27 = new t27_vlproduto();
                        {
                            for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                            {
                                t27.t10_cd_produto = t10.t10_cd_produto;
                                t27.nu_ano = i;
                                t27.Retrieve();

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

                                if (t27.Found)
                                {
                                    char c = char.Parse(",");
                                    int casadecimal = 0;
                                    if (txtvl_p1 != null)
                                    {
                                        casadecimal = Int32.Parse(t27.vl_p1.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p1.Text = t27.vl_p1.ToString("N2");
                                        else
                                            txtvl_p1.Text = t27.vl_p1.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p4.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p4.Text = t27.vl_p4.ToString("N2");
                                        else
                                            txtvl_p4.Text = t27.vl_p4.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p8.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p8.Text = t27.vl_p8.ToString("N2");
                                        else
                                            txtvl_p8.Text = t27.vl_p8.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_p12.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_p12.Text = t27.vl_p12.ToString("N2");
                                        else
                                            txtvl_p12.Text = t27.vl_p12.ToString("N0");

                                        casadecimal = Int32.Parse((t27.vl_p1 + t27.vl_p4 + t27.vl_p8 + t27.vl_p12).ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_ptotal.Text = (t27.vl_p1 + t27.vl_p4 + t27.vl_p8 + t27.vl_p12).ToString("N2");
                                        else
                                            txtvl_ptotal.Text = (t27.vl_p1 + t27.vl_p4 + t27.vl_p8 + t27.vl_p12).ToString("N0");

                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        casadecimal = Int32.Parse(t27.vl_r1.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r1.Text = t27.vl_r1.ToString("N2");
                                        else
                                            txtvl_r1.Text = t27.vl_r1.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r4.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r4.Text = t27.vl_r4.ToString("N2");
                                        else
                                            txtvl_r4.Text = t27.vl_r4.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r8.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r8.Text = t27.vl_r8.ToString("N2");
                                        else
                                            txtvl_r8.Text = t27.vl_r8.ToString("N0");

                                        casadecimal = Int32.Parse(t27.vl_r12.ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_r12.Text = t27.vl_r12.ToString("N2");
                                        else
                                            txtvl_r12.Text = t27.vl_r12.ToString("N0");

                                        casadecimal = Int32.Parse((t27.vl_r1 + t27.vl_r4 + t27.vl_r8 + t27.vl_r12).ToString("N2").Split(c)[1].ToString());
                                        if (casadecimal > 0)
                                            txtvl_rtotal.Text = (t27.vl_r1 + t27.vl_r4 + t27.vl_r8 + t27.vl_r12).ToString("N2");
                                        else
                                            txtvl_rtotal.Text = (t27.vl_r1 + t27.vl_r4 + t27.vl_r8 + t27.vl_r12).ToString("N0");
                                    }
                                }
                                else
                                {
                                    if (txtvl_p1 != null)
                                    {
                                        txtvl_p1.Text = "0";
                                        txtvl_p4.Text = "0";
                                        txtvl_p8.Text = "0";
                                        txtvl_p12.Text = "0";
                                        txtvl_ptotal.Text = "0";
                                    }
                                    if (txtvl_r1 != null)
                                    {
                                        txtvl_r1.Text = "0";
                                        txtvl_r4.Text = "0";
                                        txtvl_r8.Text = "0";
                                        txtvl_r12.Text = "0";
                                        txtvl_rtotal.Text = "0";
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

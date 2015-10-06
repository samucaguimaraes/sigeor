using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Resultados : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtvl_t0.Attributes.Add("onKeyPress", "return(MascaraMoeda(this,'.',',',event))");
            //txtnu_ano.Attributes.Add("onkeydown", "return numeros(event)");
            lblTitle.Text = "Resultados pactuados";
            this.Panel1.Visible = false;
            cod.Value = "0";
            GridBind("");
        }
        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
        }
    }
    protected void perfil_Load(object sender, EventArgs e)
    {
        Control obj = (Control)sender;
        if (!pb.fl_gerente()) //se não for gerente
        {
            obj.Visible = false;
        }
    }

    private void Exibir()
    {
        Panel1.Visible = true;
        dlResultado.Visible = false;
        PanelAdd.Visible = false;
        txtds_resultado.Text = "";
        txtnm_medida.Text = "";
        txtnm_resultado.Text = "";
        txtnu_ano.Text = "";
        txtvl_t0.Text = "";
        rblfl_acumulado.ClearSelection();
    }
    private void Ocultar()
    {
        Panel1.Visible = false;
        dlResultado.Visible = true;
        PanelAdd.Visible = true;
        txtds_resultado.Text = "";
        txtnm_medida.Text = "";
        txtnm_resultado.Text = "";
        txtnu_ano.Text = "";
        txtvl_t0.Text = "";
        rblfl_acumulado.ClearSelection();
        GridBind("");
    }
    private void GridBind(string order)
    {
        t14_resultado t14 = new t14_resultado();
        {
            t14.t03_cd_projeto = pb.cd_projeto();
            dlResultado.DataSource = t14.List();
            dlResultado.DataBind();
            if (pb.fl_gerente())
            {
                if ((dlResultado.Items.Count == 0) && (pb.fl_gerente()))
                {
                    lblHeader.Text = "Cadastro";
                    btnAcao.Text = "Cadastrar";
                    Exibir();
                }
            }
        }
    }
    protected void dlResultado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Exibir();
        this.lblHeader.Text = "Alteração";
        this.btnAcao.Text = "Alterar";
        cod.Value = dlResultado.SelectedValue.ToString();
        Retrieve(); 
    }
    private void Retrieve()
    {
        t14_resultado t14 = new t14_resultado();
        {
            t14.t14_cd_resultado = Int32.Parse(cod.Value);
            t14.Retrieve();
            if (t14.Found)
            {
                txtds_resultado.Text = t14.ds_resultado;
                txtnm_medida.Text = t14.nm_medida;
                txtnm_unid.Text = t14.nm_unid;
                txtnm_resultado.Text = t14.nm_resultado;
                txtnu_ano.Text = t14.nu_ano.ToString();
                txtvl_t0.Text = t14.vl_t0.ToString("N");
                ListItem li = rblfl_acumulado.Items.FindByValue(t14.fl_acumulado.ToString());
                if (li != null) li.Selected = true;

                t03_projeto t03 = new t03_projeto();
                {
                    t03.t03_cd_projeto = pb.cd_projeto();
                    t03.Retrieve();
                    if (t03.Found)
                    {
                        for (int i = t03.dt_inicio.Year; i <= t03.dt_fim.Year; i++)
                        {
                            t15_vlresultado t15 = new t15_vlresultado();
                            {
                                t15.t14_cd_resultado = t14.t14_cd_resultado;
                                t15.nu_ano = i;
                                t15.Retrieve();
                                TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
                                TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
                                if (t15.Found)
                                {
                                    if (txtPrev != null) txtPrev.Text = t15.vl_previsto.ToString("N");
                                    if (txtReal != null) txtReal.Text = t15.vl_realizado.ToString("N");
                                }
                                else
                                {
                                    if (txtPrev != null) txtPrev.Text = "0";
                                    if (txtReal != null) txtReal.Text = "0";
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    protected void dlResultado_ItemDataBound(object sender, DataListItemEventArgs e)
    {
            DataRowView drv = ((DataRowView)e.Item.DataItem);

            ImageButton btn = (ImageButton)e.Item.FindControl("btnExc");
            if (btn != null) btn.CommandArgument = drv["t14_cd_resultado"].ToString();
            
            Label lbl;
            lbl = (Label)e.Item.FindControl("lblnm_resultado");
            if (lbl != null) lbl.Text = drv["nm_resultado"].ToString();

            lbl = (Label)e.Item.FindControl("lblds_resultado");
            if (lbl != null) lbl.Text = drv["ds_resultado"].ToString();

            lbl = (Label)e.Item.FindControl("lblnm_medida");
            if (lbl != null) lbl.Text = drv["nm_medida"].ToString();

            lbl = (Label)e.Item.FindControl("lblnu_ano");
            if (lbl != null) lbl.Text = drv["nu_ano"].ToString();

            lbl = (Label)e.Item.FindControl("lblvl_t0");
            if (lbl != null) lbl.Text = drv["vl_t0"].ToString().Replace(",00","");

            lbl = (Label)e.Item.FindControl("lblnm_unid");
            if (lbl != null) lbl.Text = drv["nm_unid"].ToString();

            bool fl_ac = bool.Parse(drv["fl_acumulado"].ToString());
            Panel pn = (Panel)e.Item.FindControl("PanelValores");
            if (pn != null) ValoresBind(fl_ac, pn, Int32.Parse(drv["t14_cd_resultado"].ToString()));

            pn = (Panel)e.Item.FindControl("PanelGrafico");
            if (pn != null) getFCXMLData(Int32.Parse(drv["t14_cd_resultado"].ToString()), e.Item.ItemIndex, fl_ac, pn);

            
            //getFCXMLData(
    }

    protected void btnCadastro_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Cadastro";
        btnAcao.Text = "Cadastrar";
        Exibir();
        Panel1.Visible = true;
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                for (int i = t03.dt_inicio.Year; i <= t03.dt_fim.Year; i++)
                {
                    TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
                    if (txtPrev != null) txtPrev.Text = "0";
                    TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
                    if (txtReal != null) txtReal.Text = "0";

                }
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Ocultar();
        cod.Value = "0";
    }
    protected void btnAcao_Click(object sender, EventArgs e)
    {
        bool result = false;
        bool erro = false;
        string msg = "";
        t14_resultado t14 = new t14_resultado();
        {
            t14.t03_cd_projeto = pb.cd_projeto();
            t14.ds_resultado = txtds_resultado.Text;
            t14.nm_medida = txtnm_medida.Text;
            t14.nm_resultado = txtnm_resultado.Text;
            t14.nm_unid = txtnm_unid.Text;
            
            if (txtnu_ano.Text != "")
            {
                if (txtnu_ano.Text.Length == 4)
                {
                    t14.nu_ano = Int32.Parse(txtnu_ano.Text);
                }
                else
                {
                    erro = true;
                    msg = pb.Message("Ano inválido", "erro");
                }
            }
            else{t14.nu_ano = 0;}
            if (txtvl_t0.Text != ""){t14.vl_t0 = decimal.Parse(txtvl_t0.Text);}
            else{t14.vl_t0 = 0;}

            t14.fl_acumulado = bool.Parse(rblfl_acumulado.SelectedValue.ToString());
            t14.dt_cadastro = DateTime.Now;
            t14.dt_alterado = DateTime.Now;
            t15_vlresultado t15 = new t15_vlresultado();
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t14.t14_cd_resultado = Int32.Parse(dlResultado.SelectedValue.ToString());
                    result = t14.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    if (result)
                    {
                        //Altera os Valores
                        if (t03.Found)
                        {
                            t15.t14_cd_resultado = Int32.Parse(cod.Value.ToString());
                            t15.Delete();

                            for (int i = t03.dt_inicio.Year; i <= t03.dt_fim.Year; i++)
                            {
                                TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
                                TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
                                if (txtPrev != null)
                                {
                                    if (txtPrev.Text == "") txtPrev.Text = "0";
                                    if (txtReal.Text == "") txtReal.Text = "0";

                                    t15.t14_cd_resultado = Int32.Parse(cod.Value.ToString());
                                    t15.nu_ano = i;
                                    t15.vl_previsto = decimal.Parse(txtPrev.Text);
                                    t15.vl_realizado = decimal.Parse(txtReal.Text);
                                    t15.dt_alterado = DateTime.Now;
                                    t15.dt_cadastro = DateTime.Now;
                                    t15.Save();

                                }
                            }
                        }

                        cod.Value = "0";
                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t14_resultado", "update", t14.t14_cd_resultado.ToString());
                    }
                }
                else
                {
                    result = t14.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    if (result)
                    {
                        if (t03.Found)
                        {
                            t14.RetrieveCod();
                            if (t14.Found)
                            {
                                for (int i = t03.dt_inicio.Year; i <= t03.dt_fim.Year; i++)
                                {
                                    TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
                                    TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
                                    if (txtPrev != null)
                                    {
                                        if (txtPrev.Text == "") txtPrev.Text = "0";
                                        if (txtReal.Text == "") txtReal.Text = "0";
                                        t15.t14_cd_resultado = t14.t14_cd_resultado;
                                        t15.nu_ano = i;
                                        t15.vl_previsto = decimal.Parse(txtPrev.Text);
                                        t15.vl_realizado = decimal.Parse(txtReal.Text);
                                        t15.dt_alterado = DateTime.Now;
                                        t15.dt_cadastro = DateTime.Now;
                                        t15.Save();

                                    }
                                }
                            }
                        }
                        pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t14_resultado", "update", t14.nm_resultado);
                    }

                }
                if (result)
                {
                    Ocultar();
                    GridBind("");
                }
                else
                {
                    msg = pb.Message(pb.msgerro, "erro");
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }
    protected void btnExc_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        t14_resultado t14 = new t14_resultado();
        {
            t14.t14_cd_resultado = Int32.Parse(btn.CommandArgument);
            t14.Delete();
            pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t14_resultado", "delete", btn.CommandArgument);
            GridBind("");
            lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok"); ;
            lblMsg.Visible = true; 
        }
    }

    protected void ValoresBind(bool fl_acumulado, Panel pn, int cd_resultado)
    {
        decimal prev = 0;
        decimal real = 0;
        decimal prevac = 0;
        decimal realac = 0;
        pn.Controls.Clear();
        pn.Controls.Add(pb.GetLiteral("<table cellspacing='0' cellpadding='5' rules='all' border='1' style='color:#333333;border-color:#20669B;border-width:1px;border-style:solid;width:80%;border-collapse:collapse;'>"));
        pn.Controls.Add(pb.GetLiteral("<tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>"));
        pn.Controls.Add(pb.GetLiteral("<td>Ano</td><td>Previsto</td><td>Realizado</td></tr>"));

        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = pb.cd_projeto();
            t03.Retrieve();
            if (t03.Found)
            {
                for (int i = t03.dt_inicio.Year; i <= t03.dt_fim.Year; i++)
                {
                    pn.Controls.Add(pb.GetLiteral("<tr style='background-color:#F1F5F5;text-align:center;'>"));
                    pn.Controls.Add(pb.GetLiteral("<td>"));
                    //Ano
                    pn.Controls.Add(pb.GetLiteral(i.ToString()));
                    pn.Controls.Add(pb.GetLiteral("</td><td>"));

                    t15_vlresultado t15 = new t15_vlresultado();
                    {
                        t15.t14_cd_resultado = cd_resultado;
                        t15.nu_ano = i;
                        t15.Retrieve();
                        //Previsto
                        if (t15.Found)
                        {
                            prev += t15.vl_previsto;
                            prevac = t15.vl_previsto;
                            pn.Controls.Add(pb.GetLiteral(t15.vl_previsto.ToString("N").Replace(",00","")));
                        }
                        pn.Controls.Add(pb.GetLiteral("</td><td>"));
                        //Realizado
                        if (t15.Found)
                        {
                            real += t15.vl_realizado;
                            realac = t15.vl_realizado;
                            pn.Controls.Add(pb.GetLiteral(t15.vl_realizado.ToString("N").Replace(",00", "")));
                        }
                        pn.Controls.Add(pb.GetLiteral("</td></tr>"));

                    }
                }
            }

        }

        pn.Controls.Add(pb.GetLiteral("<tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>"));
        pn.Controls.Add(pb.GetLiteral("<td>Total:"));
        pn.Controls.Add(pb.GetLiteral("</td><td>"));
        if (fl_acumulado)
        {
            //Previsto
            pn.Controls.Add(pb.GetLiteral(prevac.ToString().Replace(",00", "")));
            pn.Controls.Add(pb.GetLiteral("</td><td>"));
            //Realizado
            pn.Controls.Add(pb.GetLiteral(realac.ToString().Replace(",00", "")));
            pn.Controls.Add(pb.GetLiteral("</td>"));
        }
        else
        {
            //Previsto
            pn.Controls.Add(pb.GetLiteral(prev.ToString()));
            pn.Controls.Add(pb.GetLiteral("</td><td>"));
            //Realizado
            pn.Controls.Add(pb.GetLiteral(real.ToString()));
            pn.Controls.Add(pb.GetLiteral("</td>"));
        }
        pn.Controls.Add(pb.GetLiteral("</tr>"));

        pn.Controls.Add(pb.GetLiteral("</table>"));
    }

    public void getFCXMLData(int cd_resultado, int contador, bool fl_acumulado, Panel pn)
    {
        
        StringBuilder sb = new StringBuilder();
        string bg;
        if (contador % 2 == 0)
        {
            bg = "FFFFFF";
        }
        else
        {
            bg = "FFFFFF";
        }

        if (!fl_acumulado)
        {
            sb.Append("<graph bgColor='" + bg + "' showValues='0' decimalPrecision='2' anchorRadius='4' anchorBgAlpha='0' lineThickness='2' numberPrefix='' limitsDecimalPrecision='2' divLineDecimalPrecision='2'><categories>\n");
        }
        else
        {
            sb.Append("<graph bgColor='" + bg + "' caption='Valores Acumulados' showValues='0' decimalPrecision='2' anchorRadius='4' anchorBgAlpha='0' lineThickness='2' numberPrefix='' limitsDecimalPrecision='2' divLineDecimalPrecision='2'><categories>\n");
        }

        t15_vlresultado t15 = new t15_vlresultado();
        {
            t15.t14_cd_resultado = cd_resultado;
            foreach (DataRow dr in t15.List().Tables[0].Rows)
            {
                sb.Append("<category name='" + dr["nu_ano"] + "' />\n");
            }
            sb.Append("</categories>\n");

            sb.Append("<dataset seriesName='Previsto' color='66CC66'>\n");

            foreach (DataRow dr in t15.List().Tables[0].Rows)
            {
                decimal prev = decimal.Parse(dr["vl_previsto"].ToString());
                if (prev > 0)
                {
                    sb.Append("<set value='" + prev.ToString().Replace(",", ".") + "' alpha='100' />\n");
                }
                else
                {
                    sb.Append("<set value='' alpha='100' />\n");
                }

            }
            sb.Append("</dataset>");

            sb.Append("<dataset seriesName='Realizado' color='81BCE9'>\n");
            foreach (DataRow dr in t15.List().Tables[0].Rows)
            {
                decimal real = decimal.Parse(dr["vl_realizado"].ToString());
                if (real > 0)
                {
                    sb.Append("<set value='" + real.ToString().Replace(",", ".") + "' alpha='100' />\n");
                }
                else
                {
                    sb.Append("<set value='' alpha='100' />\n");
                }

            }
        }
        sb.Append("</dataset>\n");
        sb.Append("</graph>\n");
        
        pn.Controls.Clear();
        pn.Controls.Add(pb.GetLiteral("<object id=\"FC2Column\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\""));
		pn.Controls.Add(pb.GetLiteral("height=\"200\" width=\"400\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">"));
		pn.Controls.Add(pb.GetLiteral("<param name=\"Movie\" value=\"Charts/FC_2_3_MSColumn3D.swf\">"));
		pn.Controls.Add(pb.GetLiteral("<param name=\"FlashVars\" value=\"&chartWidth=400&chartHeight=220&dataXML="+ sb.ToString()+"\">"));
		pn.Controls.Add(pb.GetLiteral("<embed src=\"Charts/FC_2_3_MSColumn3D.swf\" flashvars=\"&chartWidth=400&chartHeight=220&dataXML="+ sb.ToString() +"\""));
        pn.Controls.Add(pb.GetLiteral("quality=\"high\" width=\"400\" height=\"300\" name=\"FC2Column\" type=\"application/x-shockwave-flash\" pluginspace=\"http://www.macromedia.com/go/getflashplayer\"> </embed></object>"));
    } 
}

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

public partial class Monitoramento2 : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Buffer = true;
        Response.AddHeader("pragma", "no-cache");
        Response.Expires = 0;
        if (!(IsPostBack))
        {
                ddlBind();
        }
    }

    

    protected void btnFiltro_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder query = new System.Text.StringBuilder();
        string cd_entidade = "0";
        if (pb.fl_admin())
        {
            trParceiro.Visible = true;
            if (ddlt01_cd_entidade.SelectedValue != "")
            {
                cd_entidade = ddlt01_cd_entidade.SelectedValue;
            }
        }
        else
        {
            cd_entidade = pb.cd_entidade().ToString();
            trParceiro.Visible = false;
        }
        if (cd_entidade != "0")
        {
            query.Append("and (t01_cd_entidade=" + cd_entidade + ") ");
        }
        else
        {   //usuário parceiro
            query.Append("and (t01_cd_entidade in (select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")) ");
        }
     
        if (ddlt03_cd_projeto.SelectedValue != "")
        {
            query.Append("and (t03_cd_projeto=" + ddlt03_cd_projeto.SelectedValue + ") ");
        }
        else 
        {
            if (!(pb.fl_estrategico() || pb.fl_adminparceiro() || pb.fl_admin()))
             query.Append(" and (t03_cd_projeto in (select t03_cd_projeto from t03_projeto where t02_cd_usuario='" + pb.cd_usuario() + "')) ");
        }

        if (ddlt04_cd_tipologia.SelectedValue != "")
        {
            query.Append("and (t04_cd_tipologia=" + ddlt04_cd_tipologia.SelectedValue + ") ");
        }
        if (ddlt05_cd_parceiro.SelectedValue != "")
        {
            query.Append("and (t05_cd_parceiro=" + ddlt05_cd_parceiro.SelectedValue + ") ");
	      // query.Append("and (t03_cd_projeto in (select t03_cd_projeto from t20_faseprojeto where t05_cd_parceiro=" + ddlt05_cd_parceiro.SelectedValue + "')) ");		
        }		
        if (ddlt19_cd_fase.SelectedValue != "")
        {
            query.Append("and (t03_cd_projeto in (select t03_cd_projeto from t20_faseprojeto where t19_cd_fase=" + ddlt19_cd_fase.SelectedValue + " and fl_ativa=1)) ");
        }
        //Response.Write(query.ToString());
        t03_projeto t03 = new t03_projeto();
        {
            t03.order = "select * from t03_projeto where (fl_ativa=1) " + query.ToString();
            int i = t03.ListQuery().Tables[0].Rows.Count;
            
            if (i > 0)
            {
                Session["sqlfiltro"] = query.ToString();
                
                //Detalhes do Filtro
                t03.order = "select max(dt_fim) as datafim, min(dt_inicio) as dataini from t03_projeto where (fl_ativa=1) and (dt_alterado is not null) " + query.ToString();
                foreach (DataRow drp in t03.ListQuery().Tables[0].Rows)
                {
                    if (drp["dataini"] != DBNull.Value)
                    {
                        DateTime dti = (DateTime)drp["dataini"];
                        DateTime dtf = (DateTime)drp["datafim"];
                        Session["mondti"] = dti.ToShortDateString();
                        Session["mondtf"] = dtf.ToShortDateString();
                    }
                }
                t01_entidade t01 = new t01_entidade();
                {
                    if (cd_entidade == "0")
                    {
                        t05_parceiro t05 = new t05_parceiro();
                        {
                            t05.t05_cd_parceiro = pb.cd_parceiro();
                            t05.Retrieve();
                            if (t05.Found)
                            {
                                cd_entidade = t05.t01_cd_entidade.ToString();
                            }
                        }
                    }
                    t01.t01_cd_entidade = Int32.Parse(cd_entidade);
                    t01.Retrieve();
                    if (t01.Found)
                    {
                        Session["monparceiro"] = t01.nm_entidade;
                    }
                }
                Session["monquantproj"] = i.ToString();
                Session["monprojeto"] = ddlt03_cd_projeto.SelectedItem.Text;
                Session["montipologia"] = ddlt04_cd_tipologia.SelectedItem.Text;
				Session["monresponsavel"] = ddlt05_cd_parceiro.SelectedItem.Text;
                Session["monfase"] = ddlt19_cd_fase.SelectedItem.Text;
                Response.Redirect("MonPainel2.aspx");
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
            }
        }

    }

    protected void ddlBind()
    {
        if (pb.fl_admin())
        {
            t01_entidade t01 = new t01_entidade();
            {
                t01.fl_ativa = true;
                t01.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where fl_ativa=1) order by nm_entidade";
                ddlt01_cd_entidade.DataSource = t01.List();
                ddlt01_cd_entidade.DataTextField = "nm_entidade";
                ddlt01_cd_entidade.DataValueField = "t01_cd_entidade";
                ddlt01_cd_entidade.DataBind();
                pb.AddEmptyItem(ddlt01_cd_entidade, "Selecione");
            }
            ddlt03_cd_projeto.Enabled = false;
            trParceiro.Visible = true;
        }
        else
        {
            trParceiro.Visible = false;
        }
        t03_projeto t03 = new t03_projeto();
        {
            if (pb.cd_entidade() != 0)
            {
                //se usuário normal
                if (pb.fl_estrategico() || pb.fl_adminparceiro())
                {
                    t03.order = "where t01_cd_entidade=" + pb.cd_entidade() + " and fl_ativa=1 order by nm_projeto";
                }
                else
                {
                    t03.order = "where t02_cd_usuario='" + pb.cd_usuario() + "' ";
                }
            }
            else
            {
                //se usuário parceiro
                t03.order = "where t02_cd_usuario='" + pb.cd_usuario() + "' ";
            }
            ddlt03_cd_projeto.Items.Clear();
            ddlt03_cd_projeto.DataSource = t03.List();
            ddlt03_cd_projeto.DataTextField = "nm_projeto";
            ddlt03_cd_projeto.DataValueField = "t03_cd_projeto";
            ddlt03_cd_projeto.DataBind();
            pb.AddEmptyItem(ddlt03_cd_projeto, "Todos");
        }
        t04_tipologia t04 = new t04_tipologia();
        {
            t04.fl_ativa = true;
            t04.order = "order by nm_tipologia";
            ddlt04_cd_tipologia.DataSource = t04.List();
            ddlt04_cd_tipologia.DataTextField = "nm_tipologia";
            ddlt04_cd_tipologia.DataValueField = "t04_cd_tipologia";
            ddlt04_cd_tipologia.DataBind();
            pb.AddEmptyItem(ddlt04_cd_tipologia, "Todas");
        }
        t05_parceiro t05 = new t05_parceiro();
        {
          //  t05.fl_entidade = true;		
            t05.order = "order by nm_parceiro";
            ddlt05_cd_parceiro.DataSource = t05.List();
            ddlt05_cd_parceiro.DataTextField = "nm_parceiro";
            ddlt05_cd_parceiro.DataValueField = "t05_cd_parceiro";
            ddlt05_cd_parceiro.DataBind();
            pb.AddEmptyItem(ddlt05_cd_parceiro, "Todos");
        }		
        t19_fase t19 = new t19_fase();
        {
            t19.order = "order by nm_fase";
            ddlt19_cd_fase.DataSource = t19.List();
            ddlt19_cd_fase.DataTextField = "nm_fase";
            ddlt19_cd_fase.DataValueField = "t19_cd_fase";
            ddlt19_cd_fase.DataBind();
            pb.AddEmptyItem(ddlt19_cd_fase, "Todas");
        }
    }

    protected void ddlt01_cd_entidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (ddl.SelectedValue == "")
        {
            ddlt03_cd_projeto.Enabled = false;
        }
        else
        {
            ddlt03_cd_projeto.Enabled = true;
            t03_projeto t03 = new t03_projeto();
            {
                t03.order = "where t01_cd_entidade=" + ddl.SelectedValue + " order by nm_projeto";
                ddlt03_cd_projeto.Items.Clear();
                ddlt03_cd_projeto.DataSource = t03.List();
                ddlt03_cd_projeto.DataTextField = "nm_projeto";
                ddlt03_cd_projeto.DataValueField = "t03_cd_projeto";
                ddlt03_cd_projeto.DataBind();
                pb.AddEmptyItem(ddlt03_cd_projeto, "Todos");
            }
        }
        

    }
}

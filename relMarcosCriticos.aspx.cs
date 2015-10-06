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

public partial class relMarcosCriticos : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        if (!IsPostBack)
        {
            ViewState["sql"] = "";
            if (Request["page"] != null)
            {
               string page = Request["page"].ToString();
                if (page == "1")
                {
                    lblHeader.Text = "Marcos críticos não superados";
                    ViewState["sql"] = "and (dt_realizada is null) " ;
                    GridView1.Columns[5].Visible = false;
                }
                else if (page == "2")
                {
                    lblHeader.Text = "Marcos críticos revisados";
                    ViewState["sql"] = "and (dt_original <> dt_prevista) ";
                }
                else
                {
                    Response.Redirect("Default2.aspx");
                }
                FormBind();
                ViewState["sentido"] = "DESC";
                ViewState["campo"] = "dt_prevista";
                if (!(pb.fl_admin()))
                {
                    GridBind();
                    Panel1.Visible = false;
                    GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
                }
            }
            else
            {
                Response.Redirect("Default2.aspx");
            }
        }
        
    }

    private void FormBind()
    {
        t01_entidade t01 = new t01_entidade();
        {
            t01.fl_ativa = true;
            t01.order = " and t01_cd_entidade in (select t01_cd_entidade from t03_projeto where fl_ativa=1)";
            DropDownList ddl = ddlt01_cd_entidade;
            ddl.DataSource = t01.List();
            ddl.DataTextField = "nm_entidade";
            ddl.DataValueField = "t01_cd_entidade";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    }

    private void GridBind()
    {
        t09_marco t09 = new t09_marco();
        {
            string sqlfixo="";
            //if ((cd_entidade == "0") && (!pb.fl_estrategico()))
            //{
            //   sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=" + cd_entidade + "))";
            //}
            //else 
            //{
            //    sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t02_cd_usuario='" + pb.cd_usuario() + "'))";
            //}

            if (pb.fl_admin())
            {
                sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=" + ddlt01_cd_entidade.SelectedValue + "))";
            }
            else 
            {
                if ((pb.cd_parceiro() != 0) && (!pb.fl_estrategico())) //parceiro
                {
                    sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t02_cd_usuario='" + pb.cd_usuario() + "'))";
                }
                else //administrador parceiro
                {
                    if (pb.cd_parceiro() != 0)
                    {
                        sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade in (select t01_cd_entidade from t05_parceiro where t05_cd_parceiro=" + pb.cd_parceiro() + ")))";
                    }
                    else
                    {
                        sqlfixo = " and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=" + pb.cd_entidade() + "))";
                    }
                }
            }

            t09.order = ViewState["sql"].ToString() + sqlfixo + " order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
            GridView1.DataSource = t09.ListStatus();
            GridView1.DataBind();
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection sd;
        if (ViewState["sentido"].ToString() == "ASC")
        {
            ViewState["sentido"] = "DESC";
            sd = SortDirection.Descending;
        }
        else
        {
            ViewState["sentido"] = "ASC";
            sd = SortDirection.Ascending;
        }


        pb.AppendSortOrderImageToGridHeader(sd, e.SortExpression, this.GridView1);
        ViewState["campo"] = e.SortExpression;
        
        GridBind();
    }

    protected void btnFiltro_Click(object sender, EventArgs e)
    {
        GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
    }
}

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

public partial class frmTipologia : System.Web.UI.Page
{
    string cd_usuario;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cd_usuario"] != null)
        {
            cd_usuario = Session["cd_usuario"].ToString();
        }

        if (!(IsPostBack))
        {
            ViewState["Sentido"] = "DESC";
            GridBind("order by nm_tipologia");
            GridView1.Sort("nm_tipologia", SortDirection.Descending);
            lblHeader.Text = "Tipologia";
            
        }
    }

    private void GridBind(String order)
    {
        t04_tipologia t04 = new t04_tipologia();
        {
            t04.order = order;
            t04.fl_ativa = true;
            GridView1.DataSource = t04.List();
            GridView1.DataBind();
            
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t04_tipologia t04 = new t04_tipologia();
        {
            try
            {
                t04.nm_tipologia = txtnm_tipologia.Text;
                t04.fl_ativa = true;
                t04.dt_cadastro = DateTime.Now;
                t04.dt_alterado = DateTime.Now;
                result = t04.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(cd_usuario, 0, "", "t04_tipologia", "insert", t04.nm_tipologia);
                txtnm_tipologia.Text = "";
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("order by nm_tipologia");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridBind("order by nm_tipologia");
        GridView1.Rows[e.NewEditIndex].FindControl("txtnm_tipologia").Focus();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridBind("order by nm_tipologia");
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = GridView1.Rows[e.RowIndex];
        int cod = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        TextBox txt1 = (TextBox)row.FindControl("txtnm_tipologia");

        bool result;
        string msg;
        t04_tipologia t04 = new t04_tipologia();
        {
            try
            {
                t04.t04_cd_tipologia = cod;
                t04.nm_tipologia = txt1.Text;
                t04.dt_alterado = DateTime.Now;
                pb.saveLog(cd_usuario, 0, "", "t04_tipologia", "update" , t04.t04_cd_tipologia.ToString());
                result = t04.Update();
                msg = pb.Message("Alteração realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;

            GridView1.EditIndex = -1;
            GridBind("order by nm_tipologia");
        }
    }
    protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t04_tipologia t04 = new t04_tipologia();
        {
            try
            {
                t04.t04_cd_tipologia = Int32.Parse(btn.CommandArgument);
                result = t04.Delete();
                pb.saveLog(cd_usuario, 0, "", "t04_tipologia", "delete", t04.t04_cd_tipologia.ToString());
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("order by nm_tipologia");
        }

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection sd;
        if (ViewState["Sentido"].ToString() == "ASC")
        {
            ViewState["Sentido"] = "DESC";
            sd = SortDirection.Descending;
        }
        else
        {
            ViewState["Sentido"] = "ASC";
            sd = SortDirection.Ascending;
        }
        pb.AppendSortOrderImageToGridHeader(sd, e.SortExpression, this.GridView1);
        GridBind("order by " + e.SortExpression + " " + ViewState["Sentido"]);
    }

}

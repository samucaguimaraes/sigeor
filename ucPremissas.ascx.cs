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

public partial class ucPremissas : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "nm_premissa";
            GridBind();
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
            lblHeader.Text = "Premissas";
        }

        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
        }
    }

    private void GridBind()
    {
        t12_premissa t12 = new t12_premissa();
        {
            t12.t03_cd_projeto = pb.cd_projeto();
            t12.order = "order by " + ViewState["campo"].ToString() +" "+ ViewState["sentido"].ToString();
            t12.fl_ativa = true;
            GridView1.DataSource = t12.List();
            GridView1.DataBind();

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t12_premissa t12 = new t12_premissa();
        {
            try
            {
                t12.t03_cd_projeto = pb.cd_projeto();
                t12.nm_premissa = txtnm_premissa.Text;
                t12.fl_ativa = true;
                t12.dt_cadastro = DateTime.Now;
                t12.dt_alterado = DateTime.Now;
                result = t12.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t12_premissa", "insert", t12.nm_premissa);
                txtnm_premissa.Text = "";
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridBind();
        GridView1.Rows[e.NewEditIndex].FindControl("txtnm_premissa").Focus();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = GridView1.Rows[e.RowIndex];
        int cod = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        TextBox txt1 = (TextBox)row.FindControl("txtnm_premissa");

        bool result;
        string msg;
        t12_premissa t12 = new t12_premissa();
        {
            try
            {
                t12.t12_cd_premissa = cod;
                t12.nm_premissa = txt1.Text;
                t12.dt_alterado = DateTime.Now;
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t12_premissa", "update", t12.t12_cd_premissa.ToString());
                result = t12.Update();
                msg = pb.Message("Alteração realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;

            GridView1.EditIndex = -1;
            GridBind();
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
        t12_premissa t12 = new t12_premissa();
        {
            try
            {
                t12.t12_cd_premissa = Int32.Parse(btn.CommandArgument);
                result = t12.Delete();
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t12_premissa", "delete", t12.t12_cd_premissa.ToString());
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind();
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

}


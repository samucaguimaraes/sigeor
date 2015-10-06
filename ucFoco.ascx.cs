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

public partial class ucFoco : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            ViewState["sentido"] = "DESC";
            ViewState["campo"] = "nm_foco";
            GridBind();
            GridView1.Sort(ViewState["campo"].ToString(), SortDirection.Descending);
            if (pb.gestaoInterna())
            {
                lblHeader.Text = "Demanda";
                lblnovo.Text = "Nova Demanda";
                GridView1.Columns[2].HeaderText = "Demanda";
            }
            else
            {
                lblnovo.Text = "Novo Foco estratégico";
                lblHeader.Text = "Foco estratégico";
                GridView1.Columns[2].HeaderText = "Foco estratégico";
            }
            
        }

        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
        }

        pb.MaxLength(txtnm_foco);
    }

    private void GridBind()
    {
        t13_foco t13 = new t13_foco();
        {
            t13.t03_cd_projeto = pb.cd_projeto();
            t13.order = "order by " + ViewState["campo"].ToString() + " " + ViewState["sentido"].ToString();
            t13.fl_ativa = true;
            GridView1.DataSource = t13.List();
            GridView1.DataBind();

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t13_foco t13 = new t13_foco();
        {
            try
            {
                t13.nm_foco = txtnm_foco.Text;
                t13.t03_cd_projeto = pb.cd_projeto();
                t13.fl_ativa = true;
                t13.dt_cadastro = DateTime.Now;
                t13.dt_alterado = DateTime.Now;
                result = t13.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t13_foco", "insert", t13.nm_foco);
                txtnm_foco.Text = "";
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
        pb.MaxLength((TextBox)GridView1.Rows[e.NewEditIndex].FindControl("txtnm_foco"));
        GridView1.Rows[e.NewEditIndex].FindControl("txtnm_foco").Focus();
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
        TextBox txt1 = (TextBox)row.FindControl("txtnm_foco");

        bool result;
        string msg;
        t13_foco t13 = new t13_foco();
        {
            try
            {
                t13.t13_cd_foco = cod;
                t13.nm_foco = txt1.Text;
                t13.dt_alterado = DateTime.Now;
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t13_foco", "update", t13.t13_cd_foco.ToString());
                result = t13.Update();
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
        t13_foco t13 = new t13_foco();
        {
            try
            {
                t13.t13_cd_foco = Int32.Parse(btn.CommandArgument);
                result = t13.Delete();
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t13_foco", "delete", t13.t13_cd_foco.ToString());
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


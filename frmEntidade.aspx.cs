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

public partial class frmEntidade : System.Web.UI.Page
{
    string cd_usuario;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        if (Session["cd_usuario"] != null)
        {
            cd_usuario = Session["cd_usuario"].ToString();
        }

        if (!(IsPostBack))
        {
            ViewState["Sentido"] = "DESC";
            GridBind("order by nm_entidade");
            GridView1.Sort("nm_entidade", SortDirection.Descending);
            lblHeader.Text = "Parceiro";
            ddlBind(ddlnm_uf);
        }
    }

    private void GridBind(String order)
    {
        t01_entidade t01 = new t01_entidade();
        {
            t01.order = order;
            t01.fl_ativa = true;
            GridView1.DataSource = t01.List();
            GridView1.DataBind();
            ddlBind(ddlnm_uf);
        }

    }
    private void ddlBind(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Selecione", ""));
        ddl.Items.Add(new ListItem("Nacional", "NA"));
        ddl.Items.Add(new ListItem("AC", "AC"));
        ddl.Items.Add(new ListItem("AL", "AL"));
        ddl.Items.Add(new ListItem("AM", "AM"));
        ddl.Items.Add(new ListItem("AP", "AP"));
        ddl.Items.Add(new ListItem("BA", "BA"));
        ddl.Items.Add(new ListItem("BR", "BR"));
        ddl.Items.Add(new ListItem("CE", "CE"));
        ddl.Items.Add(new ListItem("DF", "DF"));
        ddl.Items.Add(new ListItem("ES", "ES"));
        ddl.Items.Add(new ListItem("GO", "GO"));
        ddl.Items.Add(new ListItem("MA", "MA"));
        ddl.Items.Add(new ListItem("MG", "MG"));
        ddl.Items.Add(new ListItem("MS", "MS"));
        ddl.Items.Add(new ListItem("MT", "MT"));
        ddl.Items.Add(new ListItem("PA", "PA"));
        ddl.Items.Add(new ListItem("PB", "PB"));
        ddl.Items.Add(new ListItem("PE", "PE"));
        ddl.Items.Add(new ListItem("PI", "PI"));
        ddl.Items.Add(new ListItem("PR", "PR"));
        ddl.Items.Add(new ListItem("RJ", "RJ"));
        ddl.Items.Add(new ListItem("RN", "RN"));
        ddl.Items.Add(new ListItem("RO", "RO"));
        ddl.Items.Add(new ListItem("RR", "RR"));
        ddl.Items.Add(new ListItem("RS", "RS"));
        ddl.Items.Add(new ListItem("SC", "SC"));
        ddl.Items.Add(new ListItem("SE", "SE"));
        ddl.Items.Add(new ListItem("SP", "SP"));
        ddl.Items.Add(new ListItem("TO", "TO"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t01_entidade t01 = new t01_entidade();
        {
            try
            {
                t01.nm_entidade = txtnm_entidade.Text;
                t01.nm_uf = ddlnm_uf.SelectedValue;
                t01.nm_arquivo = "";
                t01.nm_cnpj = "";
                t01.fl_ativa = true;
                t01.dt_cadastro = DateTime.Now;
                t01.dt_alterado = DateTime.Now;
                result = t01.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(cd_usuario, 0, "", "t01_entidade", "insert", t01.nm_entidade);
                txtnm_entidade.Text = "";

                if (result)
                {
                    t01.RetrieveCod();
                    if (t01.Found)
                    {
                        t05_parceiro t05 = new t05_parceiro();
                        {
                            t05.nm_parceiro = t01.nm_entidade;
                            t05.t01_cd_entidade = t01.t01_cd_entidade;
                            t05.fl_entidade = true;
                            t05.nm_arquivo = "";
                            t05.nm_cnpj = "";
                            t05.dt_cadastro = DateTime.Now;
                            t05.dt_alterado = DateTime.Now;
                            t05.Save();
                            pb.saveLog(cd_usuario, 0, "", "t05_parceiro", "insert", t05.nm_parceiro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = pb.Message(pb.msgerro + " <small>Descrição: "+ ex.Message + "</small>", "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("order by nm_entidade");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridBind("order by nm_entidade");
        GridView1.Rows[e.NewEditIndex].FindControl("txtnm_entidade").Focus();
        
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridBind("order by nm_entidade");
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = GridView1.Rows[e.RowIndex];
        int cod = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        TextBox txt1 = (TextBox)row.FindControl("txtnm_entidade");
        DropDownList ddl = (DropDownList)row.FindControl("ddlnm_uf");
        bool result;
        string msg;
        t01_entidade t01 = new t01_entidade();
        {
            try
            {
                t01.t01_cd_entidade = cod;
                t01.nm_entidade = txt1.Text;
                t01.nm_arquivo = "";
                t01.nm_cnpj = "";
                t01.nm_uf = ddl.SelectedValue;
                t01.dt_alterado = DateTime.Now;
                pb.saveLog(cd_usuario, 0, "", "t01_entidade", "update" , t01.t01_cd_entidade.ToString());
                result = t01.Update();
                msg = pb.Message("Alteração realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;

            GridView1.EditIndex = -1;
            GridBind("order by nm_entidade");
        }
    }
    protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = ((DataRowView)e.Row.DataItem);
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlnm_uf");
            if (ddl != null)
            {
                ddlBind(ddl);
                ddl.SelectedValue = drv["nm_uf"].ToString().Trim();
            }
            if ((string)drv["nm_uf"] == "NA")
            {
                Label lbl = (Label)e.Row.FindControl("lblnm_uf");
                if (lbl != null)
                {
                    lbl.Text = "Nacional";
                }
            }

        }
        
    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t01_entidade t01 = new t01_entidade();
        {
            try
            {
                t01.t01_cd_entidade = Int32.Parse(btn.CommandArgument);
                result = t01.Delete();
                pb.saveLog(cd_usuario, 0, "", "t01_entidade", "delete", t01.t01_cd_entidade.ToString());
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("order by nm_entidade");
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

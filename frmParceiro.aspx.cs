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

public partial class frmParceiro : System.Web.UI.Page
{
    string cd_usuario;
    int cd_entidade;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        if (Session["cd_usuario"] != null)
        {
            cd_usuario = Session["cd_usuario"].ToString();
        }
        if (Session["cd_entidade"] != null)
        {
            cd_entidade = Int32.Parse(Session["cd_entidade"].ToString());
        }

        if (!(IsPostBack))
        {
            ViewState["Sentido"] = "DESC";
            GridBind("order by nm_parceiro");
            GridView1.Sort("nm_parceiro", SortDirection.Descending);
            lblHeader.Text = "Parceiros";

        }
    }

    private void GridBind(String order)
    {
        t05_parceiro t05 = new t05_parceiro();
        {
            t05.t01_cd_entidade = cd_entidade;
            t05.order = order;
            GridView1.DataSource = t05.List();
            GridView1.DataBind();

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result=false;
        string msg;
        t05_parceiro t05 = new t05_parceiro();
        {
            try
            {
                t05.nm_parceiro = txtnm_parceiro.Text;
                t05.t01_cd_entidade = cd_entidade;
                t05.nm_arquivo = "";
                t05.nm_cnpj = txtnm_cnpj.Text;
                t05.dt_cadastro = DateTime.Now;
                t05.dt_alterado = DateTime.Now;
                result = t05.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            if (result)
            {
                pb.saveLog(cd_usuario, 0, "", "t05_parceiro", "insert", t05.nm_parceiro);
                txtnm_parceiro.Text = "";
                txtnm_cnpj.Text = "";
                GridBind("order by nm_parceiro");
            }
            else
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridBind("order by nm_parceiro");
        GridView1.Rows[e.NewEditIndex].FindControl("txtnm_parceiro").Focus();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridBind("order by nm_parceiro");
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = GridView1.Rows[e.RowIndex];
        int cod = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        TextBox txt1 = (TextBox)row.FindControl("txtnm_parceiro");
        TextBox txt2 = (TextBox)row.FindControl("txtnm_cnpj");
        RadioButtonList rbl = (RadioButtonList)row.FindControl("rblFoto");
        FileUpload fu = (FileUpload)row.FindControl("FileUpload1");

        bool result;
        string msg;
        t05_parceiro t05 = new t05_parceiro();
        {
            try
            {
                t05.t05_cd_parceiro = cod;
                t05.nm_parceiro = txt1.Text;
                t05.nm_cnpj = txt2.Text;
                t05.dt_alterado = DateTime.Now;
                if (rbl.SelectedValue == "1")
                {
                    uploadArquivo up = new uploadArquivo();
                    {
                        up.pasta = "Documentos";
                        up.nomeinicial = "logo_";
                        up.fu = fu;
                        up.Save();
                        t05.order = ", nm_arquivo='"+ up.nomearquivo +"'";
                    }
                    
                }
                pb.saveLog(cd_usuario, 0, "", "t05_parceiro", "update", t05.t05_cd_parceiro.ToString());
                result = t05.Update();
                msg = pb.Message("Alteração realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;

            GridView1.EditIndex = -1;
            GridBind("order by nm_parceiro");
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
            Image img = (Image)e.Row.Cells[4].FindControl("imgArquivo");
            if (img != null)
            {
                if (drv["nm_arquivo"].ToString() != "")
                {
                    img.ImageUrl = "Thumb.aspx?file=documentos/" + drv["nm_arquivo"];
                    img.ToolTip = drv["nm_parceiro"].ToString();
                }
                else
                {
                    img.ImageUrl = "Thumb.aspx?file=images/foto_indisponivel.jpg";
                    img.ToolTip = "Foto indisponível";
                }
            }
        }
    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t05_parceiro t05 = new t05_parceiro();
        {
            try
            {
                t05.t05_cd_parceiro = Int32.Parse(btn.CommandArgument);
                result = t05.Delete();
                pb.saveLog(cd_usuario, 0, "", "t05_parceiro", "delete", t05.t05_cd_parceiro.ToString());
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("order by nm_parceiro");
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

    protected void rblFoto_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rbl = (RadioButtonList)sender;
        FileUpload fu = (FileUpload)rbl.Parent.FindControl("FileUpload1");
        if (rbl.SelectedValue == "1")
        {
            fu.Visible = true;
        }
        else
        {
            fu.Visible = false;
        }
    }

}

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

public partial class ucParceiro : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            GridBind();
            lblHeader.Text = "Parceiros";
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
    private void GridBind()
    {
        t06_parceiroprojeto t06 = new t06_parceiroprojeto();
        {
            t06.t03_cd_projeto = pb.cd_projeto();
            DataList1.DataSource = t06.List();
            DataList1.DataBind();
        }

        t05_parceiro t05 = new t05_parceiro();
        {
            int cd_entidade = pb.cd_entidade();
            if (cd_entidade == 0)
            {
                    t05.t05_cd_parceiro = pb.cd_parceiro();
                    t05.Retrieve();
                    if (t05.Found)
                    {
                        cd_entidade = t05.t01_cd_entidade;
                    }
            }
            t05.t01_cd_entidade = cd_entidade;
            t05.order = "and t05_cd_parceiro not in (select t05_cd_parceiro from t06_parceiroprojeto where t03_cd_projeto="+ pb.cd_projeto().ToString() +")";
            DropDownList ddl = ddlt05_cd_parceiro;
            ddl.DataSource = t05.List();
            ddl.DataTextField = "nm_parceiro";
            ddl.DataValueField = "t05_cd_parceiro";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t06_parceiroprojeto t06 = new t06_parceiroprojeto();
        {
            try
            {
                t06.t03_cd_projeto = pb.cd_projeto();
                t06.t05_cd_parceiro = Int32.Parse(ddlt05_cd_parceiro.SelectedValue);
                t06.dt_cadastro = DateTime.Now;
                t06.dt_alterado = DateTime.Now;
                result = t06.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t06_parceiroprojeto", "insert", "parceiro:"+ t06.t05_cd_parceiro.ToString());
                ddlt05_cd_parceiro.ClearSelection();
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

    protected void DataList1_ItemDataBound(Object sender,  DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image img = (Image)e.Item.FindControl("imgArquivo");
            if (img != null)
            {
                if (DataBinder.Eval(e.Item.DataItem,"nm_arquivo").ToString().Length > 1)
                {
                    img.ImageUrl = "Thumb.aspx?file=documentos/" + DataBinder.Eval(e.Item.DataItem, "nm_arquivo").ToString();
                    img.ToolTip = DataBinder.Eval(e.Item.DataItem, "nm_parceiro").ToString();
                }
                else
                {
                    img.ImageUrl = "Thumb.aspx?file=images/foto_indisponivel.jpg";
                    img.ToolTip = "Foto indisponível";
                }
            }
            Label lbl = (Label)e.Item.FindControl("lblnm_parceiro");
            if (lbl != null) lbl.Text = DataBinder.Eval(e.Item.DataItem, "nm_parceiro").ToString();
        }
    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t06_parceiroprojeto t06 = new t06_parceiroprojeto();
        {
            try
            {
                t06.t06_cd_parceiroprojeto = Int32.Parse(btn.CommandArgument);
                result = t06.Delete();
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t06_parceiroprojeto", "delete", t06.t06_cd_parceiroprojeto.ToString());
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


}


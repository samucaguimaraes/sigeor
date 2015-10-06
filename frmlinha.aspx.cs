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

public partial class frmlinha : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = "Linha Decisória";
        this.lblHeader.Text = "Selecione os usuários que irão receber e-mail quando uma restricão for cadastrada.";
        if (!IsPostBack)
        {
            ddlUsuarioBind(ddlUsuario);
            GridBind();
        }
        //Response.Write(pb.cd_entidade());
    }


    private void GridBind()
    {
        t02_usuario t02 = new t02_usuario();
        {
            t02.t01_cd_entidade = pb.cd_entidade();
            GridView1.DataSource = t02.ListLinha();
            GridView1.DataBind();
        }
    }

    public void ddl(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
    }

    private void ddlUsuarioBind(DropDownList ddl)
    {
        t02_usuario t02 = new t02_usuario();
        {
            t02.fl_ativa = true;
            t02.order = " and t02.t05_cd_parceiro in (select t05_cd_parceiro from t05_parceiro where t01_cd_entidade = " + pb.cd_entidade() + ") and t02.t02_cd_usuario not in (select t02_cd_usuario from t32_usuariolinha where t01_cd_entidade =" + pb.cd_entidade() + ")";
            ddl.DataSource = t02.List();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    }



    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        string msg = "";
        msg = pb.Message("Cadastro realizado com sucesso!", "ok");
        t32_usuariolinha T32 = new t32_usuariolinha();
        {
            T32.t01_cd_entidade = pb.cd_entidade();
            T32.t02_cd_usuario = this.ddlUsuario.SelectedValue;
            T32.Save(); 
        }

        GridBind();
        ddlUsuarioBind(ddlUsuario);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        t32_usuariolinha t32 = new t32_usuariolinha();
        {
            t32.t02_cd_usuario = btn.CommandArgument.ToString();
            t32.t01_cd_entidade = pb.cd_entidade();
            t32.Delete();
            //Response.Write(btn.CommandArgument.ToString());
        }
        GridBind();
        ddlUsuarioBind(ddlUsuario);
    }
}

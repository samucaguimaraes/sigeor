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

public partial class ucColaborador : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            GridBind();
            lblHeader.Text = "Colaboradores";
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
        t17_colaborador t17 = new t17_colaborador();
        {
            t17.t03_cd_projeto = pb.cd_projeto();
            GridView1.DataSource = t17.List();
            GridView1.DataBind();
        }

        t02_usuario t02 = new t02_usuario();
        {

            t02.order = "and t02_cd_usuario not in (select t02_cd_usuario from t17_colaborador where t03_cd_projeto="+ pb.cd_projeto() +") order by nm_nome";
            int cd_entidade = pb.cd_entidade();
            if (cd_entidade == 0)
            {
                t05_parceiro t05 = new t05_parceiro();
                {
                    t05.t05_cd_parceiro = pb.cd_parceiro();
                    t05.Retrieve();
                    if (t05.Found)
                    {
                        cd_entidade = t05.t01_cd_entidade;
                    }
                }
            }
            t02.t01_cd_entidade = cd_entidade;
            t02.fl_ativa = true;
            DropDownList ddl = ddlt02_cd_usuario;
            ddl.DataSource = t02.ListComboProjeto();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");

            //Response.Write(t02.order);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result;
        string msg;
        t17_colaborador t17 = new t17_colaborador();
        {
            try
            {
                t17.t03_cd_projeto = pb.cd_projeto();
                t17.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
                t17.dt_cadastro = DateTime.Now;
                t17.dt_alterado = DateTime.Now;
                result = t17.Save();
                msg = pb.Message("Inclusão realizada com sucesso", "ok");
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t17_colaborador", "insert", t17.t02_cd_usuario);
                ddlt02_cd_usuario.ClearSelection();
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
   
    protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
         GridView gv = (GridView)sender;
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             DataRowView drv = ((DataRowView)e.Row.DataItem);
             t01_entidade t01 = new t01_entidade();
             {
                 t01.t01_cd_entidade = (int)drv["t01_cd_entidade"];
                 t01.Retrieve();
                 if (!t01.Found)
                 {
                     t05_parceiro t05 = new t05_parceiro();
                     {
                         t05.t05_cd_parceiro = (int)drv["t05_cd_parceiro"];
                         t05.Retrieve();
                         if (t05.Found)
                         {
                             t01.t01_cd_entidade = t05.t01_cd_entidade;
                             t01.Retrieve();
                             if (t01.Found)
                             {
                                 e.Row.Cells[2].Controls.Add(pb.GetLiteral(t01.nm_entidade + "\\" + t05.nm_parceiro));
                             }
                         }
                     }
                 }
             }
         }
    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t17_colaborador t17 = new t17_colaborador();
        {
            try
            {
                t17.t17_cd_colaborador = Int32.Parse(btn.CommandArgument);
                result = t17.Delete();
                pb.saveLog(pb.cd_usuario(), pb.cd_projeto(), "", "t17_colaborador", "delete", t17.t17_cd_colaborador.ToString());
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


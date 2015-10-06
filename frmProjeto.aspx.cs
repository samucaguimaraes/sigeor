using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class frmProjeto : System.Web.UI.Page
{
    string cd_usuario;
    int cd_entidade;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
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
        GridBind("order by nm_projeto");
        if (!IsPostBack)
        {
            lblTitle.Text = "Eixos";
            PanelForm.Visible = false;
            cod.Value = "0";
        }
    } 
    private void Exibir()
    {
        this.PanelForm.Visible = true;
        this.PanelGrid.Visible = false;
        this.PanelAdd.Visible = false;
        txtnm_projeto.Text = "";
        ddlt02_cd_usuario.ClearSelection();
        ddlt02_cd_usuario_monitoramento.ClearSelection();
        ddlt04_cd_tipologia.ClearSelection();
    } 
    private void Ocultar() 
    { 
        this.PanelForm.Visible = false; 
        this.PanelGrid.Visible = true;
        this.PanelAdd.Visible = true;
        txtnm_projeto.Text = "";
        ddlt02_cd_usuario.ClearSelection();
        ddlt02_cd_usuario_monitoramento.ClearSelection();
        ddlt04_cd_tipologia.ClearSelection();
        GridBind("order by nm_projeto");
    }

    private void GridBind(string order)
    {
        string colspan = "7";
        PanelGrid.Controls.Clear();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table  cellspacing=\"2\" cellpadding=\"4\" class=\"tblist\">");

        t03_projeto t03 = new t03_projeto();
        {
            t03.t01_cd_entidade = cd_entidade; //somente a entidade do parceiro
            foreach (DataRow dre in t03.ListEntidadeAdm().Tables[0].Rows)
            {
                sb.Append("<tr class=\"hr_white\">");
                sb.Append("<td colspan='" + colspan + "'>" + dre["nm_entidade"] + "</td>");
                sb.Append("</tr>");

                t03.t01_cd_entidade = Int32.Parse(dre["t01_cd_entidade"].ToString());
                foreach (DataRow drt in t03.ListTipologia().Tables[0].Rows)
                {
                    sb.Append("<tr  class=\"hr_yellow\">");
                    sb.Append("<td colspan='" + colspan + "'>" + drt["nm_tipologia"] + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr class=\"hr_orange\">");
                    sb.Append("<td style='width:1%'>&nbsp</td>");
                    sb.Append("<td style='width:1%'>&nbsp</td>");
                    sb.Append("<td>Programas</td>");
                    sb.Append("<td>Fase</td>");
                    sb.Append("<td>Atualizado</td>");
                    sb.Append("<td>Restri&ccedil;&atilde;o</td>");
                    sb.Append("<td style='width:120px'>Evolu&ccedil;&atilde;o</td>");
                    sb.Append("</tr>");

                    t03.order = "where fl_ativa=1"+
                        " and t01_cd_entidade=" + dre["t01_cd_entidade"].ToString() + 
                        " and t04_cd_tipologia=" + drt["t04_cd_tipologia"].ToString() +
                        " order by nm_projeto";

                    foreach (DataRow drp in t03.List().Tables[0].Rows)
                    {
                        string nm_projeto = drp["nm_projeto"].ToString();
                        string nm_fase = "";
                        string data = "-";
                        t19_fase t19 = new t19_fase();
                        {
                            t19.t19_cd_fase = Int32.Parse(drp["t03_cd_projeto"].ToString()); //usando o t19_cd_fase para armazenar t03_cd_projeto;
                            t19.RetrieveFaseProjeto();
                            if (t19.Found)
                            {
                                nm_fase = t19.nm_fase;
                            }
                        }

                        sb.Append("<tr>");
                        PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));

                        PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                        ImageButton btne = new ImageButton();
                        btne.ID = "ImageButton1_" + drp["t03_cd_projeto"].ToString();
                        btne.CommandArgument = drp["t03_cd_projeto"].ToString();
                        btne.ImageUrl = "~/images/ico_exc.gif";
                        btne.ToolTip = "Excluir";
                        btne.OnClientClick = "javascript:return confirm('Tem certeza que deseja excluir?')";
                        btne.Click += new ImageClickEventHandler(Delete_Click);
                        PanelGrid.Controls.Add(btne); //Adiciona o botão de exclusão

                        PanelGrid.Controls.Add(pb.GetLiteral("</td><td>"));

                        btne = new ImageButton();
                        btne.ID = "ImageButton2_" + drp["t03_cd_projeto"].ToString();
                        btne.CommandArgument = drp["t03_cd_projeto"].ToString();
                        btne.ImageUrl = "~/images/ico_edit.gif";
                        btne.ToolTip = "Editar";
                        btne.Click += new ImageClickEventHandler(Edit_Click);
                        PanelGrid.Controls.Add(btne); //Adiciona o botão de edição

                        PanelGrid.Controls.Add(pb.GetLiteral("</td>"));


                        PanelGrid.Controls.Add(pb.GetLiteral("<td>"));
                        LinkButton link = new LinkButton();
                        {
                            link.ID = "link" + drp["t03_cd_projeto"].ToString();
                            link.CommandArgument = drp["t03_cd_projeto"].ToString();
                            link.Text = nm_projeto;
                            link.Click +=new EventHandler(link_Click);
                        }
                        PanelGrid.Controls.Add(link);
                        PanelGrid.Controls.Add(pb.GetLiteral("</td>"));//Projeto

                        sb = new StringBuilder();
                        sb.Append("<td>" + nm_fase + "</td>"); //Fase
                        sb.Append("<td>" + data + "</td>"); //Atualizado
                        sb = new StringBuilder();
                        sb.Append("<td>" + nm_fase + "</td>"); //Fase
                        sb.Append("<td style='text-align:center'>" + data + "</td>"); //Atualizado
                        t07_restricao t07 = new t07_restricao();
                        {
                            t07.t03_cd_projeto = Int32.Parse(drp["t03_cd_projeto"].ToString());
                            if (t07.List().Tables[0].Rows.Count == 0)
                            {
                                sb.Append("<td>&nbsp;</td>"); //Restrição
                            }
                            else
                            {
                                sb.Append("<td style='text-align:center'><b>R</b></td>"); //Restrição
                            }
                        }

                        sb.Append("<td>" + pb.Status(Int32.Parse(drp["t03_cd_projeto"].ToString())) + "</td>"); //Evolução
                        sb.Append("</tr>");
                    }
                }
            }
            sb.Append("</table>");
        }
        PanelGrid.Controls.Add(pb.GetLiteral(sb.ToString()));
    }


    private void Retrieve()
    {
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = Int32.Parse(cod.Value);
            t03.Retrieve();
            if (t03.Found)
            {
                ListItem li;
                txtnm_projeto.Text = t03.nm_projeto;
                li = ddlt02_cd_usuario.Items.FindByValue(t03.t02_cd_usuario);
                if (li != null) li.Selected = true;

                li = ddlt02_cd_usuario_monitoramento.Items.FindByValue(t03.t02_cd_usuario_monitoramento);
                if (li != null) li.Selected = true;

                li = ddlt04_cd_tipologia.Items.FindByValue(t03.t04_cd_tipologia.ToString());
                if (li != null) li.Selected = true;
            }

        }

    } 

    protected void btnAcao_Click(object sender, System.EventArgs e) 
    {
        t03_projeto t03 = new t03_projeto();
        bool result=false;
        bool erro=false;
        string msg=""; 
        {
            t03.nm_projeto = txtnm_projeto.Text;
            t03.t01_cd_entidade = cd_entidade;
            t03.t04_cd_tipologia = Int32.Parse(ddlt04_cd_tipologia.SelectedValue);
            t03.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
            t03.t02_cd_usuario_monitoramento = ddlt02_cd_usuario_monitoramento.SelectedValue;
            t03.dt_cadastro = DateTime.Now;
            t03.dt_alterado = DateTime.Now;
            t03.fl_ativa = true;
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t03.t03_cd_projeto = Int32.Parse(cod.Value);
                    result = t03.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(cd_usuario, t03.t03_cd_projeto, "", "t03_projeto", "update", t03.t03_cd_projeto.ToString());
                    cod.Value = "0";
                }
                else
                {
                    result = t03.Save();
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");

                    t19_fase t19 = new t19_fase();
                    {
                        t19.fl_fase = "ES";
                        t19.Retrieve();
                        if (t19.Found)
                        {
                            t03.RetrieveCod();
                            if (t03.Found)
                            {
                                t20_faseprojeto t20 = new t20_faseprojeto();
                                {
                                    t20.t03_cd_projeto = t03.t03_cd_projeto;
                                    t20.t19_cd_fase = t19.t19_cd_fase;
                                    t20.dt_alterado = DateTime.Now;
                                    t20.dt_cadastro = DateTime.Now;
                                    t20.Save();
                                    pb.saveLog(cd_usuario, t03.t03_cd_projeto, "", "t03_projeto", "insert", t03.t03_cd_projeto.ToString());
                                }
                            }
                        }
                    }
                }

                if (result)
                {
                    Ocultar();
                    GridBind("order by nm_projeto");
                }
                else
                {
                    msg = pb.Message(pb.msgerro, "erro");
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true; 
        } 
        
    }
    
    protected void link_Click(object sender, System.EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Session["cd_projeto"] = link.CommandArgument;
        Response.Redirect("Arvore.aspx");
    }

    protected void Edit_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
    {
        ImageButton btn = (ImageButton)sender; 
        Exibir();
        FormBind();
        this.lblHeader.Text = "Alteração"; 
        this.btnAcao.Text = "Alterar"; 
        cod.Value = btn.CommandArgument; 
        Retrieve(); 
    }

    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        //bool excluir = true;
        t03_projeto t03 = new t03_projeto();
        {
            t03.t03_cd_projeto = Int32.Parse(btn.CommandArgument.ToString());
            t03.Delete();
            pb.saveLog(cd_usuario, t03.t03_cd_projeto, "", "t03_projeto", "delete", t03.t03_cd_projeto.ToString());
        }
        GridBind("order by nm_projeto");
        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
        lblMsg.Visible = true;

    } 
   
    protected void btnCadastro_Click(object sender, System.EventArgs e) 
    { 
        this.lblHeader.Text = "Cadastro"; 
        this.btnAcao.Text = "Cadastrar";
        FormBind();
        Exibir(); 
    } 

    protected void btnCancel_Click(object sender, System.EventArgs e) 
    { 
        Ocultar(); 
        cod.Value = "0"; 
    } 


    protected void FormBind()
    {
        DropDownList ddl = ddlt04_cd_tipologia;
        t04_tipologia t04 = new t04_tipologia();
        {
            t04.fl_ativa = true;
            ddl.DataSource = t04.List();
            ddl.DataTextField = "nm_tipologia";
            ddl.DataValueField = "t04_cd_tipologia";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
         
        t02_usuario t02 = new t02_usuario();
        {
            t02.order = "order by nm_nome";
            t02.t01_cd_entidade = cd_entidade;
            t02.fl_ativa = true;

            ddl = ddlt02_cd_usuario;
            ddl.DataSource = t02.ListComboProjeto();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");

            ddl = ddlt02_cd_usuario_monitoramento;
            ddl.DataSource = t02.ListComboProjeto();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    } 
}

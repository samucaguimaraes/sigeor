using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class frmFiltro : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e) 
    {
        Session["cd_projeto"] = null; //solução para não exibir menu do projeto.
        if (pb.fl_admin() && pb.fl_adminparceiro())
        {
            trEntidade.Visible = true;
            trParceiro.Visible = false;
        }
        else if (pb.fl_admin())
        {
            trEntidade.Visible = true;
            trParceiro.Visible = false;
        }
        else if (pb.fl_adminparceiro())
        {
            trParceiro.Visible = true;
            trEntidade.Visible = false;
            trPerfil.Visible = false;
            GridView1.Columns[8].Visible = false;
            lblfindtotal.Visible = false;
            lbltotal.Visible = false;
        }


        if (!IsPostBack)
        {
            lblTitle.Text = "Usuários"; 
            this.Panel1.Visible = false; 
            cod.Value = "0"; 
            FormBind(); 
            GridBind("order by nm_nome"); 
            ViewState["sentido"] = "DESC";
            ViewState["pesquisa"] = "";
            GridView1.Sort("nm_nome", SortDirection.Descending);
            if (Request["altersenha"] != null) { 
                lblMsg.Text = pb.Message("Alteração realizada com sucesso!", "ok"); 
                lblMsg.Visible = true; 
            } 
        } 
    } 
    private void Exibir() 
    {
        if (pb.fl_admin() && pb.fl_adminparceiro())
        {
            trEntidade.Visible = true;
            trParceiro.Visible = false;
        }
        else if (pb.fl_admin())
        {
            trEntidade.Visible = true;
            trParceiro.Visible = false;
        }
        else if (pb.fl_adminparceiro())
        {
            trParceiro.Visible = true;
            trEntidade.Visible = false;
            trPerfil.Visible = false;
            GridView1.Columns[8].Visible = false;
            lblfindtotal.Visible = false;
            lbltotal.Visible = false;
        }

        this.Panel1.Visible = true; 
        this.GridView1.Visible = false; 
        this.PanelAdd.Visible = false;
        this.txtnm_nome.Text = "";
        this.txtnm_email.Text = "";
        this.txtt02_cd_usuario.Text = "";
        this.txtnu_dddc.Text = "";
        txtnm_cpf.Text = "";
        this.txtnu_celular.Text = "";
        this.txtnu_dddt.Text = "";
        this.txtnu_telefone.Text = "";
        ddlt01_cd_entidade.ClearSelection();
        ddlt05_cd_parceiro.ClearSelection();
        txtnm_cargo.Text = "";
        cblt25_cd_perfil.ClearSelection();
    } 
    private void Ocultar() 
    { 
        this.Panel1.Visible = false; 
        this.GridView1.Visible = true;
        this.PanelAdd.Visible = true; 
        this.txtnm_nome.Text = "";
        this.txtnm_email.Text = "";
        this.txtt02_cd_usuario.Text = "";
        this.txtnu_dddc.Text = "";
        txtnm_cpf.Text = "";
        this.txtnu_celular.Text = "";
        this.txtnu_dddt.Text = "";
        this.txtnu_telefone.Text = "";
        ddlt01_cd_entidade.ClearSelection();
        ddlt05_cd_parceiro.ClearSelection();
        txtnm_cargo.Text = "";
        cblt25_cd_perfil.ClearSelection();
        GridBind("order by nm_nome");
    } 

    private void GridBind(string order) 
    {
        string query = "";
        if (!pb.fl_admin())
        {
            query = " and t02.t05_cd_parceiro <> 0 ";
           // query = " and t02.t05_cd_parceiro in (select t05_cd_parceiro from t05_parceiro where t01_cd_entidade = " + pb.cd_entidade() +")";
            //GridView1.Columns[7].Visible = false;
        }
        t02_usuario t02 = new t02_usuario(); 
        {
            t02.fl_ativa = true;
            lbltotal.Text = "(total: "+ t02.List().Tables[0].Rows.Count +" usuários)";
            t02.order = query + ViewState["pesquisa"] + order;
            if (ViewState["pesquisa"] != null)
            {
                if (ViewState["pesquisa"].ToString() != "")
                { lblfindtotal.Text = t02.List().Tables[0].Rows.Count + " encontrados "; }
                else { lblfindtotal.Text = ""; }
            }

            GridView1.DataSource = t02.List();
            GridView1.DataBind(); 
        } 
    }
    protected void Pesquisa(object sender, EventArgs e)
    {
        string order = "";
        string find = txtPesquisa.Text;
        ViewState["pesquisa"] = " and (t02.nm_nome like '%" + find + "%' or t02.t02_cd_usuario LIKE '%" + find + "%' or " +
        "t02.nm_email like '%" + find + "%')";
        order = ViewState["pesquisa"].ToString() + " order by nm_nome";
        GridBind(order);
        this.btnListar.Visible = true;
    }
    private void Retrieve()
    {
        t02_usuario t02 = new t02_usuario();
        {
            t02.t02_cd_usuario = GridView1.SelectedValue.ToString();
            t02.Retrieve();
            if (t02.Found)
            {
                this.txtnm_nome.Text = t02.nm_nome;
                this.txtnm_email.Text = t02.nm_email;
                this.txtnm_cargo.Text = t02.nm_cargo;
                txtnm_cpf.Text = t02.nm_cpf;
                if (t02.nu_telefone.ToString().Length == 10)
                {
                    this.txtnu_dddt.Text = t02.nu_telefone.ToString().Substring(0, 2);
                    this.txtnu_telefone.Text = t02.nu_telefone.ToString().Substring(2, 8);
                }
                if (t02.nu_celular.ToString().Length == 10)
                {
                    this.txtnu_celular.Text = t02.nu_celular.ToString().Substring(2, 8);
                    this.txtnu_dddc.Text = t02.nu_celular.ToString().Substring(0, 2);
                }

                //Response.Write(String.Format("t02.t05_cd_parceiro = {0}, t02.t01_cd_entidade={1}<br>", t02.t05_cd_parceiro, t02.t01_cd_entidade));

                if (t02.t05_cd_parceiro > 0)
                {
                    cblt25_cd_perfil.Items.FindByValue("1").Enabled = false;
                    cblt25_cd_perfil.Items.FindByValue("2").Enabled = false;

                   DropDownList ddl = ddlt05_cd_parceiro;
                    t05_parceiro t05 = new t05_parceiro();
                    {
                        t05.t05_cd_parceiro = t02.t05_cd_parceiro;
                        t05.Retrieve();
                        if (t05.Found)
                        {
                            t05.order = "order by nm_parceiro";
                            //t05.t01_cd_entidade = pb.cd_entidade();
                            ddl.DataSource = t05.List();
                            ddl.DataTextField = "nm_parceiro";
                            ddl.DataValueField = "t05_cd_parceiro";
                            ddl.DataBind();
                            pb.AddEmptyItem(ddl, "Selecione");
                        }
                    }

                    trEntidade.Visible = false; trParceiro.Visible = true;
                    this.ddlt05_cd_parceiro.ClearSelection();
                    ListItem li2 = this.ddlt05_cd_parceiro.Items.FindByValue(t02.t05_cd_parceiro.ToString());
                    if (li2 != null)
                        li2.Selected = true;
                }
                else
                {
                    cblt25_cd_perfil.Items.FindByValue("1").Enabled = true;
                    cblt25_cd_perfil.Items.FindByValue("2").Enabled = true;

                    trEntidade.Visible = true; trParceiro.Visible = false;
                    this.ddlt01_cd_entidade.ClearSelection();
                    ListItem li = this.ddlt01_cd_entidade.Items.FindByValue(t02.t01_cd_entidade.ToString());
                    if (li != null)
                        li.Selected = true;
                }
                //Response.Write(String.Format("trEntidade.Visible = {0}; trParceiro.Visible = {1};", trEntidade.Visible, trParceiro.Visible));
            }


            t26_usuarioperfil t26 = new t26_usuarioperfil();
            {
                t26.t02_cd_usuario = t02.t02_cd_usuario;
                foreach (DataRow dr in t26.List().Tables[0].Rows)
                {
                    ListItem li = cblt25_cd_perfil.Items.FindByValue(dr["t25_cd_perfil"].ToString());
                    if (li != null) li.Selected = true;
                }
            }
        }

    }

    protected void btnAcao_Click(object sender, System.EventArgs e) 
    { 
        t02_usuario t02 = new t02_usuario();
        bool result=false;
        bool erro=false;
        string msg=""; 
        { 
            t02.nm_nome = this.txtnm_nome.Text; 
            t02.nm_email = this.txtnm_email.Text;
            t02.nm_cargo = txtnm_cargo.Text;
            t02.nm_cpf = txtnm_cpf.Text;
            t02.pw_senha = this.txtpw_senha.Text;
            if (txtnu_dddt.Text != "" && txtnu_telefone.Text != "") t02.nu_telefone = Int64.Parse(txtnu_dddt.Text + txtnu_telefone.Text);
            if (txtnu_dddc.Text != "" && txtnu_celular.Text != "") t02.nu_celular = Int64.Parse(txtnu_dddc.Text + txtnu_celular.Text);

            //if (trParceiro.Visible)
            //{
                if (ddlt05_cd_parceiro.SelectedValue != "")
                    t02.t05_cd_parceiro = Int32.Parse(ddlt05_cd_parceiro.SelectedValue);
            //}
            //else if (trEntidade.Visible)
            //{
                if (ddlt01_cd_entidade.SelectedValue != "")
                    t02.t01_cd_entidade = Int32.Parse(ddlt01_cd_entidade.SelectedValue);
            //}

            t02.fl_ativa = true;
            t02.dt_cadastro = DateTime.Now;
            t02.dt_alterado = DateTime.Now;

            if ((txtnu_dddt.Text + txtnu_telefone.Text).Length != 10 && (txtnu_dddt.Text + txtnu_telefone.Text).Length > 0)
            {
                msg = pb.Message("Formato de telefone inválido! ", "erro");
                erro = true;
            }
            if ((txtnu_dddc.Text + txtnu_celular.Text).Length != 10 && (txtnu_dddc.Text + txtnu_celular.Text).Length > 0)
            {
                msg += pb.Message("Formato de celular inválido! ", "erro");
                erro = true;
            }
            if (!(erro))
            {
                if (cod.Value != "0")
                {
                    t02.t02_cd_usuario = GridView1.SelectedValue.ToString();
                    result = t02.Update();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(pb.cd_usuario(), 0, "", "t02_usuario", "update", t02.t02_cd_usuario);
                    cod.Value = "0";
                }
                else
                {
                    if (this.txtt02_cd_usuario.Text != "")
                    {
                        t02.t02_cd_usuario = pb.ReplaceAspas(txtt02_cd_usuario.Text.Replace(" ", ""));
                        t02.Retrieve();
                        if (!t02.Found)
                        {
                            result = t02.Save();
                            msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                            pb.saveLog(pb.cd_usuario(), 0, "", "t02_usuario", "insert", t02.t02_cd_usuario);
                        }
                        else
                        {
                            msg = pb.Message("\"" + txtt02_cd_usuario.Text + "\" não está disponível. Por favor, tente outro login de usuário.", "erro");
                            txtt02_cd_usuario.BackColor = System.Drawing.Color.Yellow;
                            PanelCad.Visible = true; 
                        }
                    }
                    else
                    {
                        msg = pb.Message("Campo usuário é obrigatório.", "erro");
                        txtt02_cd_usuario.BackColor = System.Drawing.Color.Yellow;
                        PanelCad.Visible = true; 
                    }

                }

                if (result)
                {
                    if (pb.fl_admin())
                    {
                        t26_usuarioperfil t26 = new t26_usuarioperfil();
                        {
                            t26.t02_cd_usuario = t02.t02_cd_usuario;
                            t26.Delete();
                            foreach (ListItem li in cblt25_cd_perfil.Items)
                            {
                                if (li.Selected)
                                {
                                    t26.t25_cd_perfil = Int32.Parse(li.Value);
                                    t26.dt_cadastro = DateTime.Now;
                                    t26.dt_alterado = DateTime.Now;
                                    t26.Save();
                                }
                            }
                        }
                    }
                    Ocultar();
                    GridBind("order by nm_nome");
                    ddlt01_cd_entidade.ClearSelection();
                    ddlt05_cd_parceiro.ClearSelection();
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true; 
        } 
        
    } 

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e) 
    { 
        Exibir();
        this.lblHeader.Text = "Alteração (Usuário: <b>" + GridView1.SelectedValue.ToString() + "</b>)"; 
        this.btnAcao.Text = "Alterar"; 
        cod.Value = GridView1.SelectedValue.ToString(); 
        Retrieve(); 
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = ((DataRowView)e.Row.DataItem);
            t26_usuarioperfil t26 = new t26_usuarioperfil();
            {
                t26.t02_cd_usuario = drv["t02_cd_usuario"].ToString();
                foreach (DataRow dr in t26.List().Tables[0].Rows)
                {
                    e.Row.Cells[9].Controls.Add(pb.GetLiteral("- " + dr["nm_perfil"].ToString() + "<br />"));
                }
            }
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
                                if (pb.fl_admin())
                                {
                                    e.Row.Cells[8].Controls.Add(pb.GetLiteral(t01.nm_entidade + "\\" + t05.nm_parceiro));
                                }
                                else
                                {
                                    e.Row.Cells[8].Controls.Add(pb.GetLiteral(t05.nm_parceiro));
                                }
                            }
                        }
                    }
                }
            }

            if ((Int64)drv["nu_telefone"] == 0)
                e.Row.Cells[5].Text = "-";

            if ((Int64)drv["nu_celular"] == 0)
                e.Row.Cells[6].Text = "-";
        }
    }
    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
    {
        ImageButton btn = (ImageButton)sender; 
        //bool excluir = true; 
        t02_usuario t02 = new t02_usuario(); 
        { 
                t02.t02_cd_usuario = btn.CommandArgument.ToString(); 
                t02.Delete();
                pb.saveLog(pb.cd_usuario(), 0, "", "t02_usuario", "delete", t02.t02_cd_usuario);
        } 
        GridBind("order by nm_nome"); 
        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok"); 
        lblMsg.Visible = true; 
        
    } 
   
    protected void Senha_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
    {
        ImageButton btn = (ImageButton)sender;
        Response.Redirect("frmSenha.aspx?cd_usuario=" + btn.CommandArgument.ToString()); 
    } 

    protected void btnCadastro_Click(object sender, System.EventArgs e) 
    { 
        this.lblHeader.Text = "Cadastro"; 
        this.btnAcao.Text = "Cadastrar";

        if (!pb.fl_admin() && ddlt05_cd_parceiro.Items.Count == 1)
        {
            lblMsg.Text = pb.Message("Antes de cadastrar um usuário, é necessário cadastrar um parceiro, <a href=frmParceiro.aspx>clique aqui</a> para fazer isso agora.", "erro"); ;
            lblMsg.Visible = true; 
        }
        else
        {
            Exibir();
            PanelCad.Visible = true; 
        }

        
    } 

    protected void btnCancel_Click(object sender, System.EventArgs e) 
    { 
        Ocultar(); 
        cod.Value = "0"; 
    } 


    protected void FormBind()
    {
        DropDownList ddl = this.ddlt01_cd_entidade;
        t01_entidade t01 = new t01_entidade();
        {
            t01.order = "order by nm_entidade";
            t01.fl_ativa = true;
            ddl.DataSource = t01.List();
            ddl.DataTextField = "nm_entidade";
            ddl.DataValueField = "t01_cd_entidade";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
        ddl = ddlt05_cd_parceiro;
        t05_parceiro t05 = new t05_parceiro();
        {
            t05.order = "order by nm_parceiro";
            t05.t01_cd_entidade = pb.cd_entidade();
            ddl.DataSource = t05.List();
            ddl.DataTextField = "nm_parceiro";
            ddl.DataValueField = "t05_cd_parceiro";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
        CheckBoxList cbl = cblt25_cd_perfil;
        t25_perfil t25 = new t25_perfil();
        {
            cbl.DataSource = t25.List();
            cbl.DataTextField = "nm_perfil";
            cbl.DataValueField = "t25_cd_perfil";
            cbl.DataBind();
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
        GridBind("order by "+ e.SortExpression + " " + ViewState["sentido"]);
    } 

    protected void btnListar_Click(object sender, System.EventArgs e) 
    {
        ViewState["pesquisa"] = "";
        GridBind("order by nm_nome"); 
        txtPesquisa.Text = "";
        btnListar.Visible = false;

    }



}

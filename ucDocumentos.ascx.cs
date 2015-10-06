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

public partial class ucDocumentos : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    bool resp;
    bool fl_foto;
    bool fl_video;
    bool fl_cronograma;
    bool fl_outros;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        Response.Buffer = true;
        Response.AddHeader("pragma", "no-cache");
        Response.Expires = 0;

        if (Request["tipo"] != null)
        {
            fl_foto = false;
            fl_video = false;
            fl_cronograma = false;
            fl_outros = false;

            switch (Request["tipo"].ToString())
            {
                case "foto":
                    fl_foto = true; imgTipo.ImageUrl = "~/images/ico_foto.gif"; lblTipo.Text = "Foto";
                    break;
                case "video":
                    fl_video = true; imgTipo.ImageUrl = "~/images/ico_video.gif"; lblTipo.Text = "Vídeo";
                    GridDocumentos.Columns[1].HeaderText = lblTipo.Text;
                    break;
                case "cronograma":
                    fl_cronograma = true; imgTipo.ImageUrl = "~/images/ico_cronograma.gif"; lblTipo.Text = "Cronograma";
                    GridDocumentos.Columns[1].HeaderText = lblTipo.Text;
                    linkModelo.NavigateUrl = "~/Manuais/modelo_cronogramafisico.xls";
                    linkModelo.ToolTip = "Download do modelo de cronograma físico";
                    linkModelo.Text = "<br /><br />Modelo de cronograma físico <img src=\"images/ico_download.gif\" />";
                    break;
                case "outros":
                    fl_outros = true; imgTipo.ImageUrl = "~/images/ico_outros.gif"; lblTipo.Text = "Outros";
                    GridDocumentos.Columns[1].HeaderText = lblTipo.Text;
                    linkModelo.NavigateUrl = "~/Manuais/modelo_matriz_acoes_resultados.xls";
                    linkModelo.ToolTip = "Download do modelo da matriz ações x resultados";
                    linkModelo.Text = "<br /><br />Modelo da matriz ações x resultados <img src=\"images/ico_download.gif\" />";
                    break;
            }

        }
        else
        {
            Response.Redirect("~/Arvore.aspx");
        }
        if (!pb.fl_gerente()) //se não for gerente
        {
            PanelAdd.Visible = false;
            GridDocumentos.Columns[0].Visible = false;
        }
        if (!(IsPostBack))
        {

            GridBind("");
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
    private void GridBind(String order)
    {
        t18_documento t18 = new t18_documento();
        {
            t18.t03_cd_projeto = pb.cd_projeto();
            t18.fl_foto = fl_foto;
            t18.fl_cronograma = fl_cronograma;
            t18.fl_outros = fl_outros;
            t18.fl_video = fl_video;
            if (fl_foto) 
            { 
                dlFotos.DataSource = t18.List(); dlFotos.DataBind();
            }
            else 
            { 
                GridDocumentos.DataSource = t18.List(); GridDocumentos.DataBind();
            }

            if (pb.fl_gerente())
            { //se não for gerente
                if (t18.List().Tables[0].Rows.Count == 0) Exibir(); lblHeader.Text = "Cadastro"; btnAcao.Text = "Cadastrar";
            }
        }
    }
    private void Exibir()
    {
        PanelEdit.Visible = true;
        PanelGrid.Visible = false;
        PanelAdd.Visible = false;
        txtnm_documento.Text = "";
        rblArquivo.ClearSelection();
        rblArquivo.SelectedValue = "N";

    }
    private void Ocultar()
    {
        PanelEdit.Visible = false;
        PanelGrid.Visible = true;
        PanelAdd.Visible = true;
        txtnm_documento.Text = "";
        PanelOpcao.Visible = false;
        rblArquivo.ClearSelection();
        rblArquivo.SelectedValue = "N";
    }
    protected void btnCadastro_Click(object sender, EventArgs e)
    {
        Exibir();
        lblHeader.Text = "Cadastro";
        btnAcao.Text = "Cadastrar";
    }
    protected void Delete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        bool result;
        string msg;
        t18_documento t18 = new t18_documento();
        {
            try
            {
                t18.t18_cd_documento = Int32.Parse(btn.CommandArgument);
                result = t18.Delete();
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
            GridBind("");
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Exibir();
        lblHeader.Text = "Alteração";
        btnAcao.Text = "Alterar";
        cod.Value = GridDocumentos.SelectedValue.ToString();
        Retrieve("");
        PanelArquivo.Visible = false;
        PanelOpcao.Visible = true;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Exibir();
        lblHeader.Text = "Alteração";
        btnAcao.Text = "Alterar";
        cod.Value = btn.CommandArgument;
        Retrieve(btn.CommandArgument);
        PanelArquivo.Visible = false;
        PanelOpcao.Visible = true;
    }
    private void Retrieve(string cd)
    {
        t18_documento t18 = new t18_documento();
        {
            if (fl_foto)
            {
                t18.t18_cd_documento = Int32.Parse(cd);
            }
            else
            {
                t18.t18_cd_documento = Int32.Parse(GridDocumentos.SelectedValue.ToString());
            }
            t18.Retrieve();
            if (t18.Found)
            {
                txtnm_documento.Text = t18.nm_documento;
            }
        }

    }
    protected void rblArquivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblArquivo.SelectedValue == "N")
        {
            PanelArquivo.Visible = false;
        }
        else
        {
            PanelArquivo.Visible = true;
        }
    }
    protected void btnAcao_Click(object sender, EventArgs e)
    {
        t18_documento t18 = new t18_documento();
        {
            bool result = false;
            string msg = "";
            string tipo = "";

            if (Request["tipo"] != null) tipo = Request["tipo"].ToString();
            uploadArquivo up = new uploadArquivo();
            {
                if (funm_arquivo.Visible)
                {
                    up.pasta = "Documentos";
                    up.nomeinicial = tipo;
                    up.fu = funm_arquivo;
                    result = up.Save();
                    msg = pb.Message(up.msg, "erro");
                }
                else
                {
                    result = true;
                }
                if (result)
                {
                    result = false;
                    t18.t03_cd_projeto = pb.cd_projeto();
                    t18.fl_foto = fl_foto;
                    t18.fl_cronograma = fl_cronograma;
                    t18.fl_outros = fl_outros;
                    t18.fl_video = fl_video;
                    t18.ds_descricao = "";
                    t18.nm_documento = txtnm_documento.Text;
                    t18.dt_cadastro = DateTime.Now;
                    t18.dt_alterado = DateTime.Now;

                    if (cod.Value != "0")
                    {
                        t18.t18_cd_documento = Int32.Parse(cod.Value);
                        if (funm_arquivo.Visible)
                        {
                            t18.order = ", nm_arquivo='" + up.nomearquivo + "' ";
                        }
                        result = t18.Update();
                        msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    }
                    else
                    {
                        t18.nm_arquivo = up.nomearquivo;
                        result = t18.Save();
                        msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                    }


                    if (result)
                    {
                        Ocultar();
                        GridBind("");
                        cod.Value = "0";
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Ocultar();
        GridBind("");

    }

}

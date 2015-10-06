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

public partial class frmDados : System.Web.UI.Page
{
    string cd_usuario;
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtnu_dddt.Attributes.Add("onkeydown", "return numeros(event)");
        //txtnu_dddc.Attributes.Add("onkeydown", "return numeros(event)");
        //txtnu_celular.Attributes.Add("onkeydown", "return numeros(event)");
        //txtnu_telefone.Attributes.Add("onkeydown", "return numeros(event)");
        if (Session["cd_usuario"] != null)
        {
            cd_usuario = Session["cd_usuario"].ToString();
        }

        cod.Value = cd_usuario;
        if (!IsPostBack)
        {
            Retrieve();
        }
    }
    private void Retrieve()
    {
        t02_usuario t02 = new t02_usuario();
        {
            t02.t02_cd_usuario = cd_usuario;
            t02.Retrieve();
            if (t02.Found)
            {
                this.txtnm_nome.Text = t02.nm_nome;
                this.txtnm_email.Text = t02.nm_email;
                this.txtnm_cargo.Text = t02.nm_cargo;
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

            }
        }

    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t02_usuario t02 = new t02_usuario();
        bool result = false;
        bool erro = false;
        string msg = "";
        {
            t02.nm_nome = this.txtnm_nome.Text;
            t02.nm_email = this.txtnm_email.Text;
            t02.nm_cargo = txtnm_cargo.Text;
            if (txtnu_dddt.Text != "" && txtnu_telefone.Text != "") t02.nu_telefone = Int64.Parse(txtnu_dddt.Text + txtnu_telefone.Text);
            if (txtnu_dddc.Text != "" && txtnu_celular.Text != "") t02.nu_celular = Int64.Parse(txtnu_dddc.Text + txtnu_celular.Text);
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
                if (cod.Value != "")
                {
                    t02.t02_cd_usuario = cd_usuario;
                    result = t02.UpdateDados();
                    msg = pb.Message("Alteração realizada com sucesso!", "ok");
                    pb.saveLog(cd_usuario, 0, "", "t02_usuario", "update", t02.t02_cd_usuario);
                }
                if (result)
                {
                    Retrieve();
                }
            }
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

    } 
}

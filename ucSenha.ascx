<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSenha.ascx.cs" Inherits="ucSenha" %>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
       

<table style="text-align: left">
<asp:Panel ID="PanelSenhaAtual" Visible="false" runat="server">
   <tr>
        <td>
            <label>Senha Atual</label></td>
        <td>
            <asp:TextBox ID="txtSenhaAtual" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtSenhaAtual" ErrorMessage="Favor digitar a Senha Atual" runat="server"/></td>
    </tr>
</asp:Panel>    
    <tr>
        <td>
            <label>Nova Senha</label></td>
        <td style="width: 510px">
            <asp:TextBox ID="txtNovaSenha" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtNovaSenha" ErrorMessage="Favor digitar a Nova Senha" runat="server"/></td>
    </tr>
    <tr>
        <td>
            <label>Confirma Nova Senha</label></td>
        <td style="width: 510px">
            <asp:TextBox ID="txtNovaSenha2" TextMode="Password" runat="server"></asp:TextBox><asp:CompareValidator
                ID="CompareValidator1" ControlToValidate="txtNovaSenha2" ControlToCompare="txtNovaSenha"
                ErrorMessage="Favor digitar a mesma Senha" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    ControlToValidate="txtNovaSenha2" ErrorMessage="Favor confirmar a Nova Senha"
                    runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: left">
            <asp:Button CssClass="btn" ID="btnAlterar" OnClick="btnAlterar_Click" runat="server" Text="Alterar" />
            &nbsp;&nbsp;
            <asp:Button CssClass="btn" ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" />
        </td>
    </tr>
</table>
</div>
</fieldset>
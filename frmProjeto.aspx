<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmProjeto.aspx.cs" Inherits="frmProjeto" Title="Agenda Bahia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>

    <asp:Panel ID="PanelGrid" runat="server">
    </asp:Panel>
    
    <asp:Panel ID="PanelForm" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Título:</label></td>
            <td><asp:TextBox  ID="txtnm_projeto" Width="350px" MaxLength="200" runat="server"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtnm_projeto" ErrorMessage="*campo obrigatório" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Tipologia:</label></td>
            <td>
                <asp:DropDownList ID="ddlt04_cd_tipologia" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="ddlt04_cd_tipologia" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>
        <tr>
            <td><label>Gestor:</label></td>
            <td>
            <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="ddlt02_cd_usuario" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>
        <tr>
            <td><label>Responsável <br />pelo Monitoramento:</label></td>
            <td>
            <asp:DropDownList ID="ddlt02_cd_usuario_monitoramento" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="ddlt02_cd_usuario_monitoramento" ErrorMessage="*campo obrigatório" runat="server"/>            
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="buttons">
                <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar"/>
                <asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" />
                </div>
            </td>
        </tr>
        </table>
        </div>
         <asp:HiddenField ID="cod" runat="server" />
      </fieldset>
    </asp:Panel>
</asp:Content>


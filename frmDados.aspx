<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmDados.aspx.cs" Inherits="frmDados" Title="Agenda Bahia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <fieldset>
       <legend>Alterar Dados</legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Nome:</label></td>
            <td><asp:TextBox  ID="txtnm_nome" Width="350px" MaxLength="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtnm_nome" ErrorMessage="*campo obrigatório" runat="server"/></td>
        </tr>
        <tr>
            <td><label>E-mail:</label></td>
            <td><asp:TextBox  ID="txtnm_email" Width="200px" MaxLength="100" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtnm_email" ErrorMessage="*campo obrigatório" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Telefone:</label></td>
            <td><asp:TextBox  ID="txtnu_dddt" MaxLength="2" Width="15px" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_dddt"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <asp:TextBox  ID="txtnu_telefone" MaxLength="8" Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_telefone"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <small>(apenas números)</small></td>
        </tr>
        <tr>
            <td><label>Celular:</label></td>
            <td><asp:TextBox  ID="txtnu_dddc" MaxLength="2" Width="15px" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_dddc"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <asp:TextBox  ID="txtnu_celular" MaxLength="8"  Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_celular"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   <small>(apenas números)</small>
            </td>
        </tr>
        <tr>
            <td><label>Cargo:</label></td>
            <td><asp:TextBox  ID="txtnm_cargo" Width="200px" MaxLength="100" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="buttons" style="text-align:center">
                <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" 
                        Text="Alterar" Height="28px" Width="82px"/>
                </div>
            </td>
        </tr>
        </table>
        </div>
         <asp:HiddenField ID="cod" runat="server" />
      </fieldset>                
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucParceiro.ascx.cs" Inherits="ucParceiro" %>
<h3><asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <fieldset style="border:none">
        <div class="formElement">
<asp:Panel ID="PanelAdd" runat="server">
            <br />
                <label>Novo Parceiro</label>
            <asp:DropDownList ID="ddlt05_cd_parceiro" runat="server">
            </asp:DropDownList>
                <asp:Button ID="btnAdd" runat="server" Text="Adicionar" 
                            onclick="btnAdd_Click" ValidationGroup="colaborador" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="colaborador" ControlToValidate="ddlt05_cd_parceiro"
            runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
            <hr class="dashed" />
</asp:Panel>
<asp:DataList ID="DataList1" DataKeyField="t06_cd_parceiroprojeto" CellPadding="6" CellSpacing="4" 
     runat="server" HorizontalAlign="Center" Width="80%" OnItemDataBound="DataList1_ItemDataBound"
    RepeatColumns="2" RepeatLayout="Table" RepeatDirection="Horizontal">
<ItemTemplate>
<div style="text-align:center">
    <asp:Image ID="imgArquivo" runat="server" /><br />
    <asp:ImageButton ID="ImageButton2" OnLoad="perfil_Load" CausesValidation="false" CommandArgument='<%#Eval("t06_cd_parceiroprojeto")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/> 
    <asp:Label ID="lblnm_parceiro" runat="server"></asp:Label>
</div>    
</ItemTemplate>
</asp:DataList>
</div>
</fieldset>

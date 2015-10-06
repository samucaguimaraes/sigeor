<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmlinha.aspx.cs" Inherits="frmlinha" Title="Agenda Bahia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h3><img alt="" src="images/arrow_right.gif" /> <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
<br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <br /><br />
        <asp:GridView ID="GridView1" DataKeyNames="t02_cd_usuario" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    AllowPaging="true" PageSize="100"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        
        <asp:TemplateField>
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("t02_cd_usuario")%>' 
            ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
            OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
        </ItemTemplate>
        <ItemStyle Width="25px" />
        </asp:TemplateField>
        
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Nome" SortExpression="nm_nome" ></asp:BoundField>
        
        </Columns>
    </asp:GridView>
        <br /><br />
        Usuário: <asp:DropDownList ID="ddlUsuario" runat="server"></asp:DropDownList>
        <br /><br />
        <div class="buttons">
                <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar"/>
        </div>
        
        </div>
    </fieldset>
    </asp:Panel>

</asp:Content>


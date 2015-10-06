<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmParceiro.aspx.cs" Inherits="frmParceiro" Title="Agenda Bahia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h3>
    <img alt="" src="images/arrow_right.gif" />
     <asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <fieldset>
        <div class="formElement">
        <asp:Panel ID="PanelAdd" runat="server">

            <br />
                <label style="font-weight:bold">Parceiro</label>
                <asp:TextBox ID="txtnm_parceiro" Width="250px" MaxLength="500" runat="server"></asp:TextBox>
                 <b>CNPJ </b><asp:TextBox ID="txtnm_cnpj" Width="100px" MaxLength="100" runat="server"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="Adicionar" 
                            onclick="btnAdd_Click" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtnm_parceiro"
                 runat="server" ErrorMessage="*campo Parceiro é obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
            <hr class="dashed" />

            
        </asp:Panel>
        
        <asp:GridView ID="GridView1" DataKeyNames="t05_cd_parceiro" Width="100%" GridLines="Both"
             OnRowEditing="GridView1_RowEditing" OnSorting="GridView1_Sorting" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
            EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
            <RowStyle CssClass="RowGrid"  />
            <HeaderStyle  CssClass="headerGrid"/>
            <EditRowStyle BackColor="#DBDBDB" />
            <AlternatingRowStyle CssClass="AlternatingRowGrid" />
            <Columns>
            <asp:TemplateField HeaderStyle-Width="20px">
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Edit" CausesValidation="false" ImageUrl="~/images/ico_edit.gif" ToolTip="Editar" runat="server" />
            </ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
            <ItemTemplate>
             <asp:ImageButton ID="ImageButton2" CausesValidation="false" CommandArgument='<%#Eval("t05_cd_parceiro")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            </ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parceiro" SortExpression="nm_parceiro" HeaderStyle-Width="60%">
            <ItemTemplate>
            <%#Eval("nm_parceiro")%></ItemTemplate>
            <EditItemTemplate>
              <asp:Button ID="Button1" ValidationGroup="grid"  CommandName="Update" Text="Alterar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:Button ID="Button2"  CausesValidation="false" CommandName="Cancel" Text="Cancelar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:TextBox ID="txtnm_parceiro" Width="250px" MaxLength="500" Text='<%#Eval("nm_parceiro")%>' runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtnm_parceiro"
                 runat="server" ErrorMessage="*campo obrigatório" ValidationGroup="grid" Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CNPJ" SortExpression="nm_cnpj">
            <ItemTemplate>
            <%#Eval("nm_cnpj")%>
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtnm_cnpj" Width="100px" MaxLength="100"  Text='<%#Eval("nm_cnpj")%>' runat="server"></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Logotipo" 
            HeaderStyle-HorizontalAlign="Center" 
            ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Image ID="imgArquivo" runat="server" />
            </ItemTemplate>
            <EditItemTemplate>
            Nova Logotipo? 
                <asp:RadioButtonList ID="rblFoto" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblFoto_SelectedIndexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                <asp:ListItem Value="1">Sim</asp:ListItem>
                </asp:RadioButtonList> 
                <asp:FileUpload ID="FileUpload1" Visible="false" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="FileUpload1"
                runat="server" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
            

        </div>
    </fieldset>   

</asp:Content>


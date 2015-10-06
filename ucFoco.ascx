<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFoco.ascx.cs" Inherits="ucFoco" %>
<h3><asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <fieldset style="border:none">
        <div class="formElement">
        <asp:Panel ID="PanelAdd" runat="server">
                <label>
                <asp:Label ID="lblnovo" runat="server" Text="Novo Foco estratégico"></asp:Label></label><br />
                <asp:TextBox ID="txtnm_foco" Width="250px" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnAdd" runat="server" Text="Adicionar" 
                            onclick="btnAdd_Click" ValidationGroup="premissas" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="premissas" ControlToValidate="txtnm_foco"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
            <hr class="dashed" />

            
        </asp:Panel>
        <asp:GridView ID="GridView1" DataKeyNames="t13_cd_foco" Width="100%" GridLines="Both" ShowHeader="false"
             OnRowEditing="GridView1_RowEditing" OnSorting="GridView1_Sorting" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
            EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
            <RowStyle CssClass="RowGrid"  />
            <HeaderStyle  CssClass="headerGrid"/>
            <EditRowStyle BackColor="#CCCCCC" />
            <AlternatingRowStyle CssClass="AlternatingRowGrid" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="20px">
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Edit" CausesValidation="false" ImageUrl="~/images/ico_edit.gif" ToolTip="Editar" runat="server" />
            </ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20px">
            <ItemTemplate>
             <asp:ImageButton ID="ImageButton2" CausesValidation="false" CommandArgument='<%#Eval("t13_cd_foco")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            </ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Foco estratégico" SortExpression="nm_foco">
            <ItemTemplate>
            <%#Eval("nm_foco")%></ItemTemplate>
            <EditItemTemplate>
              <asp:Button ID="Button1" ValidationGroup="grid"  CommandName="Update" Text="Alterar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:Button ID="Button2"  CausesValidation="false" CommandName="Cancel" Text="Cancelar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:TextBox ID="txtnm_foco"  TextMode="MultiLine" Width="250px" MaxLength="500" Text='<%#Eval("nm_foco")%>' runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtnm_foco"
                 runat="server" ErrorMessage="*campo obrigatório" ValidationGroup="grid" Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
            

        </div>
    </fieldset> 
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucColaborador.ascx.cs" Inherits="ucColaborador" %>
<h3><asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <fieldset style="border:none">
        <div class="formElement">
        <asp:Panel ID="PanelAdd" runat="server">

            <br />
                <label>Novo Colaborador</label>
            <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
            </asp:DropDownList>
                <asp:Button ID="btnAdd" runat="server" Text="Adicionar" 
                            onclick="btnAdd_Click" ValidationGroup="colaborador" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="colaborador" ControlToValidate="ddlt02_cd_usuario"
            runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
            <hr class="dashed" />

            
        </asp:Panel>
        <asp:GridView ID="GridView1" DataKeyNames="t17_cd_colaborador" Width="100%" GridLines="Both" 
            OnRowDataBound="GridView1_RowDataBound"  EmptyDataText="Nenhum registro encontrado" 
            runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
            <RowStyle CssClass="RowGrid"  />
            <HeaderStyle  CssClass="headerGrid"/>
            <EditRowStyle BackColor="#CCCCCC" />
            <AlternatingRowStyle CssClass="AlternatingRowGrid" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="20px">
            <ItemTemplate>
             <asp:ImageButton ID="ImageButton2" CausesValidation="false" CommandArgument='<%#Eval("t17_cd_colaborador")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Nome" SortExpression="nm_nome" ></asp:BoundField>
            <asp:BoundField DataField="nm_entidade" HtmlEncode="false" HeaderText="Parceiro" SortExpression="nm_entidade" ></asp:BoundField>
            <asp:BoundField DataField="nm_email" HtmlEncode="false" HeaderText="E-mail" SortExpression="nm_email" ></asp:BoundField>
            </Columns>
            </asp:GridView>
            

        </div>
    </fieldset> 
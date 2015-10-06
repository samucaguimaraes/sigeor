﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEntidade.aspx.cs" Inherits="frmEntidade" Title="Agenda Bahia" %>
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
                <asp:TextBox ID="txtnm_entidade" Width="250px" MaxLength="500" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtnm_entidade"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
                UF: <asp:DropDownList ID="ddlnm_uf" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlnm_uf"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" Text="Adicionar" 
                            onclick="btnAdd_Click" />
                
            <hr class="dashed" />

            
        </asp:Panel>
        <asp:GridView ID="GridView1" DataKeyNames="t01_cd_entidade" Width="100%" GridLines="Both"
             OnRowEditing="GridView1_RowEditing" OnSorting="GridView1_Sorting" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
            EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
            <RowStyle CssClass="RowGrid"  />
            <HeaderStyle  CssClass="headerGrid"/>
            <EditRowStyle BackColor="#CCCCCC" />
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
             <asp:ImageButton ID="ImageButton2" CausesValidation="false" CommandArgument='<%#Eval("t01_cd_entidade")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            </ItemTemplate>
            <EditItemTemplate></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parceiro" SortExpression="nm_entidade">
            <ItemTemplate>
            <%#Eval("nm_entidade")%></ItemTemplate>
            <EditItemTemplate>
              <asp:Button ID="Button1" ValidationGroup="grid"  CommandName="Update" Text="Alterar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:Button ID="Button2"  CausesValidation="false" CommandName="Cancel" Text="Cancelar" Font-Size="8pt" Width="45px" Runat="Server"/>
              <asp:TextBox ID="txtnm_entidade" Width="250px" MaxLength="500" Text='<%#Eval("nm_entidade")%>' runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtnm_entidade"
                 runat="server" ErrorMessage="*campo obrigatório" ValidationGroup="grid" Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UF" SortExpression="nm_uf">
            <ItemTemplate>
            <asp:Label ID="lblnm_uf" runat="server" Text='<%#Eval("nm_uf")%>'></asp:Label></ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlnm_uf" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlnm_uf"
                 runat="server" ErrorMessage="*campo obrigatório" ValidationGroup="grid"  Display="Dynamic"></asp:RequiredFieldValidator>            
            </EditItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
            

        </div>
    </fieldset>   
</asp:Content>


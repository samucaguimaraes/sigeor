<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmFiltro.aspx.cs" Inherits="frmFiltro" Title="Agenda Bahia" %>
<%@ Register Src="~/ucSenha.ascx" TagName="Senha" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3><img alt="" src="images/arrow_right.gif" /> <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
        <asp:TextBox ID="txtPesquisa" OnTextChanged="Pesquisa" runat="server"></asp:TextBox>
        <asp:LinkButton ID="btnOK" runat="server" OnClick="Pesquisa" CssClass="btnSearch">Pesquisar</asp:LinkButton>
        <asp:LinkButton ID="btnListar" runat="server" OnClick="btnListar_Click" CssClass="btnShowAll" Visible="False">Listar Todos</asp:LinkButton>
        <div style="text-align:right"><asp:Label ID="lblfindtotal" runat="server"></asp:Label> <asp:Label ID="lbltotal" runat="server"></asp:Label></div>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t02_cd_usuario" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="true" PageSize="100"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        <asp:ButtonField ButtonType="Image" CommandName="Select" ImageUrl="~/images/ico_edit.gif"  Text="Editar"  ItemStyle-Width="25px" />
        <asp:TemplateField>
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("t02_cd_usuario")%>' 
            ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
            OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
        </ItemTemplate>
        <ItemStyle Width="25px" />
        </asp:TemplateField>
        
        <asp:BoundField DataField="t02_cd_usuario" HtmlEncode="false" HeaderText="Usuário" SortExpression="t02_cd_usuario" ></asp:BoundField>
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Nome" SortExpression="nm_nome" ></asp:BoundField>
        <asp:BoundField DataField="nm_email" HtmlEncode="false" HeaderText="E-mail" SortExpression="nm_email" ></asp:BoundField>
        <asp:BoundField DataField="nu_telefone" HtmlEncode="false" NullDisplayText="-" HeaderText="Telefone" DataFormatString="{0:(##) ####-####}" SortExpression="nu_telefone" ></asp:BoundField>
        <asp:BoundField DataField="nu_celular" HtmlEncode="false" NullDisplayText="-" HeaderText="Celular" DataFormatString="{0:(##) ####-####}" SortExpression="nu_celular" ></asp:BoundField>
        <asp:BoundField DataField="nm_cpf" HtmlEncode="false" HeaderText="CPF"></asp:BoundField>
        <asp:BoundField DataField="nm_entidade" HtmlEncode="false" HeaderText="Parceiro" SortExpression="nm_entidade" ></asp:BoundField>
        <asp:TemplateField HeaderText="Perfil">
        <ItemTemplate>
        </ItemTemplate>
        </asp:TemplateField>
                
        </Columns>
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Nome:</label></td>
            <td><asp:TextBox  ID="txtnm_nome" Width="350px" MaxLength="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtnm_nome" ErrorMessage="Favor digitar o Nome" runat="server"/></td>
        </tr>
        
        <asp:Panel ID="PanelCad" Visible="false" EnableViewState="false" runat="server">
        <tr>
            <td><label>Usuário:</label></td>
            <td><asp:TextBox  ID="txtt02_cd_usuario" MaxLength="20" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="txtt02_cd_usuario" ErrorMessage="Favor digitar o Login" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Senha:</label></td>
            <td><asp:TextBox  ID="txtpw_senha" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="txtpw_senha" ErrorMessage="Favor digitar a Senha" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Confirma Senha:</label></td>
            <td><asp:TextBox  ID="txtpw_senha2" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" ControlToValidate="txtpw_senha2" ErrorMessage="Favor confirmar Senha"
                    runat="server" /><asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtpw_senha2"
                        ControlToCompare="txtpw_senha" ErrorMessage="Favor digitar a mesma Senha" runat="server" /></td>
        </tr>            
        </asp:Panel>

        <tr>
            <td><label>E-mail:</label></td>
            <td><asp:TextBox  ID="txtnm_email" Width="200px" MaxLength="100" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtnm_email" ErrorMessage="Favor digitar o E-mail" runat="server"/></td>
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
            <td><label>CPF:</label></td>
            <td><asp:TextBox  ID="txtnm_cpf" Width="200px" MaxLength="100" runat="server"></asp:TextBox></td>
        </tr>
        <tr id="trEntidade" visible="false" runat="server">
            <td><label>Parceiro:</label></td>
            <td>
                <asp:DropDownList ID="ddlt01_cd_entidade" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                ErrorMessage="*campo obrigatório" ControlToValidate="ddlt01_cd_entidade"></asp:RequiredFieldValidator>
             </td>   
        </tr>   
        <tr id="trPerfil" runat="server">
            <td><label>Perfil:</label></td>
            <td>
                <asp:CheckBoxList ID="cblt25_cd_perfil" RepeatDirection="Horizontal" runat="server">
                </asp:CheckBoxList>
            </td>   
        </tr>   
        <tr id="trParceiro" visible="false" runat="server">
            <td><label>Parceiro:</label></td>
            <td>
                <asp:DropDownList ID="ddlt05_cd_parceiro" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                ErrorMessage="*campo obrigatório" ControlToValidate="ddlt05_cd_parceiro"></asp:RequiredFieldValidator>
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
    
    <asp:Panel ID="PanelSenha" Visible="false" runat="server">
    </asp:Panel>
</asp:Content>


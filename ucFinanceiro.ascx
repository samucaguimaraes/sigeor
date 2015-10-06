<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFinanceiro.ascx.cs" Inherits="ucFinanceiro" %>
<%@ Register Src="~/ucPrevistoAno.ascx" TagName="Previsto" TagPrefix="uc" %>
<%@ Register Src="~/ucRealizadoAno.ascx" TagName="Realizado" TagPrefix="uc" %>
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t11_cd_financeiro" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    AllowPaging="true" PageSize="100" OnRowCommand="GridView1_RowCommand"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/ico_edit.gif"  Text="Editar"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Deletar" ImageUrl="~/images/ico_exc.gif"  Text="Excluir"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif"  Text="Detalhamento"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
		
        <asp:BoundField DataField="nm_parceiro" ItemStyle-Width="35%" HtmlEncode="false" HeaderText="Entidade / Unidade" />
        <asp:TemplateField HeaderText="Tipo" ItemStyle-Width="15%"> 
        <ItemTemplate></ItemTemplate>
        </asp:TemplateField>
       
		<asp:BoundField DataField="vl_p" HtmlEncode="false" DataFormatString="{0:N}" NullDisplayText="0,00"  HeaderText="Valor Previsto (R$)" />
        <asp:BoundField DataField="vl_r" HtmlEncode="false" DataFormatString="{0:N}" NullDisplayText="0,00"  HeaderText="Valor Realizado (R$)"  /> 
		
        </Columns> 
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table style="width:100%; text-align:left">
        <tr>
            <td><label style="font-weight:bold">Orgão Responsável:</label> <asp:DropDownList ID="ddlt05_cd_parceiro" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlt05_cd_parceiro"
                runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>  </td>
        </tr>
    <!-- LEVI <tr>
            <td><label style="font-weight:bold">Tipo:</label>
                <asp:RadioButtonList ID="rblfl_economico" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
                <asp:ListItem Value="False">Financeiro</asp:ListItem>
                <asp:ListItem Value="True">Econômico</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rblfl_economico"
                runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>  </td>
        </tr>
        <tr>
            <td ><label style="font-weight:bold">Previsto:</label><br /><uc:Previsto editar="true" runat="server" ID="ucPrevisto" /></td>
        </tr>  
        <tr id="trReal" runat="server">
            <td ><label style="font-weight:bold">Realizado:</label><br /><uc:Realizado editar="true" runat="server" ID="ucRealizado" /></td>
        </tr>   LEVI -->         
        <tr>
            <td >
		
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
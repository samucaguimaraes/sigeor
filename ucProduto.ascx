<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProduto.ascx.cs" Inherits="ucProduto" %>
<%@ Register Src="~/ucPrevistoAno.ascx"TagName="Previsto" TagPrefix="uc" %>
<%@ Register Src="~/ucRealizadoAno.ascx"TagName="Realizado" TagPrefix="uc" %>
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t10_cd_produto" Width="100%" GridLines="Both"
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
        <asp:BoundField DataField="ds_produto" HtmlEncode="false" HeaderText="Descrição" ItemStyle-Width="35%" />
        <asp:BoundField DataField="nm_medida" HtmlEncode="false" HeaderText="Unidade de Medida" ItemStyle-Width="15%" />
        <asp:BoundField DataField="vl_p" DataFormatString="{0:N0}" HtmlEncode="false" HeaderText="Meta Prevista" />
        <asp:BoundField DataField="vl_r" DataFormatString="{0:N0}" HtmlEncode="false" HeaderText="Meta Realizada"  />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label style="font-weight:bold">Descrição:</label><br />
            <asp:TextBox  ID="txtds_produto" Width="350px" runat="server"  TextMode="MultiLine"
                    Height="50px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator6" 
                    ControlToValidate="txtds_produto" ErrorMessage="*campo obrigatório" runat="server" 
                    Display="Dynamic"/></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Unidade de Medida:</label><br />
            <asp:TextBox ID="txtnm_medida" MaxLength="50" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Previsto:</label><br /><uc:Previsto editar="true" runat="server" ID="ucPrevisto" /></td>
        </tr>  
        <tr id="trReal" runat="server">
            <td><label style="font-weight:bold">Realizado:</label><br /><uc:Realizado editar="true" runat="server" ID="ucRealizado" /></td>
        </tr>             
        <tr>
            <td>
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
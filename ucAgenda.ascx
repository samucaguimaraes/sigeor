﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAgenda.ascx.cs" Inherits="ucAgenda" %>
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t24_cd_agenda" Width="100%" GridLines="Both"
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
        <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif" Text="Exibir Projeto"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="ds_agenda" ItemStyle-Wrap="true" HeaderText="Título" />
        <asp:BoundField DataField="dt_data" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data" SortExpression="dt_data" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Título:</label></td>
            <td><asp:TextBox  ID="txtds_agenda" Width="400px" runat="server"  TextMode="MultiLine"
                    Height="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><label>Data:</label></td>
            <td><asp:TextBox ID="txtdt_data" Width="100px"  runat="server" ReadOnly="true" Text=''></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar3" runat="server"
            Buttons="[<][m][y]  [>]" Control="txtdt_data"
            ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
            Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
            Move="True" RequiredDate="False" RequiredDateMessage="Selecione Data Início" Separator="/"
            Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" /> 
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" 
                    ControlToValidate="txtdt_data" ErrorMessage="*campo obrigatório" 
                    runat="server" Display="Dynamic"/>
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
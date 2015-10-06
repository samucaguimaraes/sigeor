<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Financeiro.aspx.cs" Inherits="Financeiro" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucPrevistoAno.ascx" TagName="Previsto" TagPrefix="uc" %>
<%@ Register Src="~/ucRealizadoAno.ascx" TagName="Realizado" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" financeiro="true" /> 
<uc:Projeto runat="server" ID="ucProjeto" />
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2">Detalhamento do Financeiro</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
         <table cellpadding="4" cellspacing="4">
         <tr>
            <td><label style="font-weight:bold">Ação:</label> 
                <asp:Label ID="lblnm_acao" runat="server"></asp:Label>
             </td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Parceiro:</label> 
                <asp:Label ID="lblnm_parceiro" runat="server"></asp:Label>
             </td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Tipo:</label> 
                <asp:Label ID="lbltipo" runat="server"></asp:Label>
             </td>
        </tr>        
        <tr>
            <td ><label style="font-weight:bold">Previsto:</label><br /><uc:Previsto runat="server" ID="ucPrevisto" /></td>
        </tr>  
        <tr id="trReal" runat="server">
            <td ><label style="font-weight:bold">Realizado:</label><br /><uc:Realizado runat="server" OnLoad="ucRealizado_Load" ID="ucRealizado" /></td>
        </tr>             
        </table>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
    </asp:Content>

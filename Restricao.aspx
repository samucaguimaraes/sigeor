<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Restricao.aspx.cs" Inherits="Restricao" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" restricao="true"/> 

<uc:Projeto runat="server" ID="ucProjeto" />

         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2">Detalhamento da Restrição</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
         
      <table cellpadding="4" cellspacing="4">
        <tr>
            <td><label style="font-weight:bold">Data de Inclusão:</label></td>
            <td><asp:Label ID="lbldt_cadastro" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Restrição:</label></td>
            <td><asp:Label ID="lblds_restricao" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Medida de gestão:</label></td>
            <td><asp:Label ID="lblds_medida" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Data limite para solução:</label></td>
            <td><asp:Label ID="lbldt_limite" runat="server"></asp:Label></td>
        </tr> 
        <tr runat="server" id="trAcao"  visible="false">
            <td><label style="font-weight:bold">Ação relacionada:</label></td>
            <td><asp:Label ID="lblnm_acao" runat="server"></asp:Label></td>
        </tr> 
        <tr runat="server" id="trProjeto" visible="false">
            <td colspan=2><b>Restrição vinculada ao </b><strong>Programa</strong></td>
        </tr>           
      </table>
      
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


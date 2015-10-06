<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrgaoResponsavel.aspx.cs" Inherits="OrgaoResponsavel" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:NavMon runat="server" ID="navmonFiltro" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">Visão Orgão Responsável</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    

<asp:Panel ID="PanelFiltro" runat="server">
    <asp:Label ID="lblMsg" Visible="false" EnableViewState="false" runat="server"></asp:Label>
<div style="text-align:center">
<table style="text-align:left; width:80%; font-weight:bold;" cellpadding="4" class="tblist">
<tr id="trParceiro" runat="server">
    <td>Parceiro</td>
    <td>
        <asp:DropDownList ID="ddlt01_cd_entidade" AutoPostBack="true" runat="server" 
            onselectedindexchanged="ddlt01_cd_entidade_SelectedIndexChanged">
        </asp:DropDownList>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlt01_cd_entidade"
        ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
</tr>

<tr>
    <td>Orgão Responsável:</td>
    <td><asp:DropDownList ID="ddlt03_cd_projeto" runat="server">
        </asp:DropDownList></td>
</tr>



 <!-- LEVI

<tr>
    <td>Orgão Responsável</td>
    <td><asp:DropDownList ID="ddlt05_cd_parceiro" runat="server">
        </asp:DropDownList></td>
</tr> 


<tr>
    <td>Tipologia</td>
    <td><asp:DropDownList ID="ddlt04_cd_tipologia" runat="server">
        </asp:DropDownList></td>
</tr> 
<tr>
    <td>Fase</td>
    <td><asp:DropDownList ID="ddlt19_cd_fase" runat="server">
        </asp:DropDownList></td>
</tr>LEVI --> 
<tr> 
    <td colspan="2" style="text-align:center">
        <asp:Button ID="btnFiltro" runat="server" Text="Avançar" Height="24px" 
            onclick="btnFiltro_Click" Width="72px" /></td>
</tr>
</table>
</div>
</asp:Panel>


<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


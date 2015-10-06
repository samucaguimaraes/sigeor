<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc:Nav ID="ucNav" runat="server"/>
    <div style="padding-left:100px">
    <asp:Panel ID="PanelFiltro" runat="server" Visible="false">
    
    Parceiro: <asp:DropDownList ID="ddlt01_cd_entidade" runat="server"></asp:DropDownList>
    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" />
    </asp:Panel>
    </div>    
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    

    
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Projetos.aspx.cs" Inherits="Projetos" Title="Untitled Page" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucLegenda.ascx" TagName="Legenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server"/>  
    <asp:Panel ID="PanelGrid" runat="server">
    </asp:Panel>
    <br />
<uc:Legenda ID="ucLegenda" runat="server" />    
</asp:Content>


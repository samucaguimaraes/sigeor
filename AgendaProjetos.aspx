<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgendaProjetos.aspx.cs" Inherits="AgendaProjetos" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucAgenda.ascx" TagName="Agenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server"  agendaProjetos="true"/> 
<uc:Agenda runat="server" ID="ucAgenda" projetos="true" />
</asp:Content>


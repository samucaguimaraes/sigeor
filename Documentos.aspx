<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Documentos.aspx.cs" Inherits="Documentos" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucDocumentos.ascx" TagName="Documentos" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" documentos="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />
<uc:Documentos runat="server" ID="ucDocumentos" />
</asp:Content>
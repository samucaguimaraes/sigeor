<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Noticias.aspx.cs" Inherits="Noticias" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucNoticias.ascx" TagName="Noticias" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" noticias="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />

<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Notícias</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
<uc:Noticias runat="server" ID="ucNoticias" />

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


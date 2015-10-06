<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Agenda.aspx.cs" Inherits="Agenda" Title="Untitled Page" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucAgenda.ascx" TagName="Agenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" agenda="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />

<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Agenda</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
<uc:Agenda runat="server" ID="ucAgenda" />

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />

</asp:Content>

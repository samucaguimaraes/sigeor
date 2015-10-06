<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Colaboradores.aspx.cs" Inherits="Colaboradores" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucColaborador.ascx" TagName="Colaborador" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" colaboradores="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />


<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Colaboradores</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
<uc:Colaborador runat="server" ID="ucColaborador" />

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />

</asp:Content>


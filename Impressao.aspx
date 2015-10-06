<%@ Page Language="C#" MasterPageFile="~/MasterPageX.master" AutoEventWireup="false" CodeFile="Gerenciamento.aspx.cs" Inherits="Gerenciamento" Title="Agenda Bahia" %>
<%@ Register Src="~/ucLegenda.ascx" TagName="Legenda" TagPrefix="uc" %>
<%@ Register Src="~/ucAcoes2.ascx" TagName="Acao" TagPrefix="uc" %>
<%@ Register Src="~/ucRestricoes.ascx" TagName="Restricao" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto2.ascx" TagName="Projeto" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:Projeto runat="server" ID="ucProjeto" />

         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2">  </h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
         
         <uc:Acao ID="ucAcao" runat="server" />
       <!--  <uc:Restricao ID="ucRestricao" runat="server"/>-->

         
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
		 <uc:Legenda ID="ucLegenda" runat="server" />  
</asp:Content>


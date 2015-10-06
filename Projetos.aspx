<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Projetos.aspx.cs" Inherits="Projetos" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucLegenda.ascx" TagName="Legenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server"/>  
    <asp:Panel ID="PanelGrid" runat="server">
    </asp:Panel>
    <br />
    
	<table cellpadding="20" style="margin: 0 auto;text-align:center">
    <tr>
        <td >
            <asp:HyperLink ID="HyperLinkNoticias" Font-Underline="false" runat="server" ToolTip="Últimas Notícias" NavigateUrl="~/NoticiasProjetos.aspx">
        <img src="images/ico_noticia.gif" alt="Últimas Notícias" /><br /> <b>Últimas Notícias</b></asp:HyperLink>
        </td>
        <td>
        <asp:HyperLink ID="HyperLink1Agenda" Font-Underline="false" runat="server" ToolTip="Agenda dos Eixos" NavigateUrl="~/AgendaProjetos.aspx">
        <img src="images/ico_agenda.gif" alt="Agenda dos Projetos" /><br /> <b>Agenda do Eixo</b>
        </asp:HyperLink>
        </td>
    </tr>
        <uc:legenda ID="ucLegenda" runat="server" />  
    </table>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmSenha.aspx.cs" Inherits="frmSenha" Title="Agenda Bahia" %>
<%@ Register Src="~/ucSenha.ascx" TagName="Senha" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Senha Destino="" SenhaAtual="true"  runat="server" ID="ucSenha" />
</asp:Content>


﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MonFisicoInd.aspx.cs" Inherits="MonFisicoInd" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" fisind="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    Índice de realização física</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPageX.master" AutoEventWireup="true" CodeFile="MonImpressao2.aspx.cs" Inherits="MonImpressao" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<%@ Register Src="~/ucLegenda2.ascx" TagName="Legenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <!--<uc:NavMon runat="server" ID="navmon" acoes="true" /> -->
        

        
<div ><div class="heading_right_top"></div>
   
<h2 id="H1">
    <asp:Label ID="lblHeader" runat="server"></asp:Label></h2></div>
<div><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    

     <asp:Panel ID="Panel1" runat="server">

         <input name ="btnimprimir" type="submit" value="Imprimir" onclick=" javascript: this.style.display = 'none'; window.print();" /> 
         
         <br />
         <br />
           

		
    </asp:Panel>
	<uc:legenda ID="ucLegenda" runat="server" />  
<div class="clear"></div></div><div><span></span></div></div><br />
     
</asp:Content>
     


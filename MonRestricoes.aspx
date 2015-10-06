<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MonRestricoes.aspx.cs" Inherits="MonRestricoes" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" restricao="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    <asp:Label ID="lblHeader" runat="server"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
   <asp:Panel ID="PanelOpcao" runat="server">
   <div style="text-align:center"><br />
       <asp:Panel ID="PanelGraf" Width="30%" runat="server">
       </asp:Panel>
       <br />
       <table style="width:60%; background:#FFFFFF" cellpadding="4" class="tblist">
                        <tr>
                        <td style="width:65%"><b>
                        Status</b></td>
                        <td colspan="2"><b>%</b></td>
                        <td ><b>Qtd</b></td>
                        </tr>
                        <tr>
                        <td style="text-align:left">
                        <span style="background:url('images/B.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkConcluidos"  NavigateUrl="~/MonRestricoes.aspx?fl_status=B" runat="server">
                         Concluídos
                        </asp:HyperLink>
                        </td>
                        <td  colspan="2">
                            <asp:Label ID="lblFatiaAzul" runat="server"></asp:Label>
                            </td><td >
                             <asp:Label ID="lblAzul" runat="server"></asp:Label></td>
                        </tr>

                         <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/G.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkPrazos"  NavigateUrl="~/MonRestricoes.aspx?fl_status=G" runat="server">
                        Dentro dos prazos previstos
                        </asp:HyperLink>
                        </td>
                        <td colspan="2">
                        <asp:Label ID="lblFatiaVerde" runat="server"></asp:Label>
                             </td><td>
                                 <asp:Label ID="lblVerde" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/R.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkAtraso"  NavigateUrl="~/MonRestricoes.aspx?fl_status=R" runat="server">    
                            Com atraso
                        </asp:HyperLink>    
                            </td>
                        <td colspan="2">
                        <asp:Label ID="lblFatiaVermelha" runat="server"></asp:Label>
                            </td><td>
                          <asp:Label ID="lblVermelha" runat="server"></asp:Label></td>
                        </tr>
                        </table>   
      </div> 
   </asp:Panel>
   <asp:Panel ID="PanelRel" runat="server">
   </asp:Panel>
   <div style="text-align:center">
   <br />
       <asp:HyperLink ID="linkVoltar" Font-Underline="false"  NavigateUrl="~/MonRestricoes.aspx" runat="server">    
           << Voltar
        </asp:HyperLink>
   </div>
   
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


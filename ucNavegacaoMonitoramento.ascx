<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNavegacaoMonitoramento.ascx.cs" Inherits="ucNavegacaoMonitoramento" %>
<div class="navegacao">
Monitoramento 
<asp:HyperLink ID="linkFiltro" NavigateUrl="~/Monitoramento.aspx" runat="server">> Visão geral</asp:HyperLink>
<asp:HyperLink ID="linkPainel" Visible="false" NavigateUrl="~/MonPainel.aspx" runat="server">> Resumo executivo</asp:HyperLink>
<asp:HyperLink ID="linkMonRestricoes" Visible="false" NavigateUrl="~/MonRestricoes.aspx" runat="server">> Restrições</asp:HyperLink>
<asp:HyperLink ID="linkMonAlavancagem" Visible="false" NavigateUrl="~/MonAlavancagem.aspx" runat="server">> Índice de alavancagem </asp:HyperLink>
<asp:HyperLink ID="linkMonFinanceiroGraf" Visible="false" NavigateUrl="~/MonFinanceiroGraf.aspx" runat="server">> Acompanhamento financeiro</asp:HyperLink>
<asp:HyperLink ID="linkMonFisicoGraf" Visible="false" NavigateUrl="~/MonFisicoGraf.aspx" runat="server">> Acompanhamento físico</asp:HyperLink>
<asp:HyperLink ID="linkMonFinanceiroInd" Visible="false" NavigateUrl="~/MonFinanceiroInd.aspx" runat="server">> Índice de realização financeira </asp:HyperLink>
<asp:HyperLink ID="linkMonFisicoInd" Visible="false" NavigateUrl="~/MonFisicoInd.aspx" runat="server">> Índice de realização física </asp:HyperLink>
<asp:HyperLink ID="linkMonMarcos" Visible="false" NavigateUrl="~/MonMarcos.aspx" runat="server">> Ações</asp:HyperLink>
<asp:HyperLink ID="linkMonMediaDias" Visible="false" NavigateUrl="~/MonMediaDias.aspx" runat="server">> Índice médio de atualização dos Programas</asp:HyperLink>
<asp:HyperLink ID="linkMonParceiros" Visible="false" NavigateUrl="~/MonParceiros.aspx" runat="server">> Quantidade média de parceiros</asp:HyperLink>
<asp:HyperLink ID="linkMonProjetos" Visible="false" NavigateUrl="~/MonProjetos.aspx" runat="server">> Programas analisados</asp:HyperLink>
</div>
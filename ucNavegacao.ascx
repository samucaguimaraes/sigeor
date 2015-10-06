<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNavegacao.ascx.cs" Inherits="ucNavegacao" %>

<div class="navegacao">
<asp:HyperLink ID="linkFiltro" NavigateUrl="~/Projetos.aspx" runat="server">Home</asp:HyperLink>
<asp:HyperLink ID="linkNoticiasProjetos" NavigateUrl="~/NoticiasProjetos.aspx" runat="server">Últimas notícias</asp:HyperLink>
<asp:HyperLink ID="linkAgendaProjetos" NavigateUrl="~/AgendaProjetos.aspx" runat="server">Agenda dos Eixos</asp:HyperLink>
<asp:HyperLink ID="linkProjetos" NavigateUrl="~/Projetos.aspx" runat="server">> Eixos</asp:HyperLink>
<asp:HyperLink ID="linkArvore" NavigateUrl="~/Arvore.aspx" runat="server">> Nome do Eixo</asp:HyperLink>
<asp:HyperLink ID="linkResultado" NavigateUrl="~/Resultados.aspx" runat="server">> Resultados pactuados</asp:HyperLink>
<asp:HyperLink ID="linkFoco" NavigateUrl="~/Foco.aspx" runat="server">> Foco estratégico</asp:HyperLink>
<asp:HyperLink ID="linkPremissas" NavigateUrl="~/Premissa.aspx" runat="server">> Premissas</asp:HyperLink>
<asp:HyperLink ID="linkColaboradores" NavigateUrl="~/Colaboradores.aspx" runat="server">> Colaboradores</asp:HyperLink>
<asp:HyperLink ID="linkAgenda" NavigateUrl="~/Agenda.aspx" runat="server">> Agenda</asp:HyperLink>
<asp:HyperLink ID="linkNoticias" NavigateUrl="~/Noticias.aspx" runat="server">> Noticias</asp:HyperLink>
<asp:HyperLink ID="linkParceiros" NavigateUrl="~/Parceiros.aspx" runat="server">> Parceiros</asp:HyperLink>
<asp:HyperLink ID="linkDocumentos" NavigateUrl="~/Documentos.aspx" runat="server">> Documentos</asp:HyperLink>
<asp:HyperLink ID="linkGerenciamento" NavigateUrl="~/Gerenciamento.aspx" runat="server">> Painel de Gerenciamento</asp:HyperLink>
<asp:HyperLink ID="linkAcao" runat="server" NavigateUrl="~/Acao.aspx">> Detalhamento da Ação</asp:HyperLink>
<asp:HyperLink ID="linkFinanceiro" runat="server" NavigateUrl="~/Financeiro.aspx">> Detalhamento do Financeiro</asp:HyperLink>
<asp:HyperLink ID="linkProduto" runat="server" NavigateUrl="~/Produto.aspx">> Detalhamento do Eixos</asp:HyperLink>
<asp:HyperLink ID="linkAcaoGraf" runat="server" NavigateUrl="~/GraficoAcao.aspx">> Cronograma das Ações</asp:HyperLink>
<asp:HyperLink ID="linkRestricao" runat="server" NavigateUrl="~/Restricao.aspx">> Detalhamento da Restrição</asp:HyperLink>
</div>
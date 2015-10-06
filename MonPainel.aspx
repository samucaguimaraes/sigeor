<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MonPainel.aspx.cs" Inherits="MonPainel" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:NavMon runat="server" ID="navmonFiltro" painel="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">Resumo executivo</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    

<asp:Panel ID="PanelMon" runat="server">
<table cellspacing="6" style="width:100%" >
<tr style="vertical-align:top; background:#FFFFFF">
   <!--LEVI  <td style="width:50%" class="dashed">
        <div class="monTitPainel">Atualização dos Eixos</div>
        <div style="text-align:center">
        <asp:Panel ID="pnAtualizacao" runat="server">
        </asp:Panel>
        <asp:HyperLink ID="linkAtualiza"
        NavigateUrl="~/monMediaDias.aspx" runat="server"></asp:HyperLink>
        </div>
        <br />
    </td> LEVI-->
    <td class="dashed">
        <div class="monTitPainel">Áreas</div>
        <div style="text-align:center"><br />
            <asp:Panel ID="pnAcaoStatus" Width="60%" runat="server">
            </asp:Panel>
            <br />
                        <table style="width:80%;" cellpadding="4" class="tblist">
                        <tr>
                        <td style="width:65%"><b>
                        Status</b></td>
                <!--LEVI <td colspan="2"><b>%</b></td>
                        <td ><b>Qtd</b></td> LEVI-->
                        <td><b> Imprimir</b></td>
                        </tr>

                        <tr>
                        <td style="text-align:left">
                        <span style="background:url('images/B.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkConcluidos"  NavigateUrl="~/MonMarcos.aspx?fl_status=B" runat="server">
                         Concluídos
                        </asp:HyperLink>
                        </td>
                <!--LEVI <td  colspan="2">
                            <asp:Label ID="lblFatiaAzul" runat="server"></asp:Label>
                            </td><td >
                             <asp:Label ID="lblAzul" runat="server"></asp:Label></td>   LEVI-->
                            <td><asp:Button ID="btn" runat="server" Text="Imprimir" OnClientClick="window.open('MonImpressao.aspx?fl_status=B');" /></td >
                        </tr>

                         <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/G.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkPrazos"  NavigateUrl="~/MonMarcos.aspx?fl_status=G" runat="server">
                        Dentro dos prazos previstos
                        </asp:HyperLink>
                        </td>
             <!--LEVI   <td colspan="2">
                        <asp:Label ID="lblFatiaVerde" runat="server"></asp:Label>
                             </td><td>
                                 <asp:Label ID="lblVerde" runat="server"></asp:Label></td>  LEVI-->
                             <td><asp:Button ID="Button1" runat="server" Text="Imprimir" OnClientClick="window.open('MonImpressao.aspx?fl_status=G');" /></td >
                        </tr>

                         <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/Y.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkVencimento"  NavigateUrl="~/MonMarcos.aspx?fl_status=A" runat="server">
                        Próximo do prazo de Vencimento
                        </asp:HyperLink>
                        </td>
            <!--LEVI    <td colspan="2">
                        <asp:Label ID="lblFatiaAmarela2" runat="server"></asp:Label>
                             </td><td>
                                 <asp:Label ID="lblAmarela2" runat="server"></asp:Label></td>  LEVI-->
                             <td><asp:Button ID="ButtonA" runat="server" Text="Imprimir" OnClientClick="window.open('MonImpressao.aspx?fl_status=A');" /></td >
                        </tr> 

						
                       <!-- LEVI <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/Y.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkComRestricoes"  NavigateUrl="~/MonMarcos.aspx?fl_status=Y"  runat="server">
                         Com restrição
                        </asp:HyperLink>
                        </td>
                        <td  colspan="2">
                        <asp:Label ID="lblFatiaAmarela"  runat="server"></asp:Label>
                            </td><td >
                                <asp:Label ID="lblAmarela" runat="server"></asp:Label></td>
                        </tr> LEVI -->

                        <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/R.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkAtraso"  NavigateUrl="~/MonMarcos.aspx?fl_status=R" runat="server">    
                            Com atraso
                        </asp:HyperLink>    
                            </td>
                      <!-- LEVI  <td colspan="2">
                        <asp:Label ID="lblFatiaVermelha" runat="server"></asp:Label>
                            </td><td>
                          <asp:Label ID="lblVermelha" runat="server"></asp:Label></td>  LEVI-->
                          <td><asp:Button ID="Button2" runat="server" Text="Imprimir" OnClientClick="window.open('MonImpressao.aspx?fl_status=R');" /></td >
                        </tr>
						
						
						
						<tr>
						<td  style="text-align:left">                       
                        <asp:HyperLink ID="linkComTodos"  NavigateUrl="~/MonMarcos.aspx?fl_status=T"  runat="server">
                         Todas as Ações
                        </asp:HyperLink>
                        </td>
                       <!-- LEVI  <td colspan="2">
                        </td>
                         <td></td >  LEVI-->
                         <td><asp:Button ID="Button3" runat="server" Text="Imprimir" OnClientClick="window.open('MonImpressao.aspx?fl_status=T');" /></td >      
						</tr>	
						
					<tr>
						<td  style="text-align:left">                       
                        <asp:HyperLink ID="HyperLink1"  NavigateUrl="~/busca.aspx"  runat="server">
                         Orgão Responsável
                        </asp:HyperLink>
                        </td>
                       <!-- LEVI  <td colspan="2">
                        </td>
                         <td></td >  LEVI-->
                         <td><asp:Button ID="Button4" runat="server" Text="Imprimir" OnClientClick="window.open('busca2.aspx');" /></td >      
						</tr>
						
						<tr>
						<td  style="text-align:left">                       
                        <asp:HyperLink ID="HyperLink2"  NavigateUrl="~/buscaCidade.aspx"  runat="server">
                         Local de Atuação
                        </asp:HyperLink>
                        </td>
                       <!-- LEVI  <td colspan="2">
                        </td>
                         <td></td >  LEVI-->
                         <td><asp:Button ID="Button5" runat="server" Text="Imprimir" OnClientClick="window.open('buscaCidadeImpressao.aspx');" /></td >      
						</tr>
						
                            <tr>
                                <td class="auto-style1" style="text-align:left">Busca por data de início</td>
                                <td>
                                    <asp:Button ID="Button6" runat="server" OnClientClick="window.open('buscaDataInicio.aspx');" Text="Pesquisar" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" style="text-align:left">Busca por data de termino</td>
                                <td>
                                    <asp:Button ID="Button7" runat="server" OnClientClick="window.open('buscaDataFim.aspx');" Text="Pesquisar" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" style="text-align:left">Busca por Ano</td>
                                <td>
                                    <asp:Button ID="Button8" runat="server" OnClientClick="window.open('buscaAno.aspx');" Text="Pesquisar" />
                                </td>
                            </tr>
						
                        </table>            
        </div>
    </td>
</tr>



<!-- LEVI
<tr style="vertical-align:top">
    <td class="dashed" >
        <div class="monTitPainel">Acompanhamento Físico - Financeiro</div>
        <div style="text-align:center"><br />
            <asp:Panel ID="pnFisicoFinanceiro" runat="server">
            </asp:Panel>
            <br />
            <asp:HyperLink ID="linkFisico"  NavigateUrl="~/MonFisicoGraf.aspx" Visible="false" runat="server">Acompanhamento Físico</asp:HyperLink> &nbsp;&nbsp;
            <asp:HyperLink ID="linkFinanceiro"  NavigateUrl="~/MonFinanceiroGraf.aspx" runat="server">Acompanhamento Financeiro </asp:HyperLink>
        </div>
    </td>
    <td class="dashed">
        <div class="monTitPainel">Indicadores</div>
        <div style="text-align:center">
        <br />
        <br />
            <table style="width:80%;text-align:left" cellpadding="4" class="tblist">
	            <tr>
	            <td>
	             <asp:HyperLink ID="linkAlavancagem" NavigateUrl="~/MonAlavancagem.aspx" runat="server">
	                - Índice de alavancagem
	             </asp:HyperLink>
	             </td>
	             <td style="width:20%;text-align: center;">
                    <asp:Label ID="lblialavancagem" runat="server" Text="0,00"></asp:Label></td>
	            </tr>

	            <tr>
	            <td>
	            <asp:HyperLink ID="linkParceiros" NavigateUrl="~/MonParceiros.aspx" runat="server">
	               - Quantidade média de parceiros	
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	            	<asp:Label ID="lblimobilizacao" runat="server" Text="0,00"></asp:Label></td>
	            </tr>

	            <tr>
	            <td>
	            <asp:HyperLink ID="linkRealFisica"  NavigateUrl="~/MonFisicoInd.aspx" runat="server">
	               - Índice de realização física
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	                <asp:Label ID="lblifisica" runat="server" Text="0,00"></asp:Label></td>
	            </tr>

	            <tr>
	            <td>
	            <asp:HyperLink ID="linkRealFinanceira"  NavigateUrl="~/MonFinanceiroInd.aspx" runat="server">
	               - Índice de realização financeira
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	                <asp:Label ID="lblifinanceira" runat="server" Text="0,00"></asp:Label></td>
	            </tr>
	            <tr>
	            <td colspan="2">
	              </td>
                </tr>
	            <tr>
	            <td colspan="2" style="text-align:center">
                 <asp:HyperLink ID="linkRestricoes" NavigateUrl="~/MonRestricoes.aspx" runat="server">
	                 Restrições
	             </asp:HyperLink>    
	              </td>
	            </tr>
            </table>    
            <br />
        </div>
    </td>
</tr>  LEVI -->
</table>
        <table>
          <tr>
            <td style="width:55%">

              <!-- LEVI  <b>Parceiro:</b> 
                <asp:Label ID="lblfiltroparceiro" runat="server"></asp:Label><br />   LEVI -->
				
                <b>Eixos:</b> <asp:Label ID="lblfiltroprojeto" runat="server"></asp:Label><br />
				
		<!-- LEVI 				
				<b>Orgão Responsável:</b> <asp:Label ID="lblfiltroresponsavel" runat="server"></asp:Label><br />   
               <b>Tipologia:</b> <asp:Label ID="lblfiltrotipologia" runat="server"></asp:Label><br />
                <b>Fase:</b> <asp:Label ID="lblfiltrofase" runat="server"></asp:Label><br /> 
																									LEVI -->
	        </td>
            <td>
             <!-- LEVI  <strong>Eixos</strong><b> analisados:</b> 
                <asp:HyperLink ID="linkfiltroprojetos" NavigateUrl="~/MonProjetos.aspx" runat="server"></asp:HyperLink>
                <br />
                   <b>Período analisado:</b> <asp:Label ID="lblfiltroperiodo" runat="server"></asp:Label><br />LEVI -->
                <b>Monitoramento gerado em:</b> <asp:Label ID="lblfiltrogerado" runat="server"></asp:Label><br />
            </td>
			
			
			
          </tr>
        </table>
</asp:Panel>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
    <asp:Label ID="lblerror" runat="server"></asp:Label>
</asp:Content>


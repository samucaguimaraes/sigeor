<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Acao.aspx.cs" Inherits="Acao" Title="Untitled Page" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucMarcos.ascx" TagName="Marco" TagPrefix="uc" %>
<%@ Register Src="~/ucProduto.ascx" TagName="Produto" TagPrefix="uc" %>
<%@ Register Src="~/ucFinanceiro.ascx" TagName="Financeiro" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc:Nav ID="ucNav" runat="server" acao="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2">Detalhamento da Ação</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
            
            <fieldset>
            <legend>Área: 
                <asp:Label ID="lblnm_acao" runat="server"></asp:Label></legend>
                
                <table style="width:100%">
                <tr>
                <td style="width:130px">Ação:</td>
                <td><asp:Label ID="lblds_acao" runat="server"></asp:Label></td>
                </tr>
			    <td>Responsável:</td>
                <td><asp:Label ID="lblfinanciadores" runat="server"></asp:Label></td>
                </tr>
				<td>Público Alvo:</td>
                <td><asp:Label ID="lblds_palvo" runat="server"></asp:Label></td>
                </tr>
                <td>Local de Atuação:</td>
                <td><asp:Label ID="lblds_latuacao" runat="server"></asp:Label></td>
                </tr>
               <!-- LEVI  <td>Entidade/Unidade: </td>
                <td><asp:Label ID="lblnm_parceiro" runat="server"></asp:Label></td> LEVI -->
                </tr>
				<tr>
                <td>Início - Término: </td>
                <td><asp:Label ID="lbldt_inicio" runat="server"></asp:Label> - <asp:Label ID="lbldt_fim" runat="server"></asp:Label></td>
                </tr>
                <tr><td><br></td></tr>
				<!-- LEVI<tr>               
			    <td>Coordenador(a)</td>
                <td><asp:Label ID="lblnm_nome" runat="server"></asp:Label></td>
                </tr> LEVI -->
                <tr>				
				<td>Andamento:</td>
                <td><asp:Label ID="lblds_andamento" runat="server"></asp:Label></td>
                </tr>
                </table>
                
            </fieldset>
            
           <uc:Marco runat="server" ID="ucMarco" />
            
            <!-- LEVI <uc:Produto runat="server" ID="ucProduto" />  LEVI -->
            
          <!-- <uc:Financeiro runat="server" ID="ucFinanceiro"  OnPreRender="ucFinanceiro_PreRender" />   LEVI -->
          
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


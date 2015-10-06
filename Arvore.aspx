<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arvore.aspx.cs" Inherits="Arvore" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucLegenda.ascx" TagName="Legenda" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" arvore="true"/> 
<uc:Projeto runat="server" ID="ucProjeto" />

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>  

<asp:Panel ID="PanelArvore" runat="server">

<table style="width:100%" cellspacing="4" cellpadding="4">
<tr>
<td style="width:70%; vertical-align:top">
         <table style="width:100%" border="0" cellpadding="2" cellspacing="0">
         <tr>
        <!-- <td style="vertical-align:top"><div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2">
                <asp:Label ID="lblh_publico" runat="server" Text="Público-alvo"></asp:Label></h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
              <asp:Panel ID="Panelds_publico" runat="server">
               <asp:Label ID="lblds_publico" runat="server"></asp:Label><br />
               <asp:LinkButton ID="linkds_publico" OnClick="link_Click" CommandArgument="ds_publico" runat="server">Editar</asp:LinkButton>
             </asp:Panel>
             <asp:Panel ID="PanelEditds_publico" Visible="false" runat="server">
               <asp:TextBox ID="txtds_publico" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
               <asp:Button CssClass="btn" ID="btnSalvards_publico" OnClick="btnSalvar_Click" CommandArgument="ds_publico" runat="server" Text="Salvar" />
               <asp:Button CssClass="btn" ID="btnCancelards_publico" OnClick="btnCancelar_Click" CommandArgument="ds_publico" runat="server" Text="Cancelar" />
             </asp:Panel>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br /></td>--> 
         <td id="tdUsuarioFinal" runat="server" style="width:50%;vertical-align:top">
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H9">Usuário final</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>
              <asp:Panel ID="Panelds_usuariofinal" runat="server">
               <asp:Label ID="lblds_usuariofinal" runat="server"></asp:Label><br />
               <asp:LinkButton ID="linkds_usuariofinal" OnClick="link_Click" CommandArgument="ds_usuariofinal" runat="server">Editar</asp:LinkButton>
             </asp:Panel>
             <asp:Panel ID="PanelEditds_usuariofinal" Visible="false" runat="server">
               <asp:TextBox ID="txtds_usuariofinal" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
               <asp:Button CssClass="btn" ID="btnSalvards_usuariofinal" OnClick="btnSalvar_Click" CommandArgument="ds_usuariofinal" runat="server" Text="Salvar" />
               <asp:Button CssClass="btn" ID="btnCancelards_usuariofinal" OnClick="btnCancelar_Click" CommandArgument="ds_usuariofinal" runat="server" Text="Cancelar" />
             </asp:Panel> 
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
         
         </td>
         </tr>
         </table>
         
                
                
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H1">Objetivo geral 
               <!-- <span style="margin-left:250px"><asp:HyperLink ID="linkFoco" 
                NavigateUrl="~/Foco.aspx" 
                runat="server"><img src="images/lupa.gif" /> <asp:Label ID="lblh_foco" runat="server" Text="Foco estratégico"></asp:Label></asp:HyperLink>
                </span>-->
             </h2> 
             </div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
 
             <asp:Panel ID="Panelds_objetivo" runat="server">
               <asp:Label ID="lblds_objetivo" runat="server"></asp:Label><br />
               <asp:LinkButton ID="linkds_objetivo" OnClick="link_Click" CommandArgument="ds_objetivo" runat="server">Editar</asp:LinkButton>
             </asp:Panel>
             <asp:Panel ID="PanelEditds_objetivo" Visible="false" runat="server">
               <asp:TextBox ID="txtds_objetivo" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
               <asp:Button CssClass="btn" ID="btnSalvards_objetivo" OnClick="btnSalvar_Click" CommandArgument="ds_objetivo" runat="server" Text="Salvar" />
               <asp:Button CssClass="btn" ID="btnCancelards_objetivo" OnClick="btnCancelar_Click" CommandArgument="ds_objetivo" runat="server" Text="Cancelar" />
             </asp:Panel>  
        <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
 
         <!--<div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H4">Resultados pactuados
             <span style="margin-left:214px"><asp:HyperLink ID="linkPremissas" 
                NavigateUrl="~/Premissa.aspx" 
                runat="server"><img src="images/lupa.gif" /> Premissas</asp:HyperLink>
                </span>
            </h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
           <asp:Datalist ID="dlResultados" RepeatColumns="2" Width="100%" OnItemDataBound="dlResultado_ItemDataBound"
           runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" 
           BorderWidth="1px" CellPadding="3" GridLines="Both">
           <ItemTemplate>
               <asp:Label ID="lblds_resultado" runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle ForeColor="#000066" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1PX" Width="50%" />
           </asp:Datalist>
                                  
             <asp:HyperLink ID="linkResultados" NavigateUrl="~/Resultados.aspx" runat="server">Exibir Completo</asp:HyperLink>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />-->
 
        
      <!--   <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H3">Situação atual </h2></div>
	
	<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
             <asp:Panel ID="Panelds_situacao" runat="server">
               <asp:Label ID="lblds_situacao" runat="server"></asp:Label><br />
               <asp:LinkButton ID="linkds_situacao" OnClick="link_Click" CommandArgument="ds_situacao" runat="server">Editar</asp:LinkButton>
             </asp:Panel>
             <asp:Panel ID="PanelEditds_situacao" Visible="false" runat="server">
               <asp:TextBox ID="txtds_situacao" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
               <asp:Button CssClass="btn" ID="btnSalvards_situacao" OnClick="btnSalvar_Click" CommandArgument="ds_situacao" runat="server" Text="Salvar" />
               <asp:Button CssClass="btn" ID="btnCancelards_situacao" OnClick="btnCancelar_Click" CommandArgument="ds_situacao" runat="server" Text="Cancelar" />
             </asp:Panel> 
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br /> -->
         
        <table style="width:100%">
        <tr>
        <td>
		
        <!-- LEVI
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H5">Rede</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         LEVI -->
        <!-- LEVI <asp:HyperLink ID="linkColaboradores" 
                NavigateUrl="~/Colaboradores.aspx" 
                runat="server"><img src="images/lupa.gif" /> Colaboradores</asp:HyperLink>
        <br /><br /> LEVI -->
    <!-- LEVI    <asp:HyperLink ID="linkParceiros" 
                NavigateUrl="~/Parceiros.aspx" 
                runat="server"><img src="images/lupa.gif" /> Orgão Responsável</asp:HyperLink>                
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />   LEVI -->
        </td>
        <td>
        <div class="heading_container"><div class="heading_right_top"></div>
         <h2 id="H12">Comunicação</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
            <div style="height:49px">
            <table style="width: 100%; font-size:x-small; text-align:center;">
                        <tr>
                            <td style="width:15%" id="tdAgenda" runat="server">
                                
                                <asp:HyperLink ID="linkAgenda" Font-Underline="false" NavigateUrl="~/Agenda.aspx" runat="server" >
                                <img title="Agenda" src="images/ico_agenda.gif" /><br />
                                    Agenda (<asp:Label ID="lblAgenda" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>
                            <td style="width:15%" id="tdNoticias" runat="server">
                                
                                <asp:HyperLink ID="linkNoticias" Font-Underline="false" NavigateUrl="~/Noticias.aspx" runat="server" >
                                <img title="Notícias" src="images/ico_noticia.gif" /><br />
                                    Notícias (<asp:Label ID="lblNoticias" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>      
                      </tr> 
              </table>       
             </div>
             <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
        </td>
        <td style="width:55%" id="tdDocumentos" runat="server">
         
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H6">Documentos</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
            <div style="height:49px">
            <table style="width: 100%; font-size:x-small; text-align:center;">
                        <tr>
                                                                         
                            <td style="width:15%" id="tdFoto" runat="server">
                                
                                <asp:HyperLink ID="linkFoto" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=foto" runat="server" >
                                <img title="Fotos" src="images/ico_foto.gif" /><br />
                                    Fotos (<asp:Label ID="lblFoto" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>
                            <td style="width:15%" id="tdVideo" runat="server">
                                
                                <asp:HyperLink ID="linkVideo" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=video" runat="server">
                                <img title="Vídeos" src="images/ico_video.gif" /><br />
                                Vídeos (<asp:Label ID="lblVideo" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>
                          <!-- LEVI <td style="width:15%" id="tdCronograma" runat="server">
                                
                                <asp:HyperLink ID="linkCronograma" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=cronograma" runat="server" >
                                <img title="Cronogramas" src="images/ico_cronograma.gif" /><br />
                                Cronogramas (<asp:Label ID="lblCronograma" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>  LEVI -->
                            <td style="width:15%" id="tdOutros" runat="server">
                               
                                <asp:HyperLink ID="linkOutros" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=outros" runat="server">
                                 <img title="Outros" src="images/ico_outros.gif" /><br />
                                 Outros (<asp:Label ID="lblOutros" runat="server"></asp:Label>)
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </table>
            </div>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
		 
		</td>
        </tr>
		
		
		
		
        </table> 
</td>



<td style="vertical-align:top">
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H7">Situação Atual</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
             <div class="category_item_book">
             <div style="text-align:center"> <asp:Label ID="lblstatus" runat="server"></asp:Label></div>
             </div>    
				
				<uc:Legenda ID="ucLegenda" runat="server" />  

			 <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div>                
             
			 
			 
			 
			  <div style="text-align:center;">
         <asp:LinkButton ID="btnGerenciamento" runat="server" CssClass="btnG" 
                 onclick="btnGerenciamento_Click"><span class="PostButton">Gerenciamento</span></asp:LinkButton>
				</div>
			<br />
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H8">Responsável</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
             <asp:Panel ID="PanelUsuario" runat="server">
             </asp:Panel>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
			 
			 
			 
			  
			 
  <!-- <div class="category_item_book">
             <div style="text-align:center"><asp:Label ID="lblgrafico" runat="server"></asp:Label></div>
             </div>
             
            <asp:Panel ID="Panelt19_cd_fase" CssClass="category_item_book" runat="server">
               Fase: <asp:Label ID="lblnm_fase" Font-Bold="true" runat="server"></asp:Label>
                    <asp:LinkButton ID="linkt19_cd_fase" OnClick="link_Click" CommandArgument="t19_cd_fase" runat="server">Editar</asp:LinkButton>
            </asp:Panel>
            <asp:Panel ID="PanelEditt19_cd_fase" Visible="false" runat="server">
             Fase:  
                <asp:DropDownList ID="ddlt19_cd_fase" runat="server">
                </asp:DropDownList>
             <br />
             <asp:Button CssClass="btn" ID="btnSalvart19_cd_fase" OnClick="btnSalvar_Click" CommandArgument="t19_cd_fase" runat="server" Text="Salvar" />
              <asp:Button CssClass="btn" ID="btnCancelart19_cd_fase" OnClick="btnCancelar_Click" CommandArgument="t19_cd_fase" runat="server" Text="Cancelar" CausesValidation="false" />
            </asp:Panel>
              
              
              <div class="category_item_book">Data de atualização: <asp:Label ID="lbldt_alterado" Font-Bold="true" runat="server"></asp:Label></div>
             <asp:Panel ID="Paneldt_acordo" CssClass="category_item_book" runat="server">
               Data do acordo de resultados: <asp:Label ID="lbldt_acordo" Font-Bold="true" runat="server"></asp:Label> 
               <asp:LinkButton ID="linkdt_acordo" OnClick="link_Click" CommandArgument="dt_acordo" runat="server">Editar</asp:LinkButton>
             </asp:Panel>
             <asp:Panel ID="PanelEditdt_acordo" Visible="false" runat="server">
                            Data do acordo de resultados <br /><asp:TextBox ID="txtdt_acordo" Width="100px"   runat="server" ReadOnly="true"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar3" runat="server" BackColor="Yellow" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" Buttons="[<][m][y]  [>]" Control="txtdt_acordo"
                            ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
                            Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
                            Move="True" RequiredDate="False" RequiredDateMessage="Selecione Data de Início" Separator="/"
                            Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtdt_acordo"  ErrorMessage="*campo obrigarório"></asp:RequiredFieldValidator>
               <asp:Button CssClass="btn" ID="btnSalvardt_acordo" OnClick="btnSalvar_Click" CommandArgument="dt_acordo" runat="server" Text="Salvar" />
               <asp:Button CssClass="btn" ID="btnCancelardt_acordo" OnClick="btnCancelar_Click" CommandArgument="dt_acordo" runat="server" Text="Cancelar" CausesValidation="false" />
             </asp:Panel> 
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />-->
        
        

         <!-- <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H10">Período</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
          <asp:Panel ID="Paneldt_inicio" runat="server">
           <div class="category_item_book">Início: <asp:Label ID="lbldt_inicio" Font-Bold="true" runat="server"></asp:Label></div>
           <div class="category_item_book">Término: <asp:Label ID="lbldt_fim" Font-Bold="true" runat="server"></asp:Label><br /></div>
           <asp:LinkButton ID="linkdt_inicio" OnClick="link_Click" CommandArgument="dt_inicio" runat="server">Editar</asp:LinkButton>
         </asp:Panel>
         <asp:Panel ID="PanelEditdt_inicio" Visible="false" runat="server">
                        <b>Início:</b> &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtdt_inicio" Width="100px"   runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" BackColor="Yellow" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Buttons="[<][m][y]  [>]" Control="txtdt_inicio"
                        ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
                        Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
                        Move="True" RequiredDate="False" RequiredDateMessage="Selecione Data de Início" Separator="/"
                        Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtdt_inicio"  ErrorMessage="*campo obrigarório"></asp:RequiredFieldValidator>
                        <br />
                        <b>Término:</b>            
                        <asp:TextBox ID="txtdt_fim" Width="100px"   runat="server" ReadOnly="true"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar2" runat="server" BackColor="Yellow" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Buttons="[<][m][y]  [>]" Control="txtdt_fim"
                        ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
                        Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
                        Move="True" RequiredDate="False" RequiredDateMessage="Selecione Data de Término" Separator="/"
                        Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtdt_fim"  ErrorMessage="*campo obrigarório"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="ComparaDatas" runat="server" ControlToCompare="txtdt_inicio"
                            ControlToValidate="txtdt_fim" ErrorMessage="Data de início não pode ser superior a data de término."
                            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
           <asp:Button CssClass="btn" ID="btnSalvardt_inicio" OnClick="btnSalvar_Click" CommandArgument="dt_inicio" runat="server" Text="Salvar" />
           <asp:Button CssClass="btn" ID="btnCancelardt_inicio" CausesValidation="false" OnClick="btnCancelar_Click" CommandArgument="dt_inicio" runat="server" Text="Cancelar" />
         </asp:Panel>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div>  <br />

        <asp:Panel ID="PanelInvestimentos" runat="server">
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H11">Investimentos</h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
             <asp:GridView ID="GridInvestimento" Width="100%" ShowHeader="false" ShowFooter="true"
              CellPadding="3" AutoGenerateColumns="false" FooterStyle-BackColor="#ECECEC" 
              runat="server" GridLines="Both" BackColor="White" OnRowDataBound="GridInvestimento_RowDataBound">
             <Columns>
             <asp:BoundField DataField="nm_parceiro" />
             <asp:BoundField DataField="valor" HtmlEncode="false" DataFormatString="{0:N}"  />
             </Columns>
             </asp:GridView>
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
         </asp:Panel>-->

</td>
</tr>
</table>

 </asp:Panel>
</asp:Content>


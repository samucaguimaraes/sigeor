<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenu.ascx.cs" Inherits="ucMenu" %>


<!--  ****** Infinite Menus Structure & Links ***** -->
<div class="imrcmain0 imgl" style="width:494px;z-index:999999;position:relative;margin:0 0 10px 10px;"><div class="imcm imde" id="imouter0"><ul id="imenus0">
<li class="imatm"  style="width:119px;"><a href="#"><span class="imea imeam"><span></span></span>Eixos</a>

	<div class="imsc"><div class="imsubc" style="width:117px;top:-4px;left:0px;"><ul style="">
	<li><a href="Projetos.aspx">Home</a></li>

	</ul></div></div></li>


 <li class="imatm" id="mMonitora" runat="server" style="width:133px;"><a href="#"><span class="imea imeam"><span></span></span>Monitoramento</a>

	<div class="imsc"><div class="imsubc" style="width:180px;top:-4px;left:0px;"><ul style="">
	<li><a href="Monitoramento.aspx">Visão geral</a></li>
	<!-- <li><a href="relMarcosCriticos.aspx?page=1">Marcos críticos não superados</a></li>
	<li><a href="relMarcosCriticos.aspx?page=2">Marcos críticos revisados</a></li> -->
	</ul></div></div></li>

<!-- LEVI
<li class="imatm"  style="width:127px;"><a href="#"><span class="imea imeam"><span></span></span>Documentos</a>

	<div class="imsc"><div class="imsubc" style="width:180px;top:-4px;left:0px;"><ul style="">
	<li id="umDocGerente"  runat="server"><a href="Manuais/Manual_Usuario.pdf" target="_blank">Manual do usuário</a></li>
	 <li id="umDocArvore" runat="server"><a href="#">Nome do Eixo</a></li>
    <li id="umDocProjeto" runat="server"><a href="#">Eixos completo</a></li>
    <li id="umDocMatriz" runat="server"><a href="#">Matriz de responsabilidade</a></li> 
    <li><a href="Manuais/Manual_Geor.pdf">Manual Geor</a></li> 
    

	</ul></div></div></li> LEVI -->


<li class="imatm" id="umAdministrador" runat="server"  style="width:115px;"><a class="" href="#"><span class="imea imeam"><span></span></span>Administrador</a>

	<div class="imsc"><div class="imsubc" style="width:180px;top:-4px;left:0px;"><ul style="">
	<li id="umEntidade" runat="server"><a href="frmentidade.aspx">Parceiros</a></li>
	<li id="umTipologia" visible="false" runat="server"><a href="frmtipologia.aspx">Tipologias</a></li>
	<li id="umUsuario" runat="server"><a href="frmusuario.aspx">Usuários</a></li>
	<li id="umProjeto" runat="server"><a href="frmprojeto.aspx">Eixos</a></li>
	<li id="umParceiros" runat="server"><a href="frmparceiro.aspx">Parceiros</a></li>
	<!-- LEVI <li id="umLinha" runat="server"><a href="frmlinha.aspx">Linha Decisória</a></li> LEVI -->
	</ul></div></div></li>


</ul><div class="imclear">&nbsp;</div></div></div>


<script language="JavaScript" src="script/ocscript.js" type="text/javascript"></script>

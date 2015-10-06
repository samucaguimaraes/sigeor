<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLegenda.ascx.cs" Inherits="ucLegenda" %>
<table class="tblegenda" cellspacing="0">
    <tr>
        <td colspan="5" class="header">
            Legenda</td>
    </tr>
    <tr style="height:70px; vertical-align:top">
        <td  id="tdG" runat="server">
        <br />
            <img alt="" src="images/G.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento normal dentro dos prazos previstos</td>
        <td  id="tdR" runat="server">
        <br />
            <img alt="" alt="" src="images/R.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento com atraso / Ação não realizada</td>
        <td  id="tdB" runat="server">
        <br />
            <img alt="" src="images/B.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento concluído</td>
      <!--  LEVI   <td id="tdY" runat="server"> 
        <br />
            <img alt="" src="images/Y.gif" style="width: 54px; height: 20px" /><br />
            Próximo do prazo de Vencimento</td> 
       <td id="tdRestricao" runat="server"> 
        <br />
         <b>R<br />
            </b>Eixo possui uma ou mais restrições</td> LEVI -->
    </tr>
    </table>

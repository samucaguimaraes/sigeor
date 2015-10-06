<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resultados.aspx.cs" Inherits="Resultados" Title="Agenda Bahia" %>
<%@ Register Src="~/ucNavegacao.ascx" TagName="Nav" TagPrefix="uc" %>
<%@ Register Src="~/ucNomeProjeto.ascx" TagName="Projeto" TagPrefix="uc" %>
<%@ Register Src="~/ucResultadoAnos.ascx" TagName="Anos" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" resultado="true"/> 

<uc:Projeto runat="server" ID="ucProjeto" />

<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7"><asp:Label ID="lblTitle" runat="server"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    
    <asp:DataList ID="dlResultado" 
    DataKeyField="t14_cd_resultado"
    runat="server" Width="100%"
        OnItemDataBound="dlResultado_ItemDataBound" 
        onselectedindexchanged="dlResultado_SelectedIndexChanged">
    <ItemTemplate>
                    <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
            <asp:ImageButton ID="btnEdit" OnLoad="perfil_Load" CommandName="Select" CausesValidation="false" AlternateText="Editar" ImageUrl="~/images/ico_edit.gif" runat="server" />&nbsp;&nbsp;
            <asp:ImageButton ID="btnExc" OnLoad="perfil_Load"  CommandName="Delete" CausesValidation="false" 
                ImageUrl="~/images/ico_exc.gif" runat="server" AlternateText="Excluir" 
                onclick="btnExc_Click"  OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
   	        <div style="margin:10px"><b>Descrição:</b> 
                <asp:Label ID="lblds_resultado" runat="server"></asp:Label></div>
   	        <div style="margin:10px"><b>Indicador:</b> 
                   <asp:Label ID="lblnm_resultado" runat="server"></asp:Label></div>
	        <div style="margin:10px"><b>Unidade de Medida do Resultado:</b> 
	        <asp:Label ID="lblnm_medida" runat="server"></asp:Label></div>
	        <div class="formElement">
	        
	        <table cellspacing='0' cellpadding='5' rules='all' border='1' style='color:#333333;border-color:#20669B;border-width:1px;border-style:solid;width:40%;border-collapse:collapse;margin:10px;'>
	        <tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>
	        <td colspan='2'>Referência (T zero)</td></tr>
	        <tr style='background-color:#F1F5F5;text-align:center;'>
	        <td align='left' style="width:20%"><b>Período: </b></td>
	        <td align='left' style="width:80%"><asp:Label ID="lblnu_ano" runat="server"></asp:Label></td>
	        </tr>
	        
	        <tr style='background-color:#F1F5F5;text-align:center;'>
	        <td align='left'><b>Valor: </b></td>
	        <td align='left'><asp:Label ID="lblvl_t0" runat="server"></asp:Label></td>
	        </tr>
	        
	        <tr style='background-color:#F1F5F5;text-align:center;'>
	        <td align='left'><b>Unidade: </b></td>
	        <td align='left'><asp:Label ID="lblnm_unid" runat="server"></asp:Label></td>
	        </tr>
	        </table>
	        

	        </div>
                
	        
	        
            
	        <div style="margin:10px">
	            <asp:Panel ID="PanelValores" runat="server">
                </asp:Panel></div>
                
                </td>
                <td>
                    <asp:Panel ID="PanelGrafico" runat="server">
                    </asp:Panel>
                </td>
                </tr>
                </table>
                <hr />
    </ItemTemplate>
    
    </asp:DataList>

<asp:Panel ID="Panel1"  runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Descrição:</label></td>
            <td><asp:TextBox  ID="txtds_resultado" Width="400px" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtds_resultado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Indicador:</label></td>
            <td>
                <asp:TextBox  ID="txtnm_resultado" Width="400px" MaxLength="300" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtnm_resultado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
                </td>
        </tr>
        <tr>
            <td><label>Unidade de Medida do Resultado:</label></td>
            <td><asp:TextBox  ID="txtnm_medida" TextMode="MultiLine" Rows="3" MaxLength="200" Width="400px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="txtnm_medida" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
             </td>
        </tr>
        <tr>
        <td></td>
        <td >
        <table cellspacing='0' cellpadding='5' rules='all' border='1' style='color:#333333;border-color:#20669B;border-width:1px;border-style:solid;width:70%;border-collapse:collapse;'>
	    <tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>
	        <td colspan='2'>Referência (T zero)</td>
	    </tr>
	    <tr style='background-color:#F1F5F5;text-align:left;'>
            <td><label>Valor de referência:</label></td>
            <td>
             <asp:TextBox ID="txtvl_t0" MaxLength="18" Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" Type="Currency"
                ControlToValidate="txtvl_t0" Operator="DataTypeCheck" ErrorMessage="*formato inválido"></asp:CompareValidator></td>
        </tr>
        <tr style='background-color:#F1F5F5;text-align:left;'>
            <td><label>Ano:</label></td>
            <td><asp:TextBox  ID="txtnu_ano" Width="60px" MaxLength="4" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" Operator="DataTypeCheck" runat="server" Display="Dynamic" Type="Integer" 
                ControlToValidate="txtnu_ano" ErrorMessage="*formato inválido"></asp:CompareValidator>
            </td>
        </tr>
        <tr style='background-color:#F1F5F5;text-align:left;'>
            <td><label>Unidade:</label></td>
            <td><asp:TextBox  ID="txtnm_unid" Width="150px" MaxLength="200" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator5" ControlToValidate="txtnm_unid" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr runat="server">
            <td><label>Valores anuais:</label></td>
            <td>
                <asp:Panel ID="PanelValores" runat="server">
                </asp:Panel>
                
                <uc:Anos ID="ucAnos" runat="server" />
            </td>   
        </tr> 
        <tr>
            <td><label>Os valores da tabela:</label></td>
            <td><asp:RadioButtonList ID="rblfl_acumulado"  RepeatLayout="Flow" 
                    RepeatDirection="Horizontal" runat="server">
         <asp:ListItem Text="Estão Acumulados" Value="True"></asp:ListItem>
         <asp:ListItem Text="Devem ser somados" Value="False"></asp:ListItem>
         </asp:RadioButtonList>
         <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="rblfl_acumulado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
         </td>
        </tr>  
        <tr>
            <td colspan="2">
                <div class="buttons">
                <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar"/> 
                <asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False"  />
                </div>
            </td>
        </tr>
        </table>
        </div>
         <asp:HiddenField ID="cod" runat="server" />
      </fieldset>
    </asp:Panel>
    <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


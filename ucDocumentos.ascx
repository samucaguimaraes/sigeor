<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDocumentos.ascx.cs" Inherits="ucDocumentos" %>
<h3>
    <asp:Image ID="imgTipo" runat="server" /> 
    <asp:Label ID="lblTipo" runat="server"></asp:Label>
  
</h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    
    <asp:Panel ID="PanelEdit" Visible="false" runat="server">
       <fieldset style="margin-top:20px">
        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
         <div class="formElement">
         <b>Título: </b>
             <asp:TextBox ID="txtnm_documento" runat="server" Width="325px" MaxLength="300"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic" ControlToValidate="txtnm_documento"></asp:RequiredFieldValidator>
             <br />
             <br />
             <asp:Panel ID="PanelOpcao" Visible="false" runat="server">
                <b>Novo Arquivo? </b>
                 <asp:RadioButtonList ID="rblArquivo" AutoPostBack="true" runat="server" 
                     onselectedindexchanged="rblArquivo_SelectedIndexChanged" 
                     RepeatDirection="Horizontal" RepeatLayout="Flow">
                 <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                 <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                 </asp:RadioButtonList>
                 <br /><br />
             </asp:Panel> 
             <asp:Panel ID="PanelArquivo" runat="server">
             <b>Arquivo:</b>    
             <asp:FileUpload ID="funm_arquivo" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic" ControlToValidate="funm_arquivo"></asp:RequiredFieldValidator>
             <small>*tamanho máximo de arquivo para upload é de 10 MB.</small>
             
             <asp:HyperLink ID="linkModelo" ForeColor="Black" Font-Underline="false"
              runat="server" Target="_blank" Font-Bold="true" />
             </asp:Panel>   
                    <div class="buttons">
                        <br />
                    <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar"/>
                    <asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" />
                    </div>
         </div>
       </fieldset>
       <asp:HiddenField ID="cod" Value="0" runat="server" />
    </asp:Panel>    
    
    <asp:Panel ID="PanelGrid" runat="server">
    <asp:GridView ID="GridDocumentos" CssClass="grid" DataKeyNames="t18_cd_documento" 
    Width="100%" GridLines="Both" ToolTip="" runat="server" CellPadding="4" ForeColor="#333333" 
    AutoGenerateColumns="False" AllowSorting="False" onselectedindexchanged="GridView1_SelectedIndexChanged">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="50px">
            <ItemTemplate>
             <asp:ImageButton ID="ImageButton1"  CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("t18_cd_documento")%>' ImageUrl="~/images/ico_edit.gif" ToolTip="Editar" runat="server" />&nbsp; &nbsp;
             <asp:ImageButton ID="ImageButton2"  CausesValidation="false" CommandArgument='<%#Eval("t18_cd_documento")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            </ItemTemplate>
            </asp:TemplateField>            
            <asp:HyperLinkField DataNavigateUrlFields="nm_arquivo" DataTextField="nm_documento"  Target="_blank"
            DataNavigateUrlFormatString="~/Documentos/{0}" HeaderText="Arquivo" />
            <asp:BoundField DataField="dt_cadastro" DataFormatString="{0:d}" HeaderText="Data Publicação" HtmlEncode="false" />
            </Columns>
            </asp:GridView>  

    <asp:DataList ID="dlFotos" DataKeyField="t18_cd_documento" 
     runat="server" HorizontalAlign="Center" Width="100%" GridLines="Both"
    RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal">
    <ItemTemplate>
     <div class="box_cinza">
            <a href='Documentos/<%#Eval("nm_arquivo")%>' target="_blank">
            <img src='Thumbnail.aspx?NomeImagem=<%#Eval("nm_arquivo")%>&Largura=180' alt='Clique para ver no tamanho original' style="border:none" />
            </a></div>
            <asp:ImageButton ID="ImageButton1" OnLoad="perfil_Load" CausesValidation="false" 
            CommandArgument='<%#Eval("t18_cd_documento")%>' 
            ImageUrl="~/images/ico_edit.gif" ToolTip="Editar" runat="server" 
            onclick="ImageButton1_Click" />&nbsp; 
             <asp:ImageButton ID="ImageButton2" OnLoad="perfil_Load" CausesValidation="false" CommandArgument='<%#Eval("t18_cd_documento")%>' 
             ImageUrl="~/images/ico_exc.gif" ToolTip="Excluir" runat="server"  OnClick="Delete_Click" 
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>
            
            <b><%#Eval("nm_documento")%></b>
            <div style="font-size:11px">(Publicado em <%#Eval("dt_cadastro", "{0:d}")%>)</div>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
    </asp:DataList>
    </asp:Panel>
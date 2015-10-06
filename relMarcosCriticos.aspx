<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="relMarcosCriticos.aspx.cs" Inherits="relMarcosCriticos" Title="Agenda Bahia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div class="heading_container"><div class="heading_right_top"></div>
            <h2 id="H2"><asp:Label ID="lblHeader" runat="server"></asp:Label></h2></div>
         <div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>

     <asp:Panel ID="Panel1" runat="server">
        <b>Parceiro: </b>
         <asp:DropDownList ID="ddlt01_cd_entidade" runat="server">
         </asp:DropDownList> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
          ControlToValidate="ddlt01_cd_entidade"
         runat="server" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
         <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" 
             onclick="btnFiltro_Click" Width="80px" />
             <hr />
     </asp:Panel>
        
        
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t09_cd_marco" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" 
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        <asp:ImageField DataImageUrlField="fl_status" HeaderStyle-Width="56px" DataImageUrlFormatString="~/images/{0}.gif" HeaderText="Status"></asp:ImageField>
        <asp:BoundField DataField="ds_marco" HeaderStyle-Width="40%" HtmlEncode="false" HeaderText="Descrição" />
        <asp:BoundField DataField="nu_esforco" HtmlEncode="false" HeaderText="% esforço" SortExpression="nu_esforco" />
        <asp:BoundField DataField="dt_original" HtmlEncode="false" HeaderText="Data Original"  DataFormatString="{0:dd/MM/yyyy}" SortExpression="dt_original" />
        <asp:BoundField DataField="dt_prevista" HtmlEncode="false" HeaderText="Data Prevista"  DataFormatString="{0:dd/MM/yyyy}" SortExpression="dt_prevista" />
        <asp:BoundField DataField="dt_realizada" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data Realizada" SortExpression="dt_realizada" />
        <asp:BoundField DataField="ds_comentario" HtmlEncode="false" HeaderText="Comentários" />
        </Columns>
    </asp:GridView>
    
        
         <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMarcos.ascx.cs" Inherits="ucMarcos" %>
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t09_cd_marco" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    AllowPaging="true" PageSize="100" OnRowCommand="GridView1_RowCommand"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/ico_edit.gif"  Text="Editar" AccessibleHeaderText="Editar"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Deletar" ImageUrl="~/images/ico_exc.gif"  Text="Excluir" AccessibleHeaderText="Excluir"   ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ImageField DataImageUrlField="fl_status" Visible="false" HeaderStyle-Width="56px" DataImageUrlFormatString="~/images/{0}.gif" HeaderText="Status"></asp:ImageField>
        <%-- LEVI <asp:BoundField DataField="ds_marco" HeaderStyle-Width="40%" HtmlEncode="false" HeaderText="Descrição" /> LEVI --%>
       <%-- LEVI <asp:BoundField DataField="nu_esforco" HtmlEncode="false" HeaderText="% esforço" SortExpression="nu_esforco" /> LEVI --%>
        <asp:TemplateField HeaderText="Data Prevista" SortExpression="dt_prevista">
        <ItemTemplate><%#String.Format("{0:dd/MM/yyyy}", Eval("dt_prevista"))%> 
            <asp:Image ID="imgOriginal" ImageUrl="~/images/ico_versao.gif" 
            runat="server" Visible="false" />
        </ItemTemplate>
        </asp:TemplateField>      
        <asp:BoundField DataField="dt_realizada" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data Realizada" SortExpression="dt_realizada" />
        <asp:BoundField DataField="ds_comentario" HtmlEncode="false" HeaderText="Comentários" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblmsg_mc" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>

        <tr>
         <!--  LEVI <td><label>Descrição:</label></td>
            <td><asp:TextBox  ID="txtds_marco" Width="350px" runat="server"  TextMode="MultiLine"
                    Height="50px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtds_marco" Display="Dynamic" runat="server" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>   
           </td> LEVI -->
        </tr>
                <tr>
                <!-- LEVI <td><label>Esforço:</label></td>
            <td><asp:TextBox  ID="txtnu_esforco" Width="50px" MaxLength="3" runat="server"></asp:TextBox>%
                <asp:RequiredFieldValidator id="RequiredFieldValidator6" 
                    ControlToValidate="txtnu_esforco" ErrorMessage="*campo obrigatório" runat="server" 
                    Display="Dynamic"/>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_esforco"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>    
                    </td> LEVI -->
        </tr>
        <tr>
            <td><label>Data Prevista:</label></td>
            <td><asp:TextBox ID="txtdt_prevista" Width="100px"  runat="server" ReadOnly="true" Text=''></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar3" runat="server"
            Buttons="[<][m][y]  [>]" Control="txtdt_prevista"
            ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
            Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
            Move="True" RequiredDate="False" RequiredDateMessage="Selecione Data Início" Separator="/"
            Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" /> 
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" 
                    ControlToValidate="txtdt_prevista" ErrorMessage="*campo obrigatório" 
                    runat="server" Display="Dynamic"/>
            </td>
        </tr>      
        <tr id="trReal" runat="server">
            <td><label>Data Realizada:</label></td>
            <td><asp:TextBox ID="txtdt_realizada" Width="100px"  runat="server" ReadOnly="true" Text=''></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar4" runat="server"
            Buttons="[<][m][y]  [>]" Control="txtdt_realizada"
            ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
            Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
            Move="True" RequiredDate="false" RequiredDateMessage="Selecione data Fim" Separator="/"
            Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />
            <asp:Button ID="btnDes" runat="server" CssClass="btn" OnClick="Desrealizar_Click" Text="Limpar" /> 
            <asp:CompareValidator ID="DATA_ATUAL" runat="server" ControlToValidate="txtdt_realizada" 
        ErrorMessage="A data de realização não pode ser futura." Operator="LessThanEqual" Type="Date" >
        </asp:CompareValidator>
        
        </td>
        </tr>
        <tr>
            <td><label>Comentários:</label></td>
            <td><asp:TextBox  ID="txtds_comentario" Width="350px" runat="server"  TextMode="MultiLine"
                    Height="50px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td colspan="2">
                <div class="buttons">
                <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar"/>
                <asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" />
                </div>
            </td>
        </tr>
        </table>
        </div>
         <asp:HiddenField ID="cod" runat="server" />
      </fieldset>
    </asp:Panel>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRestricoes.ascx.cs" Inherits="ucRestricoes" %>
<h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelAdd" runat="server">
        <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
    </asp:Panel>
    <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t07_cd_restricao" Width="100%" GridLines="Both"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    AllowPaging="true" PageSize="100" OnRowCommand="GridView1_RowCommand"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True">
        <RowStyle CssClass="RowGrid"  />
        <HeaderStyle  CssClass="headerGrid"/>
        <EditRowStyle BackColor="#CCCCCC" />
        <AlternatingRowStyle CssClass="AlternatingRowGrid" />
        <Columns>
        
        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/ico_edit.gif"  Text="Editar"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Deletar" ImageUrl="~/images/ico_exc.gif"  Text="Excluir"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Superada" ImageUrl="~/images/ok.gif"  Text="Superar" HeaderText="Superar"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif"  Text="Detalhamento"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="ds_restricao" HtmlEncode="false" HeaderText="Restrição" ItemStyle-Width="35%" />
        <asp:BoundField DataField="ds_medida" HtmlEncode="false" HeaderText="Medida de gestão" ItemStyle-Width="35%" />
        <asp:BoundField DataField="dt_limite" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}"  HtmlEncode="false" HeaderText="Data limite" SortExpression="dt_limite" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Restrição:</label></td>
            <td><asp:TextBox  ID="txtds_restricao" Width="350px" runat="server"  TextMode="MultiLine"
                    Height="50px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator6" 
                    ControlToValidate="txtds_restricao" ErrorMessage="*campo obrigatório" runat="server" 
                    Display="Dynamic"/></td>
        </tr>
        <tr>
            <td><label>Medida de gestão:</label></td>
            <td><asp:TextBox  ID="txtds_medida" Width="350px" runat="server"  TextMode="MultiLine"
                    Height="50px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><label>Data limite para solução:</label></td>
            <td><asp:TextBox ID="txtdt_limite" Width="100px"  runat="server" ReadOnly="true" Text=''></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar3" runat="server"
            Buttons="[<][m][y]  [>]" Control="txtdt_limite"
            ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" 
            Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido"
            Move="True" RequiredDate="False" RequiredDateMessage="" Separator="/"
            Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" /> 
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" 
                    ControlToValidate="txtdt_limite" ErrorMessage="*campo obrigatório" 
                    runat="server" Display="Dynamic"/>
            </td>
        </tr>
        
        <tr>
            <td><label>Ação relacionada:</label></td>
            <td>
                <asp:DropDownList ID="ddlt08_cd_acao" runat="server">
                </asp:DropDownList>
                
            </td>
        </tr>     
        <tr>
        <td colspan="2">
            <br />
            <asp:CheckBox ID="cbProjeto"  
            runat="server" Text="Restrição vinculada ao projeto" />
            <br /><br />
        </td>
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
     
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAcoes2014.ascx.cs" Inherits="ucAcoes" %>

    <%@ Register Src="~/ucFinanceiro.ascx" TagName="Financeiro" TagPrefix="uc" %>



        <h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>

        <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

        <asp:Panel ID="PanelAdd" runat="server">

            <span style="padding-right:20px"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
            <span style="padding-right:20px"><asp:Button ID="btn" runat="server" Text="Imprimir" OnClientClick="window.open('Impressao.aspx');" /></span>
            <span style="padding-right:20px"><asp:Button ID="btnG" runat="server" Text="Todas as Ações" OnClientClick="window.open('Gerenciamento.aspx');" /></span>

        </asp:Panel>
        <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" DataKeyNames="t08_cd_acao" Width="100%" EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" PageSize="100" OnRowCommand="GridView1_RowCommand" runat="server"
        CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowSorting="True" EnableModelValidation="True">
            <RowStyle CssClass="RowGrid" />
            <HeaderStyle CssClass="headerGrid" />
            <EditRowStyle BackColor="#CCCCCC" />
            <AlternatingRowStyle CssClass="AlternatingRowGrid" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/ico_edit.gif" Text="Editar" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Image" CommandName="Deletar" ImageUrl="~/images/ico_exc.gif" Text="Excluir" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif" Text="Detalhamento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">

                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:ButtonField>
                <asp:TemplateField HeaderText="Programa" SortExpression="cd_programa" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("cd_programa") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Compromisso" SortExpression="ds_compromisso" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_compromisso") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Responsável" SortExpression="ds_setor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_setor") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Situação" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="56px">
                    <ItemTemplate></ItemTemplate>

                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" Width="56px"></ItemStyle>
                </asp:TemplateField>
                
                

                <asp:TemplateField HeaderText="Ação Descrição" SortExpression="ds_acao" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_acao") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="700px"></ItemStyle>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="SubAção" SortExpression="ds_subacao" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_subacao") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Orçado" SortExpression="vl_orcado" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("vl_orcado") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Fonte" SortExpression="ds_fonte" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_fonte") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Meta" SortExpression="ds_meta" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_meta") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ação N°" SortExpression="nu_acao" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("nu_acao") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                


                <asp:TemplateField HeaderText="Andamento" SortExpression="ds_andamento" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_andamento") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="500px"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Público Alvo" SortExpression="ds_palvo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_palvo") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" Width="700px"></ItemStyle>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Local de Atuação" SortExpression="ds_latuacao" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_latuacao") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>



                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ano" SortExpression="ds_ano" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <%#Eval("ds_ano") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>



                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Período" SortExpression="dt_inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                    <ItemTemplate>
                        Início:
                        <%#String.Format("{0:dd/MM/yyyy}", Eval("dt_inicio"))%>
                            <br /> Término:
                            <%#String.Format("{0:dd/MM/yyyy}", Eval("dt_fim"))%>
                                <br />
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle Width="150px"></ItemStyle>
                </asp:TemplateField>



                <asp:BoundField DataField="dt_alterado" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Atualizado" SortExpression="dt_alterado" ItemStyle-Width="100px">


                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>




            </Columns>
        </asp:GridView>



        <!--<div style="text-align:right; padding-top:5px;">
    <asp:HyperLink ID="linkGraf" Font-Bold="true" NavigateUrl="~/GraficoAcao.aspx" runat="server">Cronograma dos Empreendimentos</asp:HyperLink>
    </div>-->
        <br />
        <asp:Panel ID="Panel1" runat="server">
            <fieldset>
                <legend>
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </legend>
                <div class="formElement">
                    <table>
                        <tr>
                            <td>
                                <label>Área:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnm_acao" Width="350px" MaxLength="200" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator6" ControlToValidate="txtnm_acao" ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Compromisso:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_compromisso" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>N° Ação:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnu_acao" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Ação:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_acao" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Subação:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_subacao" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Orçado:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvl_orcado" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fonte:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_fonte" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Meta:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_meta" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Responsavel:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_setor" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Programa:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcd_programa" Width="350px" runat="server"></asp:TextBox>
                            </td>
                        </tr>



                        <tr>
                            <td>
                                <label>Público Alvo:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_palvo" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Andamento:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_andamento" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Local de Atuação:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_latuacao" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Orgão Responsável:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtds_parceiro" Width="350px" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Ano:</td>
                            <td>
                                <asp:TextBox ID="txtds_ano" Width="98px" MaxLength="200" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <!-- LEVI <td><label>Responsável:</label></td>
            <td>
                <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
                </asp:DropDownList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  
              ControlToValidate="ddlt02_cd_usuario"  
              ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
                
            </td> LEVI -->
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Data de Início:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdt_inicio" runat="server" ReadOnly="true" Text="" Width="100px"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar3" runat="server" Buttons="[&lt;][m][y]  [&gt;]" Control="txtdt_inicio" ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" Format="dd mm yyyy" From-Message="" IncrementY="-220" InvalidDateMessage="Día Inválido" Move="True"
                                RequiredDate="False" RequiredDateMessage="Selecione Data Início" Separator="/" Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdt_inicio" Display="Dynamic" ErrorMessage="*campo obrigatório" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Data de Fim:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdt_fim" Width="100px" runat="server" ReadOnly="true" Text=''></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar4" runat="server" Buttons="[<][m][y]  [>]" Control="txtdt_fim" ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido" Move="True" RequiredDate="false"
                                RequiredDateMessage="Selecione data Fim" Separator="/" Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtdt_fim" ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" />
                                <asp:CompareValidator ID="ComparaDatas" runat="server" ControlToCompare="txtdt_inicio" ControlToValidate="txtdt_fim" ErrorMessage="Data de início não pode ser superior a data de término." Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <label>Data de Aviso:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdt_aviso" Width="100px" runat="server" ReadOnly="true" Text=''></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar5" runat="server" Buttons="[<][m][y]  [>]" Control="txtdt_aviso" ControlFocusOnError="True" Culture="pt-BR" Fade="0.5" IncrementY="-220" Format="dd mm yyyy" From-Message="" InvalidDateMessage="Día Inválido" Move="True" RequiredDate="False"
                                RequiredDateMessage="Selecione Data Aviso" Separator="/" Shadow="True" ShowWeekend="True" Style="z-index: 102" To-Control="txtTo" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="txtdt_aviso" ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" />
                            </td>
                        </tr>




                        <tr>
                            <td colspan="2">
                                <div class="buttons">
                                    <asp:Button CssClass="btn" ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Cadastrar" />
                                    <asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" />
                                    <asp:Label ID="lblme" runat="server" ForeColor="red" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="cod" runat="server" />
            </fieldset>
        </asp:Panel>
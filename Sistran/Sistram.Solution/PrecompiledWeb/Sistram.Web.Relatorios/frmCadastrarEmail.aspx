<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" enabletheming="true" theme="Adm" inherits="frmCadastrarEmail, App_Web_vjligygf" title="Cadastrar Email" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
    <table width="60%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 0%;" align="left" nowrap="nowrap">
                <b>Cadastrar e-mail</b></td>
            <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 25%;" align="right">
                <asp:Button ID="btNovo" runat="server" CssClass="button" Text="Novo" 
                    onclick="btNovo_Click" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td width="50%" height="5" colspan="2">
    <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
        OnSortCommand="RadGrid1_SortCommand" CellPadding="0" 
        Skin="Default2006" PageSize="1000" style="font-size: 8pt" 
                    onitemcommand="RadGrid16_ItemCommand">
        <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
            <RowIndicatorColumn>
                <HeaderStyle Width="10px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="10px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="Nome" EmptyDataText="&amp;nbsp;" HeaderText="Nome"
                    UniqueName="column1" Visible="true">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="Email" EmptyDataText="&amp;nbsp;" HeaderText="Email"
                    UniqueName="column2" Visible="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Horas" EmptyDataText="&amp;nbsp;" HeaderText="Horas Agendadas"
                    UniqueName="column" Visible="true">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Ação" UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <table style="width: 1%">
                            <tr>
                                <td>
                                    <asp:Button ID="btEditar" runat="server" CssClass="button" Text="Editar" 
                                        CommandName='<% # Bind("IDAvisoKPI") %>' CommandArgument="Editar" 
                                        CausesValidation="False"  />
                                </td>
                                <td>
                                    <asp:Button ID="btExcluir" runat="server" CssClass="button" Text="Excluir" 
                                        CommandName='<% # Bind("IDAvisoKPI") %>' CommandArgument="Excluir" 
                                        CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="1%" 
                        Wrap="True" />
                </telerik:GridTemplateColumn>
                
                
                
            </Columns>
            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" Wrap="True" 
                Font-Names="Arial" Font-Size="X-Small" />
        </MasterTableView>
        <AlternatingItemStyle Font-Size="7pt" Height="8px" HorizontalAlign="Left" 
            VerticalAlign="Middle" />
        <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" 
            BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Middle" />
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderStyle Font-Size="7pt" Font-Bold="False" />
        <FilterMenu EnableTheming="True" Skin="Default2006">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
        <StatusBarSettings LoadingText="Carregando..." />
    </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="20">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td height="5" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td>
                        <div id="dvDados" runat="server" style="position:absolute; top:35%; left:35%" visible=false>
                            <asp:Panel ID="Panel2" runat="server" Style="text-align: left;" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1">
                                <table style="width: 400px">
                                    <tr>
                                        <td width="1%">
                                            &nbsp;</td>
                                        <td>
                                            Informe os dados abaixo e selecione a(s) horas que desejar.</td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            Nome:</td>
                                        <td>
                                            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtNome" ErrorMessage="Campo Nome Obrigatorio" 
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            E-Mail:</td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" Width="250px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtEmail" ErrorMessage="E-Mail Invalido" 
                                                SetFocusOnError="True" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtEmail" ErrorMessage="Campo E-mail Obrigatorio" 
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Horas:</td>
                                        <td>
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>                                                
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td align="right">
                                            <asp:Button ID="btSalvar" runat="server" CssClass="button" Text="Salvar" 
                                                onclick="btSalvar_Click" />
                                            &nbsp;<asp:Button ID="btSalvar0" runat="server" CssClass="button" 
                                                Text="Cancelar" onclick="btSalvar0_Click" CausesValidation="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td align="right">
                                            <asp:Label ID="lblCodigo" runat="server" Visible="False">0</asp:Label>
                                            <asp:Label ID="lblerro" runat="server" Font-Size="10pt" ForeColor="#CC0000"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

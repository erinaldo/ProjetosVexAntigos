<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NotasFiscaisAguardEmbarqueFiltro.aspx.cs"
    Inherits="NotasFiscaisAguardEmbarqueFiltro" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                        Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px;
                    font-family: arial; font-size: 8pt; font-weight: bold; text-align: right;" width="1%">
                    Exibir:
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px"
                    visible="True" width="1%">
                    <asp:DropDownList ID="cboTipoDes0" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="7pt" Height="20px" OnSelectedIndexChanged="cboTipoDes0_SelectedIndexChanged"
                        Width="35px">
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem Selected="True">100</asp:ListItem>
                        <asp:ListItem>150</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="0" cellspacing="0" width="100%">
            <tr valign="middle">
                <td class="td" width="20%">
                    Datas:
                </td>
                <td class="td" width="15%">
                    Remetente:
                </td>
                <td class="td" width="15%">
                    Destinatário:
                </td>
                <td class="td" width="1%" align="right">
                    Exibir:
                </td>
            </tr>
            <tr valign="middle">
                <td class="td">
                    <asp:DropDownList ID="cboTipoData" runat="server" Font-Names="Arial" Font-Size="7pt"
                        Height="20px">
                        <asp:ListItem Value="0">Nenhuma</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeEmissao">Emissão</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeEntrada">Entrada</asp:ListItem>
                        <asp:ListItem Value="DOC.PrevisaoDeSaida">Prev. Saída</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeSaida">Saída</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeConclusao">Entrega</asp:ListItem>
                        <asp:ListItem Value="DOC.DataPlanejada">Planejada</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtI" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtF"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <span style="font-size: 7pt">Até:</span><asp:TextBox ID="txtF" runat="server" CssClass="txt"
                        Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="td">
                    <asp:DropDownList ID="cboTipoRem" runat="server" Font-Names="Arial" Font-Size="7pt"
                        Height="20px">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="CADREM.CNPJCPF">CPF/CNPJ</asp:ListItem>
                        <asp:ListItem Value="CADREM.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                        <asp:ListItem Value="CADREM.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="txtRem" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                </td>
                <td class="td">
                    <asp:DropDownList ID="cboTipoDes" runat="server" Font-Names="Arial" Font-Size="7pt"
                        Height="20px">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="CADDES.CNPJCPF">CPF/CNPJ</asp:ListItem>
                        <asp:ListItem Value="CADDES.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                        <asp:ListItem Value="CADDES.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="txtDest" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                </td>
                <td class="td">
                    <table cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Height="20px" OnClick="Button1_Click"
                                    Text="Pesquisar" Width="65px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td_divisoria" colspan="4">
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr align="right">
                        <td>
                            <asp:Button ID="btnGerarReport0" runat="server" CssClass="button" Text="Excel"
                                Width="60px" />
                            <label id="lblsep" runat="server" style="width: 10px">
                                &nbsp;</label>
                            <asp:Button ID="btnPDF0" runat="server" CssClass="button" Text="PDF" Visible="False"
                                Width="40px" />
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                    OnSortCommand="RadGrid1_SortCommand" Width="99%" CellPadding="0" 
                    Skin="Default2006">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--  <telerik:GridBoundColumn DataField="DATADECONCLUSAO" EmptyDataText="&amp;nbsp;" 
                            HeaderText="Data de Entrega" UniqueName="column3" >
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="True"  />
                        </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." UniqueName="column001">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "frmEntregas.aspx?idDoc=" + Eval("IdDocumento") %>'
                                        Target="_blank" Text='<%# Bind("NUMERO") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Serie" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="Série" ItemStyle-HorizontalAlign="Right" UniqueName="column5" Visible="true">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Filial" DataField="Filial"
                                HeaderStyle-HorizontalAlign="Left" UniqueName="column12" Visible="True">
                                <HeaderStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DataDeEmissao" DataFormatString="{0:dd/MM/yyyy}"
                                EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center" HeaderText="Emissão"
                                ItemStyle-HorizontalAlign="Center" UniqueName="column6">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DataDeEntrada" DataFormatString="{0:dd/MM/yyyy}"
                                EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center" HeaderText="Entrada"
                                ItemStyle-HorizontalAlign="Center" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PrevisaoDeSaida" DataFormatString="{0:dd/MM/yyyy}"  Visible="false"
                                EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center" HeaderText="Prev.Saída"
                                ItemStyle-HorizontalAlign="Center" UniqueName="column8">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DataPlanejada" DataFormatString="{0:dd/MM/yyyy}" Visible="false"
                                EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center" HeaderText="Planejada"
                                ItemStyle-HorizontalAlign="Center" UniqueName="column10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CNPJ Remetente" DataField="RemeCnpj"
                                HeaderStyle-HorizontalAlign="Left" UniqueName="column12" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Data de Agendamento" DataField="DataDeAgendamento"  Visible="false"
                                HeaderStyle-HorizontalAlign="Center" UniqueName="column">
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DestCnpj" EmptyDataText="&amp;nbsp;" HeaderText="CNPJ Destinatário"
                                UniqueName="column4" Visible="False">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RemeNome" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                HeaderText="Remetente" ItemStyle-HorizontalAlign="Center" UniqueName="column13">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DestNome" HeaderStyle-HorizontalAlign="Left"
                                EmptyDataText="&amp;nbsp;" HeaderText="Destinatário" UniqueName="column1">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                    <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" BorderWidth="1px" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Size="7pt" Font-Bold="False" />
                    <FilterMenu EnableTheming="True" Skin="Default2006">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                    <StatusBarSettings LoadingText="Carregando..." />
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

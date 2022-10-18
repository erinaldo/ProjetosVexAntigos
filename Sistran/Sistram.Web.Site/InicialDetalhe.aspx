<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InicialDetalhe.aspx.cs"
    Inherits="NotasFiscaisAguardEmbarqueFiltro" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="NOTAS FISCAIS" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px;
                    font-family: arial; font-size: 8pt; font-weight: bold; text-align: right;" width="1%">
                    &nbsp;</td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px"
                    visible="True" width="1%">
                    &nbsp;</td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="0" cellspacing="0" width="100%" border="0" style="background-color:White">
            <tr style="width:99%; text-align:right">
                <td class="td">
                    <table cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" Height="0px" CssClass="button" Text="Exportar" Visible="false"
                                />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width:100%; text-align:right">
                               <asp:Button ID="btnExport" runat="server" Text="Exportar" CssClass="button" 
                                    style="margin-right:15px"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td_divisoria">
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="upda" runat="server">
            <ContentTemplate>
               <%-- <table width="100%">
                    <tr align="right">
                        <td>
                            <label id="lblsep" runat="server" style="width: 10px">
                                &nbsp;</label>
                            </td>
                    </tr>
                </table>--%>
                <div style="width:100%; text-align:center">
                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                    OnSortCommand="RadGrid1_SortCommand" Width="99%" CellPadding="0" 
                    Skin="Default2006" onitemdatabound="RadGrid16_ItemDataBound">
                    <MasterTableView CellPadding="0" GridLines="Both" >
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>                        
                            <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." UniqueName="column001" >
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

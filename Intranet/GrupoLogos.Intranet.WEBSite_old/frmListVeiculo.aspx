<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmListVeiculo.aspx.cs"
    Inherits="Intranet_frmListVeiculo" EnableTheming="True" Theme="Adm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Veículo" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                    <asp:Label ID="lblIdUsuario" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    Placa:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="txt" Width="250px"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEdittxtPlaca" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="AAA-9999" MaskType="None" 
                        TargetControlID="txtPlaca" ClearMaskOnLostFocus="False"  CultureName="pt-BR">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:CheckBox ID="chkAnttVencido" runat="server" Text="ANTT Vencido" />
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:CheckBox ID="chkLicenciamento" runat="server" Text="Licenciamento Vencido" />
                </td>
                <td class="tdpR" nowrap="nowrap" width="60%">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                        OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="button" Font-Names="arial"
                        Font-Size="7pt" Text="Novo" OnClick="Button2_Click" />
                    &nbsp;<asp:Button ID="Button3" runat="server" CssClass="button" Font-Names="arial"
                        Font-Size="7pt" Text="Gerar Relatório" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid17" runat="server" GridLines="None" Skin="Default2006"
                    AllowPaging="True" AllowSorting="True" OnPageIndexChanged="RadGrid17_PageIndexChanged"
                    OnSortCommand="RadGrid17_SortCommand" PageSize="25">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Código" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("idveiculo") %>' NavigateUrl='<% # "frmCadVeiculo.aspx?idveiculo=" + Eval("idveiculo") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="1%" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Código" DataField="IDVeiculo"
                                UniqueName="column1" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Placa" DataField="Placa"
                                UniqueName="column2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Marca" DataField="Marca"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Modelo" DataField="Modelo"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Tipo" DataField="TIPO"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ano" DataField="Ano"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cor" DataField="Cor"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Chassi" DataField="Chassi" Visible="false"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Renavan" DataField="Renavan" Visible="false"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Proprietário" DataField="NOMEPROPRIETARIO"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CPF Proprietário" Visible="false"
                                DataField="CPFPROPRIETARIO" UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Motorista" DataField="NOMEMOTORISTA"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CPF Motorista" DataField="CPFMOTORISTA" Visible="false"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="ANTT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                DataField="AnttVencimento" UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Licencenciamento" DataField="DATADELICENCIAMENTO" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Data Limite Licenc."
                                DataField="DATADELICENCIAMENTO" UniqueName="column" DataFormatString="{0:d}"
                                Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

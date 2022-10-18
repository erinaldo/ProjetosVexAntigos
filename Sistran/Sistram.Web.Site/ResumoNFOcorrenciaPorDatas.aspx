<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ResumoNFOcorrenciaPorDatas.aspx.cs"
    Inherits="ResumoNFOcorrenciaPorDatas" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Filial"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" runat="server" cellpadding="1" cellspacing="0" class="table" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" style="width: 1%" valign="middle">
                    Data de Emissão</td>
                <td class="tdp" nowrap="nowrap" style="width: 1%" valign="middle">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender54" runat="server" 
                        Format="dd/MM/yyyy" TargetControlID="txtI" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI" 
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" style="width: 1%" valign="middle">
                    Data da Ocorrência:</td>
                <td class="tdp" nowrap="nowrap" style="width: 2%" valign="middle">
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" style="width: 60%" valign="baseline">
                    <table align="left" border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="xxx" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                            OnClick="Button1_Click" Text="Pesquisar" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnGerarReport" runat="server" CssClass="button" Font-Names="Arial"
                                            Font-Size="7pt" OnClick="btnGerarReport_Click" Text="Relatório" Width="60px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" Font-Size="7pt"
                                            OnClick="btnPDF_Click" Text="PDF" Visible="False" Width="40px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;<table style="width: 100%">
            <tr>
                <td valign="top" width="25%">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Outlook" MultiPageID="rmp"
                        TabIndex="1" SelectedIndex="1" BorderStyle="None" BorderWidth="1px" Width="100%">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw1" Text="Ocorrência Geral">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw2" Text="Por Responsabilidade" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw3" Text="Por Filial" Visible="False">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw4" Text="Responsabilidade Logos"
                                Visible="False">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw5" Text="Resp. Cliente por Filial "
                                Visible="False">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="PNGrpw6" Text="Resp. Logos por Filial "
                                Visible="False">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="rmp" runat="server" BackColor="White" CssClass="bordaTabs"
                        Width="99%" BorderStyle="Solid" BorderWidth="1px">
                        <telerik:RadPageView ID="rpw1" runat="server" Width="99%">
                            <asp:Panel ID="pnlOcorrenciaGeral" runat="server" BorderStyle="None">
                                <asp:Panel ID="pnlGrafico" runat="server">
                                    <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td valign="top" width="50%" align="center" style="text-align: left">
                                                <b style="text-align: center"><span style="font-size: 8pt">OCORRÊNCIAS GERAIS</span></b>
                                            </td>
                                            <td align="center" style="text-align: left" valign="top" width="10">
                                                &nbsp;
                                            </td>
                                            <td align="center" rowspan="2" style="text-align: left" valign="top" width="5%">
                                                &nbsp;
                                            </td>
                                            <td align="center" style="text-align: left" valign="top" width="10">
                                                &nbsp;
                                            </td>
                                            <td valign="top" width="65%" style="font-size: 8pt; text-align: center">
                                                <b style="text-align: left">&nbsp; GRÁFICO DE OCORRÊNCIAS GERAIS</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" width="35%">
                                                <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                                            </td>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                            <td valign="top" width="65%">
                                                <asp:Panel ID="pnlGrafAcum" runat="server" HorizontalAlign="Center" Width="99%" BorderStyle="None">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" valign="top" width="35%" class="divider">
                                                <hr style="height: 1PX" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" width="35%" align="center" style="text-align: center">
                                                &nbsp; <b><span style="font-size: 8pt">OCORRÊNCIAS POR FILIAL</span></b>
                                            </td>
                                            <td align="center" style="text-align: center" valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="center" rowspan="2" style="text-align: center" valign="top" width="1">
                                                &nbsp;
                                            </td>
                                            <td align="center" style="text-align: center" valign="top">
                                                &nbsp;
                                            </td>
                                            <td valign="top" width="65%" style="font-size: 8pt; text-align: center">
                                                &nbsp; <b>&nbsp; GRÁFICO DE OCORRÊNCIAS POR FILIAL</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" width="35%">
                                                <asp:PlaceHolder ID="PHOcorrenciaPorFilial" runat="server"></asp:PlaceHolder>
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td valign="top" width="65%">
                                                <asp:Panel ID="pnlGrafFilial" runat="server" HorizontalAlign="Center" BorderStyle="None">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpw2" runat="server" Width="99%" Selected="true">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: center; font-size: 8pt" width="49%">
                                        <b style="text-align: left">RESPONSABILIDADE CLIENTE</b>
                                    </td>
                                    <td style="text-align: center; font-size: 8pt">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center; font-size: 8pt" width="5%">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center; font-size: 8pt">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="font-size: 8pt; text-align: center;" width="49%">
                                        <b style="text-align: center">RESPONSABILIDADE TRANSPORTE</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:PlaceHolder ID="phResponsabulidadeCliente" runat="server"></asp:PlaceHolder>
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:PlaceHolder ID="phResponsabulidadeTransporte" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" valign="top">
                                        <hr class="divider" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        <b style="text-align: left">GRÁFICO RESPONSABILIDADE CLIENTE</b>
                                    </td>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center; font-size: 8pt" valign="top">
                                        <b style="text-align: left">GRÁFICO RESPONSABILIDADE TRANSPORTE</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafRespCliente" runat="server" HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafRespTransporte" runat="server" HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" id="divider" class="divider" colspan="5" runat="server">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-size: 8pt" valign="top">
                                        <b>GRÁFICO RESPONSABILIDADE CLIENTE POR FILIAL</b>
                                    </td>
                                    <td style="text-align: center; font-size: 8pt" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center; font-size: 8pt" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center; font-size: 8pt" valign="top">
                                        &nbsp;
                                    </td>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        <b style="text-align: center">GRÁFICO RESPONSABILIDADE TRANSPOTE POR FILIAL</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafRespFilialCliente" runat="server" HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafRespFilialTransporte" runat="server" HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

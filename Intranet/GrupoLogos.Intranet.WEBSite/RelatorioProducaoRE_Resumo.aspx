<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RelatorioProducaoRE_Resumo.aspx.cs"
    Inherits="RelatorioProducaoRE_Resumo" Title="Relatório de Produção por Relação de Entrega" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px; width: 0%;" align="left" nowrap="nowrap">
                    <b>RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA</b>
                </td>
                <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px; width: 25%;" align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="50%" height="5" colspan="2">
                    <table id="novatb" runat="server" __designer:mapid="a" cellpadding="1" cellspacing="0"
                        class="table" width="100%">
                        <tr __designer:mapid="b" valign="baseline">
                            <td __designer:mapid="c" class="tdp" nowrap="nowrap" valign="middle" width="1%">
                                Emissão:
                            </td>
                            <td __designer:mapid="d" class="tdp" nowrap="nowrap" valign="middle" width="1%">
                                <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtI" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                                    UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>
                                &nbsp;Até:
                                <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                                <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>
                            </td>
                            <td __designer:mapid="1d" class="tdp" nowrap="nowrap" style="width: 60%" valign="bottom">
                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                    OnClick="Button1_Click" Text="PESQUISAR" />
                            <input type="button" onclick="javascript:window.open('frmgdrExcelSobras.aspx?t=RE'); return false;" value="Gerar Excel"  class="button" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="50%" colspan="5">
                    <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                        <asp:PlaceHolder ID="PhTotais" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="20">
                    &nbsp;</td>
            </tr>
        </table>
    </center>
</asp:Content>

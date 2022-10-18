<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ResumoPorFilialPrazoNovo, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                    Emiss�o:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 2%">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtI" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    &nbsp;At�:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" valign="baseline" style="width: 60%">
                    <asp:UpdatePanel ID="updBot" runat="server">
                        <ContentTemplate>
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
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnGerarReport" runat="server" CssClass="button" Font-Names="Arial"
                                                    Font-Size="7pt" Text="Relat�rio" Width="60px" OnClick="btnGerarReport_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table id="tbGraf" runat="server" visible="false" border="0" cellpadding="3" 
            cellspacing="3" width="1000">
            <tr>
            <td style="font-weight: 700; text-align: center">N.F. ENTREGUES</td>
            <td style="text-align: center"><b>N.F. N�O ENTREGUES</b></td>
            </tr>
            <tr>
                <td style="font-weight: 700; text-align: center">
                    <asp:Panel ID="pnlEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel ID="pnlNaoEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

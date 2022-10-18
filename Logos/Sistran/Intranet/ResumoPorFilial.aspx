<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ResumoPorFilial.aspx.cs"
    Inherits="ResumoPorFilial" Theme="Adm" EnableTheming="true" %>

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
                    Emissão:
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
                    &nbsp;Até:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" style="width: 2%" valign="middle">
                    &nbsp; Detalhar Transit Time até</td>
                <td class="tdp" nowrap="nowrap" style="width: 2%" valign="middle">
                    <asp:TextBox ID="txtQtdTransitTime" runat="server" CssClass="txtValor" 
                        Width="25px">5</asp:TextBox>
                    &nbsp;Dias</td>
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
                                                    Font-Size="7pt" Text="Relatório" Width="60px" OnClick="btnGerarReport_Click" />
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
            cellspacing="3" width="1100">
            <tr>
            <td style="font-weight: 700; text-align: center">N.F. ENTREGUES<span 
                    style="font-family: Verdana; text-align: right;"><asp:Button ID="Button5" 
                    runat="server" BackColor="White" BorderStyle="None" ForeColor="#999999" 
                    Height="5px" OnClick="Button4_Click" Text="." Width="20px" />
                </span></td>
                
            <td style="text-align: center" id="tdNaoEntregue" runat="server" visible="false"><b>N.F. NÃO ENTREGUES</b></td>
                <td style="text-align: center">
                    <b>LEGENDA</b></td>
            </tr>
            <tr>
                <td style="font-weight: 700; text-align: center" >
                    <asp:Panel ID="pnlEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
                <td id="tdNaoEntregue2" runat="server" visible=false>
                    <asp:Panel ID="pnlNaoEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
                <td valign="top">
                    <br />
                    <br />
                    <br />
                    <table border="1" cellpadding="1" cellspacing="1">
                        <tr>
                            <td width="10">
                                &nbsp;</td>
                            <td nowrap="nowrap" style="font-weight: 700">
                                Transit Time 1</td>
                            <td nowrap="nowrap" style="font-weight: 700">
                                Transit Time 2</td>
                        </tr>
                        <tr>
                            <td class="tdpRVerdanaVerde" nowrap="nowrap">
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td style="text-align: right">
                                Maior 80%</td>
                            <td style="text-align: right">
                                Maior 90%</td>
                        </tr>
                        <tr>
                            <td class="tdpRVerdanaAmarelo">
                                &nbsp;</td>
                            <td style="text-align: right">
                                75% à 80%</td>
                            <td style="text-align: right">
                                85% à 90%</td>
                        </tr>
                        <tr>
                            <td class="tdpRVerdanaVermelho">
                                &nbsp;</td>
                            <td style="text-align: right">
                                Inferior 75%</td>
                            <td style="text-align: right">
                                Inferior 85%</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

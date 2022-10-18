<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmestoquecda_data.aspx.cs"
    Inherits="frmestoquecda_data" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <div id='dvCliente' runat="server" style="position: absolute; top: 35%; left: 20%;
            width: 60%; background-color: White; border: 1px solid black" visible="False">
            <table class="grid">
                <tr>
                    <td class="tdpCabecalho">
                        Cliente:
                        <asp:Label ID="lblDivCliente" runat="server" Text="Label"></asp:Label>
                        <asp:Button ID="btnFoco" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                            Height="0px" Width="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr></hr>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phDados" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <input id="btnImprimir" type="button" value="Imprimir" class="button" onclick="javascript:window.open('PrintPalletsConsolidados.aspx'); return false;" />
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" Font-Size="8pt"
                            OnClick="LinkButton1_Click">Fechar</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        ** Não aplicar para clientes que fracionam seus pallets.
                    </td>
                </tr>
            </table>
        </div>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                    Data:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtI" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    &nbsp;</td>
                <td class="tdp" nowrap="nowrap" valign="baseline">
                    <asp:UpdatePanel ID="updBot" runat="server">
                        <ContentTemplate>
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="x" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                    OnClick="Button1_Click" Text="Pesquisar" />
                                                &nbsp;
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
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
                        <div id="dvCli" runat="server" style="width: 100%; border: 1px solid black; display: none">
                        </div>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
</asp:Content>

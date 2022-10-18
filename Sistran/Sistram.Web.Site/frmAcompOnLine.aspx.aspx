<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAcompOnLine.aspx.aspx.cs"
    Inherits="frmAcompOnLine" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function Expandir(rua) {

            //alert(rua);
            if (document.getElementById(rua).style.display == 'block') {
                document.getElementById(rua).style.display = 'none';
            }
            else {
               document.getElementById(rua).style.display = 'block';
            }
        }
    </script>

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Acompanhamento on-line de entregas"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                    Data de Saída:&nbsp;
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
                    &nbsp;Até:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" style="width: 1%" valign="middle">
                    Filial:</td>
                <td class="tdp" width="1%">
                    </td>
                <td class="tdp" valign="1%">
                    <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        Visible="False">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="CADREM.CNPJCPF">CPF/CNPJ</asp:ListItem>
                        <asp:ListItem Selected="True" Value="CADREM.RAZAOSOCIALNOME">R. Social</asp:ListItem>
                        <asp:ListItem Value="CADREM.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdp" nowrap="nowrap" valign="baseline" style="width:99%">
                    <asp:UpdatePanel ID="updBot" runat="server">
                        <ContentTemplate>
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="x" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                    OnClick="Button1_Click" Text="Pesquisar" />
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
        <table align="center" style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Width="400px">
                        <table style="width: 100%">
                            <tr>
                                <td class="tdpCenter" valign="top">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

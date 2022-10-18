<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HomeIrwin.aspx.cs"
    Inherits="HomeIrwin" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Home Page" Font-Bold="True" Font-Size="14px"></asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="50000" ontick="Timer1_Tick">
                    </asp:Timer>
                </td>
            </tr>
        </table>
        <center>
        <table style="width: 70%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="49%" class="tdpCabecalho" colspan="2" style="width: 98%">
                    <span style="font-family: Verdana; text-align: right; font-size: 8pt;"><b>Período:
                        <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                    </b></span>
                </td>
            </tr>
            <tr>
                <td height="30">
                </td>
                <td height="30">
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlPedidoEmSeparacao1" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center" style="width: 90%" width="45%">
                                    <asp:Label ID="Label9" runat="server" Style="font-weight: 700; font-size: 8pt" 
                                        Text="Notas Fiscais Aguardando Embarque"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:PlaceHolder ID="phAguardandoEmbarque" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel4" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center" colspan="2" nowrap="nowrap">
                                    <asp:Label ID="Label5" runat="server" Style="font-weight: 700; font-size: 8pt" Text="Pedido de Venda em Separação"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:PlaceHolder ID="phPedidoEmSeparacao" runat="server"></asp:PlaceHolder>
                                </td>
                                <td style="text-align: center" valign="top">
                                    <asp:PlaceHolder ID="phPedidoEmSeparacaoPorData" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlPedidoEmSeparacao0" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center" style="width: 90%" width="45%">
                                    <asp:Label ID="Label7" runat="server" Style="font-weight: 700; font-size: 8pt" Text="Entrada de Kit"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:PlaceHolder ID="phEntradaDeKit" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel5" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="Label8" runat="server" Style="font-weight: 700; font-size: 8pt" Text="Entrada para Armazenagem"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:PlaceHolder ID="phEntradaParaArmazenagem1" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    &nbsp;</td>
            </tr>
            
                <tr>
                 <td valign="top">
                     
                     <asp:Panel ID="pnlPedidoEmSeparacao" runat="server">
                         <table style="width: 100%">
                             <tr>
                                 <td align="center" nowrap="nowrap" style="width: 90%" width="45%">
                                     <asp:Label ID="Label6" runat="server" Style="font-weight: 700; font-size: 8pt" 
                                         Text="Pedido de Kit em Separação"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align: center" valign="top">
                                     <asp:PlaceHolder ID="phPedidoMontagemDeKitSeparacao" runat="server">
                                     </asp:PlaceHolder>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                     
                     </td>
                     <td valign="top">
                     
                         &nbsp;</td>
            </tr>
           
        </table>
        </center>
        <table class="grid">
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label10" runat="server" Style="font-weight: 700; font-size: 8pt" 
                        Text="Pedidos em Andamento"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="phPedidosEmAndamento" runat="server"></asp:PlaceHolder>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

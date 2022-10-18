<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MovimentosIrwin.aspx.cs"
    Inherits="MovimentosIrwin" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Movimentos Gerais" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="49%">
                    <span style="font-family: Verdana; text-align: right; font-size: 8pt;"><b>
                    Período:
                    <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                    </b></span>
                </td>
                <td width="49%">
                </td>
            </tr>
            
             <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            
             <tr>
                <td valign="top">
                    <asp:Panel ID="pnlNFEntrda" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Notas Fiscais de Entrada"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phNFEntrada" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="pnlNFSaida" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Notas Fiscais de Saída"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phNFSaida" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            
             <tr>
                <td colspan="2">
                <hr style='width:99%; color:silver; height:1px' />
                    </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlPedidodeVenda" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label3" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Pedido de Venda"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="phPedidoDeVenda" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="pnlPedidoMontagemKit" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label4" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Pedido de Montagem de Kit"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="phPedidoMontagemKit" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            
             <tr>
                <td colspan="2">
                <hr style='width:99%; color:silver; height:1px' />
                    </td>
            </tr>
            
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlPedidoEmSeparacao" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center" width="45%">
                                    <asp:Label ID="Label5" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Pedido de Venda em Separação"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;</td>
                                <td align="center" width="45%">
                                    <asp:Label ID="Label6" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Pedido de Kit em Separação"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phPedidoEmSeparacao" runat="server"></asp:PlaceHolder>
                                </td>
                                <td valign="top">
                                    &nbsp;</td>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phPedidoMontagemDeKitSeparacao" runat="server">
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="pnlPedidoEmSeparacao0" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td align="center" width="45%">
                                    <asp:Label ID="Label7" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Entrada de Kit"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;</td>
                                <td align="center" width="45%">
                                    <asp:Label ID="Label8" runat="server" style="font-weight: 700; font-size: 8pt" 
                                        Text="Entrada para Armazenagem"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phEntradaDeKit" runat="server"></asp:PlaceHolder>
                                </td>
                                <td valign="top">
                                    &nbsp;</td>
                                <td valign="top">
                                    <asp:PlaceHolder ID="phEntradaParaArmazenagem1" runat="server">
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            
             <tr>
                <td colspan="2">
                <hr style='width:99%; color:silver; height:1px' />
                    </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

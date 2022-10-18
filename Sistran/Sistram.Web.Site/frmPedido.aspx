<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmPedido.aspx.cs"
    Inherits="frmPedido" Title="Bem Vindo" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;
            if ((tecla > 47 && tecla < 58)) return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }

        function showOn(obj) {
            //alert(obj);
            //obj = document.getElementById(obj);
            obj.style.visibility = "visible";

        }

        function showOff(obj) {
            //alert(obj);
            //obj = document.getElementById(obj);
            obj.style.visibility = "hidden";

        }

    </script>

    <table border="0" width="100%">
        <tr valign="top" style="height:1%">
            <td rowspan="3">
                <asp:UpdatePanel ID="uplBot" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMenu" runat="server" ScrollBars="Auto" BorderColor="#CCCCCC" 
                            BorderStyle="Solid" BorderWidth="0px">
                            <asp:PlaceHolder ID="PlaceHolderMenuDivisao" runat="server" Visible="False"></asp:PlaceHolder>
                            <br />
                            <asp:PlaceHolder ID="PlaceHolderMenuGrupo" runat="server" Visible="False"></asp:PlaceHolder>
                            <br />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width:100%; ">
                <asp:Panel ID="pnlMensagem" runat="server" Width="100%" HorizontalAlign="Right" CssClass="pnlMenus">
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr align="center">
                            <td align="center" style="width: 100%" height="0">
                                <asp:Label ID="lblMensagem" runat="server" CssClass="txt" BorderStyle="None" Font-Names="Verdana"
                                    Font-Size="7pt" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblMensagem0" runat="server" BorderStyle="None" CssClass="txt" Font-Names="Arial"
                                    Font-Size="7pt" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="center" style="width: 100%">
                                <asp:LinkButton ID="LinkButton1" runat="server" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="0px" Font-Names="Verdana" Font-Size="7pt" Font-Underline="False"
                                    ForeColor="#666666" PostBackUrl="~/frmPedidoLista.aspx" Style="text-align: center"
                                    BackColor="White">Exibir Lista</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlBuscar" runat="server" HorizontalAlign="left" DefaultButton="btnBuscar"
                                    Width="99%">
                                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="left" style="font-family: Verdana; font-size: 7pt; width: 5%">
                                                <b>Buscar:</b>
                                            </td>
                                            <td align="left" style="font-family: Verdana; font-size: 7pt; width: 75%">
                                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="txt" Width="99%"></asp:TextBox>
                                            </td>
                                            <td align="right" width="1%">
                                                <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="OK" OnClick="btnBuscar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <table style="width: 100%">
                    <tr>
                        <td height="20" valign="middle" width="99%">
                            <asp:Button ID="Button3" runat="server" CssClass="button" Font-Size="12pt" 
                                Height="30px" PostBackUrl="~/frmCarrinho.aspx" Text="Fechar Pedido" />
                        </td>
                        <td height="20" valign="top">
                                    <table align="right" border="0" cellpadding="0" cellspacing="0" 
                                        style="width: 100%;">
                                        <tr >
                                            <td style="text-align: center; font-size: 9px;" 
                                                nowrap="nowrap">
                                                <b >Carrinho de Compras</b></td>
                                            <td rowspan="2" style="text-align: center" >
                                                <asp:ImageButton ID="imgCar" runat="server" Height="30px" 
                                                    ImageUrl="~/images/VerCarrinho.jpg" PostBackUrl="frmCarrinho.aspx" 
                                                    Style="text-align: center" ToolTip="Ver Carrinho" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; font-size: 9px;" >
                                                <asp:Label ID="lblcar" runat="server" Style="font-weight: 700;
                                                    color: #FF0000; font-size: 9px" Text="Nenhum Item."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" style="height:99%">
            <td>
               <asp:DataList ID="DataList1" runat="server" BorderColor="Silver" BorderWidth="1px"
                                OnItemDataBound="DataList1_ItemDataBound" RepeatColumns="4" Width="100%" OnItemCommand="DataList1_ItemCommand"
                                BorderStyle="Solid" CellPadding="0">
                                <AlternatingItemStyle BorderColor="Silver" BorderStyle="None" BorderWidth="1px" />
                                <ItemTemplate>
                                    <table style="font-family: Verdana; font-size: 8pt; width: 100%" border="1" cellpadding="1"
                                        cellspacing="1" align="center">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="Image1" runat="server" Height="160px" ImageUrl="~/Images/naoDisponivel.jpg" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblCodigoProdutoCliente" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblTitulo" runat="server" ForeColor="Red" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdDivisao" runat="server" style="text-align: center">
                                                <asp:Label ID="lblDivisao" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblPreco" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="btnADDCart" runat="server" ImageUrl="~/Images/add_03.jpg" CommandName="CAR" />
                                                <asp:Label ID="lblIdClienteDivisao" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
            </td>
        </tr>
    </table>
    <br />
<%--    <table id="tblTotal" runat="server" width="100%">
        <tr id="cabecalho" runat="server" valign="top">
            <td style="width: 85%; text-align: right" nowrap="nowrap" id="tdCarrinhos" runat="server"
                colspan="2">
            </td>
        </tr>
    </table>--%>
    <table style="width: 100%" border="0" bgcolor="White" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 21px; font-family: verdana; font-size: 8pt; text-align: center;">
                <asp:UpdatePanel ID="uplpag" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAjax" runat="server" BackColor="White" HorizontalAlign="Center" style="min-width:659px">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="font-family: Verdana; font-size: 8pt; width: 100%" cellpadding="1"
                                cellspacing="0" class="table" border="0">
                                <tr>
                                    <td colspan="2" style="text-align: right" bgcolor="White">
                                        <asp:Label ID="lblIdCliDivPop" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                                        <asp:Label ID="lblCodigo0" runat="server" Visible="False"></asp:Label>
                                        <asp:Button ID="Button2" runat="server" Height="0px" Width="0px" BackColor="White"
                                            BorderStyle="None" BorderWidth="0px" />
                                        <asp:Label ID="lblIDProdutoClientePop" runat="server" Visible="False"></asp:Label>
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Fechar [X]" OnClick="btnCancelar_Click1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" bgcolor="White">
                                        <asp:ImageButton ID="imgPop" runat="server" Height="300px" ImageUrl="~/Images/naoDisponivel.jpg"
                                            BorderColor="#333333" BorderStyle="Solid" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="tdp7">
                                        <b>Código:</b>
                                    </td>
                                    <td style="text-align: left" class="tdp7">
                                        <asp:Label ID="lblCodigoProdutoCliente" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="tdp7">
                                        <b>Descrição:</b>
                                    </td>
                                    <td style="text-align: left" class="tdp7">
                                        <asp:Label ID="lblTitulo" runat="server" ForeColor="Red" Style="font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="tdp7">
                                        <b>
                                            <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                    <td style="text-align: left" class="tdp7">
                                        <asp:Label ID="lblDisponivel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="tdp7">
                                        <b>Divisão:</b>
                                    </td>
                                    <td style="text-align: left" class="tdp7">
                                        <asp:Label ID="lblDivisaoPop" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" style="text-align: left" class="tdp7">
                                        <b>Código de Barras:</b>
                                    </td>
                                    <td style="text-align: left" class="tdp">
                                        <asp:Label ID="lblMetro0" runat="server" Font-Bold="True" Style="font-family: Arial"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; font-weight: 700" class="tdp7">
                                        Metragem:
                                    </td>
                                    <td style="text-align: left" class="tdp7">
                                        <asp:Label ID="lblMetro" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" style="text-align: left" class="tdp7">
                                        <b>Preço:</b>
                                    </td>
                                    <td style="text-align: left" valign="bottom" class="tdp7">
                                        <asp:Label ID="lblPreco" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" style="text-align: left" class="tdp7">
                                        <b>Quantidade:</b>
                                    </td>
                                    <td style="text-align: left" valign="bottom" class="tdp7">
                                        <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" Height="14px"
                                            onkeypress="return SomenteNumero(event)" Width="25px" Font-Size="7pt">1</asp:TextBox>
                                        <asp:Button ID="btnConfirmar" runat="server" CssClass="button" OnClick="btnConfirmar_Click"
                                            Text="Confirmar" Height="15px" />
                                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalPopup1_Background"  
                                            CancelControlID="btnCancelar" DropShadow="true" PopupControlID="pnlAjax" TargetControlID="Button2">
                                        </asp:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>

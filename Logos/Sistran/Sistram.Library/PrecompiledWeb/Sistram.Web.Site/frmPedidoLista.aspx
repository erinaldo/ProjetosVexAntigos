<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmPedidoLista, App_Web_p3uplnwq" title="Bem Vindo" %>

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

    <table border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr valign="top">
            <td style="width: 2%;" align="left" rowspan="2">
                <asp:UpdatePanel ID="uplBot" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMenu" runat="server" ScrollBars="Auto" CssClass="pnlMenus">
                            <asp:PlaceHolder ID="PlaceHolderMenuDivisao" runat="server"></asp:PlaceHolder>
                            <br />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center" width="86%" valign="top" id="tdTable" runat="server">
                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; font-family: Tahoma;
                    font-size: 7pt;">
                    <tr>
                        <td nowrap="nowrap" style="text-align: left" width="1%" height="50">
                            Ordernar Por:
                        </td>
                        <td style="text-align: left" width="1%" height="50">
                            <asp:LinkButton ID="lnkDescricao" runat="server" Font-Size="7pt" ForeColor="#666666"
                                OnClick="lnkDescricao_Click" Font-Bold="True" Font-Names="Verdana">Descrição</asp:LinkButton>
                        </td>
                        <td style="text-align: left" width="1%" nowrap="nowrap" height="50">
                            <asp:LinkButton ID="lnkCodigo" runat="server" Font-Names="Verdana" Font-Size="7pt"
                                ForeColor="#666666" OnClick="lnkCodigo_Click" Font-Bold="True">Código</asp:LinkButton>
                        </td>
                        <td style="text-align: left" width="99%" nowrap="nowrap" height="50">
                            &nbsp;
                        </td>
                        <td style="text-align: right" height="50">
                            <asp:Panel ID="pnlCar" runat="server" Font-Names="Arial" Font-Size="8px" ForeColor="Black"
                                HorizontalAlign="Right" Style="text-align: right" Visible="false" Width="100%">
                                <table cellpadding="0" cellspacing="0" style="width: 1%;" border="0">
                                    <tr>
                                        <td style="text-align: center" nowrap="nowrap">
                                            Ver Carrinho
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="imgCar" runat="server" ImageUrl="~/images/VerCarrinho.jpg" PostBackUrl="frmCarrinho.aspx?tipo=lista"
                                                Style="text-align: center" ToolTip="Ver Carrinho" Height="35px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblcar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" style="text-align: center" colspan="5">
                            <hr style="border: thin solid #C0C0C0; width: 99%; height: 1px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" width="86%" valign="top" id="tdTable2" runat="server">
                <asp:Panel ID="Panel1" runat="server" Height="550px" Width="100%" ScrollBars="Auto">
                    <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound" OnItemCommand="rpt_ItemCommand">
                        <HeaderTemplate>
                            <table class="tableFundoClaro" border="0" cellpadding="1" cellspacing="1" style="width: 99%">
                                <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg')">
                                    <td class="tdpCabecalho" align="center" style="width: 1%">
                                        FOTO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: left">
                                        DESCRIÇÃO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: left; width: 1%">
                                        CÓDIGO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: left; width: 1%">
                                        DIVISÃO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%">
                                        QUANTIDADE&nbsp;
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%" nowrap="nowrap">
                                        VALOR UNITÁRIO&nbsp;
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%">
                                        DISPONÍVEL
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White">
                                <td class="tdpCenter">
                                    <div class="aumentaIMG">
                                        <a href=""><em>
                                            <asp:ImageButton ID="img" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="15px" />
                                        </em><span>
                                            <%--<asp:ImageButton ID="img1" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="300px" />--%>
                                        </span></a>
                                    </div>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblDescricao" runat="server" Text='<% #Eval("Descricao") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblIdProdutoCliente" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdProduro" runat="server" Text='<% #Eval("IDPRODUTO") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCodigoProduto" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" Text="0" Width="60"></asp:TextBox>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblValorUnitario" runat="server" Text='<% #Eval("ValorUnitario") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldo" runat="server" Text='<% #Eval("SaldoDivisaoDisponivel") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="tdpCenter">
                                    <div class="aumentaIMG">
                                        <a href=""><em>
                                            <asp:ImageButton ID="img" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="15px" />
                                        </em><span>
                                            <%-- <asp:ImageButton ID="img1" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="300px" />--%>
                                        </span></a>
                                    </div>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblDescricao" runat="server" Text='<% #Eval("Descricao") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblIdProdutoCliente" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdProduro" runat="server" Text='<% #Eval("IDPRODUTO") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCodigoProduto" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" Text="0" Width="60"></asp:TextBox>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblValorUnitario" runat="server" Text='<% #Eval("ValorUnitario") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldo" runat="server" Text='<% #Eval("SaldoDivisaoDisponivel") %>'></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton3" runat="server" Height="35px" ImageUrl="~/images/VerCarrinho.jpg"
                                    OnClick="ImageButton2_Click1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lb" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt"
                                    ForeColor="#CC0000" Text="Confirmar"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

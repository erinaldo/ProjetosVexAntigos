<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmPedidoLista.aspx.cs"
    Inherits="frmPedidoLista" Title="Bem Vindo" %>

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
    <asp:Panel ID="pnlMenu" runat="server" ScrollBars="Auto" CssClass="pnlMenus" Visible="False">
        <asp:PlaceHolder ID="PlaceHolderMenuDivisao" runat="server"></asp:PlaceHolder>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="width: 1%">
                Ordenar
            </td>
            <td>
                <asp:LinkButton ID="lnkDescricao" runat="server" Font-Size="7pt" ForeColor="#666666"
                    OnClick="lnkDescricao_Click" Font-Bold="True" Font-Names="Verdana">Descrição</asp:LinkButton>
            </td>
            <td style="width: 1%">
                <asp:LinkButton ID="lnkCodigo" runat="server" Font-Names="Verdana" Font-Size="7pt"
                    ForeColor="#666666" OnClick="lnkCodigo_Click" Font-Bold="True">Código</asp:LinkButton>
            </td>
            <td style="width: 90%; text-align:center;">
                <div style="margin:0 auto; width:350px; border:1px solid silver;">
                    <table style="width:200px">
                        <tr>
                            <td>
                                PESQUISA:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPesq" CssClass="txt" runat="server" Text="" Width="200"> </asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" 
                                    onclick="btnPesquisar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" CssClass="button" Height="30px" 
                    Text="EXCEL" Width="50px" />
            </td>
            <td>
                <div style="width: 62px; border: 1px solid silver; text-align: center;">
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
                </div>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr valign="top">
            <%-- <td style="width: 1px;" align="left" rowspan="2">
                <asp:UpdatePanel ID="uplBot" runat="server">
                    <ContentTemplate>
                       
                            <br />
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>--%>
            <td align="center" width="86%" valign="top" id="tdTable" runat="server">
                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; font-family: Tahoma;
                    font-size: 7pt;">
                   
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
                            <table class="tableFundoClaro" border="1" cellpadding="2" cellspacing="0" style="width: 99%;
                                border: 1px solid silver;">
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
                                    <%--<td class="tdpCabecalho" style="text-align: left; width: 1%; visibility:hidden">
                                        DIVISÃO
                                    </td>--%>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%">
                                        SALDO TOTAL&nbsp;
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 100px">
                                        &nbsp;UA&nbsp;
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: center; width: 1%">
                                        ENDEREÇO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: center; width: 1%">
                                        VALIDADE DO LOTE
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%">
                                        SALDO
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: left; width: 1%">
                                        LOTE
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%">
                                        QUANTIDADE&nbsp;
                                    </td>
                                    <td class="tdpCabecalho" style="text-align: right; width: 1%" nowrap="nowrap">
                                        VALOR UNITÁRIO&nbsp;
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #E0E0E0; cursor: pointer" onmouseover="javascript:this.style.backgroundColor='#F0F8FF'"
                                onmouseout="javascript:this.style.backgroundColor='#E0E0E0'">
                                <td class="tdpCenter">
                                    <div class="aumentaIMG">
                                        <a href=""><em>
                                            <asp:ImageButton ID="img" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="15px" Enabled="false" />
                                        </em><span></span></a>
                                    </div>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblDescricao" runat="server" Text='<% #Eval("Descricao") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblIdProdutoCliente" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdProduro" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblCodigoProduto" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                    <asp:Label ID="lblIDUnidadeDeArmazenagemLote" runat="server" Text='<% #Eval("IDUnidadeDeArmazenagemLote") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblChaveDoProduto" runat="server"  Visible="false"></asp:Label>

                                </td>
                                <%--  <td class="tdp" nowrap="nowrap" style="visibility:hidden">
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>'></asp:Label>
                                </td>--%>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldo" runat="server" Text='<% #Eval("SaldoDivisaoDisponivel") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblUA" runat="server" Text='<% #Eval("IDUnidadeDeArmazenagem") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblEndereco" runat="server" Text='<% #Eval("endereco") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblValidade" runat="server" Text='<% #Eval("Validade") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldoEndereco" runat="server" Text='<% #Eval("Saldo") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblLote" runat="server" Text='<% #Eval("loteReferencia") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" Text="0" Width="60"></asp:TextBox>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblValorUnitario" runat="server" Text='<% #Eval("ValorUnitario") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #F0F0F0; cursor: pointer" onmouseover="javascript:this.style.backgroundColor='#F0F8FF'"
                                onmouseout="javascript:this.style.backgroundColor='#F0F0F0'">
                                <td class="tdpCenter">
                                    <div class="aumentaIMG">
                                        <a href=""><em>
                                            <asp:ImageButton ID="img" runat="server" ImageUrl="~/Images/Maquina.jpg" Height="15px" Enabled="false" />
                                        </em><span></span></a>
                                    </div>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblDescricao" runat="server" Text='<% #Eval("Descricao") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblIdProdutoCliente" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdProduro" runat="server" Text='<% #Eval("IDProdutoCliente") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblCodigoProduto" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblChaveDoProduto" runat="server"  Visible="false"></asp:Label>
                                    <asp:Label ID="lblIDUnidadeDeArmazenagemLote" runat="server" Text='<% #Eval("IDUnidadeDeArmazenagemLote") %>'
                                        Visible="false"></asp:Label>
                                </td>
                                <%--  <td class="tdp" nowrap="nowrap" style="visibility:hidden">
                                    <asp:Label ID="lblIdClienteDivisao" runat="server" Text='<% #Eval("IDCLIENTEDIVISAO") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblDivisao" runat="server" Text='<% #Eval("DIVISAO") %>'></asp:Label>
                                </td>--%>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldo" runat="server" Text='<% #Eval("SaldoDivisaoDisponivel") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblUA" runat="server" Text='<% #Eval("IDUnidadeDeArmazenagem") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblEndereco" runat="server" Text='<% #Eval("endereco") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblValidade" runat="server" Text='<% #Eval("Validade") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblSaldoEndereco" runat="server" Text='<% #Eval("Saldo") %>'></asp:Label>
                                </td>
                                <td class="tdp">
                                    <asp:Label ID="lblLote" runat="server" Text='<% #Eval("loteReferencia") %>'></asp:Label>
                                </td>
                                <td class="tdpR">
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" Text="0" Width="60"></asp:TextBox>
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="lblValorUnitario" runat="server" Text='<% #Eval("ValorUnitario") %>'></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div style="margin: 0 auto; border: 1px solid silver; width: 60px; text-align: center;">
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
    </div>
</asp:Content>

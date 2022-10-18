<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmCarrinho, App_Web_p3uplnwq" title="Untitled Page" enableeventvalidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px" Text="Carrinho de Compras"></asp:Label>
            </td>
        </tr>
    </table>
    &nbsp;<asp:Panel ID="Panel3" runat="server" Height="560px" ScrollBars="Auto">
        <asp:UpdatePanel ID="uplValor" runat="server">
            <Triggers>
            <asp:PostBackTrigger ControlID="btnContinuarComprando"  />
            <asp:PostBackTrigger ControlID="btnFecharPedido" />
            </Triggers>
            <ContentTemplate>
            
                <table style="width: 500">
                    <tr>
                        <td style="text-align: right">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td width="99%" style="text-align: right">
                            <asp:Button ID="btnFecharPedido" runat="server" CssClass="button" OnClick="btnFecharPedido_Click"
                                Text="Fechar Pedido" Width="100px" />
                            &nbsp;<asp:Button ID="btnContinuarComprando" runat="server" CssClass="button" OnClick="Button2_Click"
                                Text="Continuar Comprando" Width="120px" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

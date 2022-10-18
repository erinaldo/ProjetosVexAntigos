<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Inicial, App_Web_vjligygf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" BackColor="White" Width="1024px">
        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); text-align: left;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Tracking" Font-Bold="True" 
                        Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');" width="1%">
                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Relatório" />
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');" width="1%">
                    <asp:Button ID="Button3" runat="server" CssClass="button" Text="PDF" Width="35px" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="pnl" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel3" runat="server" BorderStyle="None" BorderWidth="0px" BorderColor="White">
                    <center>
                        <table border="0" cellpadding="3" cellspacing="3" style="width: 100%; font-size: 8pt;
                            border-style: solid; border-width: 0px">
                            <tr>
                                <td style="text-align: left; font-family: VERdana; font-size: 8pt; font-weight: bold;"
                                    valign="top">
                                    <span style="font-family: Verdana; text-align: right;">PERÍODO:<asp:Button ID="Button5"
                                        runat="server" BackColor="White" BorderStyle="None" ForeColor="#999999" Height="5px"
                                        OnClick="Button4_Click" Text="." Width="20px" />
                                        &nbsp;&nbsp;<asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                                    </span>
                                </td>
                                <td style="font-family: VERdana; font-size: 8pt; font-weight: bold;" valign="top">
                                    NOTAS FISCAIS EMITIDAS: <span style="font-family: Verdana; text-align: right;">
                                        <asp:Label ID="lblqtdEmitidas" runat="server"></asp:Label>
                                    </span>
                                </td>
                                <td valign="top" style="font-family: VERdana; font-size: 8pt; font-weight: bold;
                                    text-align: right;">
                                    <asp:Label ID="lblTempo" runat="server" Font-Names="Verdana" Font-Size="7pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="3">
                                    <hr style="height: 1PX" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; font-size: 8pt;" nowrap="nowrap" width="1%">
                                    <b style="text-align: center">AGUARDANDO EMBARQUE POR DATA GERAL</b>
                                </td>
                                <td style="text-align: center; font-size: 8pt;" nowrap="nowrap" width="1%">
                                    <b style="text-align: center">AGUARDANDO EMBARQUE POR FILIAL</b>
                                </td>
                                <td nowrap="nowrap" style="font-size: 8pt">
                                    <b>GRÁFICO (AGUARDANDO EMBARQUE)</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" valign="top">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </td>
                                <td style="text-align: left" valign="top">
                                    <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                                </td>
                                <td valign="top">
                                    <asp:Panel ID="pnlGrafAguardandoEmbarque" runat="server" BorderStyle="None" Style="text-align: center"
                                        Visible="False" Width="99%">
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="3">
                                    <hr style="height: 1PX" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" width="1%">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="NOTAS FISCAIS EMBARCADAS"
                                        Style="font-size: 8pt"></asp:Label>
                                </td>
                                <td style="text-align: center" width="1%">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                        Text="NOTAS FISCAIS COM OCORRÊNCIA" Style="font-size: 8pt"></asp:Label>
                                </td>
                                <td style="font-size: 8pt">
                                    <b>GRÁFICO GERAL (NÃO ENTREGUE)</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" valign="top">
                                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                </td>
                                <td style="text-align: left" valign="top">
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                                </td>
                                <td valign="top">
                                    <asp:Panel ID="pnlGrafGeral" runat="server" BorderStyle="None" Style="text-align: center"
                                        Visible="False" Width="99%">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; float: left">
            <tr>
                <td style="text-align: right">
                    <asp:Timer ID="Timer1" runat="server" Interval="50000" OnTick="Timer1_Tick">
                    </asp:Timer>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

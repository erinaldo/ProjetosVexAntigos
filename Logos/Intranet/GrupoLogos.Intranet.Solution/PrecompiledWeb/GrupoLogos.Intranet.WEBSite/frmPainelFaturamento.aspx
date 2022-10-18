<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmPainelFaturamento, App_Web_frmpainelfaturamento.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px">Painel De Faturamento</asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px" 
                    width="1%">
                    <input id="Button4" type="button" value="Gerar Excel" class="button" 
                    onclick="javascript:window.open('GerarExcelPainelFaturamento.aspx'); return false;"
                    />
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" 
                            SelectedIndex="1" Skin="Outlook" Width="100%">
                            <Tabs>
                                <telerik:RadTab runat="server" PageViewID="rpvUsuarios" 
                                    Text="Mês Atual (Cliente)" />
                                <telerik:RadTab runat="server" Text="Mês Atual Filial" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="rpvPerfis" 
                                    Text="Histórico de 12 meses" />
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="rmp" runat="server" BackColor="White" 
                            CssClass="bordaTabs" SelectedIndex="1" Width="99%">
                            <telerik:RadPageView ID="rpvUsuarios" runat="server" Width="100%">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvUsuarios0" runat="server" Width="100%">
                                <asp:PlaceHolder ID="phFilial" runat="server"></asp:PlaceHolder>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvPerfis" runat="server" Selected="true" 
                                Style="text-align: left; font-size: 6pt;" Width="100%">
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
</asp:Content>

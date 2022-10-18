<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmLocalizacaoProduto.aspx.cs"
    Inherits="frmLocalizacaoProduto" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">  

    <asp:Panel ID="pnlteste" runat="server" >
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Mapa de Indicadores de Desempenho"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="left" style="width:99%" >
                        <table style="width: 100%">
                            <tr>
                                <td class="tdpCenter" valign="top">
                                    <table class="grid">
                                        <tr>
                                            <td style="text-align: right">
                                                <table class="grid" bgcolor="#CCCCCC">
                                                    <tr>
                                                        <td width="1%">
                                                            Iniciais:</td>
                                                        <td style="text-align: left" width="1%">
                                                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left" width="1%">
                                                            Ordernar:</td>
                                                        <td style="text-align: left">
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cbo" 
                                                                Width="300px">
                                                                <asp:ListItem Value="Descricao">Descrição</asp:ListItem>
                                                                <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp;<asp:Button ID="Button2" runat="server" CssClass="button" 
                                                                onclick="Button2_Click" Text="Pesquisar" />
                                                            &nbsp;<asp:Button ID="Button3" runat="server" CssClass="button" 
                                                                OnClientClick="window.open('kpi/GerarExcelInic.aspx'); return false;" 
                                                                Text="Excel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
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

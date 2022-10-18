<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="RelatorioProducaoRE.aspx.cs" Inherits="RelatorioProducaoRE" Title="Relatório de Produção por Relação de Entrega" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <center>
    <table width="60%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 0%;" align="left" nowrap="nowrap">
                <b>RELATÓRIO DE PRODUÇÃO POR RELAÇÃO DE ENTREGA</b>
            </td>
            <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 25%;" align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="50%" height="5" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="50%" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                    <asp:PlaceHolder ID="PhTotais" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="20">
                <hr />
            </td>
        </tr>
               
    </table>
    </center>
</asp:Content>

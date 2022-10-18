<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="OperacaoCliente.aspx.cs" Inherits="OperacaoCliente" Title="Detalhe Operação Cliente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
    <table width="60%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style=" font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 0%;" align="left" nowrap="nowrap">
                <b>OPERAÇÃO CLIENTE</b>
            </td>
            <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 25%;" align="right">
                <asp:Button ID="btntodos" runat="server" CssClass="button" 
                    Text="EXPORTAR TODAS NOTAS FISCAIS"  
                    Visible="False" />
            &nbsp;<asp:Button ID="Button1" runat="server" CssClass="button" 
                    Text="IMPRIMIR / EXPORTAR" Visible="False" />
            </td>
        </tr>
        <tr>
            <td width="50%" height="5" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="50%" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                    <asp:PlaceHolder ID="phDetalhe" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td height="5" colspan="2">
            </td>
        </tr>
       
    </table>
    </center>
</asp:Content>

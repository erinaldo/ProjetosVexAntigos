<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmEstoqueCdaDetalhe.aspx.cs" Inherits="frmEstoqueCdaDetalhe"
    Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <left>
    <asp:Panel ID="pnlteste" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
        <asp:Panel ID="Panel3" runat="server" style="text-align: left">
            <table style="width: 100%">
            <tr style="text-align:right">
            <td>
            <input type="button" onclick="javascript:window.open('frmgdrExcelSobras.aspx'); return false;" value="Gerar Excel"  />
            </td>
            
            </tr>
                <tr>
                    <td width="25%" valign="top">
                        <table class="grid">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" style="font-weight: 700; font-size: 9pt" 
                                        Text="ARMAZENAGEM"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    
    
    
    
    </asp:Panel>
    </left>
</asp:Content>

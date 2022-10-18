<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="detFaturamentoPaletesRecebidos.aspx.cs"
    Inherits="detFaturamentoPaletesRecebidos" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" style="text-align: center">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px; font-size: 3pt;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="background-image: url('../Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px" 
                                    Text="Pallets Recebidos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            
            <tr>
            <td>
                <table class="table">
                    <tr>
                        <td class="tdp">
                            Resultado:</td>
                        <td class="tdp">
                            <asp:Label ID="lblResultado" runat="server" style="font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" colspan="2">
                        <hr />
                            </td>
                    </tr>
                    <tr>
                        <td class="tdpCenter" colspan="2" style="font-size: 10pt">
                            Detalhes</td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
        
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <input ID="Button1" class="button" type="button" value="Fechar" onclick="javascript:window.close();" /><br />
    </asp:Panel>
</asp:Content>

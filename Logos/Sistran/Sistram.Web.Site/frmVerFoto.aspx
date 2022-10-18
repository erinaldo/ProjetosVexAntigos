<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="frmVerFoto.aspx.cs" Inherits="frmVerFoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="600px" style="text-align: center" 
        Width="100%">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">
                    <%--<asp:Button ID="Button1" runat="server" CssClass="button" Text="Imprimir" />--%>
                    <input id="Button2" type="button" value="Imprimir"  class="button" onclick="window.print()" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image2" runat="server" Height="520px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


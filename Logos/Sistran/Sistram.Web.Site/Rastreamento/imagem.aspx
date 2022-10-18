<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="imagem.aspx.cs" Inherits="imagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="600px" style="text-align: center" 
        Width="100%">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Image ID="Image2" runat="server" Height="520px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


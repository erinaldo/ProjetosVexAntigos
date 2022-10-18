<%@ page language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="imagem, App_Web_ajfeais2" %>

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


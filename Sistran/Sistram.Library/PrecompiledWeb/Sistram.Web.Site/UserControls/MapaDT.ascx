<%@ control language="C#" autoeventwireup="true" inherits="UserControls_MapaDT, App_Web_acmihi2r" %>
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>

<div>
    <table>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                            <cc1:GoogleMap ID="mapa" runat="server" Width="100%" Height="100%">
        </cc1:GoogleMap>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>

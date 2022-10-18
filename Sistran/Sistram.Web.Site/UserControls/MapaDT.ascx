<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MapaDT.ascx.cs" Inherits="UserControls_MapaDT" %>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="objMapa.aspx.cs" Inherits="objMapa" %>
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
    <table>
        <tr>
            <td>
                <artem:GoogleMap ID="GoogleMap1" runat="server" Zoom="4" Latitude="37.559819" Longitude="-122.210540"
                    Width="500px" Height="560px">
                    <Markers>
                        <artem:GoogleMarker Latitude="49.496675" Longitude="-102.65625" Text="BASI" />
                    </Markers>
                    <Directions>
                        <artem:GoogleDirection RoutePanelId="route"  />
                    </Directions>
                </artem:GoogleMap>
            </td>
            <td style="width: 320px; vertical-align: top;">
                <div id="route">
                </div>
            </td>
        </tr>
    </table>
</div>
    </div>
    </form>
</body>
</html>

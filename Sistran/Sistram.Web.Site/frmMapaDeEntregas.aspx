<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMapaDeEntregas.aspx.cs"
    Inherits="frmMapaDeEntregas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">

        function adjustSize() {
            var w = 0;
            w = document.getElementsByTagName('html')[0].clientWidth;
            if (w == 0) {
                w = window.innerWidth
            }
            if (w == 0) {
                w = window.outerWidth
            }
            if (w == 0) {
                w = document.body.clientWidth
            }
            rdiv = document.getElementById('text')
            if (w < 800) {
                rdiv.style.fontSize = "1.0em";
            }
            else if (w < 900) {
                rdiv.style.fontSize = "1.2em";
            }
            else if (w < 1000) {
                rdiv.style.fontSize = "1.4em";
            }
            else {
                rdiv.style.fontSize = "1.6em";
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 99%; height: 99%; text-align: left;">
        <table >
            <tr style="text-align:left">
                <td width="1%">
                    &nbsp;
                </td>
                <td rowspan="2" width="75%"  style="text-align:left">
                    <cc1:GoogleMap ID="mapa" runat="server" Width="100%" Height="100%">
                    </cc1:GoogleMap>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

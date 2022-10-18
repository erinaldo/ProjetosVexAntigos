<%@ page language="C#" autoeventwireup="true" inherits="MapaDtIframe, App_Web_qetdkgfc" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:GoogleMap ID="mapa" runat="server" Width="100%" Height="100%">
        </cc1:GoogleMap>
    </div>
    </form>
</body>
</html>

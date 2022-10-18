<%@ page language="C#" autoeventwireup="true" inherits="popMapaCidade, App_Web_qetdkgfc" uiculture="pt-BR" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Localização da Cidade</title>
    <style type="text/css">
        .button
        {
            border-style: none;
            border-color: inherit;
            border-width: 0px;
            color: Black; /*background-color: #990000;*/
            font-size: 7pt;
            font-weight: bold;
            font-family: Arial;
            background: url(../Imagens/vermelho.jpg);
        }
        #form1
        {
            text-align: center;
        }
    </style>

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

</head>
<body style="margin:0:0:0:0"  >
    <form id="form1" runat="server">
    <div style="width: 99%; height: 99%; text-align: center;">
        <cc1:GoogleMap ID="mapa" runat="server" Width="100%" Height="100%">
        </cc1:GoogleMap>
    </div>
    <asp:Button ID="fechar" runat="server" OnClientClick="javascript:window.close();"
        Text="Fechar" CssClass="button" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="1px" Font-Bold="False" Font-Names="VERDANA" Font-Size="10pt" />
    </form>
</body>
</html>

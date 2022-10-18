<%@ page language="VB" autoeventwireup="false" inherits="boletosBanco, App_Web_uu00qkta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Boleto de Cobrança</title>
</head>
<body onload="javascript:window.resizeTo(1024,768)">
    <form id="form1" runat="server">
    <div style="width: 950px; height: 768px">
    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        
        <br />
        <br />
        <br />
        <table border="0" width="100%">
        <tr align="center">
        
    <td style="text-align:center"><asp:Button ID="btnprint" OnClientClick="javascript:window.print();	" runat=server Text="Imprimir"   /></td>    
        </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

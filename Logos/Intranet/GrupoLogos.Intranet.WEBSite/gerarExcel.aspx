<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gerarExcel.aspx.cs" Inherits="KPI_gerarExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gerando Arquivo Excel ....</title>
    <style type="text/css">
        .style1
        {
            font-size: x-large;
            font-family: Verdana;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position:absolute; top:30%; left:40%; width:300px; height:300px; text-align:center">
    
        <br />
        <br />
    
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Images/AjaxGif/ajax-loader.gif" />
    
        <br />
        <br />
        <strong><span class="style1">Gerando Excel.</span><br class="style1" />
        <span class="style1">Aguarde.....</span></strong></div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="frmPopUpProduto, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produto Ampliado</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <table class="style1" width="300px">
            <tr>
                <td style="text-align: center" height="300">
    <asp:Image ID="Image1" runat="server" Height="300px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="::: Fechar :::" CssClass="button" 
                        onclientclick="window.close();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

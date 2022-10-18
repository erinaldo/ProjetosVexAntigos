<%@ page language="C#" autoeventwireup="true" inherits="PrintPalletsConsolidados, App_Web_printpalletsconsolidados.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impressao</title>
    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body onload="window.print(); window.close();">
    <form id="form1" runat="server">
    <div>
     <div  
            style="background-color:White; border:1px solid black;" 
            >
            <table class="grid">
                <tr>
                    <td class="tdpCabecalho">
                        Cliente:
                        <asp:Label ID="lblDivCliente" runat="server" Text="Label"></asp:Label>
                        <asp:Button ID="btnFoco" runat="server" BackColor="White" BorderColor="White" 
                            BorderStyle="None" Height="0px" Width="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr></hr></td>
                </tr>
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phDados" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        ** Não aplicar para clientes que fracionam seus pallets.</td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

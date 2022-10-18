<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="imgInicialDicate_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload de Imagem</title>
    <link href="../Styles/estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    &nbsp;<table class="style1">
        <tr>
            <td nowrap="nowrap">
                Selecione a Imagem:</td>
            <td>
    <asp:FileUpload ID="fl" runat="server" Font-Names="Verdana" Font-Size="7pt" 
        Width="99%"></asp:FileUpload>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Confirmar" 
        CssClass="button" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Image ID="Image1" runat="server" Height="250px" 
                    ImageUrl="~/imgInicialDicate/inicial.jpg" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Excluir Imagem" 
        CssClass="button" onclick="Button2_Click" Visible="False" />
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>

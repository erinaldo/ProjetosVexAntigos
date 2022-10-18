<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDirecionador.aspx.cs" Inherits="Sistecno.UI.Web.frmDirecionador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table >
            <tr>
                <td>Parceiro(P1)*</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cboParceiro" runat="server">                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="IR" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

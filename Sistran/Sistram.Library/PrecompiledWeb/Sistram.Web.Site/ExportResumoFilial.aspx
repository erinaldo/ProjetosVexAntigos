<%@ page language="C#" autoeventwireup="true" inherits="ExportResumoFilial, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table id="tbGraf" runat="server" visible="false" border="0" cellpadding="3" 
            cellspacing="3" width="1000">
            <tr>
            <td style="font-weight: 700; text-align: center">N.F. ENTREGUES</td>
            <td style="text-align: center"><b>N.F. NÃO ENTREGUES</b></td>
            </tr>
            <tr>
                <td style="font-weight: 700; text-align: center">
                    <asp:Panel ID="pnlEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel ID="pnlNaoEntregues" runat="server" style="text-align: center" 
                        Visible="False" Width="500px">
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    </div>
    </form>
</body>
</html>

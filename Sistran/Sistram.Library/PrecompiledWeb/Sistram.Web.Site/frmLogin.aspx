<%@ page language="C#" autoeventwireup="true" inherits="frmLogin, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN</title>
    <style type="text/css">
        .tct
        {
            border: 1px solid #999999;
            font-family: Arial;
            font-size: 9px;
            height: 15px;
            width: 100px;
            text-transform: uppercase;
        }
        .tbl
        {
            background-color: #E4E4E4;
            height: 100%;
            width: 100%;
        }
        .tbl2
        {
            background-image: url('fundo-login.gif');
            height: 100%;
            width: 100%;
        }
        </style>
</head>
<body bgcolor="#CCCCCC">
    <form id="form1" runat="server">
    <table cellpadding="2" cellspacing="2" runat="server" id="tbl" visible="True" 
        width="100%" bgcolor="#CCCCCC">

        <tr>
            <td style="font-family: Arial; font-size: 8pt; font-weight: bold; width: 5%">
                Login:</td>
            <td>
                <asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 8pt; font-weight: bold;">
                Senha:
            </td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr align="right">
            <td style="text-align: center" colspan="2">
                <asp:Button ID="btnLogar" runat="server" Text="Entrar" OnClick="btnLogar_Click" BorderStyle="Solid"
                    BackColor="#E4E4E4" Font-Size="8" Width="99%" BorderColor="Black" 
                    BorderWidth="1px" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblmensagem" runat="server" ForeColor="Red" 
            style="font-size: 7pt; font-family: Arial"></asp:Label>
    </form>
</body>
</html>

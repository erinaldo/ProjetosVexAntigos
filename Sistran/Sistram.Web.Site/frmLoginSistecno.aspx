<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLoginSistecno.aspx.cs" Inherits="frmLoginSistecno" %>

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
        .style1
        {
            color: #FFFFFF;
        }
        
        body
        {
            text-align:center;
        }
    </style>
</head>
<body bgcolor="496DBA" style="text-align:center">
    <form id="form1" runat="server">
    
    <div style="text-align:center">

    <table cellpadding="2" cellspacing="2" runat="server" id="tbl" visible="True" 
        width="100%">
        <tr>
            <td style="font-family: Arial; font-size: 8pt; font-weight: bold; width: 5%" 
                class="style1">
                Login:</td>
            <td>
                <asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 8pt; font-weight: bold;" 
                class="style1">
                Senha:
            </td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr align="right">
            <td style="text-align: center" colspan="2">
                <asp:Button ID="btnLogar" runat="server" Text="Entrar" OnClick="btnLogar_Click" BorderStyle="Solid"
                    BackColor="#496DBA" Font-Size="8pt" Width="60%" BorderColor="White" 
                    Font-Bold="True" ForeColor="White" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblmensagem" runat="server" ForeColor="Red"></asp:Label>

    </div>
    
    </form>
</body>
</html>

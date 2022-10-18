<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ServicosWEB.Sistranet.Coletor.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistranet WEB</title>
    <link href="../styles/style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border: 1px solid silver; position:absolute; top:25%; margin: 0 auto; width: 90%; left:5%; padding:10px">
            <table style="width: 100%;padding: 5px">
                <tr>
                    <td colspan="2" class="auto-style1" style="text-align: center; font-size:50px">BEM VINDO</td>                    
                </tr>
                <tr class="divider"><td></td></tr><tr class="divider"><td></td></tr>
                <tr class="divider"><td></td></tr><tr class="divider"><td></td></tr>
                <tr>
                    <td style="width:1%; white-space:nowrap; font-size:50px" class="auto-style1">LOGIN:</td>
                    <td>
                        <asp:TextBox ID="txtLogin" runat="server"  Width="100%"></asp:TextBox></td>
                </tr>
                <tr class="divider"><td class="divider"></td></tr><tr class="divider"><td></td></tr>
                <tr class="divider"><td></td></tr>
                <tr>
                    <td class="auto-style1" style="font-size:50px">SENHA:</td>
                    <td>
                        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="100%" ></asp:TextBox></td>
                </tr>
                <tr class="divider"><td></td></tr><tr class="divider"><td></td></tr>
                <tr class="divider"><td></td></tr><tr class="divider"><td></td></tr>
                <tr>
                    <td></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                        <asp:Button ID="btnConfirmar" runat="server" Text="CONFIRMAR" CssClass="button" OnClick="btnConfirmar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

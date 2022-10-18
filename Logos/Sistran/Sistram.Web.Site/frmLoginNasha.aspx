<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLoginNasha.aspx.cs" Inherits="frmLoginNasha" %>

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
            width: 100%;
        }
        .style2
        {
            font-size: 14pt;
            color: #999999;
        }
        .style3
        {
            text-align: center;
        }
    </style>
</head>
<body bgcolor="White">
    <form id="form1" runat="server">
    <div style="border: thin solid #CCCCCC; position: absolute; top: 35%; left: 38%; font-family:Verdana; font-size:10px; width:320px">
        <table cellpadding="2" cellspacing="2" runat="server" id="tbl" visible="True" 
            width="100%" bgcolor="White">
            <tr>
                <td style="font-weight: 700;" colspan="2">
                    <table class="style1" bgcolor="White">
                        <tr>
                            <td width="1%">
                                <asp:Image ID="Image1" runat="server" Height="70px" 
                                    ImageUrl="~/LogoCliente/Nasha.jpg" />
                            </td>
                            <td align="center" class="style2" valign="bottom">
                                SistranWEB<br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#CCCCCC" height="1px">
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 5%; font-weight: 700;">
                    Login:
                </td>
                <td >
                    <asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 700">
                    Senha:
                </td>
                <td>
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr align="right">
                <td style="text-align: right" colspan="2" align="right">
                    <asp:Button ID="btnLogar" runat="server" Text="Entrar" OnClick="btnLogar_Click" 
                        BorderStyle="Solid" Font-Size="8pt" Width="40%" BorderWidth="1px" 
                        Font-Bold="True" Font-Names="Verdana" />
                </td>
            </tr>
            <tr align="left">
                <td style="text-align: left" colspan="2" align="right">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logo.png"  style="cursor:hand"
                        ToolTip="Clique para visitar nosso site" Width="80px" OnClick="window.open('http://www.sistecno.com.br')" />
                </td>
            </tr>
            <tr align="left">
                <td colspan="2" align="right" class="style3">
                    <b>É recomendável desabilitar o bloqueador de pop-ups.</b></td>
            </tr>
        </table>
        <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>

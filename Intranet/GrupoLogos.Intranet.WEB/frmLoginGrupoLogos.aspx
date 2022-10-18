<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLoginGrupoLogos.aspx.cs"
    Inherits="frmLoginGrupoLogos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bem Vindo a Intranet</title>
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
    </style>
    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="White">
    <form id="form1" runat="server">
    <div 
        style="border: thin solid #CCCCCC; position: absolute; top: 30%; left: 35%;
        font-family: Verdana; font-size: 10px; width: 400px; background-color: #FFFFFF;">
        <table cellpadding="2" cellspacing="5" runat="server" id="tbl" visible="True" width="99%"
            bgcolor="White" border="0">
            <tr>
                <td style="font-weight: 700;" colspan="2">
                    <table class="style1" bgcolor="White" width="99%">
                        <tr>
                            <td width="1%">
                                <asp:Image ID="Image1" runat="server" Height="40px" ImageUrl="Imagens/LOGOS-LOGTRANSP-03.jpg" />
                            </td>
                            <td align="right" class="style2" valign="bottom">
                                INTRANET<br />
                            </td>
                        </tr>
                        <tr style="height:1px" >
                            <td colspan="2" bgcolor="#CCCCCC"  >
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 1%; font-weight: 700; text-align: left;">
                    Login:
                </td>
                <td width="98%" style="text-align: left">
                    <asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 700; text-align: left;" width="1%">
                    Senha:
                </td>
                <td width="99%" style="text-align: left">
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 700; text-align: left;" width="1%">
                    &nbsp;
                </td>
                <td width="99%" style="text-align: right">
                    <asp:Button ID="btnLogar" runat="server" Text="Entrar" OnClick="btnLogar_Click" BorderStyle="Solid"
                        Font-Size="8pt" Width="40%" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana"
                        CssClass="button" Height="25px" />
                </td>
            </tr>
            
            <tr>
                <td style="font-weight: 700; text-align: left; width: 100%;" width="1%" 
                    colspan="2">
                    <table class="style1" bgcolor="White" width="99%">
                        <tr style="height:1px" >
                            <td bgcolor="#CCCCCC"  >
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            </table>

            <tr align="left">
                <td style="text-align: center" colspan="2" align="right">
                    Desenvolvido por Sistecno - Visite nosso site
                    <br />
                    <a href='http://www.sistecno.com.br' target="_blank">www.sistecno.com.br</a>
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>

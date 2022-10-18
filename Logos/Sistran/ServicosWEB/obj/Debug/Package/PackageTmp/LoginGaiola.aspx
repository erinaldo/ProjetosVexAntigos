<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginGaiola.aspx.cs" Inherits="ServicosWEB.LoginGaiola" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reposição Roge</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
        }
        
        .direito-inferior
        {
            position: absolute;
  float: right;
  width: 114px;
  height: 76px;
  bottom: 5px;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
            height: 23px;
        }
        .style5
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="direito-inferior">
        <%--<img src="Img/NovoLogoDistecno.jpg" alt="Sistecno" width="100" />--%>
    </div>
    <div style="position: absolute; top: 25%; left: 35%; width: 320px; text-align: right; border-radius:5px;
        border: 1px solid silver; padding: 2px">
        <table class="style1" style="padding: 1px; margin: 1px">
            <tr>
                <td style="text-align: left" width="1%" colspan="2">
                    <table class="style1">
                        <tr>
                            <td>
                     <img src="Img/NovoLogoDistecno.jpg" alt="Sistecno" width="100" />
                            </td>
                            <td style="text-align: right">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Img/roge.jpg" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center" class="style4">
                    <strong>CONTROLE DE GAIOLA</strong>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center" class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" style="width:1%">
                    Login:
                </td>
                <td style="width:99%">
                    <asp:TextBox ID="TextBox1" runat="server" Width="99%" BorderColor="Silver" BorderWidth="1px"
                        CssClass="style2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style5">
                    Senha:
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="99%" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" CssClass="style2" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                    <asp:Button ID="Button1" runat="server" Text="CONFIRMAR" OnClick="Button1_Click"
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" BackColor="White" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

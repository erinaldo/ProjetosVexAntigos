﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComproveiRec.aspx.cs" Inherits="ServicosWEB.ComproveiRec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>BAIXAS DO COMPROVEI</title>
    <style type="text/css">
        body
        {
            font-size: 9px;
            margin: 0 0 0 0;
            font-family: Tahoma;
            white-space: nowrap;
            width:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="style1" style="width:100%">
            <tr>
                <td>
                    <h1 style="text-align: center">
                       BAIXAS DO COMPROVEI
                    </h1>
                    <div style="text-align: right">
                        <asp:Label ID="txtProcessado" runat="server"></asp:Label>
                    </div>

                     <div style="text-align: center">
                         Número:&nbsp;
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                         <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                             Text="PESQUISAR" BackColor="White" BorderColor="#666666" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                        ForeColor="Black" GridLines="Vertical" Width="100%">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
    </asp:Timer>
    </form>
</body>
</html>
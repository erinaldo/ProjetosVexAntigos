<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprovei.aspx.cs" Inherits="ServicosWEB.Comprovei" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ENVIO PARA O COMPROVEI</title>
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
                        ENVIO COMPROVEI
                    </h1>
                    <div style="text-align: right">
                        <asp:Label ID="txtProcessado" runat="server"></asp:Label>
                    </div>

                     <div style="text-align: center">
                         IdDocumento:&nbsp;
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                         <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                             Text="PESQUISAR" BackColor="White" BorderColor="#666666" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                        ForeColor="Black" GridLines="Vertical" Width="100%" 
                        onrowcommand="GridView1_RowCommand">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReenviar" runat="server" BackColor="White" BorderColor="Red" 
                                        BorderStyle="Solid" BorderWidth="1px" CommandArgument="r" 
                                        Text="Reenviar Ao Comprovei" CommandName='<% # Eval("IdDocumento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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

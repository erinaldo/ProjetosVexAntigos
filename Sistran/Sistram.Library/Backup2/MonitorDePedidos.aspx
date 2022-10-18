<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorDePedidos.aspx.cs" Inherits="MonitorDePedidos.MonitorDePedidos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::: Monitor de Pedidos :::</title>
    <link href="estilos.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
        <div style="position: absolute; top: 5px; left: 85%; border: 1px solid silver; background-color: White">
            <table>
                <tr>
                    <td>
                        Tempo:
                        <asp:TextBox ID="txtTempo" runat="server" Text="1" BackColor="White" BorderColor="#999999"
                                     BorderStyle="Solid" CssClass="txt" Width="20px" AutoPostBack="True" 
                                     OnTextChanged="txtTempo_TextChanged"></asp:TextBox>
                        em Minutos
                    </td>
                </tr>
            </table>
            <table id="tblFiliais" border="0" style="display: none; width:100%; background-color:White">
           
           
                <tr style="text-align: left;">
                    <td style="text-align: left;">
                        <asp:CheckBoxList ID="chkFiliais" runat="server" Font-Names="Verdana" 
                                          Font-Size="6pt" Height="1px" RepeatLayout="Flow" BorderColor="White">
                        </asp:CheckBoxList>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div style="position: absolute; top: 5px; left: 10px">
            <img src="Imagens/vex.png" alt="" width="200" />
        </div>
        
        
        <div style="margin-top: 80px">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>                
        </div>
        
        <br />
        <asp:Label ID="lblTempo" runat="server"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:Timer ID="Timer1" runat="server" Interval="180000" OnTick="Timer1_Tick">
        </asp:Timer>
        
    </form>
</body>
</html>

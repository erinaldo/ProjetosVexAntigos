<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPedido.aspx.cs" Inherits="LoginPedido" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
	        width:100px;
            text-transform: uppercase;
        }
        
        .tbl
        {
        	background-color:#E4E4E4;
        	height:100%; 
        	width:50%
        }
        
        .tbl2
        {
        	background-image: url('fundo-login.gif');
        	height:100%; 
        	width:50%
        }
        
        #form1
        {
            text-align: center;
        }
        
    </style>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="1000000" />
    <asp:PlaceHolder ID="plhLogin" runat="server" > 
    <asp:Panel ID="pnl" runat="server">
         <table class="tbl" cellpadding="1" cellspacing="1" border="0" runat="server" id="tbl" width="50%"  >
        <tr><td colspan="2" style="font-size:8pt; font-family:Verdana; font-weight:bold; color:Red">É necessário logar no sistema</td></tr>
        <tr><td></td></tr>
        <tr><td></td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;width:5%" >Login:</td>
        <td><asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="95%"  ></asp:TextBox></td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;">Senha:</td><td><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="95%" ></asp:TextBox></td></tr>
        <tr><td></td><td > <asp:Button id="btnLogar" runat="server" Text="Entrar" onclick="btnLogar_Click" Width="50%" BorderStyle="Solid" BackColor="#E4E4E4" Font-Size="8"/></td></tr>
       
         <tr><td style="height:5px">&nbsp;</td></tr>
        </table>
        </asp:Panel>
         <asp:RoundedCornersExtender Corners="All" Radius="6" TargetControlID="pnl"
                    ID="RoundedCornersExtender1" runat="server">
                </asp:RoundedCornersExtender>
        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    </asp:PlaceHolder>
    </form>
</body>
</html>

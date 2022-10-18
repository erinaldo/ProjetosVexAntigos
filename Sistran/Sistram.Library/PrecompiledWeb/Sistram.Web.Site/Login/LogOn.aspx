<%@ page language="C#" autoeventwireup="true" inherits="Login_LogOn, App_Web_ibbq1xuf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server" style="background-color:Silver"> 
    <asp:PlaceHolder ID="plhLogin" runat="server"> 
         <table cellpadding="1" cellspacing="1"  style="background-color:#E4E4E4;height:100%; width:100%" >
        <tr><td> &nbsp;&nbsp;&nbsp;</td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;width:5%" >Login:</td><td><asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="95%"  ></asp:TextBox></td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;">Senha:</td><td><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="95%" ></asp:TextBox></td></tr>
        <tr><td></td><td > <asp:Button id="btnLogar" runat="server" Text="Entrar" onclick="btnLogar_Click" Width="50%" BorderStyle="Solid" BackColor="#E4E4E4" Font-Size="8"/></td></tr>
        <tr><td> &nbsp;&nbsp;&nbsp;</td></tr>
        
        </table>
    </asp:PlaceHolder>
    </form>
</body>
</html>

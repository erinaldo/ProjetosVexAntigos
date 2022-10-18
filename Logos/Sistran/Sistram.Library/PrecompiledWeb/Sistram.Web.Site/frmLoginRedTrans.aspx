<%@ page language="C#" autoeventwireup="true" inherits="frmLoginRedTrans, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
        	height:100%; 
        	width:100%
        }
        
        .tbl2
        {
        	background-image: url('fundo-login.gif');
        	height:100%; 
        	width:100%
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:PlaceHolder ID="plhLogin" runat="server"> 
         <table class="tbl" cellpadding="1" cellspacing="1" border="0" runat="server" id="tbl"  >
        <tr><td></td></tr>
        <tr><td></td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;width:5%" >Login:</td>
        <td><asp:TextBox ID="txtUser" runat="server" CssClass="tct" Width="95%"  ></asp:TextBox></td></tr>
        <tr><td style="font-family:Arial;font-size:8pt;font-weight:bold;">Senha:</td><td><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="tct" Width="95%" ></asp:TextBox></td></tr>
        <tr><td></td><td > <asp:Button id="btnLogar" runat="server" Text="Entrar" onclick="btnLogar_Click" Width="100%" BorderStyle="Solid"  Font-Size="8pt"/></td></tr>
       
         <tr><td style="height:5px">&nbsp;</td></tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    </asp:PlaceHolder>
    </form>
</body>
</html>

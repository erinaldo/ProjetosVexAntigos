<%@ page language="C#" autoeventwireup="true" inherits="frmFotoMotorista, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button2" runat="server" Text="Fechar" />
&nbsp;<asp:FileUpload ID="fileUploadArquivo" runat="server" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Carregar" />
        <asp:Image ID="Image1" runat="server" />
    
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="frmTeste, App_Web_qetdkgfc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function NewWindow(mypage, myname, w, h, scroll) 
        {
            var winl = (screen.width - w) / 2;
            var wint = (screen.height - h) / 2;
            winprops = 'status=yes,height=' + h + ',width=' + w + ',top=' + wint + ',left=' + winl + ',scrollbars=' + scroll + ',resizable=yes'
            win = window.open(mypage, myname, winprops)
           // if (parseInt(navigator.appVersion) >= 4) { win.window.focus(); }
        }
    
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="lblProprietario" runat="server" Height="130px" 
            TextMode="MultiLine" Width="822px"></asp:TextBox>
        <br />
    
        <asp:Button ID="Button1" runat="server" Text="Button" />
    
    </div>
    </form>
</body>
</html>

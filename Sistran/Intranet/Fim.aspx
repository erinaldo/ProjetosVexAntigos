<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fim.aspx.cs" Inherits="Fim" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type='text/javascript'>
        function FecharJanela() 
        {
            //alert("Processo Concluído!")
            top.window.opener=self;
            top.window.close();

        } 
    </script>
</head>
<body onload="FecharJanela();" >
    <form id="form1" runat="server">
    <div>
       
    </div>
    </form>
</body>
</html>

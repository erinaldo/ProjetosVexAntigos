<%@ Page Language="C#" AutoEventWireup="true"  Inherits="LoginLogos2" Codebehind="LoginLogos2.aspx.cs" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    
    
    <div style="width:100%">

    


        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">LOGÍSTICA</a></li>
            <li><a data-toggle="tab" href="#menu1">TRANSPORTE</a></li>            
        </ul>
        <div class="tab-content">
            <div id="home" class="tab-pane fade in active">
                 <iframe src="http://www1.logoslogistica.com.br/sistranweb" style=" padding:5px; width:100%; border:none" >
    </iframe>
               
            </div>
            <div id="menu1" class="tab-pane fade">
                <iframe src="http://www2.logoslogistica.com.br/sistranweb.net/frmlogin.aspx?b=grupologos" style=" padding:5px; width:100%; border:none" >
    </iframe>
            </div>
           
        </div>
    </div>
    </form>

   </body>
   </html>

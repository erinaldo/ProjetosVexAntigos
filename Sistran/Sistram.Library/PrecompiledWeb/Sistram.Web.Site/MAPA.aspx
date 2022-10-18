<%@ page language="C#" autoeventwireup="true" inherits="MAPA, App_Web_qetdkgfc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script language="JavaScript" type="text/javascript">
<!--
// Script to generate an automatic postBack to the server
var secs
var timerID = null
var timerRunning = false
var delay = 1000
function InitializeTimer()
{
    secs = 2
    StopTheClock()
    StartTheTimer()
}
function StopTheClock()
{
    if(timerRunning)
        clearTimeout(timerID)
    timerRunning = false
}
function StartTheTimer()
{
    if (secs==0)
    {
        StopTheClock()
        document.forms[0].submit()   
    }
    else
    {
        secs = secs - 1
        timerRunning = true
        timerID = self.setTimeout("StartTheTimer()", delay)
    }
}
//-->
</script>

    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
</head>
<%--<body onload="InitializeTimer()"></body>--%>
<body onload="InitializeTimer()">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
       <asp:Timer ID="Timer1" runat="server" Interval="100000" ontick="Timer1_Tick"></asp:Timer>
                
                


        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                aguarede..
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" />
            </Triggers>
            
            <ContentTemplate>
            <asp:Label ID="lbl" runat="server"></asp:Label>
            <br />
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
            
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

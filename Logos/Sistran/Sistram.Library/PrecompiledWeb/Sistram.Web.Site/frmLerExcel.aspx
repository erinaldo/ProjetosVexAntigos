<%@ page language="C#" autoeventwireup="true" inherits="frmLerExcel, App_Web_qetdkgfc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </asp:ToolkitScriptManager>
    <div>
    
        <telerik:RadUpload ID="uplArquivo" runat="server" BackColor="White" 
            BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="0" 
            ControlObjectsVisibility="None" CssClass="telerikTextBox" Height="20px" 
            InputSize="30" Localization-Add="None" OverwriteExistingFiles="True" 
            ReadOnlyFileInputs="false" Skin="Default2006" Width="30%">
            <Localization Add="None" Clear="None" Delete="None" Remove="None" 
                Select="Selecionar" />
        </telerik:RadUpload>
        <asp:Button ID="btnConfirmar" runat="server" onclick="btnConfirmar_Click" 
            Text="Confirma" />
    
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="217px" TextMode="MultiLine" 
            Width="1133px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>

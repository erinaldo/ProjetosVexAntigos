<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Testes_Default2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Chart ID="Chart1" runat="server" BackSecondaryColor="White"
            Width="1px" Height="1px">
            <BorderSkin BackImageTransparentColor="White"
                SkinStyle="Raised" BackColor="White" BorderColor="White" />
            <Series>
                <asp:Series Name="Series1">
                </asp:Series>
                <asp:Series Name="Series2"></asp:Series>
                <asp:Series Name="SeriesLinha"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea BackColor="White" Name="ChartArea1" BorderColor="White" 
                    ShadowColor="White">
                    <AxisY LabelAutoFitMaxFontSize="8" LabelAutoFitMinFontSize="5">
                    </AxisY>
                    <AxisX IntervalAutoMode="VariableCount" LabelAutoFitMaxFontSize="7" 
                        LabelAutoFitMinFontSize="5">
                    </AxisX>
                    <AxisX2 LabelAutoFitMaxFontSize="8" LabelAutoFitMinFontSize="5">
                    </AxisX2>
                    <AxisY2 LabelAutoFitMaxFontSize="8" LabelAutoFitMinFontSize="5">
                    </AxisY2>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Garar Run-Time" />
    <br />
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    </form>
</body>
</html>

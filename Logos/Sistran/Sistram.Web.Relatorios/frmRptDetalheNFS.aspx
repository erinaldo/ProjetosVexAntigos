<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmRptDetalheNFS.aspx.cs" Inherits="frmRptDetalheNFS" Title="NOTAS FISCAIS AGUADANDO EMBARQUE" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                align="left">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <center>
        <asp:Panel ID="Panel1" runat="server" Width="1000px">
        </asp:Panel>
    </center>
</asp:Content>

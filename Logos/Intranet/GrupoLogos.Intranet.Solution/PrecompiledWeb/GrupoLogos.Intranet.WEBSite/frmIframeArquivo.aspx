<%@ page language="C#" autoeventwireup="true" inherits="frmIframeArquivo, App_Web_frmiframearquivo.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/estilos.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/javascript.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlNovoArquivo" runat="server" Visible="False">
                        <table style="width: 100%">
                            <tr>
                                <td nowrap="nowrap" width="1%">
                                    Selecione um arquivo
                                </td>
                                <td>
                                    Selecione o arquivo:
                                    <asp:FileUpload ID="FileUpload1" Width="200px" runat="server" CssClass="fileUpload" />
                                    <div style="width: 100%; height: 6px">
                                    </div>
                                    <br />
                                </td>
                                <td align="right" width="1%">
                                    <asp:Button ID="btnConfirmarArquivos" runat="server" CausesValidation="False" CssClass="button"
                                        OnClick="btnConfirmarArquivo_Click" Text="Confirmar" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

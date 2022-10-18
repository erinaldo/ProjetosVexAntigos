<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReposicaoRoge._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Auditoria Reposição Roge</title>
    <link href="Style/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 50%; border: 1px solid red">
        <div class="titulo">
            <span>Selecione a Empresa / Filial</span>
        </div>
        <div>
            <table style="width: 65%">
                <tr>
                    <td style="width:1%">
                        EMPRESA:
                    </td>
                    <td style="width:99%">
                        <asp:DropDownList ID="cboEmpresa" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        FILIAL:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboFilial" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

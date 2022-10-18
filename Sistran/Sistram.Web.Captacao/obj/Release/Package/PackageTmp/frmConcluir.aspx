<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConcluir.aspx.cs" Inherits="Sistram.Web.Captacao.frmConcluir" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregue seu Caminhão - PASSO 1</title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 11px;
            color: Gray;
            margin: 0px 0px 0px 0px;
        }
        
        .txt
        {
            border: 1px solid #999999;
            font-family: Arial;
            font-size: 9px;
            height: 12px;
            text-transform: uppercase;
        }
        
        .tabelaTitulo
        {
            width: 100%;
            text-align: center;
        }
        
        .titulo
        {
            text-transform: uppercase;
            font-size: 16px;
            font-weight: bold;
        }
        
        .titulo2
        {
            text-transform: uppercase;
            font-size: 14px;
            font-weight: bold;
        }
        
        .titulo3
        {
            text-transform: uppercase;
            font-size: 12px;
            font-weight: bold;
        }
        
        .style1
        {
            width: 100%;
        }
        .cbo
        {
            border: 1px solid #999999;
            font-family: Arial;
            font-size: 9px;
        }
        .textbox, .textarea, .fileUpload
        {
            color: #333333;
            font-weight: lighter;
            font-size: 7pt;
            border-width: 1px;
            border-style: solid;
            border-color: Silver;
        }
        .textarea
        {
            overflow: auto;
        }
        .style2
        {
            font-size: 16pt;
        }
        </style>
</head>
<body bgcolor="#f3f3f3">
    <form id="form1" runat="server">
    <center>
        <div style="text-align: left; width: 1024px; background-color: White">
            <table class="tabelaTitulo">
                <tr>
                    <td width="1%" style="text-align: left" valign="top">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/LOGOS-LOGTRANSP-03.jpg" Width="300px" />
                    </td>
                    <td style="width: 99%">
                        <span class="titulo">Agregue seu Caminhão&nbsp;</span>
                    </td>
                </tr>
                <tr>
                    <td width="1%" colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr align="left">
                    <td width="1%" colspan="2">
                        <span class="titulo2">cadastro concluído com sucesso</span></td>
                </tr>
                <tr style="background-color: Silver; height: 3px">
                    <td colspan="2">
                    </td>
                </tr>
                <tr style="height: 3px; text-align: left">
                    <td colspan="2">
                        <table class="style1" width="99%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Height="550px" Width="100%">
                                    </asp:Panel>
                                    <div style="position:absolute; top:50%; left:35%" class="style2"><strong>Obrigado. Em breve entraremos em contato</strong></div>
                                </td>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr align="right">
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

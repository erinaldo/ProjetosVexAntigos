<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Rastreamento_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::: Grupo Logos ::: Rastrear Encomenda</title>
    <link href="Rastreamento.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">

        function fullScreen(theURL) {
            window.open(theURL, 'abreJanela', 'width=800, heigth=600, toolbar=1,resizable=1, scrollbars=yes, ,resizable=1');
        }

</script>

    <style type="text/css">
        .tct
        {
            border: 1px solid #999999;
            font-family: Arial;
            font-size: 9px;
            height: 15px;
            width: 100px;
            text-transform: uppercase;
        }
        .style1
        {
            font-size: 9px;
        }
        .style2
        {
            font-size: 7pt;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style="border: 0px solid #000000; position:absolute; top:35%; left:35%; text-align:center; background-color: #FFFFFF;">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                            <img src="Imagens/Carregando.gif" height="40px" alt="Pesquisando" /><br />
                                <span class="style2"><strong>Aguarde... </strong></span>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
    
    </div>

    <div id="container">
        <asp:UpdatePanel ID="Up" runat="server" RenderMode="Inline" UpdateMode="Always">
            <ContentTemplate>
                <table style="font-family: Verdana; font-size: 9pt; font-weight: bold" 
                    width="100%" bgcolor="#CCCCCC">
                    <tr>
                        <td  style="font-family: Arial; font-size: 8pt; font-weight: bold;">
                            Informe o CNPJ/CPF:
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCNPJ" runat="server" MaxLength="20" Width="99%" BorderColor="#999999"
                                BorderStyle="Solid" BorderWidth="1px" AutoPostBack="True" OnTextChanged="txtCNPJ_TextChanged"
                                CssClass="tct"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  style="font-family: Arial; font-size: 8pt; font-weight: bold;">
                            Informe em cada linha o número:**</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNotas" runat="server" TextMode="MultiLine" Width="99%" Height="70"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CssClass="tct"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <asp:Button ID="btnRastrear" runat="server" Text="Rastrear"
                                OnClick="btnRastrear_Click" 
                                Width="99%" BorderStyle="Solid" BackColor="#E4E4E4" Font-Size="8" 
                                BorderColor="Black" BorderWidth="1px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="text-align:left">
                            **Números de Pedidos / Notas Fiscais</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCNPJ" ErrorMessage="Informe o CPF/CNPJ" 
                                SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNotas" 
                                
        ErrorMessage="Informe uma nota por linha para ratrear" SetFocusOnError="True" 
                                Text="*"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" Visible="False" />
    </form>
</body>
</html>

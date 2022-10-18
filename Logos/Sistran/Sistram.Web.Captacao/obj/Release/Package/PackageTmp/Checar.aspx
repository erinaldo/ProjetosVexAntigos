<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checar.aspx.cs" Inherits="Sistram.Web.Captacao.Checar" %>

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
        .style3
        {
            height: 18px;
        }
        .style4
        {
            width: 100px;
        }
        .style5
        {
            font-size: 12pt;
            color: #993300;
        }
        .style6
        {
            font-size: 12pt;
        }
    </style>
</head>
<body bgcolor="#f3f3f3">
    <form id="form1" runat="server">
    <center>
        <asp:UpdatePanel runat="server" ID="uplPrincipal">
            <ContentTemplate>
                <div style="text-align: left; width: 1024px; background-color: White">
                    <table class="tabelaTitulo">
                        <tr>
                            <td width="1%" style="text-align: left" valign="top">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/LOGOS-LOGTRANSP-03.jpg" Width="300px" />
                            </td>
                            <td style="width: 1%">
                                <span class="titulo">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    Agregue seu Caminhão&nbsp;</span>
                                </td>
                            <td style="width: 1%">
                            <fieldset style="background-color: White" >
                            <legend class="style6"><strong>Dúvidas</strong></legend>
                            
                                <table bgcolor="White" class="style1">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:Image ID="Image2" runat="server" Height="60px" ImageUrl="~/duvidas.jpg" />
                                        </td>
                                        <td class="style5" 
                                            style="font-weight: bold; font-family: Arial, Helvetica, sans-serif" 
                                            width="99%">
                                            Entre em contato com a Logos</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Cel.: 99491-6607 / Nextel.: 97*169201</strong></td>
                                    </tr>
                                </table>
                            
                            </fieldset>

                                </td>
                        </tr>
                        <tr>
                            <td width="1%" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td width="1%" colspan="3">
                                <div style="text-align: left; width: 100%">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <span class="titulo2">PASSO 1 - VERIFIQUE SE JÁ NÃO ESTA CADASTRADO </span>
                                            </td>
                                        </tr>
                                        <tr style="background-color:Silver; height:3px">
                                            <td>
                                            
                                            </td>
                                        </tr>
                                        <tr style="text-align: center">
                                            <td nowrap="nowrap" width="1%">
                                                <center>
                                                    <div style="width: 600px; text-align: left;">
                                                        <table>
                                                            <tr>
                                                                <td nowrap="nowrap" width="1%">
                                                                    <span class="titulo3">Informe seu cnpj / cpf: </span>
                                                                    <td width="1%" colspan="2">
                                                                        <asp:TextBox ID="txtCpfCnpj" runat="server" CssClass="txt" Font-Names="Arial" Font-Size="Small"
                                                                            Height="20px" MaxLength="20" Width="200px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSalvar" runat="server" BackColor="White" BorderColor="#666666"
                                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                                                            OnClick="btnSalvar_Click" Text="VEFIFICAR" Height="24px" 
                                                                            CausesValidation="False" />
                                                                    </td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" nowrap="nowrap" style="text-align: left" width="1%">
                                                                    <asp:Label ID="lblNomeMotorista" runat="server" Font-Bold="False" 
                                                                        Font-Size="12pt"></asp:Label>
                                                                    <asp:Label ID="lblIdcadastro" runat="server" Font-Bold="False" Font-Size="12pt" 
                                                                        Visible="False"></asp:Label>
                                                                    <asp:Label ID="lbSenhaRecup" runat="server" Font-Bold="False" Font-Size="12pt" 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style3" colspan="4" nowrap="nowrap" style="text-align: left" 
                                                                    width="1%">
                                                                    <hr/></r></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" nowrap="nowrap" style="text-align: left" width="1%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="4" nowrap="nowrap" style="text-align: left" width="1%">
                                                                    <asp:Button ID="btnSim" runat="server" BackColor="White" BorderColor="#006600" 
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" 
                                                                        Font-Size="11pt" ForeColor="#006600" Height="24px" onclick="btnSim_Click" 
                                                                        Text="SIM" Visible="False" CausesValidation="False" />
                                                                     &nbsp;<asp:Button ID="btnNao" runat="server" BackColor="White" BorderColor="#FF3300" 
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" 
                                                                        Font-Size="11pt" ForeColor="#FF3300" Height="24px" onclick="btnNao_Click" 
                                                                        Text="NÃO" Visible="False" CausesValidation="False" />
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="4" nowrap="nowrap" style="text-align: left" width="1%">
                                                                    <table class="style4">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblSenha" runat="server" Font-Bold="False" Font-Size="12pt" 
                                                                                    Visible="False">Senha:</asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                                    ControlToValidate="txtSenha" ErrorMessage="Informe a Senha">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtSenha" runat="server" CssClass="txt" Font-Names="Arial" 
                                                                                    Font-Size="Small" Height="20px" MaxLength="20" TextMode="Password" 
                                                                                    Visible="False" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="right">
                                                                            <td>
                                                                                &nbsp;</td>
                                                                            <td>
                                                                                <asp:Button ID="btnConfirma" runat="server" BackColor="White" 
                                                                                    BorderColor="#006600" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                                    Font-Names="Arial" Font-Size="11pt" ForeColor="#006600" Height="24px" 
                                                                                    onclick="btnConfirma_Click" Text="Confirma" Visible="False" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" nowrap="nowrap" style="text-align: left; width: 0%;" width="1%">
                                                                    &nbsp;</td>
                                                                <td colspan="2" nowrap="nowrap" style="text-align: left; width: 0%;" 
                                                                    width="99%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td colspan="2" nowrap="nowrap" style="text-align: left; width: 0%;" width="1%">
                                                                    &nbsp;</td>
                                                                <td colspan="2" nowrap="nowrap" style="width: 0%;" width="99%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                </center>
                                            </div>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>

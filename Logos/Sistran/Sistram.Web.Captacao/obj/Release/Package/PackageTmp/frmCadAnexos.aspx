<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCadAnexos.aspx.cs" Inherits="Sistram.Web.Captacao.frmCadAnexos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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
            text-decoration: underline;
        }
        .style3
        {
            color: #FF3300;
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
                                <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
                                </asp:ToolkitScriptManager>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/LOGOS-LOGTRANSP-03.jpg" Width="300px" />
                            </td>
                            <td style="width: 1%">
                                <span class="titulo">
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
                <tr align="left">
                    <td width="1%" colspan="3">
                        <span class="titulo2">PASSO 4 - Envio de imagens de documentos (Motorista) </span>
                    </td>
                </tr>
                <tr style="background-color: Silver; height: 3px">
                    <td colspan="3">
                    </td>
                </tr>
                <tr style="height: 3px; text-align: left">
                    <td colspan="3">
                        <table class="style1" width="99%">
                            <tr>
                                <td>
                                    <span class="titulo2">Documentos - imagens </span>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <span class="style2">Passos:</span><br />
                                    <br />
                                    1) Selecione a imagem.<br />
                                    2) Informe a descrição. **Sugestão RG, CPF etc.<br />
                                    3) Clique em Incluir Arquivo.<br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <span class="titulo2">Documentos OBRIGATÓRIOS </span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="style3" style="font-weight: 700; font-size: 12px">
                                    <p>
                                        *CPF
                                        <br />
                                        *RG
                                        <br />
                                        *Comprovante de Residência
                                        <br />
                                        *CNH Documento do veiculo
                                        <br />
                                        *ANTT
                                        <br />
                                        *PIS
                                        <br />
                                        *FOTO 3x4
                                        <br />
                                        *Foto do veiculo
                                        <br />
                                        *Atestado de Antecedentes</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <strong>Selecione a imagem e escreva uma descriçãooo</strong>
                                            </td>
                                            <td>
                                                <strong>Imagems / Documentos </strong>
                                            </td>
                                        </tr>
                                        <tr valign="top" align="left">
                                            <td>
                                                <div style="border: 1px solid silver; background-color: White; width: 400px" id="dvCadastrarImagem"
                                                    runat="server">
                                                    <table>
                                                        <tr>
                                                            <td nowrap="nowrap" width="1%">
                                                                Ecolha a Imagem:
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" Width="100%" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap="nowrap" width="1%">
                                                              Descrição da Imagem:
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescFoto"
                                                                    ErrorMessage="a Descrição" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDescFoto" runat="server" CssClass="txt" MaxLength="20" Width="99%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Button ID="btnConfirmarImagem" runat="server" BackColor="White" BorderColor="Black"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" OnClick="btnConfirmarImagem_Click"
                                                                    Text="Incluir Arquivo" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:GridView ID="grdAnexos" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px"
                                                    CellPadding="4" CellSpacing="2" ForeColor="Black" OnRowDataBound="grdAnexos_RowDataBound"
                                                    Width="99%" OnRowCommand="grdAnexos_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="imgGrid" runat="server" Height="40px" Width="40px" 
                                                                                CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center; font-weight: 700">
                                                                            <asp:Label ID="lblGridDescricao" runat="server" Style="font-size: 12px" Text='<% # Eval("NOME") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:ImageButton ID="btnGridExcluir" runat="server" Height="25px" ImageUrl="~/Imagens/excluir.jpg"
                                                                                Width="25px" CausesValidation="False" CommandName='<% # Eval("IDCadastroImagem") %>'
                                                                                CommandArgument="excluir" ToolTip="Clique para Excluir" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                    <RowStyle BackColor="White" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="right">
                    <td colspan="2">
                        <asp:Button ID="btnConconcluirCadastro" runat="server" BackColor="White" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Text="Concluir" 
                            CausesValidation="False" onclick="btnConconcluirCadastro_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

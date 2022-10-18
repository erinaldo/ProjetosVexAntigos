<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmChecaCNPJCPF.aspx.cs" Inherits="Sistram.Web.Captacao._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregue seu Caminhão</title>
    <script language="javascript" type="text/javascript">

        function validateCboMarca(source, args) {
            var ddl = null;
            ddl = document.getElementById('<%=cboMarca.ClientID %>');

            if (ddl != null) {
                args.IsValid = !(ddl.options[ddl.selectedIndex].value == 0);
                
            }
            else
                args.IsValid = true;
        }

        function validateCboModelo(source, args) {
            var ddl = null;
            ddl = document.getElementById('<%=cboModelo.ClientID %>');

            if (ddl != null) {
                args.IsValid = !(ddl.options[ddl.selectedIndex].value == 0);
            }
            else
                args.IsValid = true;
        }

        function validateCboEstado(source, args) {
            var ddl = null;
            ddl = document.getElementById('<%=cboEstado.ClientID %>');

            if (ddl != null) {
                args.IsValid = !(ddl.options[ddl.selectedIndex].value == 0);
            }
            else
                args.IsValid = true;
        }


        function validateCboCidade(source, args) {
            var ddl = null;
            ddl = document.getElementById('<%=cboCidade.ClientID %>');

            if (ddl != null) {
                args.IsValid = !(ddl.options[ddl.selectedIndex].value == 0);
            }
            else
                args.IsValid = true;
        }

        function validatecboRastreador(source, args) {
            var ddl = null;
            ddl = document.getElementById('<%=cboRastreador.ClientID %>');

            if (ddl != null) {
                args.IsValid = !(ddl.options[ddl.selectedIndex].value == 0);
            }
            else
                args.IsValid = true;
        }    

    </script>
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
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 14px;
        }
        .style3
        {
            height: 17px;
        }
        .cbo
        {
            border: 1px solid #999999;
            font-family: Arial;
            font-size: 9px;
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
                            <td width="1%" style="text-align: left">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/LOGOS-LOGTRANSP-03.jpg" Width="300px" />
                            </td>
                            <td style="width: 99%">
                                <span class="titulo">Agrege seu Caminhão&nbsp;</span>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                        <tr>
                            <td width="1%" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td width="1%" colspan="2">
                                <div style="text-align: left; width: 100%">
                                    <table class="style1">
                                        <tr>
                                            <td class="style2" colspan="4">
                                                <span class="titulo2">Preencha o formulario e clique em enviar</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="1%">
                                                Nome:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                                                    ErrorMessage="Informe o Nome" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="300" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td width="1%">
                                                E-mail:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="Informe o E-mail" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" Width="300" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Telefone:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelefone"
                                                    ErrorMessage="Informe o telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtTelefone" runat="server" CssClass="txt" Width="300" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                Celular:<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCelular"
                                                    ErrorMessage="informe o celular" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtCelular" runat="server" CssClass="txt" Width="300" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nextel:
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtNextel" runat="server" CssClass="txt" Width="300" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style3">
                                                Endereço:<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtEndereco" ErrorMessage="Informe Endereço" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="style3">
                                                &nbsp;
                                                <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" Width="300"></asp:TextBox>
                                            </td>
                                            <td class="style3" nowrap="nowrap">
                                                Número / Compl.<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="txtNomeNumero" ErrorMessage="Informe Numero" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                :
                                            </td>
                                            <td class="style3">
                                                &nbsp;
                                                <asp:TextBox ID="txtNomeNumero" runat="server" CssClass="txt" Width="135px"></asp:TextBox>
                                                &nbsp;/
                                                <asp:TextBox ID="txtComplemento" runat="server" CssClass="txt" MaxLength="20" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Estado:
                                                <asp:CustomValidator ID="CustomValidator4" runat="server" 
                                                    ClientValidationFunction="validateCboEstado" ControlToValidate="cboEstado" 
                                                    ErrorMessage="Informe o Estado" SetFocusOnError="True">*</asp:CustomValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="cbo" Width="303px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cboEstado_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Cidade:
                                                <asp:CustomValidator ID="CustomValidator5" runat="server" 
                                                    ClientValidationFunction="validateCboCidade" ControlToValidate="cboCidade" 
                                                    ErrorMessage="Informe a Cidade" SetFocusOnError="True">*</asp:CustomValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:DropDownList ID="cboCidade" runat="server" CssClass="cbo" Width="303px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                CEP:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCEP"
                                                    ErrorMessage="Informe CEP" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtCEP" runat="server" CssClass="txt" Width="300" MaxLength="10"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <span class="titulo2">Dados Do Veículo</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Marca:
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="cboMarca"
                                                    ErrorMessage="Informe a Marcar" SetFocusOnError="True" 
                                                    ClientValidationFunction="validateCboMarca">*</asp:CustomValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:DropDownList ID="cboMarca" runat="server" CssClass="cbo" Width="303px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cboMarca_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Modelo:
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                                    ClientValidationFunction="validateCboModelo" ControlToValidate="cboModelo" 
                                                    ErrorMessage="Informe a Modelo" SetFocusOnError="True">*</asp:CustomValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:DropDownList ID="cboModelo" runat="server" CssClass="cbo" Width="303px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Ano:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAno"
                                                    ErrorMessage="Informe o Ano" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtAno" runat="server" CssClass="txt" Width="300" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td>
                                                Placa:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPlaca"
                                                    ErrorMessage="Informe a placa" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtPlaca" runat="server" CssClass="txt" Width="300" 
                                                    AutoPostBack="True" ontextchanged="txtPlaca_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                CNH:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCNH"
                                                    ErrorMessage="Informe CNH" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtCNH" runat="server" CssClass="txt" Width="300" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;ANTT:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtANTT"
                                                    ErrorMessage="Informe ANTT" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:TextBox ID="txtANTT" runat="server" CssClass="txt" Width="300"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Rastreador:<asp:CustomValidator ID="CustomValidator3" runat="server" 
                                                    ClientValidationFunction="validatecboRastreador" 
                                                    ControlToValidate="cboRastreador" ErrorMessage="Informe a Rastreador" 
                                                    SetFocusOnError="True">*</asp:CustomValidator>
&nbsp; </td>
                                            <td>
                                                &nbsp;
                                                <asp:DropDownList ID="cboRastreador" runat="server" CssClass="cbo" Width="303px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <span class="titulo2">filiais</span>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:CheckBoxList ID="lstTodasFiliais" runat="server" RepeatColumns="3">
                                                </asp:CheckBoxList>
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;
                                                <asp:Button ID="btnSalvar" runat="server" BackColor="White" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                                                    Text="ENVIAR &gt;&gt;" Height="50px" onclick="btnSalvar_Click" />
                                            </td>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCadastrarProprietario.aspx.cs"
    Inherits="Sistram.Web.Captacao.frmCadastrarProprietario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregue seu Caminhão - PASSO 3</title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 11px;
            color: Black;
            margin: 0px 0px 0px 0px;
        }
        .txtValor
        {
            border: 1px solid #999999;
            font-family: Verdana;
            font-size: 8pt;
            height: 12px;
            vertical-align: middle;
            text-align: right;
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
        
        .table
        {
            background-color: White;
            width: 100%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 7pt;
            font-weight: bold;
        }
        
        .tdp
        {
            border: 0.1pt solid #FFFFFF;
            font-size: 8pt;
            font-family: Verdana;
            nowrap: nowrap;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
        }
        .tdpR
        {
            border: 0.1pt solid #FFFFFF;
            font-size: 8pt;
            font-family: Verdana;
            text-align: right;
            nowrap: nowrap;
            font-weight: normal;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function validaDat(campo, valor) {
            var date = valor;

            if (valor == "") {
                return true;
            }

            if (valor == "__/__/____") {
                campo.value = "";
                return true;
            }

            var ardt = new Array;
            var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
            ardt = date.split("/");
            erro = false;
            if (date.search(ExpReg) == -1) {
                erro = true;
            }
            else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
                erro = true;
            else if (ardt[1] == 2) {
                if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
                    erro = true;
                if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
                    erro = true;
            }
            if (erro) {
                alert("\"" + valor + "\" não é uma data válida!!!");
                campo.focus();
                campo.value = "";
                return false;
            }
            return true;
        }

        function CopiarDados(cpfOrigen, dest) {
            //alert(cpfOrigen.value);
            alert(cpfOrigen.value); //= document.getElementById("txtcpfcnpj").value;
            alert(dest.value);


        }
  
    </script>
</head>
<body bgcolor="#f3f3f3">
    <form id="form1" runat="server">
    <center>
        <div style="text-align: left; width: 1024px; background-color: White">
            <table class="tabelaTitulo">
                <tr>
                   <td width="1%" style="text-align: left" valign="top">
                                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
                <tr>
                    <td width="1%" colspan="3">
                        <div style="text-align: left; width: 100%">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <span class="titulo2">PASSO 3 - Informe os dados do proprietário do veículo </span>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr style="text-align: center">
                                    <td nowrap="nowrap" width="1%">
                                        <center>
                                            <div style="width: 100%; text-align: left;">
                                                <table border="0" cellpadding="2" cellspacing="1" class="table">
                                                    <tr>
                                                        <td nowrap="nowrap" width="1%" align="center" colspan="4">
                                                            <asp:Label ID="lblIdProprietario" runat="server" Text="0" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblIdMotorista" runat="server" Text="0" Visible="False"></asp:Label>
                                                            <asp:Button ID="brnCopiarDados" runat="server" BackColor="White" BorderColor="Black"
                                                                BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Font-Bold="True"
                                                                Font-Size="8pt" OnClick="brnCopiarDados_Click" Text="Copiar Dados Do Cadastro de Motorista" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            CPF/CNPJ:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtcpfcnpj" runat="server" CssClass="txt" MaxLength="18" OnTextChanged="txtcpfcnpj_TextChanged"
                                                                Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcpfcnpj"
                                                                ErrorMessage="Informe
                    o CNPJ / CPF" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Nome/Razão Social:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtRazao" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRazao"
                                                                ErrorMessage="Informe a Razão Social" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            IE/RG:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtIE" runat="server" CssClass="txt" MaxLength="18" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtIE"
                                                                ErrorMessage="Informe o RG" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Fantasia/Apelido &nbsp;
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtFantasiaApelido" runat="server" CssClass="txt" MaxLength="30"
                                                                Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        CEP:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtCEP" runat="server" CssClass="txt" MaxLength="8" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCEP"
                                            ErrorMessage="Informe
                    o CEP" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Endereço:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEndereco"
                                            ErrorMessage="Informe
                    endereço." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Número:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtNumero" runat="server" CssClass="txt" MaxLength="10" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNumero"
                                            ErrorMessage="Informe o número" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Complemento:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtComplemento" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Estado:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" CssClass="cbo"
                                            OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Cidade:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboCidade" runat="server" CssClass="cbo" Enabled="False" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Telefone Res.:
                                        <asp:HiddenField ID="idtelefoneRes" runat="server" />
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtFoneResidencial" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Celular:
                                        <asp:HiddenField ID="idtelefoneCel" runat="server" />
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtFoneCelular" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Tefone Recado:
                                        <asp:HiddenField ID="idtelefoneRec" runat="server" />
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtFoneRecado" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Nextel:<asp:HiddenField ID="idtelefoneNextel" runat="server" />
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtFoneNextel" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Emite NF de Serviço:</td>
                                    <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboEmiteNF" runat="server" 
                                            CssClass="cbo" Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;</td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdp" colspan="4" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <span class="titulo2">DADOS DO VEÍCULO </span>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        Placa:
                                    </td>
                                    <td class="tdp" nowrap="nowrap" width="48%">
                                        <asp:TextBox ID="txtPlaca" runat="server" CssClass="txt" MaxLength="8" Width="95%"
                                            OnTextChanged="txtPlaca_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlaca"
                                            ErrorMessage="Informe a placa" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        <asp:MaskedEditExtender ID="MaskedEdittxtPlaca" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" Mask="AAA-9999" MaskType="None" TargetControlID="txtPlaca" ClearMaskOnLostFocus="False"
                                            CultureName="pt-BR">
                                        </asp:MaskedEditExtender>
                                    </td>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        Data do Licencimento
                                    </td>
                                    <td class="tdp" nowrap="nowrap" width="48%">
                                        <asp:TextBox ID="txtDataLicenciamento" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtenderccc1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataLicenciamento"
                                            UserDateFormat="DayMonthYear" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDataLicenciamento"
                                            ErrorMessage="Informe a
                    Data de Licenciamento" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        Chassi:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtChassi" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="tdp" nowrap="nowrap" width="1%">
                                        Renavan:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtRenavan" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRenavan"
                                            ErrorMessage="Informe o RENAVAN" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Estado:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboEstadoVeiculo" runat="server" AutoPostBack="True" CssClass="cbo"
                                            OnSelectedIndexChanged="cboEstadoVeiculo_SelectedIndexChanged" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Cidade:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboCidadeVeiculo" runat="server" CssClass="cbo" Enabled="False"
                                            Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Marca:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboMarca" runat="server" CssClass="cbo" Width="96%" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboMarca_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Modelo:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboModelo" runat="server" CssClass="cbo" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Cor:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtCor" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCor"
                                            ErrorMessage="Informe a Cor" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Ano / Modelo:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtano" runat="server" CssClass="txtValor" Width="40px"></asp:TextBox>
                                        /<asp:TextBox ID="txtanoModelo" runat="server" CssClass="txtValor" Width="45px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtano"
                                            ErrorMessage="Informe o Ano" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtanoModelo"
                                            ErrorMessage="Informe o Ano Modelo" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Tipo:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboTipo" runat="server" CssClass="cbo" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Eixos:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txteixos" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txteixos"
                                            ErrorMessage="Informe o Número de Eixos" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Capacidade m3:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtCapacidade" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Capacidade Carga KG:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtCapacidadeKG" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Restreador:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:DropDownList ID="cboRastreador" runat="server" CssClass="cbo" Width="96%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Número de Série:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtNumeroSerieEquipamento" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        Antt:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtAntt" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAntt"
                                            ErrorMessage="Informe o Antt" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Vencimento Antt:
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtVencimentoAntt" runat="server" CssClass="txt" Width="100px" onblur="validaDat(this,this.value)"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtVencimentoAntt"
                                            UserDateFormat="DayMonthYear" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtVencimentoAntt"
                                            ErrorMessage="Informe a Data
                    de Vencimento Antt" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <span class="titulo2">REFERENCIA COMERCIAL </span>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver;">
                                    <td colspan="4" height="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        1) Empresa / Falar com:<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="txtReferenciaCom1" ErrorMessage="r referencia empresa" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaCom1" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                        <asp:Label ID="lblIdContRefCom1" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Fone:<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtReferenciaComFone1"
                                            ErrorMessage="Informe o Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaComFone1" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        2) Empresa/ 
                                        Falar com:<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                            ControlToValidate="txtReferenciaCom2" ErrorMessage="r referencia empresa" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaCom2" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                        <asp:Label ID="lblIdContRefCom2" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Fone:<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtReferenciaComFone2"
                                            ErrorMessage="Informe o Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaComFone2" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <span class="titulo2">REFERENCIA PESSOAIS </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        1) Nome / Parentesco:<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="txtReferenciaPes1" ErrorMessage="Informe a Referencia" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPes1" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                        <asp:Label ID="lblIdContRefPes1" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Fone:<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtReferenciaPesFone1"
                                            ErrorMessage="Informe o Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPesFone1" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        2) Nome / Parentesco:<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                            ControlToValidate="txtReferenciaPesFone2" ErrorMessage="Informe a Referencia"
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPes2" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                        <asp:Label ID="lblIdContRefPes2" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Fone:<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtReferenciaPesFone2"
                                            ErrorMessage="Informe o Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPesFone2" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        3) Nome / Parentesco:<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="txtReferenciaPes3" ErrorMessage="Informe a Referencia" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPes3" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                        <asp:Label ID="lblIdContRefPes3" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        Fone:<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtReferenciaPesFone3"
                                            ErrorMessage="Informe o Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:TextBox ID="txtReferenciaPesFone3" runat="server" CssClass="txt" MaxLength="60"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="lblProprietario" runat="server" BorderColor="#CCCCCC" BorderStyle="None"
                                            BorderWidth="0px" CssClass="txt" Height="0px" Width="0px"></asp:TextBox>
                                    </td>
                                    <td class="tdpR" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" nowrap="nowrap">
                                        <asp:Label ID="lblCodEstado" runat="server" Visible="False"></asp:Label>
                                        <br />
                                        <asp:Button ID="brnVoltar" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Text="&lt;&lt; Voltar" CausesValidation="False"
                                            OnClick="brnVoltar_Click" />
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                        <asp:Label ID="lblidVeiculo" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td class="tdp" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td class="tdpR" nowrap="nowrap">
                                        <asp:Label ID="lblCodCidade" runat="server" Visible="False"></asp:Label>
                                        <br />
                                        <asp:Button ID="brnAvancar" runat="server" BackColor="White" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Text="Passo 4 &gt;&gt;"
                                            OnClick="brnAvancar_Click" />
                                    </td>
                                </tr>
                            </table>
    </center>
    </form>
</body>
</html>

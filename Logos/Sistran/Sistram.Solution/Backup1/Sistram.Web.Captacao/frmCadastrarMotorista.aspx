<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCadastrarMotorista.aspx.cs"
    Inherits="Sistram.Web.Captacao.frmCadastrarMotorista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregue seu Caminhão - PASSO 2</title>
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
        .style2
        {
            border: 0.1pt solid #FFFFFF;
            font-size: 8pt;
            font-family: Verdana;
            nowrap: nowrap;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
            height: 51px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;

            //alert(tecla);

            if ((tecla > 47 && tecla < 58) || tecla == 44)
                return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }

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
                                        <span class="titulo2">PASSO 2 - Informe os Dados do Motorista </span>
                                    </td>
                                </tr>
                                <tr style="background-color: Silver; height: 3px">
                                    <td>
                                    </td>
                                </tr>
                                <tr style="text-align: center">
                                    <td nowrap="nowrap" width="1%">
                                        <center>
                                            <div style="width: 100%; text-align: left;">
                                                <table border="0" cellpadding="1" cellspacing="1" class="table" style="width: 100%">
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            CPF:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtcpfcnpj" runat="server" CssClass="txt" MaxLength="18" Width="95%"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcpfcnpj"
                                                                ErrorMessage="Informe o CNPJ / CPF" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Senha de Acesso:</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtSenha" runat="server" CssClass="txt" MaxLength="60" 
                                                                Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtSenha"
                                                                ErrorMessage="Informe senha de acesso" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Nome/Razão Social:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtRazao" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtRazao"
                                                                ErrorMessage="Informe a Razão Social" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Fantasia/Apelido
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtFantasiaApelido" runat="server" CssClass="txt" MaxLength="60"
                                                                Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            RG:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtIE" runat="server" CssClass="txt" MaxLength="18" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtIE"
                                                                ErrorMessage="Informe RG / IE" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Data Expedição:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtDataExpedicao" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataExpedicao"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataExpedicao"
                                                                ErrorMessage="Informe a Data de Expedição do RG">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Orgão Expedidor:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtOrgaoExpedidor" runat="server" CssClass="txt" MaxLength="60"
                                                                Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtOrgaoExpedidor"
                                                                ErrorMessage="Informe o Orgão Expedidor do RG" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Local de Expedição:</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtLocalDeExpedicao" runat="server" CssClass="txt" MaxLength="60"
                                                                Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            CEP:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtCEP" runat="server" CssClass="txt" MaxLength="8" Width="100px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCEP"
                                                                ErrorMessage="Informe o CEP" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Endereço:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEndereco"
                                                                ErrorMessage="Informe endereço." SetFocusOnError="True">*</asp:RequiredFieldValidator>
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
                                                                OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" Width="105px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Cidade:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="cboCidade" runat="server" CssClass="cbo" Enabled="False" Width="96%"
                                                                        OnSelectedIndexChanged="cboCidade_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2" nowrap="nowrap">
                                                            Telefone Res.:
                                                            <asp:HiddenField ID="idtelefoneRes" runat="server" />
                                                        </td>
                                                        <td class="style2" nowrap="nowrap">
                                                            <asp:TextBox ID="txtFoneResidencial" runat="server" CssClass="txt" MaxLength="60"
                                                                Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtFoneResidencial"
                                                                ErrorMessage="Informe Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="style2" nowrap="nowrap">
                                                            Celular:
                                                            <asp:HiddenField ID="idtelefoneCel" runat="server" />
                                                        </td>
                                                        <td class="style2" nowrap="nowrap">
                                                            <asp:TextBox ID="txtFoneCelular" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtFoneCelular"
                                                                ErrorMessage="Informe Telefone" SetFocusOnError="True">*</asp:RequiredFieldValidator>
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
                                                            Estado Nascimento:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboEstadoNascimento" runat="server" AutoPostBack="True" CssClass="cbo"
                                                                OnSelectedIndexChanged="cboEstadoNascimento_SelectedIndexChanged" Width="105px" />
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Cidade Nascimento:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboCidadeNascimento" runat="server" CssClass="cbo" Enabled="False"
                                                                Width="96%" OnSelectedIndexChanged="cboCidadeNascimento_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Data de Nascimento:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtenderS1" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataNascimento"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDataNascimento"
                                                                ErrorMessage="Informe a Data de Nascimento">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:Label ID="lblId" runat="server" Text="0" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Nome Pai:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtNomePai" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Nome Mãe:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtNomeMae" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Estado Civil:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtEstacocivil" runat="server" CssClass="txt" MaxLength="20" Width="95%"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Conjuge:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtNomeConjuge" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="14px" Text="Habilitação"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Primeira Habilitação:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap" width="30%">
                                                            <asp:TextBox ID="txtPrimaHabilitacao" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender1S" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPrimaHabilitacao"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPrimaHabilitacao"
                                                                ErrorMessage="Informe a Data da Primeira Habilitação" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Carteira de Habilitação:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap" width="30%">
                                                            <asp:TextBox ID="txtCarteiraHabilitacao" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCarteiraHabilitacao"
                                                                ErrorMessage="Informe ao Número da Carteira de Habilitação" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Categotia:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtCategoriaHabilitacao" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCategoriaHabilitacao"
                                                                ErrorMessage="Informe a Categoria da Habilitação" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Validade:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtValidade" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender1X" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtValidade"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtValidade"
                                                                ErrorMessage="Informe a Validade">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="14px" Text="Gerencimento de Risco"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Pamcary:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtPamcary" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPamcary"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Buonny:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtBuonny" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtBuonny"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Brasilrisk:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtBrasilRisk" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtBrasilRisk"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" colspan="4">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="14px" Text="Dados Bancários"
                                                                Style="font-family: Arial, Helvetica, sans-serif"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Nº do Banco
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtNumeroBanco" runat="server" CssClass="txtValor" 
                                                                Width="100px" Enabled="False">237</asp:TextBox>
                                                        &nbsp;BRADESCO</td>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Agencia / Dígito:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtAgencia" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                                            -<asp:TextBox ID="txtAgenciaDig" runat="server" CssClass="txtValor" Width="30px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Conta / Dígito:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtConta" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                                            -<asp:TextBox ID="txtContaDig" runat="server" CssClass="txtValor" Width="30px"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Tipo de Conta:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboTipoConta" runat="server" CssClass="cbo" Width="100px">
                                                                <asp:ListItem Selected="True">CORRENTE</asp:ListItem>
                                                                <asp:ListItem>POUPANCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            CPF Favorecido:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtcpfcnpjFavorecido" runat="server" CssClass="txt" MaxLength="18"
                                                                Width="96%"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Nome Favorecido:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtNomeFavorecido" runat="server" CssClass="txt" MaxLength="60"
                                                                Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Vinculo:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtVinculoComFavorecido" runat="server" CssClass="txt" Width="96%"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Última Comprovação de Endereço:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtUltimaDataDeComprovacao" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtUltimaDataDeComprovacao"
                                                                UserDateFormat="DayMonthYear">
                                                            </asp:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUltimaDataDeComprovacao"
                                                                ErrorMessage="Informe a Data da Última Comprovação de Endereço">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14px" Text="Outros Dados"></asp:Label>
                                                            <asp:Label ID="Label5" runat="server" Text="0" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Roubo:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtRoubo" runat="server" CssClass="txtValor" Width="100px">0</asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRoubo"
                                                                ErrorMessage="Informe quantas vezes foi vitima de roubo" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap" width="1%">
                                                            Acidente:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtAcidente" runat="server" CssClass="txtValor" Width="100px">0</asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAcidente"
                                                                ErrorMessage="Informe quntas vezes foi vitima de acidente" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Dependentes:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtDependentes" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Recolhe SETS/SENAT</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboRecolheSetsSenat" runat="server" CssClass="cbo" 
                                                                Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Inscrição INSS:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtInscricaoInss" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Inscrição do PIS:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtPIS" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Valor Pensão Alimenticia:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:TextBox ID="txtPensaoAlimenticia" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Aposentado:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboAposentado" runat="server" CssClass="cbo" Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Contratado:
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboContratado" runat="server" CssClass="cbo" Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Recolhe INSS:</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboRecolheInss" runat="server" CssClass="cbo" 
                                                                Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap">
                                                            &nbsp;</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            &nbsp;</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            Recolhe IRRF:</td>
                                                        <td class="tdp" nowrap="nowrap">
                                                            <asp:DropDownList ID="cboRecolheIRPJ" runat="server" CssClass="cbo" 
                                                                Width="100px">
                                                                <asp:ListItem>NAO</asp:ListItem>
                                                                <asp:ListItem>SIM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: Silver;">
                                                        <td colspan="4" height="1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" style="height: 12px">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="14px" Text="Filiais"
                                                                Style="font-family: Arial, Helvetica, sans-serif"></asp:Label>
                                                        </td>
                                                        <td class="tdp" colspan="3" nowrap="nowrap" style="height: 12px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" nowrap="nowrap" style="height: 12px" colspan="4">
                                                            <asp:CheckBoxList ID="lstTodasFiliais" runat="server" RepeatColumns="5" Font-Names="Arial"
                                                                Font-Size="8pt">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" style="height: 12px" colspan="4" align="right">
                                                            <asp:Button ID="brnSalvar" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Bold="True" OnClick="brnSalvar_Click" Text="Passo 3 &gt;&gt;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                        </center>
                        </div>
                </tr>
            </table>
        </div>
        </td> </tr> </table> </div>
    </center>
    </form>
</body>
</html>

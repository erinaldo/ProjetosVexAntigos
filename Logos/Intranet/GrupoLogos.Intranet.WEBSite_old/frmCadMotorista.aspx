<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadMotorista.aspx.cs"
    Inherits="Intranet_frmCadMotorista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/ctrHistorico.ascx" TagName="ctrHistorico" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

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

    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Motorista" Font-Bold="True"
                    Font-Size="14px"></asp:Label>
                <asp:Label ID="lblId" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="98%">
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" class="table" style="width: 100%;
                    border-style: solid; border-width: 1px; background-color: #FFFFFF;">
                    <tr>
                        <td class="tdp">
                            <asp:CheckBox ID="chkAtivo" runat="server" Font-Bold="True" Text="ATIVO" />
                            <asp:CheckBox ID="chkProprietario" runat="server" Font-Bold="True" 
                                Text="PROPRIETÁRIO" ForeColor="Red" />
                        </td>
                        <td class="tdpR">
                            <asp:CheckBox ID="chkLiberado" runat="server" Font-Bold="True" Text="LIBERADO" Enabled="False"
                                EnableTheming="True" />
                        </td>
                        <td class="tdpR">
                            <asp:Label ID="lblDataBloqueio" runat="server" Font-Bold="True" />
                        </td>
                    </tr>
                </table>
                
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" Width="100%"
                    Skin="Outlook" CausesValidation="False" TabIndex="1" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="pgnDadosPessoais" 
                            Text="Dados Pessoais / Habilitação" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="pgnDadosBancarios" 
                            Text="Dados Bancários / Outros">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="pgnDadosFiliais" Text="Filiais">
                        </telerik:RadTab>
                        
                        <telerik:RadTab runat="server" PageViewID="pgnFotos" Text="Foto(s)">
                        </telerik:RadTab>
                        
                        <telerik:RadTab runat="server" PageViewID="pgnHistorico" Text="Histórico">
                        </telerik:RadTab>

                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmp" runat="server" BackColor="White" CssClass="bordaTabs"
                    Width="100%">
                    <telerik:RadPageView ID="subPgDadosPessoais" runat="server" Width="100%">
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
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap">
                                    Nome/Razão Social:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtRazao" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtRazao"
                                        ErrorMessage="Informe a Razão Social" SetFocusOnError="True">*</asp:RequiredFieldValidator>
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
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                </td>
                            </tr>
                            <tr style="background-color: Silver;">
                                <td colspan="4" height="1">
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
                                    <asp:UpdatePanel ID="upkl" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" CssClass="cbo"
                                                OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" Width="105px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    Cidade:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboCidade" runat="server" CssClass="cbo" Enabled="False" Width="96%">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                            <tr style="background-color: Silver;">
                                <td colspan="4" height="1">
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap">
                                    Estado Nascimento:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboEstadoNascimento" runat="server" AutoPostBack="True" CssClass="cbo"
                                                OnSelectedIndexChanged="cboEstadoNascimento_SelectedIndexChanged" Width="105px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    Cidade Nascimento:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboCidadeNascimento" runat="server" CssClass="cbo" Enabled="False"
                                                Width="96%">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                            <tr>
                                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="14px" Text="Gerencimento de Risco"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap">
                                    Pancary:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtPancary" runat="server" CssClass="txt" onblur="validaDat(this,this.value)"
                                        Width="100px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPancary"
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
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="subPgDadosBancarios" runat="server" Width="100%">
                        <table border="0" cellpadding="2" cellspacing="1" class="table" style="width: 100%">
                            <tr>
                                <td class="tdp" nowrap="nowrap" width="1%">
                                    Nº do Banco
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtNumeroBanco" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                </td>
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
                            <tr>
                                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="14px" Text="Outros Dados"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap" width="1%">
                                    Roubo:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtRoubo" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRoubo"
                                        ErrorMessage="Informe quantas vezes foi vitima de roubo" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="tdp" nowrap="nowrap" width="1%">
                                    Acidente:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtAcidente" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAcidente"
                                        ErrorMessage="Informe quntas vezes foi vitima de acidente" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap">
                                    Carregamento Autor. Até:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtCarregamentoAutor" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCarregamentoAutor"
                                        ErrorMessage="Informe o campo Carregamento Autorizado Até" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <!--Venct. Gerenc. Risco:-->
                                    MOOP:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:DropDownList ID="cboMoop" runat="server" CssClass="cbo" Width="100px">
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
                                    Alíquota Sest/Senat:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtAliquota" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAliquota"
                                        ErrorMessage="Informe a aliquita" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    Pancard:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:TextBox ID="txtPancard" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: Silver;">
                                <td colspan="4" height="1">
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
                                    Aniversário:
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:Label ID="lblAniversario" runat="server"></asp:Label>
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
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap" style="height: 12px">
                                    Observações:
                                </td>
                                <td class="tdp" colspan="3" nowrap="nowrap" style="height: 12px">
                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="txt" Height="60px" MaxLength="300"
                                        TextMode="MultiLine" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="subPgFiliais" runat="server" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1" class="table" style="width: 100%">
                            <tr>
                                <td style="width: 1%; font-family: Verdana; font-size: 8pt">
                                    Filiais
                                </td>
                                <td style="width: 1%; font-family: Verdana; font-size: 8pt">
                                </td>
                                <td style="width: 1%; font-family: Verdana; font-size: 8pt">
                                    Filiais Selecionadas
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstTodasFiliais" runat="server" CssClass="listbox2" Height="300"
                                                Width="200px"></asp:ListBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="btnPosterior" runat="server" CausesValidation="false" ImageUrl="~/Images/SetaDireita.png"
                                                            OnClick="btnPosterior_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="btnAnterior" runat="server" CausesValidation="false" ImageUrl="~/Images/setaEsqerda.png"
                                                            OnClick="btnAnterior_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstFiliaisSelecionadas" runat="server" CssClass="listbox2" Height="300"
                                                Width="200px"></asp:ListBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    
                    <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                        <asp:Panel ID="pnfoto" runat="server" Height="100%" Width="100%">
                            <iframe id="iframeFotos" runat="server" frameborder="0" height="450px" src="frmCarregarFoto.aspx?tipo=motorista"
                                width="100%"></iframe>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="subPgHistorico" runat="server" Width="100%">
                        <uc1:ctrHistorico ID="ctrHistoricos" runat="server" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" CssClass="button" OnClick="Button3_Click"
                    Text="Confirmar" />
                <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="button"
                    OnClientClick="javascript:history.back(-1);" Text="Voltar" />
                <asp:Label ID="lblCodCidade" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" cellspacing="1" class="table" style="width: 100%; visibility: hidden"
        border="0">
        <tr style="background-color: Silver;">
            <td colspan="4" height="1">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                <asp:Label ID="lblCodEstado" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:Label ID="lblCodCidadeNascimeto" runat="server" Visible="False"></asp:Label><asp:ValidationSummary
                    ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:Label ID="lblCodEstadoNascimento" runat="server" Visible="False"></asp:Label><asp:TextBox
                    ID="txtFoto" runat="server"></asp:TextBox>
            </td>
            <td class="tdpR" nowrap="nowrap">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap" valign="top">
                Observações:
            </td>
            <td class="tdp" nowrap="nowrap" valign="top">
            </td>
            <td class="tdp" nowrap="nowrap" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap" colspan="4">
            </td>
        </tr>
    </table>
</asp:Content>

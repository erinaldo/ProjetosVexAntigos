<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Intranet_frmCadVeiculo, App_Web_frmcadveiculo.aspx.cdcab7d2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <script language="javascript" type="text/javascript">
      function validaDat(campo, valor) {
          var date = valor;
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

  
  </script>
  
  <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Veículo" Font-Bold="True"
                    Font-Size="14px"></asp:Label>
                <asp:Label ID="lblId" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" cellspacing="1" class="table" style="width: 100%" border="0">
        <tr>
            <td class="tdp" nowrap="nowrap" width="1%">
                Placa:</td>
            <td class="tdp" nowrap="nowrap" width="48%">
                <asp:TextBox ID="txtPlaca" runat="server" CssClass="txt" MaxLength="8" 
                    Width="95%" ontextchanged="txtPlaca_TextChanged" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPlaca" ErrorMessage="Informe a placa" 
                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <asp:MaskedEditExtender ID="MaskedEdittxtPlaca" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="AAA-9999" MaskType="None" 
                        TargetControlID="txtPlaca" ClearMaskOnLostFocus="False"  CultureName="pt-BR">
                    </asp:MaskedEditExtender>
            </td>
            <td class="tdp" nowrap="nowrap" width="1%">
                Data do Licencimento</td>
            <td class="tdp" nowrap="nowrap" width="48%">
                <asp:TextBox ID="txtDataLicenciamento" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtenderccc1" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataLicenciamento" 
                    UserDateFormat="DayMonthYear" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtDataLicenciamento" 
                    ErrorMessage="Informe a Data de Licenciamento" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <b>Data Limite:</b>                    
                    <asp:Label ID="lblDataLimite" runat="server"  ></asp:Label>
                </td>
               
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap" width="1%">
                Chassi:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtChassi" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap" width="1%">
                Renavan:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtRenavan" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
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
            <td class="tdp" nowrap="nowrap" width="1%">
                CPF
                Motorista:</td>
            <td class="tdp" nowrap="nowrap" valign="baseline">
                <asp:TextBox ID="txtCPFMotorista" runat="server" AutoPostBack="True" 
                    CssClass="txt" ontextchanged="txtMotorista_TextChanged" Width="100px"></asp:TextBox>
&nbsp;<asp:ImageButton ID="imgProcurarMotorista" runat="server" Height="15px" 
                    ImageUrl="~/Intranet/Imagens/untitled.bmp" onclick="ImageButton2_Click" 
                    CausesValidation="False" />
            </td>
            <td class="tdp" nowrap="nowrap" width="1%">
                Nome Motorista:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtMotoristaNome" runat="server" CssClass="txt" 
                    ontextchanged="txtMotoristaNome_TextChanged" Width="95%"></asp:TextBox>
            </td>
        </tr>
        
         <tr style="background-color: Silver;">
            <td colspan="4" height="1">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                CPF
                Proprietario:</td>
            <td class="tdp" nowrap="nowrap" valign="baseline">
                <asp:TextBox ID="txtCPFProprietario" runat="server" AutoPostBack="True" 
                    CssClass="txt" ontextchanged="txtCPFProprietario_TextChanged" 
                    Width="100px"></asp:TextBox>
&nbsp;<asp:ImageButton ID="imgProcurarProprietario" runat="server" Height="15px" 
                    ImageUrl="~/Intranet/Imagens/untitled.bmp" onclick="ImageButton2_Click" 
                    CausesValidation="False" Width="16px" />
            </td>
            <td class="tdp" nowrap="nowrap">
                Nome Proprietário:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtMotoristaProprietario" runat="server" CssClass="txt" 
                    ontextchanged="txtMotoristaNome_TextChanged" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr style="background-color: Silver;">
            <td colspan="4" height="1">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Marca:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboMarca" runat="server" CssClass="cbo" Width="96%" 
                    AutoPostBack="True" onselectedindexchanged="cboMarca_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap">
                Modelo:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboModelo" runat="server" CssClass="cbo" Width="96%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Cor:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtCor" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtCor" ErrorMessage="Informe a Cor" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
            <td class="tdp" nowrap="nowrap">
                Ano / Modelo:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtano" runat="server" CssClass="txtValor" Width="40px"></asp:TextBox>
                /<asp:TextBox ID="txtanoModelo" runat="server" CssClass="txtValor" Width="45px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtano" ErrorMessage="Informe o Ano" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txtanoModelo" ErrorMessage="Informe o Ano Modelo" 
                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Tipo:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboTipo" runat="server" CssClass="cbo" Width="96%">
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap">
                Eixos:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txteixos" runat="server" CssClass="txtValor" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Capacidade m3:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtCapacidade" runat="server" CssClass="txtValor" 
                    Width="100px"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Capacidade Carga KG:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtCapacidadeKG" runat="server" CssClass="txtValor" 
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Restreador:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboRastreador" runat="server" CssClass="cbo" 
                    Width="96%">
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap">
                Número de Série:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtNumeroSerieEquipamento" runat="server" CssClass="txt" 
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr style="background-color: Silver;">
            <td colspan="4" height="1">
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Antt:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtAntt" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtAntt" ErrorMessage="Informe o Antt" 
                    SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
            <td class="tdp" nowrap="nowrap">
                Vencimento Antt:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtVencimentoAntt" runat="server" CssClass="txt" Width="100px" onblur="validaDat(this,this.value)"></asp:TextBox>
                
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtVencimentoAntt" 
                    UserDateFormat="DayMonthYear" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtVencimentoAntt" 
                    ErrorMessage="Informe a Data de Vencimento Antt" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                &nbsp;</td>
            <td class="tdp" nowrap="nowrap">
                <asp:Button ID="Button4" runat="server" Text="Button" Visible="False" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:TextBox ID="lblIdMotorista" runat="server" 
                    ontextchanged="txtMotoristaNome_TextChanged" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="0px" CssClass="txt" Height="0px" 
                    Width="0px"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:Label ID="lblCodEstado" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCodCidade" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="lblProprietario" runat="server" 
                    ontextchanged="txtMotoristaNome_TextChanged" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="0px" CssClass="txt" Height="0px" 
                    Width="0px"></asp:TextBox>
            </td>
            <td class="tdpR" nowrap="nowrap">
                <asp:Button ID="Button3" runat="server" CssClass="button" OnClick="Button3_Click"
                    Text="Confirmar" />
                &nbsp;<asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="button"
                    OnClientClick="javascript:history.back(-1);" Text="Voltar" />
            </td>
        </tr>
    </table>
</asp:Content>

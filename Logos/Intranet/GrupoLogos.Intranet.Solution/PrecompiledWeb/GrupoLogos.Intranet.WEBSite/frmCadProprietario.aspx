﻿<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Intranet_frmCadProprietario, App_Web_frmcadproprietario.aspx.cdcab7d2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Proprietário" Font-Bold="True"
                    Font-Size="14px"></asp:Label>
                <asp:Label ID="lblId" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" cellspacing="1" class="table" style="width: 100%" border="0">
        <tr>
            <td class="tdp" nowrap="nowrap" width="1%">
                CPF/CNPJ:
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtcpfcnpj" runat="server" CssClass="txt" Width="95%" OnTextChanged="txtcpfcnpj_TextChanged"
                    AutoPostBack="True" MaxLength="18"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcpfcnpj"
                    ErrorMessage="Informe o CNPJ / CPF" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
            <td class="tdp" nowrap="nowrap" width="1%">
                Nome/Razão Social:
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtRazao" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRazao"
                    ErrorMessage="Informe a Razão Social" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                IE/RG:
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtIE" runat="server" CssClass="txt" Width="95%" MaxLength="18"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Fantasia/Apelido &nbsp;
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtFantasiaApelido" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
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
                <asp:TextBox ID="txtCEP" runat="server" CssClass="txt" MaxLength="8" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCEP"
                    ErrorMessage="Informe o CEP" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
            <td class="tdp" nowrap="nowrap">
                Endereço:
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
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
                <asp:TextBox ID="txtComplemento" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
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
                <asp:TextBox ID="txtFoneResidencial" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Celular:
                <asp:HiddenField ID="idtelefoneCel" runat="server" />
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtFoneCelular" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Tefone Recado:
                <asp:HiddenField ID="idtelefoneRec" runat="server" />
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtFoneRecado" runat="server" CssClass="txt" Width="95%" MaxLength="60"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Nextel:<asp:HiddenField ID="idtelefoneNextel" runat="server" />
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtFoneNextel" runat="server" CssClass="txt" MaxLength="60" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr style="background-color: Silver;">
            <td colspan="4" height="1" style="font-size: 8pt; font-family: Verdana">
                FOTOS</td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap" colspan="4">
                  <iframe id="iframeFotos" runat="server" frameborder="0" height="390px" src="frmCarregarFoto.aspx?tipo=motorista" width="100%"></iframe>
                
                </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                <asp:Label ID="lblCodEstado" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </td>
            <td class="tdp" nowrap="nowrap">
                &nbsp;
            </td>
            <td class="tdpR" nowrap="nowrap">
                <asp:Button ID="Button3" runat="server" CssClass="button" OnClick="Button3_Click"
                    Text="Confirmar" />
                &nbsp;<asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="button"
                    OnClientClick="javascript:history.back(-1);" Text="Voltar" />
                <asp:Label ID="lblCodCidade" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

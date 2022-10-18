<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="frmDestinatarioDetalhe.aspx.cs"
    Inherits="frmDestinatarioDetalhe" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >
  
    <asp:Panel ID="pnlteste" runat="server">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Manutenção de Destinatários" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
        <asp:Label ID="lblCodDestinatario" runat="server" Text="0" Visible="False"></asp:Label>
    </td>
    </tr>
    </table>
    
        <table cellpadding="2" cellspacing="0" class="table" style="width: 100%">
            <tr>
                <td class="tdp" width="5%">
                    CNPJ/CPF:</td>
                <td class="tdp" width="35%">
                    <asp:TextBox ID="txtcpfcnpj" runat="server" CssClass="txt" Width="95%" 
                        ontextchanged="txtcpfcnpj_TextChanged" AutoPostBack="True" MaxLength="18"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtcpfcnpj" ErrorMessage="Informe o CNPJ / CPF">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap" width="5%">
                    RAZÃO SOCIAL:</td>
                <td class="tdp" width="35%">
                    <asp:TextBox ID="txtRazao" runat="server" CssClass="txt" Width="95%" 
                        MaxLength="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtRazao" ErrorMessage="Informe a Razão Social">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp" width="1%">
                    &nbsp;</td>
                <td class="tdp" width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp">
                    FANTASIA:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtFantasia" runat="server" CssClass="txt" Width="95%" 
                        MaxLength="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtFantasia" ErrorMessage="Informe o nome fantasia">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp">
                    CEP:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtCEP" runat="server" CssClass="txt" MaxLength="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtCEP" ErrorMessage="Informe o CEP">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    ENDEREÇO:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" Width="95%" 
                        MaxLength="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtEndereco" ErrorMessage="Informe endereço.">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    Nº.:</td>
                <td class="tdp" width="10%" valign="middle">
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="txt" Width="90%" 
                        MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtNumero" ErrorMessage="Informe o número">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    COMPLEMENTO:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtComplemento" runat="server" CssClass="txt" 
                        ontextchanged="TextBox4_TextChanged" Width="95%" MaxLength="60"></asp:TextBox>
                </td>
                <td class="tdp">
                    ESTADO:</td>
                <td class="tdp">
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" 
                        CssClass="cbo" onselectedindexchanged="cboEstado_SelectedIndexChanged" 
                        Width="96%">
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp">
                    CIDADE:</td>
                <td class="tdp">
                    <asp:DropDownList ID="cboCidade" runat="server" AutoPostBack="True" 
                        CssClass="cbo" Enabled="False" 
                        onselectedindexchanged="cboCidade_SelectedIndexChanged" Width="96%">
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    BAIRRO:</td>
                <td class="tdp">
                    <asp:DropDownList ID="cboBairro" runat="server" CssClass="cbo" 
                        onselectedindexchanged="cboEstado_SelectedIndexChanged" Width="96%">
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp" width="28%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp">
                    I.E.:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtIE" runat="server" CssClass="txt" Width="95%" 
                        MaxLength="18"></asp:TextBox>
                </td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    <asp:Label ID="lblCodBairro" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodEstado" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodCidade" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdpR">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="button" Text="Salvar" 
                        onclick="btnSalvar_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
        <br />
    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
        <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmSobraDetalhe, App_Web_frmsobradetalhe.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" __designer:mapid="13af" border="0" cellpadding="0" cellspacing="0">
        <tr __designer:mapid="13b0">
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                __designer:mapid="13b1">
                <asp:Label ID="lblTitulo" runat="server" Text="SOBRAS IDENTIFICADAS NAS FILIAIS"
                    Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="uplDivi" runat="server">
        <ContentTemplate>
            <div id="dvEscolherCliente" runat="server" visible="false" style="position: absolute;
                top: 30%; left: 35%; width: 400px">
                <table class="table">
                    <tr align="left" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                        height: 25px;">
                        <td colspan="3" style="font-family: Verdana; font-size: 9px; font-weight: bold">
                            Pesquisa de Cliente
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" nowrap="nowrap" style="width: 1%">
                            Iniciais:
                        </td>
                        <td class="tdp" nowrap="nowrap" style="width: 90%">
                            <asp:TextBox ID="txtFiltroNome" runat="server" CssClass="txt" Width="350px"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap" style="width: 1%">
                            <asp:Button ID="btnPesquisarFiltro" runat="server" CssClass="button" OnClick="btnPesquisarFiltro_Click"
                                Text="Pesquisar" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" colspan="3" style="font-family: Verdana; font-size: 8pt; font-weight: bold">
                            <table class="table" id="tblEscolherClientes" runat="server" visible="false">
                                <tr>
                                    <td class="tdp">
                                        <asp:Panel ID="Panel6" runat="server" Height="200px" ScrollBars="Auto" Width="100%">
                                            <asp:CheckBoxList ID="rdListClientes" runat="server" AutoPostBack="True" Font-Bold="False"
                                                Font-Names="Verdana" OnSelectedIndexChanged="rdListClientes_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblIdCliente" runat="server" Visible="False"></asp:Label>
    <br />
    <table cellpadding="1" cellspacing="1" class="table" width="99%">
        <tr>
            <td class="tdp" nowrap="nowrap" width="1%">
                Filial Origem:
            </td>
            <td class="tdp" width="48%">
                <asp:DropDownList ID="cboFilialOrigem" runat="server" CssClass="cbo" Font-Names="Arial"
                    Font-Size="7pt" Height="17px" Width="99%">
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap" width="1%">
                Filial Destino:
            </td>
            <td class="tdp" width="48%">
                <asp:DropDownList ID="cboFilialDestino" runat="server" CssClass="cbo" Font-Names="Arial"
                    Font-Size="7pt" Height="17px" Width="99%" TabIndex="1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Nome Colaborador:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtNomeColaborador" runat="server" CssClass="txt" MaxLength="100"
                    Width="99%" TabIndex="2"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Cliente:
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:TextBox ID="txtCliente" runat="server" CssClass="txt" MaxLength="100" TabIndex="3"
                    Width="70%"></asp:TextBox>
                <asp:Button ID="btnAbriPesquisarCliente" runat="server" CssClass="button" OnClick="Button2_Click"
                    Text="Pesquisar" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Número da Nota Fiscal:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="txt" MaxLength="100" Width="99%"
                    TabIndex="4"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Pré Nota Fiscal:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtPreNota" runat="server" CssClass="txt" MaxLength="100" Width="99%"
                    TabIndex="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                N° do Roteiro:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtNumeroRota" runat="server" CssClass="txt" MaxLength="100" Width="99%"
                    TabIndex="6"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                &nbsp;</td>
            <td class="tdp">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Tipo de Volume:
            </td>
            <td class="tdp">
                <asp:DropDownList ID="cboTipoDeVolume" runat="server" CssClass="cbo" Font-Names="Arial"
                    Font-Size="7pt" Height="17px" Width="99%" TabIndex="7">
                    <asp:ListItem>CAIXA</asp:ListItem>
                    <asp:ListItem>FARDOS</asp:ListItem>
                    <asp:ListItem>ITEM</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap">
                Descrição do Volume:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtDescricaoVolume" runat="server" CssClass="txt" MaxLength="100"
                    Width="99%" TabIndex="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Quantidade:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtQuantidade" runat="server" CssClass="txtValor" MaxLength="100"
                    Width="99%" TabIndex="9"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Data de Embarque:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="99%" TabIndex="10"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtI" />
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Nome do Motorista:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtNomeMotorista" runat="server" CssClass="txt" MaxLength="100"
                    Width="99%" TabIndex="11"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                Placa da Carreta:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtPlaca" runat="server" CssClass="txt" MaxLength="100" Width="99%"
                    TabIndex="12"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                Rota:
            </td>
            <td class="tdp">
                <asp:TextBox ID="txtRota" runat="server" CssClass="txt" MaxLength="100" Width="99%"
                    TabIndex="13"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                &nbsp;</td>
            <td class="tdp">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdp" nowrap="nowrap">
                &nbsp;
            </td>
            <td class="tdp">
                &nbsp;
            </td>
            <td class="tdp" nowrap="nowrap">
                &nbsp;
            </td>
            <td class="tdpR">
                <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" Width="100px"
                    OnClientClick="window.history.back()" TabIndex="15" />
                &nbsp;<asp:Button ID="btnGravarTudo" runat="server" CssClass="button" OnClick="btnGravarTudo_Click"
                    Text="Salvar" Width="100px" TabIndex="14" />
            </td>
        </tr>
    </table>
</asp:Content>

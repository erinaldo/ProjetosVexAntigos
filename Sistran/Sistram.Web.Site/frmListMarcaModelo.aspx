<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableTheming="true" Theme="Adm"
    CodeFile="frmListMarcaModelo.aspx.cs" Inherits="frmListMarcaModelo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="MARCA / MODELO" Font-Bold="true" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" style="vertical-align:top">
            <tr>
                <td style="width: 49%; vertical-align:top">
                    <asp:Label ID="lblTitulo0" runat="server" Font-Bold="True" Font-Size="14px" 
                        Text="MARCAS"></asp:Label>
                </td>
                <td style="width: 49%; vertical-align:top">
                    <asp:Label ID="lblTitulo1" runat="server" Font-Bold="True" Font-Size="14px" 
                        Text="MODELOS"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 49%; vertical-align:top">
                    <asp:Panel ID="pnlMarca" runat="server" BorderColor="#666666" BorderWidth="1">
                        <table border="0" cellpadding="1" cellspacing="1" class="table">
                            <tr>
                                <td class="tdp" nowrap="nowrap" width="99%">
                                    <asp:Label ID="lblMracaSelecionada" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtMarcaSelecionada" runat="server" CssClass="txt" 
                                        Visible="false" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtMarcaSelecionada" ErrorMessage="Informe o Nome..." 
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    &nbsp;<asp:Label ID="lblMracaID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:Button ID="btnAlterarMarca" runat="server" CssClass="button" 
                                        onclick="btnAlterarMarca_Click" Text="Novo" />
                                    &nbsp;<asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                        CssClass="button" onclick="btnCancelar_Click" Text="Cancelar" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                        ShowMessageBox="True" ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" colspan="2">
                                    <asp:PlaceHolder ID="PHMarca" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="width: 49%; vertical-align:top">
                    <asp:Panel ID="pnlModelo" runat="server" BorderColor="#666666" BorderWidth="1">
                        <table border="0" cellpadding="1" cellspacing="1" class="table">
                            <tr>
                                <td class="tdp" width="99%">
                                    <asp:Label ID="lblModeloSelecionado" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtModeloSelecionada" runat="server" CssClass="txt" 
                                        Visible="false" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtModeloSelecionada" ErrorMessage="Informe o Nome..." 
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:Label ID="lblModeloID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="tdp" nowrap="nowrap">
                                    <asp:Button ID="btnAlterarMarca0" runat="server" CssClass="button" 
                                        onclick="btnAlterarMarca0_Click" Text="Novo" />
                                    &nbsp;<asp:Button ID="btnCancelar0" runat="server" CausesValidation="False" 
                                        CssClass="button" onclick="btnCancelar0_Click" Text="Cancelar" 
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp">
                                    <asp:PlaceHolder ID="PHModelo" runat="server"></asp:PlaceHolder>
                                </td>
                                <td class="tdp">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

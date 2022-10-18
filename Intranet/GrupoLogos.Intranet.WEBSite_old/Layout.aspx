<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Layout.aspx.cs"
    Inherits="Layout" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="table" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td class="tdp" width="1%">
                    Filial:
                </td>
                <td class="tdp" width="48%">
                    <asp:DropDownList ID="cboFilial" runat="server" AutoPostBack="True" CssClass="cbo"
                        Font-Names="Arial" Font-Size="7pt" Height="17px" OnSelectedIndexChanged="cboFilial_SelectedIndexChanged"
                        Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="tdp" width="1%">
                    Depósito:
                </td>
                <td class="tdp">
                    <asp:DropDownList ID="cboDeposito" runat="server" AutoPostBack="True" CssClass="cbo"
                        Font-Names="Arial" Font-Size="7pt" Height="17px" OnSelectedIndexChanged="cboDeposito_SelectedIndexChanged"
                        Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdp" width="1%">
                    Planta:</td>
                <td class="tdp" width="48%">
                    <asp:DropDownList ID="cboPlanta" runat="server" AutoPostBack="True" 
                        CssClass="cbo" Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        OnSelectedIndexChanged="cboPlanta_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="tdp" width="1%">
                    Rua:</td>
                <td class="tdpR">
                    <asp:DropDownList ID="cboRua" runat="server" AutoPostBack="True" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        OnSelectedIndexChanged="cboRua_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdp" width="1%">
                    &nbsp;</td>
                <td class="tdp" width="48%">
                    &nbsp;</td>
                <td class="tdp" width="1%">
                    &nbsp;</td>
                <td class="tdpR">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" Text="Pesquisar" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

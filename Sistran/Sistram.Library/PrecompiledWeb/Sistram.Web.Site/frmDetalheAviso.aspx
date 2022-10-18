<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmDetalheAviso, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button2">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Avisos" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
            <table cellpadding="1" cellspacing="0" class="table" style="width: 100%">
                <tr>
                    <td class="tdp" width="1%">
                        Usuário:</td>
                    <td class="tdp" width="30%">
                        <asp:DropDownList ID="cboUsuario" runat="server" CssClass="cbo" Width="300px">
                        </asp:DropDownList>
                        <asp:Label ID="lblIdAviso" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblIdUsuario" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="tdp">
                        Operação:</td>
                    <td class="tdp" width="30%">
                        <asp:DropDownList ID="cboOperacao" runat="server" CssClass="cbo" Width="305px">
                            <asp:ListItem Selected="True">APROVAR PEDIDO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        Divisão:</td>
                    <td class="tdp">
                        <asp:DropDownList ID="cboDivisao" runat="server" CssClass="cbo" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdp" nowrap="nowrap" width="1%">
                        Canal de Venda</td>
                    <td class="tdp">
                        <asp:DropDownList ID="cboCanalVenda" runat="server" CssClass="cbo" 
                            Width="305px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdpR" colspan="4">
                        <asp:Button ID="Button2" runat="server" CssClass="button" 
                            onclick="Button2_Click" Text="Confirmar" />
                    </td>
                </tr>
            </table>
        <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs"
    Inherits="Reports" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Relatórios" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <asp:UpdatePanel ID="upp" runat="server">
    <Triggers  >
    <asp:PostBackTrigger ControlID="RadioButtonList2"/> 

    </Triggers>
    <ContentTemplate>
    
    
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    <tr valign="bottom" >
    <td class="tdp" width="10%" nowrap="nowrap">Tipo:</td>
        <td class="tdp" width="25%">
            &nbsp;</td>
    </tr>
    
        <tr valign="baseline" >
            <td class="tdp" valign="middle" colspan="2">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" 
                    >
                    <asp:ListItem Value="1">Lista Simples de Usuários</asp:ListItem>
                    <asp:ListItem Value="2">Lista de Usuários / Divisões</asp:ListItem>
                    <asp:ListItem Value="3">Lista Usuários / Divisões / Produtos</asp:ListItem>
                </asp:RadioButtonList>
                </td>
        </tr>
    
        <tr valign="baseline">
            <td class="tdp" colspan="2" valign="middle" bgcolor="White">
                <asp:Panel ID="PnlReport" runat="server" BorderColor="Silver" 
                    BorderStyle="Solid" BorderWidth="1px" Height="600px">
                </asp:Panel>
            </td>
        </tr>
    
    </table>
    </ContentTemplate></asp:UpdatePanel>
    
    
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmExportarPedido, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnConfirmar">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Operações com Arquivos" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
 <asp:UpdatePanel ID="UpdatePanel565" runat="server"> 
<Triggers> 
<asp:PostBackTrigger ControlID="btnConfirmar" /> 
</Triggers> 
<ContentTemplate> 

        
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    <tr valign="middle" >
    <td class="tdp" nowrap="nowrap" width="1%">
        Operação:</td>
        <td class="tdp" nowrap="nowrap">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">Enviar Pedido</asp:ListItem>
                <asp:ListItem Value="1">Receber Nota Fiscal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    
        <tr valign="middle">
            <td class="tdp" nowrap="nowrap" width="1%">
                Selecione o Arquivo:</td>
            <td class="tdp" nowrap="nowrap">
                <asp:FileUpload ID="uplArquivo" runat="server" CssClass="fileUpload" 
                    Enabled="False" Height="20px" />
                &nbsp;<asp:Button ID="btnConfirmar" runat="server" CssClass="button" Height="17px" 
                    onclick="btnConfirmar_Click" Text="Iniciar" />
            </td>
        </tr>
    
    </table>
    </ContentTemplate> 
</asp:UpdatePanel> 
   
    </asp:Panel>
</asp:Content>

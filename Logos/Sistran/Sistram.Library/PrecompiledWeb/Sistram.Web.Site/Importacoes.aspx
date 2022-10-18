<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Importacoes, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnConfirmar">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Importação" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
 <asp:UpdatePanel ID="UpdatePanel55" runat="server"> 
<Triggers> 
<asp:PostBackTrigger ControlID="btnConfirmar" /> 
</Triggers> 
<ContentTemplate> 

        
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    <tr valign="bottom" >
    <td class="tdp" width="10%" nowrap="nowrap">
    Selecione um Arquivo(.txt):<asp:FileUpload ID="uplArquivo" runat="server" 
            CssClass="fileUpload" /> 

          
        &nbsp;<asp:Button ID="btnConfirmar" runat="server" CssClass="button" 
            onclick="btnConfirmar_Click" Text="Confirmar" />

          
        </td>
        <td class="tdpR" width="1%" valign="bottom">
            &nbsp;</td>
    </tr>
    
    </table>
    </ContentTemplate> 
</asp:UpdatePanel> 


    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
        <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

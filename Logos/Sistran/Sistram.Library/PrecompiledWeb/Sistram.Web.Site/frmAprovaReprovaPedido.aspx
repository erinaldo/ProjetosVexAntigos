<%@ page language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="frmAprovaReprovaPedido, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >


<asp:Panel ID="pnlteste" runat="server" HorizontalAlign="Center"  Height="600">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px" 
            align="left">
    <asp:Label ID="lblTitulo" runat="server" Text="Autorizar Pedido" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    <div style="position:absolute; top:40%; left:35%; width:500px">
      
                        <asp:Panel ID="Panel4" runat="server" BackColor="#cccccc" 
                            style="text-align: center" Width="99%" HorizontalAlign="Center" 
                            Height="60">
                            <br />
                            <asp:Label ID="Label1" runat="server" style="font-weight: 700; font-size: 8pt" 
                                Text=""></asp:Label>
                            <br />
                        </asp:Panel>
 </div>   
    
</asp:Panel>
</asp:Content>

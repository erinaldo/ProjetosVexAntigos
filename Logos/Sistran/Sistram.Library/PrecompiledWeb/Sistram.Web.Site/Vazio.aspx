<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Vazio, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

<div style="position:absolute; top:50%; left:45%">
    
    <asp:Label ID="lbl" runat="server" Text="Escolha uma opção no menu." Font-Bold="true" Font-Size="8pt" Font-Names="Verdana" ></asp:Label>

</div>


    <asp:Panel ID="pnlteste" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Home Page" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" 
            Visible="False" />
    </td>
    </tr>
    </table>
    
    </asp:Panel>
</asp:Content>

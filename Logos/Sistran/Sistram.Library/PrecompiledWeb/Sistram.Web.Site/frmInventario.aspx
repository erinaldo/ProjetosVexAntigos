<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmInventario, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function Expandir(rua) {

            //alert(rua);
            if (document.getElementById(rua).style.display == 'block') {
                document.getElementById(rua).style.display = 'none';
                document.getElementById('tr' + rua + '1').style.display = 'none';
                document.getElementById('tr' + rua + '3').style.display = 'none';
            }
            else {
                document.getElementById(rua).style.display = 'block';
                document.getElementById('tr' + rua + '1').style.display = 'block';
                document.getElementById('tr' + rua + '3').style.display = 'block';
            }
        }
    
    </script>

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
                    Filial:</td>
                <td class="tdp" width="48%">
                    <asp:DropDownList ID="cboFilial" runat="server" AutoPostBack="True" 
                        CssClass="cbo" Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        OnSelectedIndexChanged="cboFilial_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="tdp" width="1%">
                    Inventario:</td>
                       <td class="tdp">
                           <asp:DropDownList ID="cboInventario" runat="server" AutoPostBack="True" 
                               CssClass="cbo" Font-Names="Arial" Font-Size="7pt" Height="17px" 
                               OnSelectedIndexChanged="cboInventario_SelectedIndexChanged" Width="100%">
                           </asp:DropDownList>
                </td>
                <td class="tdpR" width="1%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp" width="1%">
                    Contagem:</td>
                <td class="tdp" width="48%">
                    <asp:DropDownList ID="cboContagem" runat="server" AutoPostBack="True" 
                        CssClass="cbo" Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        onselectedindexchanged="cboContagem_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="tdp" width="1%">
                    &nbsp;</td>
                <td class="tdpR">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" Text="Pesquisar" Visible="False" />
                </td>
                <td class="tdpR" width="1%">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

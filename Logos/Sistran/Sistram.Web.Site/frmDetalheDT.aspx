<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmDetalheDT.aspx.cs" Inherits="frmDetalheDT" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px; text-align: left;">
                    <asp:Label ID="lblTitulo" runat="server" Text="DOCUMENTO DE TRANSPORTE" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <center>
        <table style="width: 100%" border="0" cellpadding="2" 
            cellspacing="2">
            <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; font-size: 8pt; font-family: Verdana;">
                <td width="30%" style="text-align: center" class="tdpCabecalho">
                    <b>DT </b>
                </td>
                <td style="text-align: center" class="tdpCabecalho">
                    <b>VEÍCULO </b>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:PlaceHolder ID="ph_dt" runat="server"></asp:PlaceHolder>
                </td>
                <td>                    
                    <asp:PlaceHolder ID="ph_Veiculo" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            
            <tr style="background-image: url(Images/skins/primeiro/img/menu_3_2.jpg); height: 20px">
                <td style="text-align: center; font-size: 8pt; font-family: Verdana;" 
                    colspan="2" class="tdpCabecalho">
                    <b>MOTORISTA </b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:PlaceHolder ID="ph_Motorista" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
        </center>
        
    </asp:Panel>
</asp:Content>

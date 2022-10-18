<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmLocalizarDTClienteMapa.aspx.cs" Inherits="frmLocalizarDTClienteMapa" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Cidade"
                    Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="width: 99%; height: 99%; text-align: center;">
        <cc1:GoogleMap ID="mapa" runat="server" Width="100%" Height="100%">
        </cc1:GoogleMap>
    </div>
</asp:Content>

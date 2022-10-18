<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" inherits="NFRobo, App_Web_vjligygf" title="Detalhe Notas Fiscais Aguardando Embarque" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
    <table width="60%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 0%;" align="left" nowrap="nowrap">
                <b>NOTAS FISCAIS AGUARDANDO EMBARQUE</b>
            </td>
            <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 25%;" align="right">
                <asp:Button ID="Button1" runat="server" CssClass="button" 
                    Text="IMPRIMIR / EXPORTAR" />
            </td>
        </tr>
        <tr>
            <td width="50%" height="5" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="50%" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                    <asp:PlaceHolder ID="PhAguradandoEmbarque" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="20">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url(Images/skins/primeiro/img/menu_3_2.jpg);"
                align="left" colspan="2">
                <b>
                <span style="font-size:9.0pt;font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;;
mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;;
text-transform:uppercase;mso-ansi-language:PT-BR;mso-fareast-language:PT-BR;
mso-bidi-language:AR-SA">Notas Fiscais Aguardando Embarque em Atraso (Acima de 3 dias)</span></b></td>
        </tr>
        </tr>
        <tr>
            <td height="5" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" Style="text-align: left">
                                <asp:PlaceHolder ID="PhEmAtraso" runat="server"></asp:PlaceHolder>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

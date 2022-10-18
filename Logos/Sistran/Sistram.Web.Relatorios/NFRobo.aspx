<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="NFRobo.aspx.cs" Inherits="NFRobo" Title="Detalhe Notas Fiscais Aguardando Embarque" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
    <table width="60%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style=" font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 0%;" align="left" nowrap="nowrap">
                <b>NOTAS FISCAIS AGUARDANDO EMBARQUE</b>
            </td>
            <td width="1%" style="font-size: 9pt; font-family: verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 20px; width: 25%;" align="right">
                <asp:Button ID="btntodos" runat="server" CssClass="button" 
                    Text="EXPORTAR TODAS NOTAS FISCAIS" onclick="btntodos_Click" />
            &nbsp;<asp:Button ID="Button1" runat="server" CssClass="button" 
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
                align="left" nowrap="nowrap" width="99%">
                <span class="apple-style-span"><b>
                <span style="font-size:9.0pt;font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;;color:black;
text-transform:uppercase;mso-ansi-language:PT-BR;mso-fareast-language:PT-BR;
mso-bidi-language:AR-SA">NOTAS FISCAIS AGUARDANDO EMBARQUE 72 hs ou mais</span></b></span></td>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); text-align: right;"
                align="left">
                <asp:Button ID="btntodos0" runat="server" CssClass="button" 
                    Text="EXPORTAR NOTAS FISCAIS " onclick="btntodos0_Click" />
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
          <tr>
            <td colspan="2" height="20">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url(Images/skins/primeiro/img/menu_3_2.jpg);"
                align="left">
                <span class="apple-style-span"><b>
                <span style="font-size:9.0pt;font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;;color:black;
text-transform:uppercase;mso-ansi-language:PT-BR;mso-fareast-language:PT-BR;
mso-bidi-language:AR-SA">-&gt;&nbsp;NOTAS FISCAIS AGUARDANDO EMBARQUE 48 HS</span></b></span></td>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); text-align: right;"
                align="left">
                <asp:Button ID="btntodos1" runat="server" CssClass="button" 
                    Text="EXPORTAR NOTAS FISCAIS " onclick="btntodos1_Click" />
            </td>
        </tr>
         <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" Style="text-align: left">
                                <asp:PlaceHolder ID="phEmAtraso2dias" runat="server"></asp:PlaceHolder>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr>
            <td colspan="2" height="20">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url(Images/skins/primeiro/img/menu_3_2.jpg);"
                align="left">
                <span class="apple-style-span"><b>
                <span style="font-size:9.0pt;font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;;color:black;
text-transform:uppercase;mso-ansi-language:PT-BR;mso-fareast-language:PT-BR;
mso-bidi-language:AR-SA">NOTAS FISCAIS AGUARDANDO EMBARQUE 24 HS&nbsp;</span></b></span></td>
            <td style="height: 20px; font-size: 9pt; height: 20px; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); text-align: right;"
                align="left">
                <asp:Button ID="btntodos2" runat="server" CssClass="button" 
                    Text="EXPORTAR NOTAS FISCAIS " onclick="btntodos2_Click" />
            </td>
        </tr>
         <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" Style="text-align: left">
                                <asp:PlaceHolder ID="phEmAtraso1dia" runat="server"></asp:PlaceHolder>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="5" colspan="2">
            </td>
        </tr>
       
    </table>
    </center>
</asp:Content>

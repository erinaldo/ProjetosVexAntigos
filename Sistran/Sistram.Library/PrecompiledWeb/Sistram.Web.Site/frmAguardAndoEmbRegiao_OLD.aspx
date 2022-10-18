<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmAguardAndoEmbRegiao_OLD, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr align="right">
                <td>
                    <asp:Button ID="btnGerarReport0" runat="server" CssClass="button" Text="Gerar Excel"
                        Width="60px" />
                    <label id="lblsep" runat="server" style="width: 10px">
                        &nbsp;</label>
                    <br />
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

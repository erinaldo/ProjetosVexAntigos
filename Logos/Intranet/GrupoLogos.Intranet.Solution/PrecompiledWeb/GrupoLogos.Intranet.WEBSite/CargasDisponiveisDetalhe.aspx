<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="CargasDisponiveisDetalhe, App_Web_cargasdisponiveisdetalhe.aspx.cdcab7d2" theme="Adm" enabletheming="true" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="UserControls/ctrCargasDispDet.ascx" tagname="ctrCargasDispDet" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="dvAjudaTransitTime" runat="server" style="position: absolute; top: 30%;
            left: 45%; text-align: center; display: none; width: 300px; border-color: Silver;
            border-style: solid; border-width: 1px">
            <table cellpadding="2" cellspacing="2" border="0" style="background-color: #FFFFDD"
                width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                    <asp:Label ID="lbltituloAjuda" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                    <hr />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblAjuda" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:LinkButton ID="vv" runat="server" Text="FECHAR [X]" CssClass="link"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="Panel3" runat="server">

            <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 16px" valign="top">
                        
                        
                        <uc1:ctrCargasDispDet ID="ctrCargasDispDet1" runat="server" />
                        
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

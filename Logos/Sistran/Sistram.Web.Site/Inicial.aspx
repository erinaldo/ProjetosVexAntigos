<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Inicial.aspx.cs"
    Inherits="Inicial" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                    <asp:Label ID="lblTitulo" runat="server" Text="Home" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');" width="1%">
                    &nbsp;
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');" width="1%">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="pnl" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel3" runat="server" BorderStyle="None" BorderWidth="0px" BorderColor="White">
                    <table border="0" cellpadding="3" cellspacing="3" style="width: 100%; font-size: 8pt;
                        border-style: solid; border-width: 0px">
                        <tr style="text-align: left;">
                            <td style="text-align: left; font-family: verdana; font-size: 8pt; font-weight: bold;">
                                <fieldset style="width: 98%; border: 1px solid #CCC;">
                                    <legend>NOTAS FISCAIS EMITIDAS</legend>
                                    <table width="100%" cellpadding="2" cellspacing="2" class="table">
                                        <tr>
                                            <td class="tdp" style="width: 1%">
                                                <b>PERÍODO APURADO:</b>
                                            </td>
                                            <td class="tdp" nowrap="nowrap" style="width: 30%">
                                                <b>
                                                    <asp:Label ID="lblPeriodo" runat="server"></asp:Label></b>
                                            </td>
                                            <td class="tdp" style="width: 1%">
                                                <b>QUANTIDADE APURADA NO PERÍODO:</b>
                                            </td>
                                            <td class="tdp">
                                                <b>
                                                    <asp:Label ID="lblqtdEmitidas" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td class="tdp" style="width: 1%; text-align: right;" nowrap="nowrap">
                                                <b style="text-transform: uppercase;">
                                                    <asp:Label ID="lblTempo" runat="server" Font-Names="Verdana" Font-Size="7pt"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3">
                                <table style="min-width: 50%; margin: 0 auto;" border="0">
                                    <tr>
                                        <td style="text-align: left; font-family: verdana; font-size: 7pt; font-weight: bold;">
                                            <fieldset style="width: 98%; border: 1px solid #CCC;">
                                                <legend>FILTROS </legend>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="1" style="text-align:left ">
                                                    <tr>
                                                        <td style="border:1px solid silver">
                                                            SÉRIES
                                                        </td>
                                                        <td style="border:1px solid silver">
                                                            TIPO DE CLIENTE
                                                        </td>
                                                        <td style="vertical-align:middle; text-align:right" rowspan="2">
                                                         <asp:Button ID="Button2" runat="server" CssClass="button" OnClick="Button2_Click"
                                                                Text="Atualizar"      style="height: 25px;font-size: 11px;text-transform: uppercase;"
                                                                 />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" style="border:1px solid silver">
                                                            <asp:CheckBoxList ID="chkSeries" runat="server" RepeatColumns="3">
                                                                <asp:ListItem Selected="True" Text="Outras Séries" Value="Todas"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="DEV" Value="Todas"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="RET" Value="Todas"></asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td style="border:1px solid silver">
                                                            <asp:CheckBoxList ID="chkClientes" runat="server" RepeatColumns="3">
                                                                  <asp:ListItem Text="Somente Entregas Especiais" Value="SIM"></asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                       
                                                    </tr>
                                        </td>
                                    </tr>
                                </table>
                                <%--</fieldset>--%>
                            </td>
                        </tr>
                    </table>
                    <fieldset style="width: 98%; border: 1px solid #CCC;">
                        <legend style="font-weight: bold">EXPEDIÇÃO - AGUARDANDO EMBARQUE</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="phAguardandoEmbarqueDataFilial" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </td> </tr>
                    <%--<tr>
                            <td colspan="3">
                                <fieldset style="width: 98%; border: 1px solid #CCC;">
                                    <legend style="font-weight: bold">DEVOLUÇÃO - AGUARDANDO EMBARQUE PARA O EMITENTE</legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:PlaceHolder ID="phAguardandoDevolucao" runat="server"></asp:PlaceHolder>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>--%>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3">
                                <fieldset style="width: 98%; border: 1px solid #CCC;">
                                    <legend style="font-weight: bold">NOTAS POR SITUAÇÃO</legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div style="float: left; font-weight: bold; margin-bottom: 10px; text-align: left;">
                                                    <table border="0" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td>
                                                                SITUAÇÃO:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cboSituacao" runat="server" CssClass="cbo" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="cboSituacao_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <asp:PlaceHolder ID="phPorSituacao" runat="server"></asp:PlaceHolder>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; float: left">
            <tr>
                <td style="text-align: right">
                    <asp:Timer ID="Timer1" runat="server">
                    </asp:Timer>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

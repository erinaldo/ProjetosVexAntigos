<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Inicial.aspx.cs"
    Inherits="Inicial" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                    <asp:Label ID="lblTitulo" runat="server" 
                        Text="NOTAS FICAIS - ANDAMENTO DE ENTREGAS" Font-Bold="True" Font-Size="14px"></asp:Label>
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
            <ContentTemplate>
                <fieldset style="width: 600px; margin: 0 auto; border: 1px solid silver; margin-top: 18px; font-family:Verdana; font-size:7pt; font-weight:bold">
                    <legend>FILTROS</legend>
                    <table>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Filial:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboFilial" runat="server" AutoPostBack="True" 
                                    CssClass="cbo" OnSelectedIndexChanged="cboFilial_SelectedIndexChanged1" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Cliente:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboCliente" runat="server" AutoPostBack="True" 
                                    CssClass="cbo" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged" 
                                    Width="300px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Séries:</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkSeries" runat="server" RepeatColumns="3">
                                    <asp:ListItem Selected="True" Text="Outras Séries" Value="Todas"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="DEV" Value="Todas"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="RET" Value="Todas"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="Button2" runat="server" CssClass="button" 
                                    OnClick="Button2_Click" 
                                    Style="height: 25px; font-size: 11px; text-transform: uppercase;" 
                                    Text="Atualizar" />
                            </td>
                        </tr>
                    </table>
                </fieldset><br />
                <br />
                <br />
                <br />&nbsp;<asp:Panel ID="Panel3" runat="server" 
                    BorderColor="White" BorderStyle="None" BorderWidth="0px">
                    <table>
                        <tr>
                            <td colspan="3">
                                <table border="0" style="min-width: 50%; margin: 0 auto;">
                                    <tr>
                                        <td style="text-align: left; font-family: verdana; font-size: 7pt; font-weight: bold;">
                                            &nbsp;</td>
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
                                    <asp:PlaceHolder ID="phAguardandoEmbarqueDataFilial" runat="server">
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </td>
                    </tr>
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
                                                                <asp:DropDownList ID="cboSituacao" runat="server" AutoPostBack="True" 
                                                                    CssClass="cbo" OnSelectedIndexChanged="cboSituacao_SelectedIndexChanged">
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
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RelatorioProducaoRE.aspx.cs" Inherits="RelatorioProducaoRE" Title="Relatório de Produção por Relação de Entrega" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

   <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                            height: 25px">
                            <asp:Label ID="lblTitulo" runat="server" Text="Produção Por RE"
                                Font-Bold="True" Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvAjudaTransitTime" runat="server" style="position: absolute; top: 30%;
                    left: 45%; text-align: center; display: none; width:300px; border-color:Silver; border-style:solid; border-width:1px">
                    <table cellpadding="2" cellspacing="2" border="0" style="background-color:#FFFFDD" width="100%" >
                        <tr>
                            <td>                                
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <asp:Label ID="lbltituloAjuda" runat="server" ></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        
                                          <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <hr />
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr  align="left">
                                            <td>
                                                <asp:Label ID="lblAjuda" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="vv" runat="server"  Text="FECHAR [X]" CssClass="link"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="baseline">
                        <td class="tdp" nowrap="nowrap" valign="middle" style="width: 3%">
                            Emissão:
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" style="width: 5%">
                            <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtI" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                                UserDateFormat="DayMonthYear">
                            </asp:MaskedEditExtender>
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="baseline" width="50%">
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updBot" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                    OnClick="Button1_Click" Text="Pesquisar" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width: 100%" border="0">
                        <tr>
                            <td valign="top" style="height: 16px">
                               
                                <asp:PlaceHolder ID="PhTotais" runat="server"></asp:PlaceHolder>
                                <br />
                               
                               </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

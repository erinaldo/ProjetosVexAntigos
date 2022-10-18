<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DesempenhoEntregaDia.aspx.cs"
    Inherits="DesempenhoEntregaDia" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Dia" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    
        <tr valign="baseline" >
            <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                Emissão:</td>
            <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox> 
                <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtI" />
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
                
                &nbsp;Até:
                <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtF" />
                <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtF" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
                </td>
            <td class="tdp" nowrap="nowrap" valign="baseline">
                 <asp:UpdatePanel ID="updBot" runat="server" >
               <ContentTemplate>
                <table align="left" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td >
                        <asp:UpdatePanel ID="x" runat="server"><ContentTemplate>
                            <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                                Font-Size="7pt" OnClick="Button1_Click" 
                                Text="Pesquisar" />
                        </ContentTemplate></asp:UpdatePanel>
                        
                        </td>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                            <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                                Font-Names="Arial" Font-Size="7pt" 
                                Text="EXCEL" Width="60px" onclick="btnGerarReport_Click" />
                                </ContentTemplate></asp:UpdatePanel>
                        </td>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
                            <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" 
                                Font-Size="7pt" Text="PDF" Visible="False" 
                                Width="40px" onclick="btnPDF_Click" />
                                </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
</ContentTemplate>
               </asp:UpdatePanel> 
            </td>
        </tr>
    
    </table>
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                    <td valign="top">
                        <asp:Panel ID="pnlGrafico" runat="server" Visible="False">
                            <table style="width: 100%">
                            <tr>
                            <td style="text-align: center; font-size: 8pt" align="center">
                                <b style="text-align: center">N.F. Entregues</b></td>
                            <td align="center" style="font-size: 8pt; text-align: center">
                                <b style="text-align: center">Acumulado</b></td>
                            </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafPerc" runat="server" Visible="False" BorderColor="Red" 
                                            HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafAcum" runat="server" HorizontalAlign="Center">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 8pt; text-align: center" valign="top">
                                        <b style="text-align: center">N.F. Não Entregues</b></td>
                                    <td valign="top">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="pnlGrafPerc0" runat="server" BorderColor="Red" 
                                            HorizontalAlign="Center" Visible="False">
                                        </asp:Panel>
                                    </td>
                                    <td valign="top">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    
    
    
    
    </asp:Panel>
</asp:Content>

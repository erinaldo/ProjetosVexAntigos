<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="DesempenhoEntregaCidade, App_Web_desempenhoentregacidade.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Cidade" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    
        <tr valign="baseline" >
            <td class="tdp" nowrap="nowrap" valign="middle" style="width: 1%">
                Emissão:</td>
            <td class="tdp" nowrap="nowrap" valign="middle" style="width: 5%">
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
            <td class="tdp" nowrap="nowrap" width="1%">
                Ordenar :</td>
            <td class="tdp" nowrap="nowrap" width="1%">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cbo">
                    <asp:ListItem Selected="True">Transit Time</asp:ListItem>
                    <asp:ListItem>Filial</asp:ListItem>
                    <asp:ListItem>Cidade</asp:ListItem>
                    <asp:ListItem>N.F Entregues</asp:ListItem>
                    <asp:ListItem>N.F. Não Entregues</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cbo">
                    <asp:ListItem Value="DESC" Selected=True>Decrecente</asp:ListItem>
                
                    <asp:ListItem Value="ASC">Crescente</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdp" nowrap="nowrap" valign="baseline" width="50%" align="left">
             <asp:UpdatePanel ID="updBot" runat="server" >
               <ContentTemplate>
                <table align="left" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td>
                        <asp:UpdatePanel ID="xxx" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                                Font-Size="7pt" OnClick="Button1_Click" 
                                Text="Pesquisar" />
                        </ContentTemplate> 
                        </asp:UpdatePanel>
                        </td>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                                Font-Names="Arial" Font-Size="7pt" 
                                Text="EXCEL" Width="60px" onclick="btnGerarReport_Click" />
                         </ContentTemplate> 
                        </asp:UpdatePanel>
                        </td>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" 
                                Font-Size="7pt" Text="PDF" Visible="False" 
                                Width="40px" onclick="btnPDF_Click" />
                                
                          </ContentTemplate> 
                        </asp:UpdatePanel>
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
                        <table ID="tbTotais" runat="server" border="0" cellpadding="2" cellspacing="2" 
                            class="table" 
                            style="width: 200px; font-family: Verdana; font-size: 8pt; font-weight: bold;" 
                            visible="false">
                            <tr>
                                <td class="tdp" colspan="2" 
                                    style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 14px; text-align: center; font-family: vERdana; font-size: 7pt; font-weight: bold;">
                                    TOTAIS
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" 
                                    style="font-family: VERdana; font-size: 7pt; font-weight: bold;">
                                    N.F. Entregues:
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="Label2" runat="server" Style="font-size: 7pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdp" nowrap="nowrap" 
                                    style="font-family: VERdana; font-size: 7pt; font-weight: bold;">
                                    N.F. Não Entregues:
                                </td>
                                <td class="tdpR">
                                    <asp:Label ID="Label3" runat="server" Style="font-size: 7pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 15px; font-size: 7pt;">
                                    TOTAL:
                                </td>
                                <td class="cabecalho" 
                                    style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 15px">
                                    <asp:Label ID="Label1" runat="server" Style="font-size: 7pt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="25%">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </asp:Panel>
           
    </asp:Panel>
</asp:Content>

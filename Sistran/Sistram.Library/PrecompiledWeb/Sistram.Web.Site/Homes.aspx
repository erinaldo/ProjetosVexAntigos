<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Homes, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" BorderStyle="None" 
    BorderWidth="0px">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="Home" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Button ID="Button2" runat="server" CssClass="button" Text="Relatório" 
                    Visible="False" />
                &nbsp;<asp:Button ID="Button3" runat="server" CssClass="button" Text="PDF" 
                    Width="35px" Visible="False" />
            </td>
        </tr>
    </table>
    
        <asp:Timer ID="Timer1" runat="server" Interval="50000" ontick="Timer1_Tick">
        </asp:Timer>
        
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <asp:Image ID="imgHome" runat="server" Height="450px" 
                ImageUrl="~/imgInicialDicate/logo_rastreamento.gif" Visible="False" />
            <span style="font-family: Verdana; text-align: right;">
            <asp:Button ID="Button5" runat="server" BackColor="White" BorderStyle="None" 
                ForeColor="#999999" Height="5px" onclick="Button5_Click" Text="." Width="5px" />
            </span>
        </asp:Panel>
                
    <asp:UpdatePanel ID="pnl" runat="server">
    <Triggers >
    <asp:AsyncPostBackTrigger ControlID="Timer1" />
    </Triggers>
    <ContentTemplate>
    
    
        <asp:Panel ID="Panel3" runat="server" >
        <center>
        <asp:Label ID="lblTempo" runat="server" Font-Names="Verdana" Font-Size="7pt"></asp:Label>
        <br />
            <table>
           
            <tr>
            
            <td  valign="top" class="tdpCenter">
                <asp:Label ID="Label1" runat="server" Text="AGUARDANDO TRADE MKT APROVAR" 
                    Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label> 
            </td>
            
            <td>&nbsp;</td>
            
            <td  valign="top" class="tdpCenter">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="8pt" Text="LIBERADO PARA EMBARQUE"></asp:Label>
            </td>
            </tr>
            
            
                <tr>
                    <td  valign="top">
                        <asp:PlaceHolder ID="PHAutorizacao" runat="server"></asp:PlaceHolder>
                    </td>
                    <td>&nbsp;</td>
                    <td  valign="top">
                        <asp:PlaceHolder ID="PHEnviadoParaFaturamento" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tdpCenter" colspan="3" valign="top">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                            Text="ENVIADO PARA FATURAMENTO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top">
                        <asp:PlaceHolder ID="PHEnviarParaFaturamento" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top">
                        &nbsp;</td>
                </tr>

            </table>
            </center>
        </asp:Panel>
        
        </ContentTemplate>
    </asp:UpdatePanel>
        <br />
    
    
    
    
    </asp:Panel>
</asp:Content>

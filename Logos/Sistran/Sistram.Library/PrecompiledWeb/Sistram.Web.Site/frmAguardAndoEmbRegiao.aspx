<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmAguardAndoEmbRegiao, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

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


<table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="baseline">
                        <td class="tdp" nowrap="nowrap" valign="middle" width="99%">
                           
                        </td>
                        <td class="tdpR" nowrap="nowrap" valign="baseline" width="50%">
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updBot" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                     Text="Pesquisar" Visible="false" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnGerarReport" runat="server" CssClass="button" Font-Names="Arial"
                                                    Font-Size="7pt" Text="EXCEL" Width="60px"  Visible="false" 
                                                    onclientclick="window.open('frmGerarExcelNFAgRegiao.aspx');return false;" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" Font-Size="7pt"
                                            Text="PDF" Visible="False" Width="40px" 
                                            onclientclick="window.open('frmGerarExcelNFAgRegiao.aspx?tipo=pdf');return false;"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

   <asp:UpdatePanel ID="upl" runat="server"   >
   <ContentTemplate>

        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td width="25%" valign="top">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        <asp:TextBox ID="txtFoi" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    
                </tr>
            </table>
        </asp:Panel>
        
    
    </ContentTemplate>
   </asp:UpdatePanel>
    
    
    </asp:Panel>
</asp:Content>

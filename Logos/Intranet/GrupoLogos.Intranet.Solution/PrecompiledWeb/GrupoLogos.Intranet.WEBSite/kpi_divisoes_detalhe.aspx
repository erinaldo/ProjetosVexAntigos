<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" inherits="kpi_divisoes_detalhe, App_Web_kpi_divisoes_detalhe.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <left>
    <asp:Panel ID="pnlteste" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" 
            style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:25px">
    <asp:Label ID="lblTitulo" runat="server" Text="" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
        <asp:Panel ID="Panel3" runat="server" style="text-align: left">
            <table style="width: 100%">
            <tr style="text-align:right">
            <td>
            <input type="button" onclick="javascript:window.open('frmgdrExcelSobras.aspx'); return false;" value="Gerar Excel"  />
            </td>
            
            </tr>
                <tr>
                    <td width="25%" valign="top">
                        <table class="grid">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" style="font-weight: 700; font-size: 9pt" 
                                        Text="ARMAZENAGEM"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="RadGridArmazenagem" runat="server" AllowPaging="True" 
                                        AllowSorting="True" BorderColor="#999999" 
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" GridLines="None" 
                                        PageSize="100000" Skin="Default2006" Width="100%" Visible="False">
                                        <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" 
                                            GridLines="Both">
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                        </MasterTableView>
                                        <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                                        <ItemStyle BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Names="Arial" Font-Size="7pt" Height="7px" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <HeaderStyle Font-Bold="False" Font-Size="7pt" />
                                        <FilterMenu EnableTheming="True" Skin="Default2006">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                        <StatusBarSettings LoadingText="Carregando..." />
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    
    
    
    
    </asp:Panel>
    </left>
</asp:Content>

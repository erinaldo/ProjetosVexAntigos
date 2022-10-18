<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmhistoricopalletsDetalhe.aspx.cs" Inherits="frmhistoricopalletsDetalhe"
    Theme="Adm" EnableTheming="true" %>

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
            <input type="button" onclick="javascript:window.open('frmgdrExcelSobras.aspx'); return false;" value="Gerar Excel" class="button"  />
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
                                        AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" 
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" GridLines="None" 
                                        PageSize="100000" Skin="Default2006" Width="100%">
                                        <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" 
                                            GridLines="Both">
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="codigo" EmptyDataText="&amp;nbsp;" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="CÓDIGO DO PRODUTO" 
                                                    ItemStyle-HorizontalAlign="Left" UniqueName="column1" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="descricao" EmptyDataText="&amp;nbsp;" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="DESCRIÇÃO" 
                                                    ItemStyle-HorizontalAlign="Left" UniqueName="column2" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="endereco" EmptyDataText="&amp;nbsp;" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="ENDEREÇO" 
                                                    ItemStyle-HorizontalAlign="Left" UniqueName="column3" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                
                                                <telerik:GridBoundColumn DataField="ua" EmptyDataText="&amp;nbsp;" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="UA" 
                                                    ItemStyle-HorizontalAlign="Left" UniqueName="column41" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="IDLote" EmptyDataText="&amp;nbsp;" 
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="LOTE" 
                                                    ItemStyle-HorizontalAlign="Left" UniqueName="column5" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                
                                                <telerik:GridBoundColumn DataField="SALDO" EmptyDataText="&amp;nbsp;" HeaderText="SALDO" UniqueName="column61">
                                                </telerik:GridBoundColumn>
                                                
                                                <telerik:GridBoundColumn DataField="M3" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="M3" UniqueName="column4" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="M3TOTAL" EmptyDataText="&amp;nbsp;" HeaderText="M3 Total" UniqueName="column8">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Valorunitario" EmptyDataText="&amp;nbsp;" HeaderText="VL.UNITÁRIO" UniqueName="column7">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="ValorEmEstoque" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="VL. EM ESTOQUE" UniqueName="column6">
                                                     <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PESOUNIT" EmptyDataText="&amp;nbsp;" HeaderText="PESO UNIT" UniqueName="column9">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PESOTOTAL" EmptyDataText="&amp;nbsp;" HeaderText="PESO TOTAL" UniqueName="column10">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Marca" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="MARCA" UniqueName="column11">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Divisao" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="DIVISAO" UniqueName="column12">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="GRUPO" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="GRUPO" UniqueName="column13">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FILIAL" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="FILIAL" UniqueName="column">
                                                </telerik:GridBoundColumn>

                                                 <telerik:GridBoundColumn DataField="Data" EmptyDataText="&amp;nbsp;" 
                                                    HeaderText="Data" UniqueName="Data">
                                                </telerik:GridBoundColumn>

                                            </Columns>
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
                            <tr>
                                <td>
                                    &nbsp;</td>
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

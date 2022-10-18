<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true" Theme="Adm" EnableTheming="true"
    CodeFile="nfLista.aspx.cs" Inherits="nfLista" Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <center>
    <table border="0" cellpadding="0" cellspacing="0" width="1000">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px" align="left">
                    <asp:Label ID="lblTitulo" runat="server" Text="Mapa de Indicadores de Desempenho"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px; text-align: center;" align="left" width="1%">
                <asp:Button ID="Button1" runat="server" CssClass="button" 
                    Text="IMPRIMIR / EXPORTAR" />
                </td>
            </tr>
            <tr>
                <td style="height: 15px" align="left" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
    <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
        OnSortCommand="RadGrid1_SortCommand" Width="1000px" CellPadding="0" 
        Skin="Default2006" PageSize="1000" style="font-size: 8pt">
        <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
            <RowIndicatorColumn>
                <HeaderStyle Width="10px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="10px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="REMETENTE" EmptyDataText="&amp;nbsp;" HeaderText="REMETENTE"
                    UniqueName="column51" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" Wrap="True" />
                    <ItemStyle HorizontalAlign="Left" />                    
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="DESTINATARIO" EmptyDataText="&amp;nbsp;" HeaderText="DESTINATÁRIO"
                    UniqueName="column52" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SERIE" EmptyDataText="&amp;nbsp;" HeaderText="SÉRIE"
                    UniqueName="column53" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOTAFISCAL" EmptyDataText="&amp;nbsp;" HeaderText="NOTA FISCAL"
                    UniqueName="column54" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VOLUMES" EmptyDataText="&amp;nbsp;" HeaderText="VOLUMES"
                    UniqueName="column55" Visible="true">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign ="Right"  />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="PESO" EmptyDataText="&amp;nbsp;" HeaderText="PESO" DataFormatString="{0:N2}"
                    UniqueName="column56" Visible="true">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign ="Right"  />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn DataField="VALORDANOTA" EmptyDataText="&amp;nbsp;" HeaderText="VALOR DA NOTA" DataFormatString="{0:N2}"
                    UniqueName="column57" Visible="true">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign ="Right"  />
                </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="DATADECADASTRO" 
                    EmptyDataText="&amp;nbsp;" HeaderText="DATA DE CADASTRO"
                    UniqueName="column58" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign ="Center"  />
                </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="DATADEEMISSAO" 
                    EmptyDataText="&amp;nbsp;" HeaderText="DATA DE EMISSÃO"
                    UniqueName="column5" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign ="Center"   />
                </telerik:GridBoundColumn>
                
                
                
                <telerik:GridBoundColumn DataField="OCORRENCIA" EmptyDataText="&amp;nbsp;" 
                    HeaderText="OCORRÊNCIA" UniqueName="column">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="True" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        HorizontalAlign="Left" Wrap="True" />
                </telerik:GridBoundColumn>
                
                
                
            </Columns>
            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" Wrap="True" 
                Font-Names="Arial" Font-Size="X-Small" />
        </MasterTableView>
        <AlternatingItemStyle Font-Size="7pt" Height="8px" />
        <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" BorderWidth="1px" />
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderStyle Font-Size="7pt" Font-Bold="False" />
        <FilterMenu EnableTheming="True" Skin="Default2006">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
        <StatusBarSettings LoadingText="Carregando..." />
    </telerik:RadGrid>
    <br />
                <input ID="Button2" class="button" type="button" value="Voltar" 
                    onclick="javascript:history.back();" __designer:mapid="53c" />
                    
    <br />
                    
                    <br />
                    
                    </center>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmBaterDatas.aspx.cs"
    Inherits="frmBaterDatas" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                        Font-Size="14px"></asp:Label>
                        
                        
                        
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="baseline">
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;<asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px" DataSourceID="SqlDataSource1" 
                        DataTextField="Nome" DataValueField="IDFilial">
                       
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GrupoLogosTesteConnectionString %>" 
                        SelectCommand="SELECT [IDFilial], [Nome] FROM [Filial]"></asp:SqlDataSource>
                </td>
                <td class="tdpR" nowrap="nowrap" valign="baseline">
                    <table align="right" border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                    OnClick="Button1_Click" Text="Pesquisar" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <br />
                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand"
                    Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" GridLines="None">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                          
                            <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." UniqueName="column001">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("IdDocumento") %>'
                                        NavigateUrl='<% # "frmEntregas.aspx?idDoc=" + Eval("IdDocumento") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Entrada" DataField="DataDeEntrada"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Emissão" DataField="DataDeEmissao"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column6" DataType="System.DateTime">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" 
                                HeaderText="DATA DE CONCLUSAO" DataField="DATADECONCLUSAO" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column3">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" DataField="FILIAL"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FILIAL"
                                UniqueName="column1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" 
                                HeaderText="PRAZOUTILIZADO_EDVALDO" DataField="PRAZOUTILIZADO_EDVALDO" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column2">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="PRAZO UTILIZADO MOISES" 
                                UniqueName="column">
                                <ItemTemplate>
                                    <asp:Label ID="lblprazoMoises" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                             <telerik:GridTemplateColumn HeaderText="parametros"  Visible="false"
                                UniqueName="column">
                                <ItemTemplate>
                                    <asp:Label ID="LBLIDCIDADE" runat="server" Text='<% #Eval("IDCIDADE") %>'></asp:Label>
                                    <asp:Label ID="LBLIDESTADO" runat="server" Text='<% #Eval("IDESTADO") %>'></asp:Label>
                                    <asp:Label ID="LBLCIDADE" runat="server" Text='<% #Eval("NOME") %>'></asp:Label>
                                    <asp:Label ID="LBLESTADO" runat="server" Text='<% #Eval("UF") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                    <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" BorderWidth="1px"
                        BorderColor="#666666" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Size="7pt" Font-Bold="False" />
                    <FilterMenu EnableTheming="True" Skin="Default2006">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                    <StatusBarSettings LoadingText="Carregando..." />
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

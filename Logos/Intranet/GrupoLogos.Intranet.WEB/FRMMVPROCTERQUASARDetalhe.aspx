<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true" CodeFile="FRMMVPROCTERQUASARDetalhe.aspx.cs"
    Inherits="FRMMVPROCTERQUASARDetalhe" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlteste" runat="server">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-image: url('http://localhost:6424/GrupoLogos.Intranet.WEB/Images/skins/primeiro/img/menu_3_2.jpg');
                            height: 25px; text-align: left;">
                            <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Filial"
                                Font-Bold="True" Font-Size="14px"></asp:Label>
                        </td>
                        <td style="background-image: url('http://localhost:6424/GrupoLogos.Intranet.WEB/Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; text-align: right;">
                            <asp:Button ID="btnOrigemDados" runat="server" CssClass="button" 
                                Text="Gerar Excel" />
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
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width: 100%" border="0">
                        <tr>
                            <td valign="top" style="height: 16px">
                               
                                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnPageIndexChanged="RadGrid1_PageIndexChanged" 
                    Width="100%" OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" GridLines="None" 
                                    PageSize="2000">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Emissao" DataField="DataDeEmissao"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" 
                                UniqueName="column51" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Nota Fiscal" DataField="NotaFiscal"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" 
                                UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CTRC" DataField="CTRC"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column6">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" 
                                HeaderText="Volumes" DataField="Volumes" 
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" DataField="CnpjRemetente"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Cnpj Remetente"
                                UniqueName="column8">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Remetente" 
                                DataField="NomeRemetente" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column9">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="UF Remetente" 
                                DataField="UFRemetente" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column10">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cidade Remetente" 
                                DataField="CidadeRemetente" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column21">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>


                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cnpj Destinatario" 
                                DataField="CnpjDestinatario" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column22">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Destinatario" 
                                DataField="NomeDestinatario" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column23">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                           

                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="UF Destinatario " 
                                DataField="UFDestinatario" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column24">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                                                          <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cidade Destinatario" 
                                DataField="CidadeDestinatario" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column25">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>


                                                          <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Peso Bruto" 
                                DataField="PesoBruto" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column26">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>


                                                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Valor Da Nota" 
                                DataField="ValorDaNota" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column27">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>


                                                               <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Frete" 
                                DataField="Frete" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column2">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>


                        </Columns>
                    </MasterTableView>
                    <ExportSettings IgnorePaging="True" OpenInNewWindow="True">
                        <Excel Format="ExcelML" />
                    </ExportSettings>
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
                               </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

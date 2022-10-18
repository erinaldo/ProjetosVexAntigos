<%@ page language="C#" autoeventwireup="true" inherits="frmgdrExcelSobras, App_Web_frmgdrexcelsobras.aspx.cdcab7d2" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onunload="javascript:window.close();">
    <form id="form1" runat="server">
    <div>
    
                        
    
                <telerik:RadGrid ID="RadGridUsuarios" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="0" GridLines="None" 
                    Skin="Default2006" Width="100%" PageSize="100"            >
                    <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="DATA" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                HeaderText="DATA LANÇAMENTO" ItemStyle-HorizontalAlign="Center" UniqueName="column1"
                                Visible="true">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FILIAL" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="FILIAL ORIGEM" ItemStyle-HorizontalAlign="Left" UniqueName="column2"
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FILIALDESTINO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="FILIAL DESTINO" ItemStyle-HorizontalAlign="Left" UniqueName="column3"
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DataDeEmbarqueDoVolume" 
                                EmptyDataText="&amp;nbsp;" HeaderText="DATA DE EMBARQUE" UniqueName="column8">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CLIENTE" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="CLIENTE" ItemStyle-HorizontalAlign="Left" UniqueName="column4" 
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUMERONOTAFISCAL" EmptyDataText="&amp;nbsp;"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="NOTAFISCAL" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column5" Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="quantidade" EmptyDataText="&amp;nbsp;"
                                HeaderStyle-HorizontalAlign="Right" HeaderText="VOLUMES" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column6" Visible="true" DataFormatString="{0:N2}" >
<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="DATAFINALIZACAO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                HeaderText="DATA DA BAIXA" ItemStyle-HorizontalAlign="Center" UniqueName="column7"
                                Visible="true">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NomeUsuario" EmptyDataText="&amp;nbsp;" 
                                HeaderText="USUÁRIO" UniqueName="column">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="column0011">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnBaixar" runat="server" Text="BAIXAR" CssClass="link" CommandName='<%# Bind("IDSOBRA") %>'
                                        CommandArgument="Baixar" OnClientClick="Confirmar()" />
                                        <asp:Label ID="lblStatus" runat="server" Visible="false" text ='<%# Bind("Finalizado")%>'></asp:Label>

                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="IDPROJETO" HeaderText="CÓDIGO" 
                                UniqueName="column00111">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "frmSobraDetalhe.aspx?id=" + Eval("IDSOBRA") %>'
                                        Text='<%# Bind("IDSOBRA") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                    <ItemStyle BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                        Font-Size="7pt" Height="7px" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Bold="False" Font-Size="7pt" />
                    <FilterMenu EnableTheming="True" Skin="Default2006">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                    <StatusBarSettings LoadingText="Carregando..." />
                </telerik:RadGrid>
    
                        
    
    </div>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
     <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                        <asp:PlaceHolder ID="PhTotais" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
    </form>
</body>
</html>

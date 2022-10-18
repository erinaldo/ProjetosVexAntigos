<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmProjeto, App_Web_frmprojeto.aspx.cdcab7d2" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" __designer:mapid="13af" border="0" cellpadding="0" cellspacing="0">
        <tr __designer:mapid="13b0">
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                __designer:mapid="13b1">
                <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Projetos" 
                    Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>

        <tr>
        <td>
            <table class="grid">
                <tr>
                    <td class="tdpR">
                        <asp:Button ID="Button3" runat="server" CssClass="button" Text="Novo" 
                            PostBackUrl="~/frmProjetoDetalhe.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGridUsuarios" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="0" GridLines="None" OnItemCommand="RadGrid16_ItemCommand" OnItemDataBound="RadGrid16_ItemDataBound"
                            Skin="Default2006" Width="100%" PageSize="20">
                            <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" 
                                GridLines="Both">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="IDPROJETO" HeaderText="CÓDIGO" 
                                        UniqueName="column0011">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "frmProjetoDetalhe.aspx?id=" + Eval("IDPROJETO") %>'
                                                Text='<%# Bind("IDPROJETO") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridBoundColumn DataField="PROJETO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="NOME" ItemStyle-HorizontalAlign="Left" UniqueName="column"
                                        Visible="true">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FILIAL" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="FILIAL" ItemStyle-HorizontalAlign="Left" UniqueName="column"
                                        Visible="true">
                                    </telerik:GridBoundColumn>

                                     <telerik:GridBoundColumn DataField="CONTATOCLIENTE" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="CONTATO CLIENTE" ItemStyle-HorizontalAlign="Left" UniqueName="column"
                                        Visible="true">
                                    </telerik:GridBoundColumn>

                                     <telerik:GridBoundColumn DataField="CONTATOCONTRATADO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="CONTATO CONTRATADO" ItemStyle-HorizontalAlign="Left" UniqueName="column"
                                        Visible="true">
                                    </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn DataField="STATUS" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="STATUS" ItemStyle-HorizontalAlign="Left" UniqueName="column"
                                        Visible="true">
                                    </telerik:GridBoundColumn>

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
                    </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Content>

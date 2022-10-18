<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmUsuarios.aspx.cs"
    Inherits="frmUsuarios" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;" __designer:mapid="13af" border="0" cellpadding="0" cellspacing="0">
        <tr __designer:mapid="13b0">
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                __designer:mapid="13b1">
                <asp:Label ID="lblTitulo" runat="server" Text="Usu�rios" Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" SelectedIndex="1"
        Width="100%" Skin="Outlook">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvUsuarios" Text="Usu�rios" />
            <telerik:RadTab runat="server" PageViewID="rpvPerfis" Text="Perfis" Selected="True" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmp" runat="server" SelectedIndex="1" BackColor="White"
        CssClass="bordaTabs" Width="99%">
        <telerik:RadPageView ID="rpvUsuarios" runat="server" Width="100%">
            <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnPesquisar">
                <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="99%">
                    <tr valign="baseline">
                        <td class="tdp" valign="middle" width="1%">
                            Nome:
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" width="30%">
                            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="180px"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" width="1%">
                            Login:
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" width="30%">
                            <asp:TextBox ID="txtLogin" runat="server" CssClass="txt" Width="180px"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" width="1%">
                            CNPJ/CPF:
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" width="30%">
                            <asp:TextBox ID="txtCPF" runat="server" CssClass="txt" Width="180px"></asp:TextBox>
                        </td>
                        <td class="tdpR" nowrap="nowrap" valign="baseline" width="1%">
                            <table align="right" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Font-Names="arial"
                                            Font-Size="7pt" Text="Pesquisar" OnClick="btnPesquisar_Click" Width="60px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnNovo" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                            Text="Novo" Width="60px" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="xxwewx" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid ID="RadGridUsuarios" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="0" GridLines="None" OnItemCommand="RadGrid16_ItemCommand" OnItemDataBound="RadGrid16_ItemDataBound"
                            OnPageIndexChanged="RadGrid16_PageIndexChanged" OnSortCommand="RadGrid16_SortCommand"
                            Skin="Default2006" Width="100%" PageSize="20">
                            <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="IDUsuario" HeaderText="C�digo" UniqueName="column0011">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "frmUsuariosDetalhe.aspx?id=" + Eval("IdUsuario") %>'
                                                Target="_blank" Text='<%# Bind("IDUsuario") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="IDCadastto" HeaderText="Cadastro" UniqueName="column001"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdCadastro" runat="server" Target="_blank" Text='<%# Bind("IDCadastro") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CnpjCpf" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="CNPJ/CPF" ItemStyle-HorizontalAlign="Left" UniqueName="column51"
                                        Visible="true">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Nome" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Nome" ItemStyle-HorizontalAlign="Left" UniqueName="column5" Visible="true">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Login" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Login" ItemStyle-HorizontalAlign="left" UniqueName="column6">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Perfil" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Perfil" ItemStyle-HorizontalAlign="left" UniqueName="column6">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="email" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="left"
                                        HeaderText="E-mail" ItemStyle-HorizontalAlign="left" UniqueName="column7">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Excluir" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkHabilitar" runat="server">Desabilitar / Habilitar</asp:LinkButton>
                                            <asp:Label ID="lblAtivo" runat="server" Text='<%#Eval("Ativo") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvPerfis" runat="server" Width="100%" Style="text-align: left;
            font-size: 6pt;" Selected="true">
            <table id="novatb0" runat="server" cellpadding="1" cellspacing="0" class="table"
                width="99%">
                <tr valign="baseline">
                    <td class="tdp" valign="middle" width="1%">
                        Perfil:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" width="30%">
                        <asp:TextBox ID="txtPerfil" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="baseline" width="1%">
                        <table align="right" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="btnPesquisarPerfil" runat="server" CssClass="button" Font-Names="arial"
                                        Font-Size="7pt" OnClick="btnPesquisarPerfil_Click" Text="Pesquisar" Width="60px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNovoPerfil" runat="server" CssClass="button" Font-Names="arial"
                                        Font-Size="7pt" Text="Novo" Width="60px" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="RadGridPerfis" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="0" GridLines="None" Skin="Default2006" Width="100%" OnItemCommand="RadGridPerfis_ItemCommand"
                OnItemDataBound="RadGridPerfis_ItemDataBound" OnPageIndexChanged="RadGridPerfis_PageIndexChanged"
                PageSize="20" OnSortCommand="RadGridPerfis_SortCommand">
                <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="IDUsuario" HeaderText="C�digo" UniqueName="column0011">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkPefil" runat="server" NavigateUrl='<% # "frmPerfisDetalhe.aspx?id=" + Eval("IdUsuario") %>'
                                    Target="_blank" Text='<%# Bind("IDUsuario") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="IDCadastto" HeaderText="Cadastro" UniqueName="column001"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPerfil" runat="server" Target="_blank" Text='<%# Bind("IDUsuario") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Nome" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                            HeaderText="Perfil" ItemStyle-HorizontalAlign="Left" UniqueName="column51" Visible="true">
                            <HeaderStyle HorizontalAlign="left" Width="75%" />
                            <ItemStyle HorizontalAlign="left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Excluir" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkHabilitarPerfil" runat="server">Desabilitar / Habilitar</asp:LinkButton>
                                <asp:Label ID="lblAtivoPerfil" runat="server" Text='<%#Eval("Ativo") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>

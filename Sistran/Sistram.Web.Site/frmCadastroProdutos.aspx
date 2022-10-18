<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastroProdutos.aspx.cs"
    Inherits="frmCadastroProdutos" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1" Height="600px">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Material" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" runat="server" cellpadding="1" cellspacing="0" class="table" width="100%">
            <tr valign="bottom">
                <td class="tdp" nowrap="nowrap" width="1%">
                    Código:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="100px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Código Cliente:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCodigo0" runat="server" CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Descrição:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="300px" Wrap="False"></asp:TextBox>
                </td>
                <td class="tdpR" nowrap="nowrap" width="60%">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                        OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="btnNovo" runat="server" CssClass="button" Font-Names="arial"
                        Font-Size="7pt" Text="Novo" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True"
                    GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="CODIGO" HeaderText="Código" UniqueName="column1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="CODIGOTextBox" runat="server" Text='<%# Bind("CODIGO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Clique aqui para ver o detalhe."
                                        Target="_blank" Text='<%# Bind("CODIGO") %>' NavigateUrl='<% # "frmCadastrarProduto.aspx?Codigo=" + Eval("CODIGO") + "&tipo=Mat&IdProduto=" + Eval("IDPRODUTO")  %>'></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Código Cliente" DataField="CODIGODOCLIENTE"
                                UniqueName="column3">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Divisão" DataField="NOMEDIVISAO"
                                UniqueName="column3">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Descrição" DataField="DESCRICAO"
                                UniqueName="column4">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Data Cadastro" DataField="DTDECAD"
                                UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ativo" DataField="ATIVO"
                                UniqueName="column6">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Saldo" DataField="Saldo"
                                UniqueName="column">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

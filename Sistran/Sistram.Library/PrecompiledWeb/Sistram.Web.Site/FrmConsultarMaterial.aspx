<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="FrmConsultarMaterial, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1" Height="600px">
        <table style="width: 100%;">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Material" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    Código:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="100px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Código Cliente:
                </td>
                <td class="tdp" nowrap="nowrap" width="60%">
                    <asp:TextBox ID="txtCodigo0" runat="server" AutoPostBack="True" CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="100px"></asp:TextBox>
                    &nbsp;<asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                </td>
                <td class="tdpR" nowrap="nowrap" width="1%">
                    Exibir:&nbsp;
                    <asp:DropDownList ID="cboTipoDes0" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        onselectedindexchanged="cboTipoDes0_SelectedIndexChanged" Width="35px">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem Selected="True">20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <br />
                <br />
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
                                                    <telerik:GridTemplateColumn DataField="CODIGO" HeaderText="Código" 
                            UniqueName="column1">
                            <EditItemTemplate>
                                <asp:TextBox ID="CODIGOTextBox" runat="server" Text='<%# Bind("CODIGO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server"  ToolTip="Clique aqui para ver o detalhe." Target="_blank" Text='<%# Bind("CODIGO") %>' NavigateUrl='<% # "frmDetalheProduto.aspx?Codigo=" + Eval("CODIGO") + "&tipo=Mat&IdProduto=" + Eval("IDPRODUTO")  %>' ></asp:HyperLink>
                                
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

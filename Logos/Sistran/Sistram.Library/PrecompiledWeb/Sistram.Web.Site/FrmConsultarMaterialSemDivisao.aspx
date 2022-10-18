<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="FrmConsultarMaterialSemDivisao, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

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
                    Descrição:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtDescricao" runat="server" AutoPostBack="True" 
                        CssClass="txt" OnTextChanged="txtNf_TextChanged"
                        Width="300px"></asp:TextBox>
                    &nbsp;</td>
                <td width="1%" class="tdp" nowrap="nowrap">
                    <asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="False" 
                        Font-Names="Verdana" Font-Size="7pt" Text="Com Saldo" />
                </td>
                <td class="tdp">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                        Font-Names="Arial" Font-Size="7pt" Text="Relatório" Visible="False" 
                        Width="60px" />
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
                        
                        
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Código" DataField="CODIGO"
                                UniqueName="column31">
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
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Saldo" DataField="SALDOESTOQUE"
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

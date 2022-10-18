<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true"
    CodeFile="frmDetalheProduto.aspx.cs" Inherits="frmDetalheProduto" Theme="Adm"
    EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" Height="600">
        <table style="width: 100%;">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Detalhe do Produto" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pn" runat="server" Height="500" HorizontalAlign="Center">
            <table id="novatb" class="table2" runat="server" cellpadding="2" cellspacing="0"
                width="100%">
                <tr valign="bottom">
                    <td class="tdp" width="10%" nowrap="nowrap" rowspan="9" valign="top" 
                        style="background-color: #FFFFFF">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="Image1" runat="server" Height="180px" ImageUrl="~/Images/naoDisponivel.jpg" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </td>
                    <td class="tdpR" colspan="5" style="font-size: 9pt" bgcolor="White">
                        <asp:UpdatePanel ID="UplMove" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblReg" runat="server" Visible="False"></asp:Label>
                                <asp:ImageButton ID="btnAnterior" runat="server" Height="20" ImageUrl="~/Images/setaEsqerda.png"
                                    OnClick="btnAnterior_Click" />
                                <asp:ImageButton ID="btnPosterior" runat="server" Height="20" ImageUrl="~/Images/SetaDireita.png"
                                    OnClick="btnPosterior_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr valign="bottom">
                    <td class="tdp" style="font-weight: bold;" width="1%" nowrap="nowrap">
                        Código:
                    </td>
                    <td class="tdp" width="30%">
                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" width="1%" nowrap="nowrap">
                    </td>
                    <td class="tdp" width="20%">
                        &nbsp;</td>
                    <td class="tdp" width="20%" style="font-weight: bold">
                        Divisões</td>
                </tr>
                <tr valign="bottom">
                    <td class="tdp" nowrap="nowrap" style="font-weight: bold">
                        Código Cliente:</td>
                    <td class="tdp" width="40%">
                        <asp:Label ID="lblCodCliente" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" width="1%" style="font-weight: bold">
                        Saldo: </td>
                    <td class="tdp" width="20%">
                        <asp:Label ID="lblSaldo" runat="server"></asp:Label>
                    </td>
                    <td  width="20%" rowspan="8" valign="top">
                         <asp:ListBox ID="ListBox1" runat="server" CssClass="txt" Font-Names="Verdana" 
                            Font-Size="7pt" Height="95%" Rows="8" Width="100%"></asp:ListBox>
                        
                        </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Descrição:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        &nbsp;</td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        &nbsp;</td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Data de Cadastro:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDataCadastro" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Data Limite de Uso:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDataLimite" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Saldo Minimo:
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblSaldoMinimo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Tipo de Material:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblTipoMaterial" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Ativo:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblAtivo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Consumo Mensal:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblConsumoMensal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Valor Unitário:
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblValorUnitario" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                    </td>
                </tr>
                
                <tr valign="baseline">
                    <td  colspan="5" nowrap="nowrap" style="font-weight: bold;" valign="middle" bgcolor="White">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            Width="99.8%">
                            <MasterTableView>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="CODIGODEBARRAS" EmptyDataText="&amp;nbsp;" HeaderText="Cod. Barra"
                                        UniqueName="column1">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PESOLIQUIDO" EmptyDataText="&amp;nbsp;" HeaderText="Peso Liq."
                                        UniqueName="column2">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PESOBRUTO" EmptyDataText="&amp;nbsp;" HeaderText="Peso Bruto"
                                        UniqueName="column3">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ESPECIE" EmptyDataText="&amp;nbsp;" HeaderText="Espécie"
                                        UniqueName="column4">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UNIDADEDOCLIENTE" EmptyDataText="&amp;nbsp;"
                                        HeaderText="Uni." UniqueName="column5">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CONTEUDO" EmptyDataText="&amp;nbsp;" HeaderText="Conteúdo"
                                        UniqueName="column6">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ALTURA" EmptyDataText="&amp;nbsp;" HeaderText="Altura"
                                        UniqueName="column7">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LARGURA" EmptyDataText="&amp;nbsp;" HeaderText="Largura"
                                        UniqueName="column8">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COMPRIMENTO" EmptyDataText="&amp;nbsp;" HeaderText="Comprim."
                                        UniqueName="column9">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CUBAGEM" EmptyDataText="&amp;nbsp;" HeaderText="Cubagem"
                                        UniqueName="column10">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RODOVIARIO" EmptyDataText="&amp;nbsp;" HeaderText="Cub. Rod."
                                        UniqueName="column11">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AEREO" EmptyDataText="&amp;nbsp;" HeaderText="Cub. Aereo"
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
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

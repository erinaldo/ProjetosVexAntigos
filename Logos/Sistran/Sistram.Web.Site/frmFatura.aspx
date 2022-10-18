<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFatura.aspx.cs" Inherits="frmFatura"
    MasterPageFile="~/SiteDetalhe2.master" EnableTheming="true" Theme="Adm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="pppx" runat="server">
        <ContentTemplate>
            <center>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; text-align: left;">
                            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px" 
                                Text="Fatura"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlFatura" runat="server" Visible="False" Width="75%">
                    <table class="table" border="0" cellpadding="0" cellspacing="0">
                        <tr style="height:20px; background-color:White">
                            <td width="10%" align="center" class="tdpCenter" colspan="2">
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#333333">
                            </td>
                        </tr>
                        <tr>
                            <td width="1%" class="tdp">
                                Fatura:
                            </td>
                            <td class="tdp">
                                <asp:Label ID="lblNumeroFatura" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" class="tdp">
                                Razão Social:
                            </td>
                            <td class="tdp">
                                <asp:Label ID="lblRazaoSocial" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  colspan="2" >
                                <hr />
                                
                                </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" class="tdp">
                                Vencimento:
                            </td>
                            <td class="tdp">
                                <asp:Label ID="lblVencimento" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Valor:
                            </td>
                            <td class="tdp">
                                <asp:Label ID="lblValor" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdp" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdp" colspan="2">
                                <strong>Para pagamento nesta data:</strong></td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Vencimento:</td>
                            <td class="tdp">
                                <asp:Label ID="lblVencimento0" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Valor:</td>
                            <td class="tdp">
                                <asp:Label ID="lblValor0" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#333333">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="gridFatura" runat="server" GridLines="None">
                                    <MasterTableView AutoGenerateColumns="False">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="NotaFiscal" EmptyDataText="&amp;nbsp;" HeaderText="Nota Fiscal"
                                                UniqueName="column1">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDeEmissao" EmptyDataText="&amp;nbsp;" HeaderText="Emissão"
                                                UniqueName="column2">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CTR" EmptyDataText="&amp;nbsp;" HeaderText="Conhecimento"
                                                UniqueName="column3">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu EnableTheming="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </FilterMenu>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="White">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="White">
                                <asp:Button ID="btnImprimirBoleto" runat="server" Text="Imprimir Boleto" 
                                 Height="50" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Names="verdana" ForeColor="#333333" BorderColor="Black"/></td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
                <asp:Panel ID="pnlNfPrinc" runat="server" Visible="False" Width="99%" 
                    Height="600px" ScrollBars="Horizontal">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" SelectedIndex="0"
                        Width="98%" AutoPostBack="True" OnTabClick="RadTabStrip1_TabClick" 
                        Skin="WebBlue">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="rpvNotaFiscal" Text="Nota Fiscal" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="rpvItens" Text="Itens Nota Fiscal">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="rmp" runat="server" SelectedIndex="4" BackColor="White"
                        CssClass="bordaTabs" Width="98%">
                        <telerik:RadPageView ID="rpvStatusGeral" runat="server" Width="98%">
                            <asp:Panel ID="pnlNF" runat="server" Height="500px" Width="100%" Style="text-align: left">
                                <table class="table" border="0" cellpadding="0" cellspacing="0">
                                    <table id="tbl" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr valign="top">
                                            <td>
                                                <table border="1" cellspacing="0" class="table" width="100%" bgcolor="#EAEAEA">
                                                    <tr>
                                                        <td class="tdp" colspan="4">
                                                            <asp:Panel ID="Panel1" runat="server" Style="text-align: right" BackColor="#EAEAEA">
                                                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="Imprimir" /></asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 15%" bgcolor="#EAEAEA">
                                                            Tipo Documento:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%" bgcolor="#EAEAEA">
                                                            <asp:Label ID="lblTipoDocumento" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%" bgcolor="#EAEAEA">
                                                            Tipo Serviço:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                            <asp:Label ID="lblTipoServico" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%" bgcolor="#EAEAEA">
                                                            Número:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%" bgcolor="#EAEAEA">
                                                            <asp:Label ID="lblNumero" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%" bgcolor="#EAEAEA">
                                                            Série:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%" bgcolor="#EAEAEA">
                                                            <asp:Label ID="lblSerie" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_divisoria" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Cliente:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Endereço:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblEnderecoCliente" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_divisoria" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Remetente:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblRemetente" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Endereço:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblEnderecoRemetente" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_divisoria" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Destinatário:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblDestinatario" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 14%">
                                                            Endereço:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblEnderecoDestinatadio" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table border="1" cellpadding="0" cellspacing="0" class="table" width="100%">
                                                    <tr>
                                                        <td class="tdp" style="width: 15%">
                                                            Data Movimento:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%">
                                                            <asp:Label ID="lblMovimento" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%">
                                                            Data Entrada:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                            <asp:Label ID="lblDataEntrada" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 6%">
                                                            Cancelamento:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%">
                                                            <asp:Label ID="lblCancelamento" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%">
                                                            Ativo:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                            <asp:Label ID="lblAtivo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 6%">
                                                            Data Emissão:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%">
                                                            <asp:Label ID="lblDataEmissao" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%">
                                                            Data Planejada:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                            <asp:Label ID="lblDataPlanejada" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 6%">
                                                            Data Entrega / Receb.:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%">
                                                            <asp:Label ID="lblDataConc" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%">
                                                            Cod. Barras Receb.:&nbsp;
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                            <asp:Label ID="lblCodBar" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 6%">
                                                            Data Conclusão Receb.:&nbsp;
                                                        </td>
                                                        <td class="tdp" style="width: 35%">
                                                            <asp:Label ID="lblDataConcReceb" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" width="15%">
                                                        </td>
                                                        <td class="tdp" width="35%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_divisoria" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" style="width: 6%">
                                                            Endereço do Serviço:&nbsp;
                                                        </td>
                                                        <td class="tdp" colspan="3">
                                                            <asp:Label ID="lblEnderecoServ" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="d" border="1" cellpadding="1" cellspacing="0" class="table">
                                                    <tr bgcolor="#AEFFE4">
                                                        <td class="tdp" style="width: 20%">
                                                            Data:&nbsp;
                                                            <asp:Label ID="lblDataOc" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" style="width: 20%">
                                                            Ocorrência:&nbsp;
                                                            <asp:Label ID="lblOco" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" style="width: 20%">
                                                            Descrição:&nbsp;
                                                            <asp:Label ID="lblDescricaoOco" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" style="width: 20%">
                                                            Filial:&nbsp;
                                                            <asp:Label ID="lblFilial" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="tdp" style="width: 20%">
                                                            Nome Filial:&nbsp;
                                                            <asp:Label ID="lblNomeFilial" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdp" colspan="5">
                                                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                OnItemDataBound="RadGrid1_ItemDataBound" Width="100%">
                                                                <AlternatingItemStyle Font-Names="arial" Font-Size="7pt" />
                                                                <ItemStyle Font-Names="arial" Font-Size="7pt" />
                                                                <MasterTableView>
                                                                    <RowIndicatorColumn>
                                                                        <HeaderStyle Width="20px" />
                                                                    </RowIndicatorColumn>
                                                                    <ExpandCollapseColumn>
                                                                        <HeaderStyle Width="20px" />
                                                                    </ExpandCollapseColumn>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="DataOcorrencia" EmptyDataText="&amp;nbsp;" HeaderText="Data Ocorrência"
                                                                            UniqueName="column2">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="IdOcorrencia" EmptyDataText="&amp;nbsp;" HeaderText="Código"
                                                                            UniqueName="column1">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" HeaderText="Complemento"
                                                                            UniqueName="column3">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="NomeDaOcorrencic" EmptyDataText="&amp;nbsp;"
                                                                            HeaderText="Descrição da Ocorrência" UniqueName="column4">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Foto" HeaderText="Foto" UniqueName="column5">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lnkFoto" runat="server" NavigateUrl='<% # "frmVerFoto.aspx?idDoc=" + Eval("IdDocumento") %>'
                                                                                    Target="_blank" Text="Ver"></asp:HyperLink><asp:Label ID="lblIdDocOcorrencia" runat="server"
                                                                                        Text='<%#Eval("IDDOCUMENTOOCORRENCIA") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Numero" EmptyDataText="&amp;nbsp;" HeaderText="N.DT"
                                                                            UniqueName="column7">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="IDROMANEIO" EmptyDataText="&amp;nbsp;" HeaderText="Romaneio"
                                                                            UniqueName="column">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="NOMEUSUARIO" EmptyDataText="&amp;nbsp;" HeaderText="Usuário"
                                                                            UniqueName="column6">
                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" />
                                                                <FilterMenu EnableTheming="True">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td class="tdp">
                                            </td>
                                        </tr>
                                    </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvListaEmp" runat="server" Width="100%" Style="text-align: left">
                            <asp:Panel ID="pnlItensNF" runat="server" Width="100%" Style="text-align: left" BorderColor="Silver"
                                BorderStyle="Solid" BorderWidth="1px" Height="500px">
                                <table align="left" cellspacing="0" style="width: 100%; border-style: solid; border-width: 1px"
                                    cellpadding="0" class="table">
                                    <tr>
                                        <td style="width: 15%; font-size: 8pt;" class="tdp">
                                            <b>Número:</b> &nbsp;
                                        </td>
                                        <td class="tdp">
                                            <asp:Label ID="lblNumero0" runat="server" Style="font-size: 8pt; font-weight: 700;"></asp:Label>
                                        </td>
                                        <td style="width: 15%; font-size: 8pt;" class="tdp">
                                            <b>Ativo:</b> &nbsp;
                                        </td>
                                        <td class="tdp">
                                            <asp:Label ID="lblAtivo0" runat="server" Style="font-size: 8pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" colspan="4">
                                            <b>Itens da Nota Fiscal</b> &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <telerik:RadGrid ID="grdItens" runat="server" AutoGenerateColumns="False" GridLines="None">
                                                <MasterTableView>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Codigo" EmptyDataText="&amp;nbsp;" HeaderText="Código"
                                                            UniqueName="column1">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CodigoBarras " EmptyDataText="&amp;nbsp;" HeaderText="Código de Barras"
                                                            UniqueName="column2">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" HeaderText="Descrição"
                                                            UniqueName="column3">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Qtde" EmptyDataText="&amp;nbsp;" DataFormatString="{0:N}"
                                                            HeaderText="Quantidade" UniqueName="column4">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Valor" DataFormatString="{0:c}" EmptyDataText="&amp;nbsp;"
                                                            HeaderText="Valor" UniqueName="column">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView><HeaderStyle Font-Bold="True" />
                                                <FilterMenu EnableTheming="True">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                            <asp:Button ID="Button2" runat="server" Text="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Panel ID="ppp" runat="server" Width="30%">
                                                <table style="width: 100%; font-weight: 7pt; font-size: 8pt;">
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label4" runat="server" Style="font-weight: 700" Text="Quantidade Total:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblqtdTotal" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Valor Total:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblValortotal" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

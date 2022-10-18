<%@ page title="" language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="frmEntregas, App_Web_p3uplnwq" enabletheming="true" theme="Adm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <!--inicio-->
    <br />
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" SelectedIndex="0"
        Width="100%" AutoPostBack="True" OnTabClick="RadTabStrip1_TabClick" Skin="Outlook"
        Style="border-bottom: 1px solid #CCC">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvNotaFiscal" Text="Nota Fiscal" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvItens" Text="Itens Nota Fiscal">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmp" runat="server" SelectedIndex="4" BackColor="White"
        CssClass="bordaTabs" Width="100%">
        <telerik:RadPageView ID="rpvStatusGeral" runat="server" Width="100%" Style="overflow: auto;
            min-height: 600px; border-color: #CCC">
            <asp:Panel ID="pnlNF" runat="server" Height="500px" Width="100%" Style="text-align: left">
                <%--<table class="table" border="0" cellpadding="0" cellspacing="0">--%>
                <table id="tbl" runat="server" width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr valign="top">
                        <td>
                            <fieldset style="border: 1px solid gray; background-color: #E0E0E0">
                                <legend style="font-size: 12px; font-weight: bold; background-color: #E0E0E0">INFORMAÇÕES
                                    GERAIS</legend>
                                <table border="0" cellspacing="1" class="table" width="100%" cellpadding="2" style="font-family: verdana;
                                    font-size: 8pt">
                                    <tr>
                                        <td class="tdp" colspan="4">
                                            <div>
                                                <div style="float: left">
                                                    <asp:Panel ID="Panel1" runat="server" Style="text-align: right">
                                                        <asp:Button ID="Button1" runat="server" CssClass="button" Text="Imprimir" /></asp:Panel>
                                                </div>
                                                <div style="float: right">
                                                    <asp:Label ID="lblClienteEspecial" runat="server" Font-Bold="True" ForeColor="#009933"></asp:Label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 15%">
                                            Tipo Documento:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblTipoDocumento" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" width="15%">
                                            Tipo Serviço:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblTipoServico" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 14%">
                                            Número:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblNumero" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" width="15%">
                                            Série:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblSerie" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_divisoria" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 14%">
                                            Cliente:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 14%">
                                            Endereço:
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
                                            Remetente:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblRemetente" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 14%">
                                            Endereço:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblEnderecoRemetente" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_divisoria" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="tdDets" runat="server">
                                        <td  class="tdp" style="width: 14%">
                                            Destinatário:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblDestinatario" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tdDetsEnd" runat="server">
                                        <td class="tdp" style="width: 14%">
                                            Endereço:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblEnderecoDestinatadio" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <fieldset style="border: 1px solid gray; background-color: #E0E0E0">
                                <legend style="font-size: 12px; font-weight: bold; background-color: #E0E0E0">STATUS</legend>
                                <table border="0" cellpadding="2" cellspacing="1" class="table" width="100%" style="font-family: Verdana;
                                    font-size: 8pt">
                                    <tr>
                                        <td class="tdp">
                                            Data de Emissão:
                                        </td>
                                        <td class="tdp">
                                            <asp:Label ID="lblDataEmissao" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" style="width: 6%">
                                            Cancelamento:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblCancelamento" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 15%">
                                            Data Envio Arquivo:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblMovimento" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" width="15%">
                                            Ativo:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblAtivo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" width="15%">
                                            Data Entrada:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblDataEntrada" runat="server"></asp:Label>
                                        </td>
                                         <td class="tdp" width="15%">
                                            Cod. Barras Receb.:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblCodBar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" width="15%">
                                            Data Planejada:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblDataPlanejada" runat="server"></asp:Label>
                                        </td>

                                        <td class="tdp" style="width: 6%">
                                            Data Conclusão Receb.:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblDataConcReceb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" width="15%">
                                            Data Agendamento:
                                        </td>
                                        <td class="tdp" width="35%">
                                            <asp:Label ID="lblDataAgendamento" runat="server"></asp:Label>
                                        </td>
                                         <td class="tdp"> &nbsp;</td>
                                        <td class="tdp"> &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 6%">
                                            Data de Entrega:
                                        </td>
                                        <td class="tdp" style="width: 35%">
                                            <asp:Label ID="lblDataDeConclusao" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp"> &nbsp;</td>
                                        <td class="tdp"> &nbsp;</td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="td_divisoria" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 6%">
                                            Endereço do Serviço:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblEnderecoServ" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" style="width: 6%">
                                            Motorista:
                                        </td>
                                        <td class="tdp" colspan="3">
                                            <asp:Label ID="lblMotorista" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset style="border: 1px solid gray; background-color: #E0E0E0">
                                <legend style="font-size: 12px; font-weight: bold; background-color: #E0E0E0">SITUAÇÃO
                                    E OCORRÊNCIAS</legend>
                                <table id="d" border="0" cellpadding="1" cellspacing="1" class="table">
                                    <tr bgcolor="#AEFFE4">
                                        <td class="tdp" style="width: 1%; font-weight: bold;" nowrap="nowrap">
                                            Data:
                                        </td>
                                        <td class="tdp" style="width: 1%; font-weight: bold;">
                                            Ocorrência:
                                        </td>
                                        <td class="tdp" style="width: 20%; font-weight: bold;">
                                            Descrição:
                                        </td>
                                        <td class="tdp" style="width: 1%; font-weight: bold;">
                                            Filial:
                                        </td>
                                        <td class="tdp" style="width: 20%; font-weight: bold;" nowrap="nowrap">
                                            Nome Filial:
                                        </td>
                                    </tr>
                                    <tr bgcolor="#AEFFE4">
                                        <td class="tdp" nowrap="nowrap">
                                            &nbsp;<asp:Label ID="lblDataOc" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" nowrap="nowrap">
                                            &nbsp;<asp:Label ID="lblOco" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" style="width: 20%" nowrap="nowrap">
                                            <asp:Label ID="lblDescricaoOco" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" style="width: 20%" nowrap="nowrap">
                                            <asp:Label ID="lblFilial" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdp" nowrap="nowrap">
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
                                                        <telerik:GridBoundColumn DataField="Codigo" EmptyDataText="&amp;nbsp;" HeaderText="Código"
                                                            UniqueName="column1">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" HeaderText="Complemento"
                                                            UniqueName="column3">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NomeDaOcorrencia" EmptyDataText="&amp;nbsp;"
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
                            </fieldset>
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
                <table align="left" cellspacing="2" style="width: 100%; border-style: solid; border-width: 1px;
                    font-family: Verdana; font-size: 8pt;" cellpadding="1" class="table">
                    <tr>
                        <td style="width: 15%; font-size: 8pt;" class="tdp">
                            <b>Número:</b>
                        </td>
                        <td class="tdp">
                            <asp:Label ID="lblNumero0" runat="server" Style="font-size: 8pt; font-weight: 700;"></asp:Label>
                        </td>
                        <td style="width: 15%; font-size: 8pt;" class="tdp">
                            <b>Ativo:</b>
                        </td>
                        <td class="tdp">
                            <asp:Label ID="lblAtivo0" runat="server" Style="font-size: 8pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdp" colspan="4">
                            <b>Itens da Nota Fiscal</b>
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Panel ID="ppp" runat="server" Width="30%">
                                <table style="width: 100%; font-size: 8pt;">
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
    <!--fim-->
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmProjetoDetalhe.aspx.cs"
    Inherits="frmProjetoDetalhe" Theme="Adm" EnableTheming="true" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;

            //alert(tecla);

            if ((tecla > 47 && tecla < 58) || tecla == 44)
                return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }
    </script>
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Projetos" Font-Bold="True"
                    Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>

    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" Width="100%"
        Skin="Outlook" CausesValidation="False" TabIndex="1" SelectedIndex="1">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="pgnDadosPessoais" Text="Abertura do Projeto">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="pgnDadosBancarios" Text="Estimar Recebimento"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="pgnDadosFiliais" Text="Arquivos">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="pgnFotos" Text="Planejamento de Entrega">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="pgnHistorico" Text="Faturamento">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="rmp" runat="server" BackColor="White" CssClass="bordaTabs"
        Width="100%" Height="430px">
    
    <telerik:RadPageView ID="subPgAbertura" runat="server" Width="100%">
        <table class="table" width="99%">
            <tr>
                <td class="tdp" width="10%">
                    Filial:
                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="tdp">
                    <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" Width="96%">
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    Nome:
                </td>
                <td class="tdp" width="40%">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" MaxLength="100" Width="95%"
                        TabIndex="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="INFORME O NOME"
                        Text="*" ControlToValidate="txtNome" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Contato Cliente:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtContatoCliente" runat="server" CssClass="txt" MaxLength="100"
                        Width="95%" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="INFORME CONTATO CLIENTE"
                        Text="*" ControlToValidate="txtContatoCliente" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap">
                    Contato Contratado:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtContatoContratado" runat="server" CssClass="txt" MaxLength="100"
                        Width="95%" TabIndex="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="INFORME CONTATO CONTRATADO"
                        Text="*" ControlToValidate="txtContatoContratado" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <hr style="width: 99%; height: 1px" />
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Área Climatizada:
                </td>
                <td class="tdp">
                    <asp:DropDownList ID="cboAreaClimatizada" runat="server" CssClass="cbo" Width="96%"
                        TabIndex="4">
                        <asp:ListItem>NAO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    &nbsp;
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <hr style="width: 99%; height: 1px" />
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Início da Produção:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtInicioProducao" runat="server" CssClass="txt" Width="25%" TabIndex="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="INFORME A DATA DE INICIO PRODUÇÃO"
                        Text="*" ControlToValidate="txtInicioProducao" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInicioProducao"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtInicioProducao" />
                </td>
                <td class="tdp">
                    Início da Entrega:
                </td>
                <td class="tdp">
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFinalProducao"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFinalProducao" />


                    <asp:TextBox ID="txtInicioEntrega" runat="server" CssClass="txt" Width="25%" TabIndex="7"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInicioEntrega"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtInicioEntrega" />


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="INFORME A DATA DE INICIO DA ENTREGA"
                        Text="*" ControlToValidate="txtInicioEntrega" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Final da Produção:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtFinalProducao" runat="server" CssClass="txt" Width="25%" TabIndex="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ErrorMessage="INFORME A DATA DE FIM PRODUÇÃO"
                        Text="*" ControlToValidate="txtFinalProducao" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    Final da Entrega:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtFimEntrega" runat="server" CssClass="txt" OnTextChanged="txtNome5_TextChanged"
                        Width="25%" TabIndex="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="INFORME A DATA DE FIM DA ENTREGA"
                        Text="*" ControlToValidate="txtFimEntrega" SetFocusOnError="true"></asp:RequiredFieldValidator>


                         <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFimEntrega"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFimEntrega" />
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Total de Kits:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtTotalKits" runat="server" CssClass="txtValor" Width="25%" TabIndex="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="INFORME O TOTAL DE KITS"
                        Text="*" ControlToValidate="txtTotalKits" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    Fator Caixa:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtFatorCaixa" runat="server" CssClass="txtValor" Width="25%" TabIndex="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="INFORME O FATOR CAIXA"
                        Text="*" ControlToValidate="txtFatorCaixa" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Fator Pallet:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtFatorPallet" runat="server" CssClass="txtValor" Width="25%" TabIndex="11"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="INFORME O FATOR PALLET"
                        Text="*" ControlToValidate="txtFatorPallet" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    Peso por Kit:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtPesoKit" runat="server" CssClass="txtValor" Width="25%" TabIndex="12"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="INFORME O PESO KIT"
                        Text="*" ControlToValidate="txtPesoKit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Frete por Kit:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtFeteKit" runat="server" CssClass="txtValor" Width="25%" TabIndex="13"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="INFORME O FRETE KIT"
                        Text="*" ControlToValidate="txtFeteKit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Tempo de Produção (dias):
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtTempoProducao" runat="server" CssClass="txtValor" Width="25%"
                        TabIndex="14"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="INFORME O TEMPO DE PRODUÇÃO"
                        Text="*" ControlToValidate="txtTempoProducao" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <hr style="width: 99%; height: 1px" />
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Turnos:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtTurnos" runat="server" CssClass="txtValor" Width="25%" TabIndex="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="INFORME A QTD DE TURNOS"
                        Text="*" ControlToValidate="txtTurnos" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Pessoas por Turno:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtPessoasTurno" runat="server" CssClass="txtValor" Width="25%"
                        TabIndex="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="INFORME A QTD DE PESSOAS POR TURNO"
                        Text="*" ControlToValidate="txtPessoasTurno" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <hr style="width: 99%; height: 1px" />
                    <asp:UpdatePanel ID="i" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnConfirmarArquivos" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Mão de Obra:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtMaodeObra" runat="server" CssClass="txtValor" Width="25%" TabIndex="17"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="INFORME A MAO DE OBRA"
                        Text="*" ControlToValidate="txtMaodeObra" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
                <td class="tdp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Status:
                </td>
                <td class="tdp">
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="txt" MaxLength="50" Width="95%"
                        TabIndex="18" Height="40px" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="INFORME O STATUS"
                        Text="*" ControlToValidate="txtStatus" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap">
                    &nbsp;Planejamento da Transferência:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtPlanejamentoTransferencia" runat="server" CssClass="txt" 
                        Height="40px" MaxLength="50" TabIndex="18" TextMode="MultiLine" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <hr style="width: 99%; height: 1px" />
                </td>
            </tr>
            <tr>
                <td class="tdp" colspan="2">
                    Será utilizado arquivo EDI para a importação das informações:</td>
                <td class="tdp" colspan="2">
                    Comprovantes serão digitalizados:</td>
            </tr>
            <tr>
                <td class="tdp" colspan="2">
                    <asp:DropDownList ID="cboEdi" runat="server" CssClass="cbo" 
                        TabIndex="4" Width="96%">
                        <asp:ListItem>NAO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdp" colspan="2">
                    <asp:DropDownList ID="cboComprovantes" runat="server" CssClass="cbo" 
                        TabIndex="4" Width="96%">
                        <asp:ListItem>NAO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView ID="subPgRecebimento" runat="server" Width="100%">
        <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
            ForeColor="Black" GridLines="Vertical" OnRowCommand="grdItens_RowCommand" Width="100%">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="CODIGO" HeaderText="CÓDIGO" />
                <asp:BoundField DataField="DESCRICAO" HeaderText="DESCRIÇÃO" />
                <asp:BoundField DataField="QUANTIDADE" HeaderStyle-HorizontalAlign="Right" HeaderText="QUANTIDADE"
                    ItemStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="QUANTIDADERECEBIDA" HeaderStyle-HorizontalAlign="Right"
                    HeaderText="RECEBIDA" ItemStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ULTIMORECEBIMENTO" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="ÚLTIMO RECEBIMENTO" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnExcluir" runat="server" CommandArgument="Excluir" CommandName='<%#Eval("CODIGO") %>'
                            ImageUrl="excluir_small.png" ToolTip="Excluir" />
                    </ItemTemplate>
                    <HeaderStyle Width="1%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <strong>Nenhum item cadastrado.</strong>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:Panel ID="pnlNovoItem" runat="server" BorderStyle="Solid" BorderWidth="1px"
            DefaultButton="btnConfirmarItem" Visible="False" Width="400px">
            <table class="table" style="width: 100%">
                <tr>
                    <td class="tdp" width="1%">
                        Codigo:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtItemCodigo" runat="server" CssClass="txt" MaxLength="50" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        Descrição:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtItemDescricao" runat="server" CssClass="txt" MaxLength="100"
                            Width="99%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        Quantidade:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtItemQuantidade" runat="server" CssClass="txtValor" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp" nowrap="nowrap">
                        Quantidade Recebida:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtItemQuantidadeRecebida" runat="server" CssClass="txtValor" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        Último Recebimento:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtItemUltimoRecebimento" runat="server" CssClass="txt" Width="25%"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtItemUltimoRecebimento_MaskedEditExtender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtItemUltimoRecebimento"
                            UserDateFormat="DayMonthYear">
                        </asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtItemUltimoRecebimento_CalendarExtender" runat="server"
                            Format="dd/MM/yyyy" TargetControlID="txtItemUltimoRecebimento" />
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        &nbsp;
                        <asp:Label ID="lblIdProjetoItem" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td class="tdpR">
                        <asp:Button ID="btnCancelarrItem" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="btnCancelarrItem_Click" Text="Cancelar" />
                        &nbsp;<asp:Button ID="btnConfirmarItem" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="btnConfirmarItem_Click" Text="Incluir" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Button ID="btnNovoItem" runat="server" CausesValidation="False" CssClass="button"
            OnClick="btnNovoItem_Click" TabIndex="19" Text="Novo Item" Width="150px" />
        <br />
        <br />
        <hr />
        <table class="grid">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; font-size: 9pt;">
                    <strong>Filial Recebimento</strong></td>
            </tr>
            <tr>
                <td>
                    <table class="table" width="50%">
                        <tr>
                            <td class="tdp" colspan="4">
                                Selecione a Filial e clique em + para adicionar ou - para remover</td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Filial:</td>
                            <td class="tdp" nowrap="nowrap" width="50%">
                                <asp:DropDownList ID="cboFilialRecebimento" runat="server" CssClass="cbo" 
                                    Font-Names="Arial" Font-Size="7pt" Width="150px">
                                </asp:DropDownList>
                                <asp:Button ID="btnAdicionarFilial" runat="server" BackColor="#990000" 
                                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                                    Height="17px" onclick="btnAdicionarFilial_Click" Text="+" Width="20px" />
                            </td>
                            <td class="tdp" nowrap="nowrap" width="1%">
                                Selecionados:</td>
                            <td class="tdp" nowrap="nowrap" width="99%">
                                <asp:ListBox ID="lstFilialRecebimento" runat="server" CssClass="txt" 
                                    Height="100px" Rows="5" Width="150
                        "></asp:ListBox>
                                <asp:Button ID="btnRemoverFilial" runat="server" BackColor="#990000" 
                                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                                    Height="16px" onclick="btnRemoverFilial_Click" Text="-" Width="20px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView ID="subPgArquivos" runat="server" Width="100%">
        <asp:GridView ID="grdArquivos" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
            ForeColor="Black" GridLines="Vertical" OnRowCommand="grdArquivos_RowCommand"
            OnRowDataBound="grdArquivos_RowDataBound" Width="100%">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="IDPROJETOARQUIVO" HeaderText="CÓDIGO" />
                <asp:BoundField DataField="DESCRICAO" HeaderText="DESCRIÇÃO" />
                <asp:BoundField DataField="DATA" HeaderText="DATA" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnVerArquivo" runat="server" CommandArgument="VerArquivo" CommandName='<%#Eval("IDPROJETOARQUIVO") %>'
                            ImageUrl="~/Images/lupa.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="1%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnExcluirArquivo" runat="server" CommandArgument="ExcluirArquivo"
                            CommandName='<%#Eval("IDPROJETOARQUIVO") %>' ImageUrl="excluir_small.png" ToolTip="Excluir Aruivo" />
                    </ItemTemplate>
                    <HeaderStyle Width="1%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <strong>Nenhum arquivo encontado.</strong>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <table style="width: 100%">
            <tr>
                <td nowrap="nowrap" width="1%">
                    Selecione Arquivo:
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" Width="60%" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" width="1%">
                    Descrição:
                </td>
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
                <td width="1%">
                    <asp:Button ID="btnConfirmarArquivos" runat="server" CausesValidation="False" CssClass="button"
                        OnClick="btnConfirmarArquivo_Click" Text="Confirmar" />
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView ID="subPgPlanejamento" runat="server" Width="100%">
        <asp:GridView ID="grdProducao" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
            ForeColor="Black" GridLines="Vertical" OnRowCommand="grdItens_RowCommand" Width="100%">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="Lancamento" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="LANÇAMENTO" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TURNO" HeaderStyle-HorizontalAlign="Right" HeaderText="TURNO"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NOMEUSUARIO" HeaderStyle-HorizontalAlign="Left" HeaderText="USUÁRIO"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="HORAINICIAL" HeaderStyle-HorizontalAlign="Center" HeaderText="INÍCIO"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="HORAFINAL" HeaderStyle-HorizontalAlign="Center" HeaderText="FIM"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MAODEOBRA" HeaderStyle-HorizontalAlign="Right" HeaderText="MÃO DE OBRA"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="QUANTIDADEEFETUADA" HeaderStyle-HorizontalAlign="Right"
                    HeaderText="EFETUADA" ItemStyle-HorizontalAlign="Right" />
            </Columns>
            <EmptyDataTemplate>
                <strong>Nenhuma produção cadastrada.</strong>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:Panel ID="pnlNovaProducao" runat="server" BorderStyle="Solid" BorderWidth="1px"
            DefaultButton="btnConfirmarItem" Visible="False" Width="400px">
            <table class="table" style="width: 100%">
                <tr>
                    <td class="tdp" width="1%">
                        Turno:
                    </td>
                    <td class="tdp" width="99%">
                        <asp:TextBox ID="txtTurno" runat="server" CssClass="txtValor" MaxLength="50" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp" nowrap="nowrap">
                        Hora Inicial: ex.:(10:10)
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtHoraIni" runat="server" CssClass="txt" MaxLength="5" Width="25%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdp" nowrap="nowrap">
                        Hora Final:&nbsp; ex.:(14:20)
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtHoraFim" runat="server" CssClass="txt" MaxLength="5" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp" nowrap="nowrap">
                        Mão de Obra:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtMaoObra" runat="server" CssClass="txtValor" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp" nowrap="nowrap">
                        Quantidade Efetuada:
                    </td>
                    <td class="tdp">
                        <asp:TextBox ID="txtQuantidadeEfetuada" runat="server" CssClass="txtValor" Width="25%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdp">
                        &nbsp;
                        <asp:Label ID="lblIdProjetoItem0" runat="server" ForeColor="#CC0000"></asp:Label>
                    </td>
                    <td class="tdpR">
                        <asp:Button ID="btnCancelarProducao" runat="server" CausesValidation="False" CssClass="button"
                            OnClick="btnCancelarProducao_Click" Text="Cancelar" />
                        &nbsp;<asp:Button ID="btnConfirmarProducao" runat="server" CausesValidation="False"
                            CssClass="button" OnClick="btnConfirmarProducao_Click" Text="Incluir" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Button ID="btnNovaProducao" runat="server" CausesValidation="False" CssClass="button"
            OnClick="btnNovaProducao_Click" TabIndex="20" Text="Nova Produção" Width="150px" />
        <br />
        <hr />
        <br />
        <table class="grid">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px; font-size: 9pt;">
                    <strong>Filial Entrega</strong></td>
            </tr>
            <tr>
                <td>
                    <table class="table" width="50%">
                        <tr>
                            <td class="tdp" colspan="4">
                                Selecione a Filial e clique em + para adicionar ou - para remover</td>
                        </tr>
                        <tr>
                            <td class="tdp">
                                Filial:</td>
                            <td class="tdp" nowrap="nowrap" width="50%">
                                <asp:DropDownList ID="cboFilialEntrega" runat="server" CssClass="cbo" 
                                    Font-Names="Arial" Font-Size="7pt" Width="150px">
                                </asp:DropDownList>
                                <asp:Button ID="btnAdicionarFilialEntrega" runat="server" BackColor="#990000" 
                                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                                    Height="17px" onclick="btnAdicionarFilialEntrega_Click" Text="+" Width="20px" />
                            </td>
                            <td class="tdp" nowrap="nowrap" width="1%">
                                Selecionados:</td>
                            <td class="tdp" nowrap="nowrap" width="99%">
                                <asp:ListBox ID="lstFilialEntrega" runat="server" CssClass="txt" Height="100px" 
                                    Rows="5" Width="150
                        "></asp:ListBox>
                                <asp:Button ID="btnRemoverFilialEntrega" runat="server" BackColor="#990000" 
                                    BorderStyle="None" CssClass="button" Font-Bold="True" Font-Names="arial" 
                                    Height="16px" onclick="btnRemoverFilialEntrega_Click" Text="-" Width="20px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView ID="subPgFaturamento" runat="server" Width="100%">
        <table class="table">
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Emitir CTRC:</td>
                <td class="tdp" width="50%">
                    <asp:DropDownList ID="cboCTRC" runat="server" CssClass="cbo" 
                        TabIndex="4" Width="96%">
                        <asp:ListItem>NAO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdp" nowrap="nowrap">
                    Emitir NF de Serviço:</td>
                <td class="tdp" width="50%">
                    <asp:DropDownList ID="cboEmitirNota" runat="server" CssClass="cbo" 
                        TabIndex="4" Width="96%">
                        <asp:ListItem>NAO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap">
                    Forma de Faturamento:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtFormaDeFaturamento" runat="server" CssClass="txt" 
                        MaxLength="50" TabIndex="2" Width="95%"></asp:TextBox>
                </td>
                <td class="tdp">
                    Vencimento:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtVencimentoFaturamento" runat="server" CssClass="txt" 
                        MaxLength="50" TabIndex="2" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Ordem da Coleta:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtOrdemColeta" runat="server" CssClass="txt" MaxLength="50" 
                        TabIndex="2" Width="95%"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap">
                    Valor por Coleta:</td>
                <td class="tdp">
                    <asp:TextBox ID="txtValorColeta" runat="server" CssClass="txt" MaxLength="20" 
                        TabIndex="2" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdp">
                    Observações Gerais:</td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdp" colspan="4">
                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="txt" Height="40px" 
                        MaxLength="50" TabIndex="18" TextMode="MultiLine" Width="99%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    </telerik:RadMultiPage>


    <table class="grid">
        <tr>
            <td>
                &nbsp;</td>
            <td class="tdpR">
        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="button"
            OnClick="btnCancel_Click" TabIndex="22" Text="Cancelar" Width="70px" />
        <asp:Button ID="btnSalvar" runat="server" CssClass="button" OnClick="btnSalvar_Click"
            TabIndex="21" Text="Salvar" Width="75px" />
            </td>
        </tr>
    </table>
</asp:Content>

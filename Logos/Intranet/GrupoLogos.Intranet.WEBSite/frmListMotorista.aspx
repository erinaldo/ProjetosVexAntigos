<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmListMotorista.aspx.cs"
    Inherits="Intranet_frmListMotorista" EnableTheming="True" Theme="Adm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Motorista" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                    <asp:Label ID="lblIdUsuario" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="1" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="20%">
                    Nome:
                </td>
                <td class="tdp" width="10%">
                    CPF:</td>
                <td class="tdp" width="1%" nowrap="nowrap">
                    &nbsp;Gerenciamento de Risco:</td>
                <td class="tdp" width="20%" nowrap="nowrap">
                    Favorecido:</td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:CheckBox ID="chkVencida" runat="server" Text="Hab. Vencidas" />
                </td>
                <td class="tdp" nowrap="nowrap">
                    &nbsp;</td>
            </tr>
            <tr valign="bottom">
                <td class="tdp" >
                    
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="95%"></asp:TextBox>
                    
                </td>
                <td class="tdp" >
                    
                    <asp:TextBox ID="txtCpf" runat="server" CssClass="txt" Width="95%" 
                        Wrap="False"></asp:TextBox>
                    
                </td>
                <td class="tdp" nowrap="nowrap" >
                    
                    <asp:DropDownList ID="cboGerenciamento" runat="server" CssClass="cbo" 
                        Height="17px" Width="80px">
                        <asp:ListItem Selected="True">Nenhum</asp:ListItem>
                        <asp:ListItem Value="VencimentoPancary">Pancary</asp:ListItem>
                        <asp:ListItem Value="VencimentoBrasilrisk">Brasilrisk</asp:ListItem>
                        <asp:ListItem Value="VencimentoBuonny">Buonny</asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:TextBox ID="txtGerenciamento" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtGerenciamento" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGerenciamento" 
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    
                    &nbsp;Até:<asp:TextBox ID="txtGerenciamentoFim" runat="server" CssClass="txt" 
                        Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtGerenciamentoFim" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtGerenciamentoFim" 
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    
                </td>
                <td class="tdp" nowrap="nowrap" >
                    
                    <asp:DropDownList ID="cboTipoFavorecido" runat="server" CssClass="cbo" 
                        Height="17px">
                        <asp:ListItem Selected="True">Nenhum</asp:ListItem>
                        <asp:ListItem Value="NomeFavorecido">Nome</asp:ListItem>
                        <asp:ListItem Value="CnpjCpfFavorecido">CPF</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFavorecido" runat="server" CssClass="txt" Width="150px" 
                        Wrap="False"></asp:TextBox>
                    
                </td>
                <td class="tdp" nowrap="nowrap">
                    <asp:CheckBox ID="chkativo" runat="server" Checked="True" Text="Ativo" />
                </td>
                <td class="tdp" nowrap="nowrap">
                    
                    <asp:CheckBox ID="chkInativo" runat="server" Checked="True" Text="Inativo" />
                </td>
            </tr>
            <tr valign="bottom">
                <td class="tdp">
                    Contratado:</td>
                <td class="tdp">
                    Filial:</td>
                <td class="tdp">
                    Data de Bloqueio:</td>
                <td class="tdp">
                    &nbsp;</td>
                <td class="tdp" nowrap="nowrap">
                    <asp:CheckBox ID="chkLiberado" runat="server" Checked="True" Text="Liberado" />
                </td>
                <td class="tdp" nowrap="nowrap">
                    <asp:CheckBox ID="chkNaoLiberado" runat="server" Checked="True" 
                        Text="Não Liberado" />
                </td>
            </tr>
            <tr valign="bottom">
                <td class="tdp">
                    <asp:DropDownList ID="cboContratado" runat="server" CssClass="cbo" 
                        Height="17px" Width="96%">
                        <asp:ListItem Selected="True">AMBOS</asp:ListItem>
                        <asp:ListItem Value="SIM">SIM</asp:ListItem>
                        <asp:ListItem Value="NAO">NÃO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                    <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" Height="17px" 
                        Width="96%">
                    </asp:DropDownList>
                </td>
                <td class="tdp">
                                       <asp:TextBox ID="txtDataDeBloqueio" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtDataDeBloqueio" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataDeBloqueio" 
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                                       &nbsp;Até: <asp:TextBox ID="txtDataDeBloqueioFim" runat="server" CssClass="txt" 
                        Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtDataDeBloqueioFim" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureName="pt-BR" CultureThousandsPlaceholder="" CultureTimePlaceholder="" 
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataDeBloqueioFim" 
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                                       </td>
                <td class="tdp">
                    </td>
                <td class="tdpR" colspan="2" nowrap="nowrap">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                    <asp:Button ID="Button2" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="Button2_Click" Text="Novo" />
                    <asp:Button ID="Button3" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" Text="Gerar Relatório" />
                    </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid17" runat="server" GridLines="None" Skin="Default2006"
                    AllowPaging="True" AllowSorting="True" OnPageIndexChanged="RadGrid17_PageIndexChanged"
                    OnSortCommand="RadGrid17_SortCommand" PageSize="25" 
                    onitemdatabound="RadGrid17_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Código" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("IDMOTORISTA") %>' NavigateUrl='<% # "frmCadMotorista.aspx?IDMOTORISTA=" + Eval("IDMOTORISTA") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="1%" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Código" DataField="IDMotorista"
                                UniqueName="column1" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Nome" DataField="RazaoSocialNome"
                                UniqueName="column2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CPF" DataField="CnpjCpf"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ativo" DataField="Ativo"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Liberado" DataField="liberado"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Validade Hab."
                                DataField="ValidadeDaHabilitacao" UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Nº Hab." DataField="CarteiraDeHabilitacao"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Categoria" DataField="Categoria"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Primeira Hab."
                                DataField="DataDaPrimeiraHabilitacao" DataFormatString="{0:d}" UniqueName="column">
                            </telerik:GridBoundColumn>
                            
                                                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Pancary"
                                DataField="VencimentoPancary" UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Brasilrisk"
                                DataField="VencimentoBrasilrisk" UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Buonny"
                                DataField="VencimentoBuonny" UniqueName="column" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

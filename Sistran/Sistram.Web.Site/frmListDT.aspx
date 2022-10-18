<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmListDT.aspx.cs"
    Inherits="frmListDT" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Documento de Transporte"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="2" cellspacing="1" 
            width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    DT:
                </td>
                <td class="tdp" width="5%">
                    <asp:TextBox ID="txtDt" runat="server" AutoPostBack="True" CssClass="txt" OnTextChanged="txtDT_TextChanged"
                        Width="150px"></asp:TextBox>
                </td>
                <td class="tdp" width="1%" nowrap="nowrap">
                    Número:</td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtDt0" runat="server" AutoPostBack="True" CssClass="txt" 
                        OnTextChanged="txtDT_TextChanged" Width="150px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="2%">
                    Data de Emissão:
                </td>
                <td class="tdp" width="25%" nowrap="nowrap">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtI" />
                    
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    
                    &nbsp;Até:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    
                      <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtF"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" width="1%">
                    <asp:DropDownList ID="cboTipoDes1" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        OnSelectedIndexChanged="cboTipoDes0_SelectedIndexChanged">
                        <asp:ListItem>TODOS</asp:ListItem>
                        <asp:ListItem>PENDENTES</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdpR" nowrap="nowrap" width="1%">
                    <table align="right" border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                                    Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                            </td>
                            <td>
                                <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                                    Font-Names="Arial" Font-Size="7pt" Text="Relatório" Width="60px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdpR" nowrap="nowrap" width="1%">
                    Exibir:&nbsp;
                    <asp:DropDownList ID="cboTipoDes0" runat="server" CssClass="cbo" 
                        Font-Names="Arial" Font-Size="7pt" Height="17px" 
                        OnSelectedIndexChanged="cboTipoDes0_SelectedIndexChanged" Width="35px">
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
                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand"
                    Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" GridLines="None">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="IDDT" HeaderText="DT" UniqueName="column001">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("IDDT") %>' NavigateUrl='<% # "frmDetalheDT.aspx?IDDT=" + Eval("IDDT") + "&IDFILIAL=" + Eval("IDFILIAL") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Numero" DataField="NUMERO"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            
                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Situação" DataField="situacao"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Emissão" DataField="EMISSAO"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column6" DataType="System.DateTime">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Status" DataField="ANDAMENTO"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Filial" DataField="filial"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Transportadora" DataField="Transportadora"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Motorista" DataField="MOTORISTA"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                         <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Agregado" DataField="AGREGADO"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>        

                           
                         <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Placa Veículo" DataField="PLACA"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                                                  

                        </Columns>
                    </MasterTableView>
                    <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                    <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" BorderWidth="1px"
                        BorderColor="#666666" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Size="7pt" Font-Bold="False" />
                    <FilterMenu EnableTheming="True" Skin="Default2006">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                    <StatusBarSettings LoadingText="Carregando..." />
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

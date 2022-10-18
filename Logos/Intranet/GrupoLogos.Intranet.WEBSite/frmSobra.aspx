<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmSobra.aspx.cs"
    Inherits="frmSobra" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/jscript">
        function Confirmar() {
            return window.confirm("Tem certeza que deseja baixar este item?");
        } 

    </script>
    <table style="width: 100%;" __designer:mapid="13af" border="0" cellpadding="0" cellspacing="0">
        <tr __designer:mapid="13b0">
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                __designer:mapid="13b1">
                <asp:Label ID="lblTitulo" runat="server" Text="SOBRAS IDENTIFICADAS NAS FILIAIS"
                    Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%"
        __designer:mapid="335">
        <tr valign="bottom" __designer:mapid="336">
            <td class="tdp" width="1%" nowrap="nowrap" __designer:mapid="337">
                Período</td>
                <td class="tdp" nowrap="nowrap" valign="middle" style="width: 2%" 
                    __designer:mapid="a6">
                    <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender54" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtI" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    &nbsp;Até:
                    <asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" 
                        CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
            <td class="tdp" width="1%" nowrap="nowrap" __designer:mapid="337">
                Filial Origem:
            </td>
            <td class="tdp" width="40%" __designer:mapid="338">
                <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" Font-Names="Arial"
                    Font-Size="7pt" Height="17px" AutoPostBack="True" OnSelectedIndexChanged="cboFilial_SelectedIndexChanged"
                    Width="99%">
                </asp:DropDownList>
            </td>
            <td class="tdp" __designer:mapid="339">
                <table align="right" border="0" cellpadding="1" cellspacing="0" __designer:mapid="35f">
                    <tr __designer:mapid="360">
                        <td>
                            <asp:Button ID="Button4" runat="server" CssClass="button" Text="Pesquisar" 
                                onclick="Button4_Click" />
                        &nbsp;</td>
                        <td __designer:mapid="365">
                            <asp:Button ID="Button3" runat="server" CssClass="button" Text="Novo" PostBackUrl="~/frmSobraDetalhe.aspx" />
                        </td>
                        <td __designer:mapid="365">
                        &nbsp;<asp:Button ID="Button5" runat="server" CssClass="button" 
                                Text="Gerar Relatorio" 
                                onclientclick="javascript:window.open('frmgdrExcelSobras.aspx'); return false;" />
                        </td>
                    </tr>
                </table>
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBox1_CheckedChanged" Text="Exibir Finalizados" />
            </td>
        </tr>
        <tr valign="bottom" __designer:mapid="336">
            <td class="tdp" nowrap="nowrap" __designer:mapid="337" colspan="5">
                <telerik:RadGrid ID="RadGridUsuarios" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="0" GridLines="None" OnItemCommand="RadGrid16_ItemCommand" OnItemDataBound="RadGrid16_ItemDataBound"
                    Skin="Default2006" Width="100%" PageSize="100" 
                    onpageindexchanged="RadGridUsuarios_PageIndexChanged" 
                    onpagesizechanged="RadGridUsuarios_PageSizeChanged" 
                    onsortcommand="RadGridUsuarios_SortCommand">
                    <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="IDPROJETO" HeaderText="CÓDIGO" 
                                UniqueName="column00111">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "frmSobraDetalhe.aspx?id=" + Eval("IDSOBRA") %>'
                                        Text='<%# Bind("IDSOBRA") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="DATA" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                HeaderText="DATA LANÇAMENTO" ItemStyle-HorizontalAlign="Center" UniqueName="column1"
                                Visible="true">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FILIAL" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="FILIAL ORIGEM" ItemStyle-HorizontalAlign="Left" UniqueName="column2"
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FILIALDESTINO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="FILIAL DESTINO" ItemStyle-HorizontalAlign="Left" UniqueName="column3"
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DataDeEmbarqueDoVolume" 
                                EmptyDataText="&amp;nbsp;" HeaderText="DATA DE EMBARQUE" UniqueName="column8">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CLIENTE" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="CLIENTE" ItemStyle-HorizontalAlign="Left" UniqueName="column4" 
                                Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUMERONOTAFISCAL" EmptyDataText="&amp;nbsp;"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="NOTAFISCAL" ItemStyle-HorizontalAlign="Left"
                                UniqueName="column5" Visible="true">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="quantidade" EmptyDataText="&amp;nbsp;"
                                HeaderStyle-HorizontalAlign="Right" HeaderText="VOLUMES" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column6" Visible="true" DataFormatString="{0:N2}" >
<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="DATAFINALIZACAO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                HeaderText="DATA DA BAIXA" ItemStyle-HorizontalAlign="Center" UniqueName="column7"
                                Visible="true">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NomeUsuario" EmptyDataText="&amp;nbsp;" 
                                HeaderText="USUÁRIO" UniqueName="column">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="column0011">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnBaixar" runat="server" Text="BAIXAR" CssClass="link" CommandName='<%# Bind("IDSOBRA") %>'
                                        CommandArgument="Baixar" OnClientClick="Confirmar()" />
                                        <asp:Label ID="lblStatus" runat="server" Visible="false" text ='<%# Bind("Finalizado")%>'></asp:Label>

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
                <br />
                <table border="1" cellpadding="1" cellspacing="1" class="grid" width="100%">
                    <tr>
                        <td style="text-align: center">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

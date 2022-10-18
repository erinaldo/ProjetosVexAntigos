<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FRMMVPROCTERQUASAR.aspx.cs"
    Inherits="FRMMVPROCTERQUASAR" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                            height: 25px">
                            <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Filial"
                                Font-Bold="True" Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvAjudaTransitTime" runat="server" style="position: absolute; top: 30%;
                    left: 45%; text-align: center; display: none; width:300px; border-color:Silver; border-style:solid; border-width:1px">
                    <table cellpadding="2" cellspacing="2" border="0" style="background-color:#FFFFDD" width="100%" >
                        <tr>
                            <td>                                
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <asp:Label ID="lbltituloAjuda" runat="server" ></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        
                                          <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <hr />
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr  align="left">
                                            <td>
                                                <asp:Label ID="lblAjuda" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="vv" runat="server"  Text="FECHAR [X]" CssClass="link"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="baseline">
                        <td class="tdp" nowrap="nowrap" valign="middle" style="width: 3%">
                            Emissão:
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="middle" style="width: 5%">
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
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                            <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                            </asp:MaskedEditExtender>
                        </td>
                        <td class="tdp" nowrap="nowrap" valign="baseline" width="50%">
                            <table align="left" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updBot" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                                    OnClick="Button1_Click" Text="Pesquisar" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnGerarReport" runat="server" CssClass="button" Font-Names="Arial"
                                                    Font-Size="7pt" Text="EXCEL" Width="60px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" Font-Size="7pt"
                                            Text="PDF" Visible="False" Width="40px"  
                                            Height="16px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width: 100%" border="0">
                        <tr>
                            <td valign="top" style="height: 16px">
                               
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                <br />
                               
                                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnPageIndexChanged="RadGrid1_PageIndexChanged" 
                    Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" GridLines="None" 
                                    Visible="False">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="CnpjRemetente" HeaderText="CNPJ Remetente" 
                                UniqueName="column001">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("CnpjRemetente") %>'
                                        NavigateUrl='<% # "FRMMVPROCTERQUASARDetalhe.aspx?opc=Mov. Procter Quasar &cnpj=" + Eval("CnpjRemetente") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Remetente" DataField="NomeRemetente"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" 
                                UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="UF" DataField="UFRemetente"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column6" DataType="System.DateTime">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" 
                                HeaderText="Cidade Remetente" DataField="CidadeRemetente" 
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" DataField="Volumes"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="Volumes"
                                UniqueName="column8">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Peso Bruto" 
                                DataField="PesoBruto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column9">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Valor Da Nota" 
                                DataField="ValorDaNota" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column10">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                           <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Quantidade de Notas" 
                                DataField="QuantidadeDeNotas" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column10">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Frete" 
                                DataField="Frete" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                UniqueName="column2">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                    </MasterTableView>
                    <ExportSettings IgnorePaging="True" OpenInNewWindow="True">
                        <Excel Format="ExcelML" />
                    </ExportSettings>
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
                               </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Filtro.aspx.cs"
    Inherits="Filtro" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="10%" nowrap="nowrap">
                    Nota Fiscal:
                </td>
                <td class="tdp" width="35%" nowrap="nowrap">
                    Datas:
                </td>
                <td class="tdp" width="35%">
                    Remetente:
                </td>
                <td class="tdp" width="30%">
                    Destinatário:
                </td>
                <td class="tdpR" nowrap="nowrap" >
                    Exibir:&nbsp;
                    <asp:DropDownList ID="cboTipoDes0" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px" OnSelectedIndexChanged="cboTipoDes0_SelectedIndexChanged"
                        Width="45px">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem Selected="True">20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>300</asp:ListItem>
                        <asp:ListItem>400</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    <asp:TextBox ID="txtNf" runat="server" CssClass="txtPesquisa" Width="150px" AutoPostBack="True"
                        OnTextChanged="txtNf_TextChanged"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;<asp:DropDownList ID="cboTipoData" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px">
                        <asp:ListItem Value="DOC.DataDeEmissao">Emissão</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeEntrada">Entrada</asp:ListItem>
                        <asp:ListItem Value="DOC.DataDeConclusao">Entrega</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtI" runat="server" CssClass="txtPesquisa" Width="60px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtI" />
                    <span style="font-size: 7pt">Até:</span><asp:TextBox ID="txtF" runat="server" CssClass="txtPesquisa"
                        Width="60px"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureName="pt-BR" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtI"
                        UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                    <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtF" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;<asp:DropDownList ID="cboTipoRem" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="CADREM.CNPJCPF">CPF/CNPJ</asp:ListItem>
                        <asp:ListItem Selected="True" Value="CADREM.RAZAOSOCIALNOME">R. Social</asp:ListItem>
                        <asp:ListItem Value="CADREM.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtRem" runat="server" CssClass="txtPesquisa"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;<asp:DropDownList ID="cboTipoDes" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="CADDES.CNPJCPF">CPF/CNPJ</asp:ListItem>
                        <asp:ListItem Selected="True" Value="CADDES.RAZAOSOCIALNOME">R. Social</asp:ListItem>
                        <asp:ListItem Value="CADDES.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtDest" runat="server" CssClass="txtPesquisa"></asp:TextBox>
                </td>
                <td class="tdpR" nowrap="nowrap" valign="baseline">
                    <table align="right" border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                    OnClick="Button1_Click" Text="Pesquisar" />
                            </td>
                            <td>
                                <asp:Button ID="btnGerarReport" runat="server" CssClass="button" Font-Names="Arial"
                                    Font-Size="7pt" Text="EXCEL" Width="60px" />
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" CssClass="button" Font-Names="Arial" Font-Size="7pt"
                                    Text="PDF" Visible="False" Width="40px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <br />
                <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand"
                    Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" GridLines="None"
                    OnItemCommand="RadGrid16_ItemCommand">
                    <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" BorderWidth="1px">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." 
                                UniqueName="column0011">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("NUMERO") %>'
                                        NavigateUrl='<% # "frmEntregas.aspx?idDoc=" + Eval("IdDocumento") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Série" DataField="Serie"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"
                                UniqueName="column5">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Emissão" DataField="DataDeEmissao"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column6" DataType="System.DateTime">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Entrada" DataField="DataDeEntrada"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column7">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" DataField="PrevisaoDeSaida" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Prev.Saída"
                                UniqueName="column8" Visible=false>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Saída" DataField="DataDeSaida"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column9" Visible=false>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Planejada" DataField="DataPlanejada"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column10" Visible=false>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Entrega/Ocor." DataField="DataDeConclusao"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                UniqueName="column2">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="FOTO" HeaderText="Foto" UniqueName="column001">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPossuiFoto" runat="server" Text='<%# Bind("FOTO") %>' CommandName='<% # Eval("IdDocumento") %>'
                                        CommandArgument="VerFoto"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Lead Time" DataField="TransitTime"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" UniqueName="column3">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ocorrência" DataField="DescricaoOcorrencia"
                                HeaderStyle-HorizontalAlign="Left" UniqueName="column11">
                                <HeaderStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CNPJ Remetente" DataField="RemeCnpj"
                                HeaderStyle-HorizontalAlign="Left" UniqueName="column12" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Status" DataField="Situacao"
                                HeaderStyle-HorizontalAlign="Left" UniqueName="column">
                                <HeaderStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DestCnpj" EmptyDataText="&amp;nbsp;" HeaderText="CNPJ Destinatário"
                                UniqueName="column4" Visible="False">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RemeNome" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Remetente" UniqueName="column13">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DestNome" HeaderStyle-HorizontalAlign="Left"
                                EmptyDataText="&amp;nbsp;" HeaderText="Destinatário" UniqueName="column1">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Localização Nota Fiscal" 
                                UniqueName="TemplateColumn" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgGoogle" runat="server" ImageUrl="~/Imagens/GOOLGLE.jpg" 
                                        Visible="False" Width="20px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
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
            </ContentTemplate>
        </asp:UpdatePanel>
         </asp:Panel>
        <div id="dvFoto" runat="server" style="position: absolute; left: 30%; top: 20px; width: 40%;
            background-color: White; text-align: center" visible="false">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
               
                <tr style="height:25px">
                    <td width="100%" 
                        style="background-color: #808080; font-family: Verdana; font-size: 9px;">
                        <b style="font-family: Verdana">Imagem da Ocorrência / Entrega</b>
                    </td>
                    
                </tr>
               
                <tr style="height:25px">
                    <td width="100%" 
                        style="background-color: #808080; font-family: Verdana; font-size: 9px;">
                        <asp:DataList ID="lstFoto" runat="server" RepeatDirection="Horizontal" 
                            onitemdatabound="lstFoto_ItemDataBound" 
                            onitemcommand="lstFoto_ItemCommand1">
                            <ItemTemplate>
                                <table class="grid">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblData" runat="server" Font-Size="7pt" ForeColor="White" 
                                                Text="Fotos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnAmpliarImagem" runat="server" Height="70px"
                                                CommandArgument='<% # Bind("IDDOCUMENTOOCORRENCIAARQUIVO") %>' 
                                                CommandName="Ampliar"/>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    
                </tr>
                <tr>
                    <td >
                     <asp:Image ID="imgFotoGrande" runat="server" Height="550px" />
                    </td>
                    
                </tr>
                <tr>
                <td>
                    <asp:Button ID="btnFecharImagem" runat="server" CssClass="button" 
                            onclick="btnFecharImagem_Click" Text="FECHAR" />
                </td>
                </tr>
            </table>
        </div>
   
</asp:Content>

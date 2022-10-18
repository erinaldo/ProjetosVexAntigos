<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="NotasFiscaisEntregueFiltro, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    <tr valign="middle" >
        <td class="tdp" width="30%" valign="middle" nowrap="nowrap">
            Datas:</td>
    <td class="tdp" width="30%" valign="middle" nowrap="nowrap" >Remetente:</td>
        <td class="tdp" width="30%" valign="middle" nowrap="nowrap">
            Destinatário:</td>
        <td class="tdpR" width="1%" align="right" valign="middle" nowrap="nowrap">
            Exibir:
            <asp:DropDownList ID="cboTipoDes0" runat="server" Font-Names="Arial" 
                Font-Size="7pt" Height="20px" 
                onselectedindexchanged="cboTipoDes0_SelectedIndexChanged" Width="35px">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">20</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
        <tr valign="middle" >
            <td class="tdp" valign="baseline" nowrap="nowrap">
                &nbsp;<asp:DropDownList ID="cboTipoData" runat="server" CssClass="cbo" 
                    Font-Names="Arial" Font-Size="7pt" Height="17px">
                    <asp:ListItem Value="DOC.DataDeEmissao" Selected="True">Emissão</asp:ListItem>
                    <asp:ListItem Value="DOC.DataDeEntrada">Entrada</asp:ListItem>
                    <asp:ListItem Value="DOC.DataDeConclusao">Entrega</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtI" />
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtI" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
                
                &nbsp;<asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtF" />
                <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtF" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
            </td>
            <td class="tdp" valign="baseline" nowrap="nowrap">
                <asp:DropDownList ID="cboTipoRem" runat="server" Font-Names="Arial" 
                    Font-Size="7pt" Height="17px" Width="70px" CssClass="cbo">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="CADREM.CNPJCPF">CPF/CNPJ</asp:ListItem>
                    <asp:ListItem Value="CADREM.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                    <asp:ListItem Value="CADREM.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtRem" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td class="tdp" valign="baseline" nowrap="nowrap">
                <asp:DropDownList ID="cboTipoDes" runat="server" Font-Names="Arial" 
                    Font-Size="7pt" Height="17px" Width="70px" CssClass="cbo">
                   <asp:ListItem Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="CADDES.CNPJCPF">CPF/CNPJ</asp:ListItem>
                    <asp:ListItem Value="CADDES.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                    <asp:ListItem Value="CADDES.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtDest" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td class="tdpR" align="right">
                <table cellpadding="1" cellspacing="0" align="right">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="button" 
                                OnClick="Button1_Click" Text="Pesquisar" Width="65px" />
                        </td>
                        <td>
                            <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                                Text="Excel" Width="60px" />
                        </td>
                        <td>
                            <asp:Button ID="btnPDF" runat="server" CssClass="button" 
                                Text="PDF" Visible="False" Width="40px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
        <tr>
            <td class="td_divisoria" colspan="4">
                </td>
        </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
        <br />
            <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                OnSortCommand="RadGrid1_SortCommand" Width="100%" 
                OnItemDataBound="RadGrid1_ItemDataBound" CellPadding="0" Skin="Default2006">
                <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" 
                    BorderWidth="1px">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    
                    <Columns>
                       
                      <%--  <telerik:GridBoundColumn DataField="DATADECONCLUSAO" EmptyDataText="&amp;nbsp;" 
                            HeaderText="Data de Entrega" UniqueName="column3" >
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="True"  />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." 
                            UniqueName="column001">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" 
                                    NavigateUrl='<% # "frmEntregas.aspx?idDoc=" + Eval("IdDocumento") %>' 
                                    Target="_blank" Text='<%# Bind("NUMERO") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Serie" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Left" HeaderText="Série" UniqueName="column5" 
                            Visible="false">
                            <HeaderStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DataDeEmissao" 
                            DataFormatString="{0:dd/MM/yyyy}" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Emissão" 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DataDeEntrada" 
                            DataFormatString="{0:dd/MM/yyyy}" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Entrada" 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column7">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DataDeSaida"  Visible=false
                            DataFormatString="{0:dd/MM/yyyy}" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Saída" 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column9">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DataDeConclusao" 
                            DataFormatString="{0:dd/MM/yyyy}" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Entrega/Ocor." 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column2">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Lead Time"  
                            DataField="TransitTime"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            UniqueName="column3">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>   
                        
                         <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ocorrência"  
                            DataField="DescricaoOcorrencia" HeaderStyle-HorizontalAlign ="Left"
                            UniqueName="column11">
                             <HeaderStyle HorizontalAlign="Left" />
                             </telerik:GridBoundColumn>           
                        
                         <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" 
                            HeaderText="CNPJ Remetente"  DataField="RemeCnpj" HeaderStyle-HorizontalAlign="Left"
                            UniqueName="column12" Visible="False">
                             <HeaderStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>       
                        
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Status"  
                            DataField="Situacao" HeaderStyle-HorizontalAlign="Left"
                            UniqueName="column8" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>      
                        
                        <telerik:GridBoundColumn DataField="DestCnpj"  
                            EmptyDataText="&amp;nbsp;" HeaderText="CNPJ Destinatário" 
                            UniqueName="column4" Visible="False">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                Wrap="True" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RemeNome" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Remetente" 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column13">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DestNome"  HeaderStyle-HorizontalAlign ="Left"
                            EmptyDataText="&amp;nbsp;" HeaderText="Destinatário" UniqueName="column1">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False"  
                                Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False"  
                                Wrap="True" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                <ItemStyle Font-Size="7pt" Font-Names="Arial" Height="7px" BorderStyle="Solid" 
                    BorderWidth="1px" />
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

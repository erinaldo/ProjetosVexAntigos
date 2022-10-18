<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NotasFiscaisAbertoFiltro.aspx.cs"
    Inherits="NotasFiscaisEntregueFiltro" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1" 
        BorderWidth="0px">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true" Font-Size="14px"></asp:Label>
    </td>
        <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px; text-align: right;">
            <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                Text="Relatório" Width="60px" />
        </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%" border="0" runat="server">
    <tr valign="middle" >
        <td class="tdp" width="25%">
            Datas:</td>
    <td class="tdp" width="25%" >Remetente:</td>
        <td class="tdp" width="25%">
            Destinatário:</td>
        <td class="tdpR" width="1%" align="right" nowrap="nowrap">
            <span style="font-size: 7pt">&nbsp;</span><asp:DropDownList ID="cboTipoDes0" 
                runat="server" Font-Names="Arial" 
                Font-Size="7pt" Height="17px" 
                onselectedindexchanged="cboTipoDes0_SelectedIndexChanged" Width="45px" 
                CssClass="cbo" style="font-family: Verdana; font-size: 7pt">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">20</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
        <tr valign="middle" >
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboTipoData" runat="server" Font-Names="Arial" 
                    Font-Size="7pt" Height="17px" style="font-family: Verdana; font-size: 7pt">
                    <asp:ListItem Value="DOC.DataDeEmissao">Emissão</asp:ListItem>
                    <asp:ListItem Value="DOC.DataDeEntrada">Entrada</asp:ListItem>
                    <asp:ListItem Value="DOC.DataDeConclusao">Entrega</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="70px" 
                    style="font-family: Verdana; font-size: 7pt"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtI" />
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtF" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
                
                Até:<asp:TextBox ID="txtF" runat="server" CssClass="txt" 
                    Width="70px" style="font-family: Verdana; font-size: 7pt"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtF" />
                <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtF" 
                    UserDateFormat="DayMonthYear">
                </asp:MaskedEditExtender>
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboTipoRem" runat="server" Font-Names="Arial" 
                    Font-Size="7pt" Height="17px" CssClass="cbo" 
                    style="font-family: Verdana; font-size: 7pt">
                    <asp:ListItem Value="CADREM.CNPJCPF">CPF/CNPJ</asp:ListItem>
                    <asp:ListItem Value="CADREM.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                    <asp:ListItem Value="CADREM.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtRem" runat="server" CssClass="txt" 
                    style="font-family: Verdana; font-size: 7pt"></asp:TextBox>
            </td>
            <td class="tdp" nowrap="nowrap">
                <asp:DropDownList ID="cboTipoDes" runat="server" Font-Names="Arial" 
                    Font-Size="7pt" Height="17px" CssClass="cbo" 
                    style="font-family: Verdana; font-size: 7pt">
                    <asp:ListItem Value="CADDES.CNPJCPF">CPF/CNPJ</asp:ListItem>
                    <asp:ListItem Value="CADDES.RAZAOSOCIALNOME" Selected="True">R. Social</asp:ListItem>
                    <asp:ListItem Value="CADDES.FANTASIAAPELIDO">Fantasia</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtDest" runat="server" CssClass="txt" 
                    style="font-family: Verdana; font-size: 7pt"></asp:TextBox>
            </td>
            <td class="tdpR" align="right">
                <table cellpadding="1" cellspacing="0" align="right">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="button" 
                                OnClick="Button1_Click" Text="Pesquisar" Width="65px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnPDF" runat="server" CssClass="button" 
                                Text="PDF" Visible="False" Width="40px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
    </table>   
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
            <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                OnSortCommand="RadGrid1_SortCommand" Width="100%" CellPadding="0" 
                Skin="Default2006">
                <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" 
                    BorderWidth="1px">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    
                    <Columns>
                        <telerik:GridTemplateColumn DataField="NUMERO" HeaderText="N.F." 
                            UniqueName="column001">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("NUMERO") %>' NavigateUrl='<% # "frmEntregas.aspx?idDoc=" + Eval("IdDocumento") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Série"  DataField="Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"
                            UniqueName="column5">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Emissão" DataField="DataDeEmissao" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            UniqueName="column6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Entrada" DataField="DataDeEntrada"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            UniqueName="column7">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;"  DataField="PrevisaoDeSaida"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Prev.Saída" UniqueName="column8">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Saída" DataField="DataDeSaida"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            UniqueName="column9">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Planejada" DataField="DataPlanejada"  DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            UniqueName="column10">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Entrega/Ocor." 
                            DataField ="DataDeConclusao"  DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"    
                            UniqueName="column2">
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
                        
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Remetente"  
                            DataField="RemeNome" HeaderStyle-HorizontalAlign="Left" Visible="false"
                            UniqueName="column13">
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>      
                        
                        <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Status"  DataField="Situacao" HeaderStyle-HorizontalAlign ="Left" 
                            UniqueName="column">
                            
                            <HeaderStyle HorizontalAlign="Left" />
                            
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

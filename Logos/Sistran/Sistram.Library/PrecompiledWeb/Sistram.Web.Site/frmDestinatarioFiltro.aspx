<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmDestinatarioFiltro, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <script language="javascript" type="text/javascript">
    function showdiv()
    {
        debugger
        document.all("serverdiv").style.visibility="visible";
    }
    
    function hidediv()
    {
        debugger
        document.all("serverdiv").style.visibility="hidden";
    }
</script>

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Manutenção de Destinatários" 
            Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%" border="0">
    <tr valign="middle" >
    <td class="tdp" width="1%" nowrap="nowrap" >Filtrar por:</td>
        <td class="tdp" width="95%" nowrap="nowrap">
            <asp:DropDownList ID="cboTipoDes" runat="server" CssClass="cbo" 
                Font-Names="Arial" Font-Size="7pt" Height="17px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="CNPJCPF">CPF/CNPJ</asp:ListItem>
                <asp:ListItem Selected="True" Value="RAZAOSOCIALNOME">R. Social</asp:ListItem>
                <asp:ListItem Value="FANTASIAAPELIDO">Fantasia</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:TextBox ID="txtDest" runat="server" CssClass="txt" 
                ontextchanged="txtDest_TextChanged"></asp:TextBox>
        </td>
        <td class="tdp" width="1%" nowrap="nowrap">
            Exibir:</td>
        <td class="tdp" nowrap="nowrap" width="1%">
            <asp:DropDownList ID="cboTipoDes0" runat="server" CssClass="cbo" 
                Font-Names="Arial" Font-Size="7pt" Height="17px" 
                onselectedindexchanged="cboTipoDes0_SelectedIndexChanged" Width="35px">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">20</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="tdpR" nowrap="nowrap">
            <table cellpadding="1" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;<asp:Button ID="Button1" runat="server" CssClass="button" 
                            OnClick="Button1_Click" Text="Pesquisar" Width="65px" />
                    </td>
                    <td>
                        <asp:Button ID="btnNovo" runat="server" CssClass="button" 
                            Text="Novo" Width="65px" />
                    </td>
                    <td>
                        <asp:Button ID="btnGerarReport" runat="server" CssClass="button" 
                            Text="Relatório" Width="60px" Visible="False" />
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btnPDF" runat="server" CssClass="button" Text="PDF" 
                            Visible="False" Width="40px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
        <tr>
            <td class="td_divisoria" colspan="5">
                </td>
        </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="xxwewx" runat="server">
        <ContentTemplate>
        <br />
            <telerik:RadGrid ID="RadGrid16" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                OnSortCommand="RadGrid1_SortCommand" Width="99%" 
                 CellPadding="0" Skin="Default2006" PageSize="20">
                <MasterTableView CellPadding="0" GridLines="Both" BorderColor="#CCCCCC" 
                    BorderWidth="1px">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    
                    <Columns>
                                         
                        <telerik:GridTemplateColumn DataField="IDCADASTRO" HeaderText="Código" 
                            UniqueName="column001">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" 
                                    NavigateUrl='<% # "frmDestinatarioDetalhe.aspx?idCadastro=" + Eval("IDCADASTRO") %>' 
                                    Target="_blank" Text='<%# Bind("IDCADASTRO") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="CnpjCpf" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="Center" HeaderText="CNPJ/CPF" 
                            ItemStyle-HorizontalAlign="Center" UniqueName="column5" >
                            <HeaderStyle HorizontalAlign="LEFT" />
                            <ItemStyle HorizontalAlign="LEFT" />
                        </telerik:GridBoundColumn>
                        
                        
                        <telerik:GridBoundColumn DataField="RazaoSocialNome" 
                            DataFormatString="{0:dd/MM/yyyy}" EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="left" HeaderText="Razão Social" 
                            ItemStyle-HorizontalAlign="left" UniqueName="column6">
                            <HeaderStyle HorizontalAlign="left" />
                            <ItemStyle HorizontalAlign="left" />
                        </telerik:GridBoundColumn>
                        
                        
                        <telerik:GridBoundColumn DataField="FantasiaApelido" 
                            EmptyDataText="&amp;nbsp;" 
                            HeaderStyle-HorizontalAlign="left" HeaderText="Fantasia" 
                            ItemStyle-HorizontalAlign="left" UniqueName="column7">
                            <HeaderStyle HorizontalAlign="left" />
                            <ItemStyle HorizontalAlign="left" />
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

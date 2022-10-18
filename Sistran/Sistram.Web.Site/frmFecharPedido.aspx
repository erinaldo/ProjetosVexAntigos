<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFecharPedido.aspx.cs" Inherits="frmFecharPedido" EnableEventValidation="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="Panel3" runat="server" Height="600px">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px" 
                        Text="Fechar Pedido"></asp:Label>
                </td>
            </tr>
        </table>
        
        <table border="0" cellpadding="1" cellspacing="0" class="table" 
            style="width: 100%">
            <tr>
                <td class="tdp" nowrap="nowrap" width="1%">
                    &nbsp;</td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Digite as iniciais dos Destinatário ou seu CNPJ/CPF:</td>
                <td>
                        &nbsp;</td>
              
            </tr>
            <tr>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Destinatário:</td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtDestinatario" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
                    <asp:Button ID="btnProcurar" runat="server" CssClass="button" 
                        onclick="btnProcurar_Click" Text="Procurar" />
                </td>
                <td>
                    &nbsp;<asp:Button ID="btnConfirma" runat="server" CssClass="button" 
                        onclick="btnConfirma_Click" Text="Confirmar" Visible="false" />
                    
                    <asp:Button ID="btnVoltarCarrinho" runat="server" CssClass="button" 
                        onclick="btnVoltarCarrinho_Click" PostBackUrl="~/frmCarrinho.aspx" 
                        Text="Voltar ao Carrinho" Visible="False" />
                </td>
            </tr>
        </table>
        
        <table align="center" style="width: 95%">
            <tr align="center">
                <td style="text-align: center">
                    <asp:Panel ID="pnlConfirmacao" runat="server" HorizontalAlign="Left" 
                        Visible="False" Width="99%" Height="560px" ScrollBars="Auto">
                        <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            Font-Size="7pt"></asp:Label>
                        <br />
                        <br />
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        
        
     <div id="dvPesquisa" runat="server" style="position: absolute; top: 15%; left: 40%; width: 400px" visible="false">
    <asp:Panel ID="pnl" runat="server" Height="400px">
        <table border="1" class="table" cellpadding="0" cellspacing="0" style="height:399px" >
        <tr valign="top">
        <td align="center" valign="top" style="font-size:8pt; height:20px" class="tdpCabecalho" > Selecione um Destinatario </td>
        </tr>
            <tr valign="top">
                <td>
                    <telerik:RadGrid ID="RadGrid16" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" GridLines="None" PageSize="200"
                        Skin="Default2006" Width="99%" OnItemCommand="RadGrid16_ItemCommand" AllowPaging="True"
                        AllowSorting="True">
                        <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="IDPRODUTOCLIENTE" EmptyDataText="&amp;nbsp;"
                                    HeaderText="IDPRODUTOCLIENTE" UniqueName="column1" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="CODIGO" HeaderText="Código" UniqueName="column2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCodigo" runat="server" Text='<%#Eval("IDCADASTRO") %>' Font-Size="7pt"
                                            CommandArgument='Fechar' CommandName='<% #Eval("IDCADASTRO") %>'></asp:LinkButton> 
                                         <asp:Label ID="lblNomes" runat="server" Text='<%#Eval("RAZAOSOCIALNOME") %>'  Visible="false" /> 
                                                                                   
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridBoundColumn DataField="CNPJCPF" EmptyDataText="&amp;nbsp;" HeaderText="CNPJ / CPF"
                                    UniqueName="column">
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="RAZAOSOCIALNOME" EmptyDataText="&amp;nbsp;" HeaderText="Nome"
                                    UniqueName="column">
                                </telerik:GridBoundColumn>
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
                </td>
            </tr>
        <tr>
        <td align="right" style="font-size:8pt;height:20px" > <asp:Button ID="btnFecharDiv" runat="server" Text="Fechar [ x ]"  CssClass="button" OnClick="btnFecharDiv_Click" /> </td>
        </tr>
        </table>
        </asp:Panel>
    </div>
<asp:DropDownList ID="cboDestinatario" runat="server" CssClass="cbo" 
                        Visible="false" Width="1">
                    </asp:DropDownList>        
    </asp:Panel>
</asp:Content>


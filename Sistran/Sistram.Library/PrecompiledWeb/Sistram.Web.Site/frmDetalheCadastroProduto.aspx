<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" inherits="frmDetalheCadastroProduto, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server" >

        <table style="width: 100%;">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Detalhe do Produto" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        
            <table id="novatb" class="table2" runat="server" cellpadding="1" cellspacing="0"
                width="100%">
                <tr valign="top">
                    <td class="tdp" width="10%" nowrap="nowrap" rowspan="12" valign="top" style="background-color: #FFFFFF">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="Image1" runat="server" Height="180px" ImageUrl="~/Images/naoDisponivel.jpg" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </td>
                </tr>
                <tr valign="bottom">
                    <td class="tdp" style="font-weight: bold;" width="100">
                        Código:
                    </td>
                    <td class="tdp" width="400">
                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" width="100">
                        &nbsp;
                    </td>
                    <td class="tdp" width="200">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Código Cliente:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblCodCliente" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Saldo:
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblSaldo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Descrição:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        &nbsp;
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="baseline" bgcolor="White">
                    <td nowrap="nowrap" colspan="4" height="5">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Data de Cadastro:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDataCadastro" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Data Limite de Uso:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblDataLimite" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Saldo Minimo:
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblSaldoMinimo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Tipo de Material:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblTipoMaterial" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Ativo:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblAtivo" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Consumo Mensal:
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblConsumoMensal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        Valor Unitário:
                    </td>
                    <td class="tdpR" nowrap="nowrap" valign="middle">
                        <asp:Label ID="lblValorUnitario" runat="server"></asp:Label>
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle" style="font-weight: bold;">
                        &nbsp;
                    </td>
                    <td class="tdp" nowrap="nowrap" valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="baseline">
                    <td class="tdp" colspan="4" nowrap="nowrap" style="font-size: 9pt" valign="middle"
                        bgcolor="White">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cod. Barra" DataField="CODIGODEBARRAS"
                                        UniqueName="column1">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Peso Liq." DataField="PESOLIQUIDO"
                                        UniqueName="column2">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Peso Bruto" UniqueName="column3"
                                        DataField="PESOBRUTO">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Espécie" UniqueName="column4"
                                        DataField="ESPECIE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Uni." DataField="UNIDADEDOCLIENTE"
                                        UniqueName="column5">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Conteúdo" DataField="CONTEUDO"
                                        UniqueName="column6">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Altura" DataField="ALTURA"
                                        UniqueName="column7">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Largura" DataField="LARGURA"
                                        UniqueName="column8">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Comprim." DataField="COMPRIMENTO"
                                        UniqueName="column9">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cubagem" DataField="CUBAGEM"
                                        UniqueName="column10">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cub. Rod." DataField="RODOVIARIO"
                                        UniqueName="column11">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Cub. Aereo" DataField="AEREO"
                                        UniqueName="column">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu EnableTheming="True">
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                            </FilterMenu>
                        </telerik:RadGrid>
                        
                     
                    </td>
                </tr>
                <tr valign="baseline">
                    <td bgcolor="White" class="tdp" colspan="4" nowrap="nowrap" style="font-size: 9pt"
                        valign="middle">
                       
                    </td>
                </tr>
                
                <tr>
                <td colspan="4" align="center">              
                         
                     <table width="60%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblSelecionado" runat="server" Text="Item Selecionado:" 
                                    Font-Size ="8pt" Font-Names="Verdana" Font-Bold="True" Visible="False" />
                                    <asp:HiddenField ID="hdCodigoDivisaoCliente" runat="server" />
                                <asp:HiddenField ID="hdCodigoDivisaoCliente0" runat="server" />
                            </td>
                        </tr>
                        <tr>
                        
                            <td width="50%" >
                               <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand"
                                OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <table border="0">                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblIDClienteDivisao" runat="server" Font-Names="Verdana" Font-Size="7pt"
                                                Text='<% #Eval("IDCLIENTEDIVISAO") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lblNome0" runat="server" Font-Bold="false" Font-Names="Verdana"
                                                Font-Size="7pt" Font-Underline="false" ForeColor="Black" Text='<% #Eval("Nome") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            </td>
                            <td>
                                <table align="center" style="width: 100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnAnterior" runat="server" 
                                                ImageUrl="~/Images/setaEsqerda.png" onclick="btnAnterior_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnPosterior" runat="server" 
                                                ImageUrl="~/Images/SetaDireita.png" onclick="btnPosterior_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" align="right" valign="top">
                                    <asp:ListBox ID="ListBox1" runat="server" CssClass="listbox2" Rows="10" Height="100%"
                                    Width="60%"></asp:ListBox>
                            </td>                            
                        </tr>
                    </table>
                </td>                
                </tr>
            </table>         
                    
</asp:Panel>
 
    
   
</asp:Content>

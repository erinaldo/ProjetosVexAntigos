<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTransfSaldoDivisao.aspx.cs"
    Inherits="frmTransfSaldoDivisao" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content5" ContentPlaceHolderID="cphMain" runat="Server">
<script language="javascript" type="text/javascript">
function SomenteNumero(e)
{    
    var tecla=(window.event)?event.keyCode:e.which;  
  
    
    if((tecla > 47 && tecla < 58)) 
        return true;
    else
    {
    return false;
        /*if (tecla != 8) 
            return false;
        else 
            return true;*/
    }
}




</script>

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Transferência de Saldo Por Divisão"
                        Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        
        <div id="divqtd" runat="server" 
            style="width: 300px; position:absolute; top:1%; left:1%; border:thin solid #666666; background-color: #FFFFFF;" 
            visible="false">
        
            <asp:Panel ID="pnlqtd" runat="server" BackColor="White" BorderStyle="None">
                <table cellpadding="1" cellspacing="1" 
                    
                    
                    style="width: 99%; font-family: Verdana; font-size: 7pt; font-weight: bold;" 
                    border="0">
                    <tr>
                        <td>
                            Origem:
                        </td>
                        <td>
                            <asp:Label ID="lblDivOrigem" runat="server"></asp:Label>
                        </td>
                        <td>
                            Destino:</td>
                        <td>
                            <asp:Label ID="lblDivDestino" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Disponível:</td>
                        <td>
                            <asp:Label ID="lblDivDisponivel" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Transferir:</td>
                        <td>
                            <asp:TextBox ID="txtDivDisponivel" runat="server" CssClass="txtValor"  onkeypress="return SomenteNumero(event)"
                                Width="30px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnDivConfirmar" runat="server" CssClass="button" 
                                onclick="btnDivConfirmar_Click" Text="Confirmar" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnDivCancelar" runat="server" CssClass="button" 
                                 Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        
        </div>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
            
                <table id="novatb" runat="server" cellpadding="1" cellspacing="0" class="table" width="100%">
                    <tr valign="baseline">
                        <td class="tdp" nowrap="nowrap" style="width: 1%" valign="middle">
                            Código:
                        </td>
                        <td class="tdp" nowrap="nowrap" style="width: 5%" valign="middle">
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td class="tdp" nowrap="nowrap" style="width: 5%" valign="middle">
                            Descrição:
                        </td>
                        <td class="tdp" nowrap="nowrap" style="width: 5%" valign="middle">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" Width="200px" ></asp:TextBox>
                        </td>
                        <td align="left" class="tdp" nowrap="nowrap" valign="baseline" width="50%">
                            <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                                OnClick="Button1_Click" Text="Pesquisar" />
                            <asp:Label ID="Label1" runat="server" Text="0" Visible="false" ></asp:Label>
                            
                            <asp:Button ID="btnLimparFiltro" runat="server" Text="Limpar Filtro"  OnClick="btnLimparFiltro_Click" CssClass="button" />
                            
                        </td>
                    </tr>
                    <tr valign="top">
                        <td  nowrap="nowrap" valign="top" colspan="5">
                            <asp:Panel ID="Panel5" runat="server" >
                                <table style="width: 100%; background-color: #FFFFFF;">
                                    <tr>
                                    
                                    
                                        <td  valign="top" width="35%">
                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" 
                                                Visible="False">
                                                <table style="width: 100%; background-color: #FFFFFF;">
                                                    <tr>
                                                        <td style="font-weight: 700">
                                                            <b>Selecione a Divisão de Origem.</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: 700">
                                                            Origem:
                                                            <asp:Label ID="lblOrigemDivisaoClienteSelecionado" runat="server" 
                                                                Visible="False"></asp:Label>
                                                            <asp:Label ID="lblIdEstoqueDivisaoOrigem" runat="server" Visible="False"></asp:Label>
                                                            &nbsp;<asp:Label ID="lblOrigemDivisaoOrigemSelecionatoTexto" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:PlaceHolder ID="PHOrigem" runat="server"></asp:PlaceHolder>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </asp:Panel>
                                        </td>
                                        <td class="tdp" valign="top">
                                            <asp:Panel ID="pnlAjax" runat="server" BackColor="White" 
                                                HorizontalAlign="Center" Visible="False" BorderColor="#CCCCCC" 
                                                BorderWidth="1px">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="1" cellspacing="0" class="table" 
                                                                
                                                                style="border: 1px solid #C0C0C0; font-family: Verdana; font-size: 8pt; background-color: #FFFFFF;">
                                                                <tr>
                                                                    <td bgcolor="White" colspan="2">
                                                                        <asp:ImageButton ID="imgPop" runat="server" BorderColor="#333333" 
                                                                            BorderStyle="Solid" Height="120px" ImageUrl="~/Images/naoDisponivel.jpg" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdp7" 
                                                                        style="text-align: left; font-family: Verdana; font-size: 7pt;">
                                                                        <b>Código:</b></td>
                                                                    <td class="tdp7" style="text-align: left; font-size: 7pt;">
                                                                        <asp:Label ID="lblCodigoProdutoCliente" runat="server" 
                                                                            style="font-family: Verdana"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdp7" nowrap="nowrap" 
                                                                        style="text-align: left; font-family: Verdana; font-size: 7pt;">
                                                                        <b>Código de Barras:</b></td>
                                                                    <td class="tdp" style="text-align: left">
                                                                        <span style="font-family: Verdana; font-size: 7pt">
                                                                        <span style="font-weight: normal">
                                                                        <asp:Label ID="lblMetro0" runat="server" Font-Bold="True" 
                                                                            style="font-family: Verdana; text-align: right"></asp:Label>
                                                                        </span></span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdp7" style="text-align: left; font-size: 7pt;">
                                                                        Metragem:</td>
                                                                    <td class="tdp7" 
                                                                        style="text-align: right; font-size: 7pt; font-weight: normal;">
                                                                        <asp:Label ID="lblMetro" runat="server" 
                                                                            style="font-weight: 700; font-family: Verdana"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            
                                        </td>
                                        <td  valign="top" width="45%">
                                            <asp:Panel ID="pnlDestino" runat="server" ScrollBars="Auto" Visible="False" 
                                                Width="100%">
                                                <table style="width: 100%; background-color: #FFFFFF;">
                                                    <tr>
                                                        <td style="font-weight: 700" width="35%">
                                                            Informe o Valor a Divisão Correspondente.</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: 700">
                                                            Destino:
                                                            <asp:Label ID="lblOrigemDivisaoClienteSelecionado0" runat="server" 
                                                                Visible="False"></asp:Label>
                                                            &nbsp;<asp:Label ID="lblOrigemDivisaoOrigemSelecionatoTexto0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Repeater ID="Repeater1" runat="server" 
                                                                OnItemDataBound="Repeater1_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <table border="0">                                                                    
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblIDClienteDivisao" runat="server" Font-Names="Verdana" Font-Bold="false"
                                                                                 Text='<% #Eval("IDCLIENTEDIVISAO") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNome" runat="server" Font-Names="Verdana"  Font-Bold="false"
                                                                                Text='<% #Eval("Nome") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtValor" runat="server" CssClass="txtValor" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                    <table ID="tblConfirmar" runat="server" 
                                                        style="width: 100%; background-color: #FFFFFF;" visible="False">
                                                        <tr>
                                                            <td style="text-align: right">
                                                                <asp:GridView ID="GridView1" runat="server">
                                                                </asp:GridView>
                                                                <asp:Button ID="Button3" runat="server" CssClass="button" 
                                                                    onclick="btnConfirmarTudo_Click" Text="Confirmar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="uplPopUp" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPopUp" runat="server" Height="540px" Width="505px" 
                            BackColor="#E9E9E9" BorderStyle="Solid" BorderColor="#CCCCCC" 
                            BorderWidth="1px">
                            
                            <table style="width: 100%; background-color: #FFFFFF;" border="0" 
                                cellpadding="1" cellspacing="0">
                                <tr>
                                    <td align="center" 
                                        
                                        style="font-weight: bold; font-size: 8pt; font-family: Verdana; background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                        Selecione o Produto</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btmesconde" runat="server" BorderColor="White" 
                                            BorderStyle="None" ForeColor="White" Height="1px" Width="1px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Panel ID="pnlT" runat="server" ScrollBars="Auto" Height="500px" 
                                            Width="500px" style="text-align: left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <telerik:RadGrid ID="RadGrid16" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
                                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" GridLines="None" OnSortCommand="RadGrid1_SortCommand"
                                                        PageSize="200" Skin="Default2006" Width="99%" 
                                                        onitemcommand="RadGrid16_ItemCommand">
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
                                                                        <asp:LinkButton ID="lnkCodigo" runat="server"  Text='<%#Eval("Codigo") %>' Font-Size="7pt"
                                                                            CommandArgument='Fechar' CommandName='<% #Eval("IDprodutoCliente") %>'></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDescricao" runat="server" CssClass="link" Text='<%#Eval("Descricao") %>'
                                                                            CommandArgument='Fechar' CommandName='<% #Eval("IDprodutoCliente") %>' Visible="false"></asp:LinkButton>
                                                                            
                                                                            
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" HeaderText="Descrição"
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" >
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="button" 
                                            onclick="btnCancelar_Click" Text="Fechar [ x ]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalPopup1_Background"
                            CancelControlID="btnCancelar" DropShadow="False" PopupControlID="pnlPopUp" TargetControlID="btmesconde">
                        </asp:ModalPopupExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Panel> </asp:Content> 
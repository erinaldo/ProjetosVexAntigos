<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRptPedidosPorProduto.aspx.cs"
    Inherits="frmRptPedidosPorProduto" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <script language="javascript" type="text/javascript">

 function limpa_string(S){
     var Digitos = "0123456789,"; //Voc� escreve aqui o caract�res permitidos
     var temp = ""; //Essa variavel vai ser resultante da compara��o
     var digito = ""; //Essa variavel vai servir de auxilio para a compara��o
        //Aqui vai ser loop de compara��o 
        for (var i=0; i<S.length; i++)
         {
             //'digito' recebe o caracter da posi��o 'i' da variavel 'S'
              digito = S.charAt(i);

            //Compara se o caracter da variavel 'digito' t�m na variavel 'Digito'
             if (Digitos.indexOf(digito)>=0){temp=temp+digito;}
         }

     //Retorna o resultado da compara��o  
     return temp;
  }

</script>
    <div id="dvPesquisa" runat="server" style="position: absolute; top: 15%; left: 40%; width: 400px" visible="false">
    <asp:Panel ID="pnl" runat="server" Height="400px">
        <table border="1" class="table" cellpadding="0" cellspacing="0" style="height:399px" >
        <tr valign="top">
        <td align="center" valign="top" style="font-size:8pt; height:20px" class="tdpCabecalho" > Selecione um Produto </td>
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
                                <telerik:GridTemplateColumn DataField="CODIGO" HeaderText="C�digo" UniqueName="column2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCodigo" runat="server" Text='<%#Eval("Codigo") %>' Font-Size="7pt"
                                            CommandArgument='Fechar' CommandName='<% #Eval("IDprodutoCliente") %>'></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDescricao" runat="server" CssClass="link" Text='<%#Eval("Descricao") %>'
                                            CommandArgument='Fechar' CommandName='<% #Eval("IDprodutoCliente") %>' Visible="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" HeaderText="Descri��o"
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
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Relat�rio de Pedido por Produto" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                    <asp:Label ID="lblIdProdutoCliente" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    C�digo:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Descri��o:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" Width="300px" Wrap="False"></asp:TextBox>
                </td>
                <td class="tdpR" nowrap="nowrap" width="60%">
                <asp:Button ID="btnImprimir" runat="server" Text ="Imprimir" Visible="false" 
                        CssClass="button" onclick="btnImprimir_Click" />
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" OnClick="btnLimpar_Click" Text="Limpar Filtro" />
                </td>
            </tr>
        </table>
        
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel4" runat="server">
                    <table style="width: 100%" border="0">
                        <tr>
                            <td style="text-align: right" valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="rptGrid" runat="server" onitemcommand="rptGrid_ItemCommand" 
                                                OnItemDataBound="rptGrid_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" class="table" 
                                                        style="font-family:Verdana;" width="99%">
                                                        <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');">
                                                            <td class="tdpCabecalho" style="font-size:7pt; width:1%" align="right">
                                                               Pedido&nbsp;
                                                            </td>
                                                            <td class="tdpCabecalho" style="font-size:7pt; width:10%" align="left">
                                                               C�digo
                                                            </td>
                                                            <td class="tdpCabecalho" style="font-size:7pt" align="left">
                                                                Descri��o</td>
                                                            <td class="tdpCabecalho" style="font-size:7pt; width:2%" align="right">
                                                                Quantidade&nbsp;
                                                            </td>
                                                            <td class="tdpCabecalho" style="font-size:7pt; width:10%; text-align:center" nowrap=nowrap>
                                                                Data da Movimenta��o&nbsp;
                                                            </td>
                                                            
                                                            <td align="right" class="tdpCabecalho" style="font-size:7pt; text-align:left" >
                                                                USU�RIO SOLICITANTE &nbsp;</td>
                                                        
                                                            
                                                            <td align="right" class="tdpCabecalho" style="font-size:7pt; text-align:left" >
                                                                Destinat�rio &nbsp;</td>
                                                        </tr>
                                                   
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                <tr>                                                
                                                    <td class="tdpR" align="left"><asp:Label ID="lblped" runat="server" Text='<% #Eval("Numero") %>' /> &nbsp;</td>
                                                    <td class="tdp" align="left"><asp:Label ID="Label1" runat="server" Text='<% #Eval("Codigo") %>' /> </td>
                                                    <td class="tdp" align="left"><asp:Label ID="Label2" runat="server" Text='<% #Eval("Descricao") %>' />&nbsp; </td>
                                                    <td class="tdpR" align="right"><asp:Label ID="Label3" runat="server" Text='<% #Eval("Quantidade") %>' /> </td>
                                                    <td class="tdp" align="center" style="text-align:center"><asp:Label ID="Label4" runat="server" Text='<% #Eval("DATADOMOVIMENTO") %>' /> </td>
                                                    <td class="tdp" align="left"><asp:Label ID="Label6" runat="server" Text='<% #Eval("NOME") %>' /> </td>
                                                    <td class="tdp" align="left"><asp:Label ID="Label5" runat="server" Text='<% #Eval("DESTINATARIO") %>' /> </td>
                                                </tr>                                           
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

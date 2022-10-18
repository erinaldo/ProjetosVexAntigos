<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmRptPedidos, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">

        function limpa_string(S) {
            var Digitos = "0123456789,"; //Voc� escreve aqui o caract�res permitidos
            var temp = ""; //Essa variavel vai ser resultante da compara��o
            var digito = ""; //Essa variavel vai servir de auxilio para a compara��o
            //Aqui vai ser loop de compara��o 
            for (var i = 0; i < S.length; i++) {
                //'digito' recebe o caracter da posi��o 'i' da variavel 'S'
                digito = S.charAt(i);

                //Compara se o caracter da variavel 'digito' t�m na variavel 'Digito'
                if (Digitos.indexOf(digito) >= 0) { temp = temp + digito; }
            }

            //Retorna o resultado da compara��o  
            return temp;
        }

    </script>

    <div id="dvPesquisa" runat="server" style="position: absolute; top: 12%; left: 40%;
        width: 400px" visible="false">
        <asp:Panel ID="pnl" runat="server" Height="400px">
            <table border="1" class="table" cellpadding="0" cellspacing="0" style="height: 399px">
                <tr valign="top">
                    <td align="center" valign="top" style="font-size: 8pt; height: 20px" class="tdpCabecalho">
                        Selecione um Produto
                    </td>
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
                    <td align="right" style="font-size: 8pt; height: 20px">
                        <asp:Button ID="btnFecharDiv" runat="server" Text="Fechar [ x ]" CssClass="button"
                            OnClick="btnFecharDiv_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    
      <div id="DivPesqDestinatario" runat="server" style="position: absolute; top: 12%; left: 40%; width: 400px" visible="false">
    <asp:Panel ID="Panel1" runat="server" Height="400px">
        <table border="1" class="table" cellpadding="0" cellspacing="0" style="height:399px" >
        <tr valign="top">
        <td align="center" valign="top" style="font-size:8pt; height:20px" class="tdpCabecalho" > Selecione um Destinat�rio </td>
        </tr>
            <tr valign="top">
                <td>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" GridLines="None" PageSize="200"
                        Skin="Default2006" Width="99%" OnItemCommand="RadGrid1_ItemCommand" AllowPaging="True"
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
                                        <asp:LinkButton ID="lnkCodigo" runat="server" Text='<%#Eval("IDCADASTRO") %>' Font-Size="7pt"
                                            CommandArgument='Fechar' CommandName='<% #Eval("IDCADASTRO") %>'></asp:LinkButton> 
                                         <asp:Label ID="lblNomes" runat="server" Text='<%#Eval("RAZAOSOCIALNOME") %>'  Visible="false" /> 
                                                                                   
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
        <td align="right" style="font-size:8pt;height:20px" > <asp:Button ID="Button2" runat="server" Text="Fechar [ x ]"  CssClass="button" OnClick="btnFecharDiv2_Click" /> </td>
        </tr>
        </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdIdDestinatario" runat="server" Value="0" />
    
    
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="1" width="100%"
                    border="0">
                    <tr valign="bottom">
                        <td class="tdp" width="15%" nowrap="nowrap">
                            C�digo:
                        </td>
                        <td class="tdp" nowrap="nowrap" width="20%">
                            Descri��o:
                        </td>
                        <td class="tdp" nowrap="nowrap" width="20%">
                            Per�odo:
                        </td>
                        <td class="tdp" nowrap="nowrap" width="20%">
                            Destinat�rio:
                        </td>
                        <td class="tdpR" nowrap="nowrap" width="20%">
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="txt" 
                                ontextchanged="txtCodigo_TextChanged" Width="90%"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            <asp:TextBox ID="txtDescricao" runat="server" AutoPostBack="True" 
                                CssClass="txt" ontextchanged="txtDescricao_TextChanged" Width="90%" 
                                Wrap="False"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            <asp:TextBox ID="txtI" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="txtI" />
                            &nbsp;At�:<asp:TextBox ID="txtF" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                        </td>
                        <td class="tdp" nowrap="nowrap" width="1%">
                            <asp:TextBox ID="txtDest" runat="server" AutoPostBack="True" CssClass="txt" 
                                ontextchanged="txtDest_TextChanged" Width="90%"></asp:TextBox>
                        </td>
                        <td class="tdpR" nowrap="nowrap">
                            <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" 
                                Font-Size="7pt" OnClick="Button1_Click" Text="Pesquisar" />
                            <asp:Button ID="btnImprimir" runat="server" CssClass="button" Text="Imprimir" 
                                Visible="false" />
                            <asp:Button ID="btnLimpar" runat="server" CssClass="button" Font-Names="arial" 
                                Font-Size="7pt" OnClick="btnLimpar_Click" Text="Limpar Filtro" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel4" runat="server">
                    <table style="width: 100%" border="0">
                        <tr>
                            <td style="text-align: right" valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="GridResultado" runat="server" AutoGenerateColumns="False"  
                                                GridLines="None" Skin="Default2006">
                                                <MasterTableView>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="DATA" EmptyDataText="&amp;nbsp;" 
                                                            HeaderText="DATA" UniqueName="column1" DataFormatString="{0:d}">
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                                                Wrap="True" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                                                Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="CODIGO" EmptyDataText="&amp;nbsp;" 
                                                            HeaderText="C�DIGO" UniqueName="column2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" >
                                                            
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="Descricao" EmptyDataText="&amp;nbsp;" 
                                                            HeaderText="DESCRI��O" UniqueName="column3" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" >
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="QTD" EmptyDataText="&amp;nbsp;" 
                                                            HeaderText="QUANTIDADE" UniqueName="column4">
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                                                Wrap="True" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                                                Wrap="True" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="CNPJCPF_DESTINATARIO" 
                                                            EmptyDataText="&amp;nbsp;" HeaderText="CNPJ DESTINAT�RIO" UniqueName="column5" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" >
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DESTINATARIO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" 
                                                            HeaderText="NOME DESTINAT�RIO" UniqueName="column">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="True">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                            </telerik:RadGrid>
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

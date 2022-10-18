<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmListaArquivoTransp, App_Web_p3uplnwq" enabletheming="true" theme="Adm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function exibir() {
            obj = document.getElementById('dvArquivo');
            obj.style.display = 'block';
        }
        
        function esconder() {
            obj = document.getElementById('dvArquivo');
            obj.style.display = 'none';
        }
     
    </script>

    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                height: 25px">
                <asp:Label ID="lblTitulo" runat="server" Text="Arquivos" Font-Bold="True" Font-Size="14px"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
        <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 20px;
            font-size: 8pt; font-family: Verdana;">
            <td class="tdpCabecalho" style="text-align: center" width="50%">
                <b>recebidos </b>
            </td>
            <td class="tdpCabecalho" style="text-align: center">
                <b>enviados </b>
            </td>
        </tr>
        <tr valign="middle">
            <td>
                &nbsp;
            </td>
            <td style="text-align: right; vertical-align: middle">
                <div class="button" style="height: 18px; width: 90px; text-align: center; vertical-align: middle;
                    cursor: hand;" onclick="exibir();">
                    <table>
                        <tr valign="middle">
                            <td>
                                Novo Arquivo
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <telerik:RadGrid ID="grdRecebidos" runat="server" GridLines="None" 
                    OnItemDataBound="grdRecebidos_ItemDataBound" 
                    onitemcommand="grdRecebidos_ItemCommand">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="IdEdiTrocaDeArquivo" EmptyDataText="&amp;nbsp;"
                                HeaderText="Código" UniqueName="column1">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EntradaData" EmptyDataText="&amp;nbsp;" HeaderText="Data"
                                UniqueName="column2" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoDeArquivo" EmptyDataText="&amp;nbsp;" HeaderText="Tipo"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Arquivo">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkArquivo" runat="server" CssClass="link" Text='<% #Eval("NomeDoArquivo") %>'
                                        ToolTip="Clique aqui para salvar o arquivo" CommandName='<% #Eval("IdEdiTrocaDeArquivo") %>' CommandArgument="Baixar" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                                <ItemTemplate>
                                    <table style="width: 1%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnBaixarArquivo" runat="server" CommandArgument="Baixar" CommandName='<% #Eval("IdEdiTrocaDeArquivo") %>'
                                                    ImageUrl="~/Images/lupa.gif" ToolTip="Baixar Arquivo" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
            <td valign="top">
                <telerik:RadGrid ID="RadGrid2" runat="server" GridLines="None" OnItemCommand="RadGrid2_ItemCommand"
                    OnItemDataBound="RadGrid2_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="IdEdiTrocaDeArquivo" EmptyDataText="&amp;nbsp;"
                                HeaderText="Código" UniqueName="column1">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EntradaData" EmptyDataText="&amp;nbsp;" HeaderText="Data"
                                UniqueName="column2" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoDeArquivo" EmptyDataText="&amp;nbsp;" HeaderText="Tipo"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Arquivo">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkArquivo" runat="server" CssClass="link" Text='<% #Eval("NomeDoArquivo") %>'
                                        ToolTip="Clique aqui para salvar o arquivo"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                                <ItemTemplate>
                                    <table style="width: 1%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnBaixarArquivo" runat="server" CommandArgument="Baixar" CommandName='<% #Eval("IdEdiTrocaDeArquivo") %>'
                                                    ImageUrl="~/Images/lupa.gif" ToolTip="Baixar Arquivo" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="pnlup" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfirma" />
        </Triggers>
        <ContentTemplate>
            <div id="dvArquivo" style="position: absolute; border: solid:1px:silver; display: none;
                top: 35%; left: 40%; background-color: Silver">
                <asp:Panel ID="pnldiv" runat="server" Style="width: 400; border: solid 1px black">
                    <table border="1" cellpadding="2" cellspacing="2" width="400">
                        <tr>
                            <td colspan="2" style="text-align: center; font-size: 9pt; font-weight: 700;">
                                Escolha o Arquivo e Clique em Confirmar
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" style="width: 1%">
                                Tipo de Arquivo:
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipo" runat="server" CssClass="cbo" Height="17px">
                                    <asp:ListItem Text="NOTA FISCAL"></asp:ListItem>
                                    <asp:ListItem Text="CONHECIMENTO"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" style="width: 1%">
                                Selecione o Arquivo:
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txt" Width="99%" Height="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnConfirma" runat="server" Text="Confirmar" CssClass="button" OnClick="Button_Confirmar" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClientClick="esconder();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

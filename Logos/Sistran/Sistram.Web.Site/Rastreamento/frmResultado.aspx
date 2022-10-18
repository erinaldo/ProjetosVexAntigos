<%@ Page Title="" Language="C#" MasterPageFile="~/Rastreamento/Rastreamento.master"
    AutoEventWireup="true" CodeFile="frmResultado.aspx.cs" Inherits="Rastreamento_frmResultado" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Rastreamento.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .rowTable
        {
            color: Red;
        }
        .style2
        {
            width: 100%;
            font-size: 9px;
        }
        .style4
        {
            font-size: 9pt;
            font-weight: bold;
        }
        .style5
        {
            font-family: Arial;
            font-size: 9pt;
            font-weight: bold;
            margin: 0;
            text-align: left;
        }
        .style7
        {
            font-size: 9px;
        }
        .style8
        {
            font-size: 9px;
            font-weight: bold;
            background-color: White;
        }
        .style9
        {
            font-size: 9px;
            font-weight: bold;
        }
        .style11
        {
            text-align: right;
        }
        .style12
        {
            text-align: right;
            background-color: White;
        }
        .style13
        {
            width: 100%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Up" runat="server">
        <ContentTemplate>
            <br />
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmp" OnTabClick="RadTabStrip1_TabClick"
                SelectedIndex="0" Skin="Outlook" Width="100%">
                <Tabs>
                    <telerik:RadTab runat="server" PageViewID="rpvNotaFiscal" Text="Documentos" 
                        Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" PageViewID="rpvItens" Text="Dados da Remessa">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmp" runat="server" BackColor="White" SelectedIndex="4"
                Width="100%">
                <telerik:RadPageView ID="rpvStatusGeral" runat="server" Width="100%">
                    <table class="tablePgInteira">
                        <tr>
                            <td>
                                <div class="titulo">
                                    ITENS ENCONTRADOS</div>
                            </td>
                        </tr>
                        <tr>
                            <td height="2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#8B7355" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="1"
                                    Font-Names="Arial" Font-Size="8px" ForeColor="Black" GridLines="Vertical" OnRowCommand="GridView1_RowCommand1"
                                    Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                    <RowStyle BorderColor="#8B7355" BorderStyle="Solid" BorderWidth="1px" CssClass="link"
                                        Font-Bold="False" Font-Size="7px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NÚMERO">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox1" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("NOTAFISCAL") %>'>
                                                             



                                                </asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label1" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("NOTAFISCAL") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TIPO">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox3" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("TIPODEDOCUMENTO") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label31" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("TIPODEDOCUMENTO") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REMETENTE">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox3" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("REMETENTE") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label35" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("REMETENTE") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESTINATÁRIO">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox2" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Text='<%# Bind("DESTINATARIO") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label2" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("DESTINATARIO") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REMESSA">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox4" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("REMESSA") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label4" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("REMESSA") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DATA DA REMESSA">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox5" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("DATAREMESSA") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label5" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("DATAREMESSA", "{0:dd/MM/yyyy}") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PEDIDO">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox6" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("PEDIDO") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label6" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("PEDIDO") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STATUS">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="TextBox7" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("STATUS") %>'></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Label7" runat="server" CommandArgument='<%# Bind("REMESSA") %>' CommandName='<%# Bind("IDDOCUMENTO") %>'
                                                    CssClass="link" Font-Underline="False" Text='<%# Bind("STATUS") %>'></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#EEC591" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10px" ForeColor="Black" />
                                    <AlternatingRowStyle BackColor="#FFE7BA" BorderColor="#8B7355" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="7px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvListaEmp" runat="server" Style="text-align: left" Width="100%">
                    <table class="tablePgInteira">
                        <tr>
                            <td>
                                <div class="titulo">
                                    DADOS DA REMESSA
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table bgcolor="#F7E6C3" border="0" cellpadding="2" cellspacing="2" frame="box" align="center"
                                    width="75%">
                                    <tr>
                                        <td class="rowZebradoCor" nowrap="nowrap" style="text-align: left; font-weight: 700;
                                            font-size: 9pt;" width="10%">
                                            <span>Número N.F./Pedido: </span>
                                        </td>
                                        <td class="rowZebradoCor" style="text-align: left" width="40%">
                                            <asp:Label ID="lblNumero" runat="server" CssClass="style4"></asp:Label>
                                        </td>
                                        <td class="rowZebradoCor" nowrap="nowrap" style="text-align: left" width="10%">
                                            <span class="style5">Número Remessa: </span>
                                        </td>
                                        <td class="rowZebradoCor" style="text-align: left" width="40%">
                                            <asp:Label ID="lblNumeroRemessa" runat="server" CssClass="style4"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                <table class="style2">
                                    <tr>
                                        <td width="50%">
                                            <div class="titulo">
                                                Remetente
                                            </div>
                                        </td>
                                        <td width="50%">
                                            <div class="titulo">
                                                <asp:Label ID="lblRemessa" runat="server" Style="text-align: center" 
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblIdDocumento" runat="server" Style="text-align: center" 
                                                    Visible="False"></asp:Label>
                                                Destinatário</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="45%">
                                            <asp:Panel ID="Panel1" runat="server" BorderColor="#FF6600" BorderStyle="Solid" BorderWidth="1px">
                                                <table border="0" cellpadding="2" cellspacing="1" class="tablePgInteira" bgcolor="White">
                                                    <tr>
                                                        <td class="style9" nowrap="nowrap" width="1%" bgcolor="White">
                                                            Razão Social:
                                                        </td>
                                                        <td bgcolor="White">
                                                            <asp:Label ID="lblRazoRemtente" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8" bgcolor="White">
                                                            Endereço:
                                                        </td>
                                                        <td class="rowZebrado" bgcolor="White">
                                                            <asp:Label ID="lblEnderecoRemtente" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9" bgcolor="White">
                                                            Complemento:
                                                        </td>
                                                        <td bgcolor="White">
                                                            <asp:Label ID="lblComplementoRemtente" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8" bgcolor="White">
                                                            Cidade:
                                                        </td>
                                                        <td class="rowZebrado" bgcolor="White">
                                                            <asp:Label ID="lblCidadeRemtente" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td width="50%">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="#FF6600" BorderStyle="Solid" BorderWidth="1px">
                                                <table border="0" cellpadding="2" cellspacing="1" class="tablePgInteira">
                                                    <tr>
                                                        <td class="style9" nowrap="nowrap" width="1%">
                                                            Razão Social:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRazoRemtente0" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8">
                                                            Endereço:
                                                        </td>
                                                        <td class="rowZebrado">
                                                            <asp:Label ID="lblEnderecoRemtente0" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style9">
                                                            Complemento:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblComplementoRemtente0" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8">
                                                            Cidade:
                                                        </td>
                                                        <td class="rowZebrado">
                                                            <asp:Label ID="lblCidadeRemtente0" runat="server" CssClass="style7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 90%" width="45%">
                                            <div class="titulo">
                                                OUTRAS INFORMAÇÕES</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 90%" width="45%">
                                            <table class="style2">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel3" runat="server" BorderColor="#FF6600" BorderStyle="Solid" BorderWidth="1px"
                                                            Height="100px">
                                                            <table border="0" cellpadding="2" cellspacing="1" class="tablePgInteira">
                                                                <tr>
                                                                    <td class="style9" nowrap="nowrap" width="1%">
                                                                        Data de Emissão:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDataEmissao" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        Data de Entrada:
                                                                    </td>
                                                                    <td class="rowZebrado">
                                                                        <asp:Label ID="lblDataEntrada" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style9" nowrap="nowrap">
                                                                        Data do Movimento:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDataMovimento" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        Data Planejada:
                                                                    </td>
                                                                    <td class="rowZebrado">
                                                                        <asp:Label ID="lblDataPlanejada" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style9" nowrap="nowrap">
                                                                        Data de Entrega:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDataEntrega" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="50%">
                                                        <asp:Panel ID="Panel4" runat="server" BorderColor="#FF6600" BorderStyle="Solid" BorderWidth="1px"
                                                            Height="100px">
                                                            <table border="0" cellpadding="2" cellspacing="1" class="tablePgInteira">
                                                                <tr>
                                                                    <td class="style7" nowrap="nowrap" width="1%">
                                                                        <b>Peso Bruto:</b>
                                                                    </td>
                                                                    <td class="style11" width="100">
                                                                        <asp:Label ID="lblPesoBruto" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                    <td class="style11" width="30%">
                                                                        &#160;&#160;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        Peso Líquido:
                                                                    </td>
                                                                    <td class="style12" width="100">
                                                                        <asp:Label ID="lblPesoLiquido" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                    <td class="rowZebrado">
                                                                        &#160;&#160;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style7" nowrap="nowrap">
                                                                        <b>Volumes:</b>
                                                                    </td>
                                                                    <td class="style11" width="100">
                                                                        <asp:Label ID="lblVolumes" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8" nowrap="nowrap">
                                                                        Metragem Cúbica:
                                                                    </td>
                                                                    <td class="style12" width="100">
                                                                        <asp:Label ID="lblMetragemCubica" runat="server" CssClass="style7"></asp:Label>
                                                                    </td>
                                                                    <td class="rowZebrado">
                                                                        &#160;&#160;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 90%" width="45%">
                                            <div class="titulo">
                                                OCORRÊNCIAS / IMAGENS</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="45%" valign="top">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#8B7355" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="1"
                                                Font-Names="Arial" Font-Size="9px" ForeColor="Black" GridLines="Vertical" OnRowCommand="GridView1_RowCommand1"
                                                OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                                <RowStyle BorderColor="#8B7355" BorderStyle="Solid" BorderWidth="1px" CssClass="link"
                                                    Font-Bold="False" Font-Size="9px" />
                                                <Columns>
                                                    <asp:BoundField DataField="DATAOCORRENCIA" HeaderText="DATA / HORA">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DESCOCO" HeaderText="HISTÓRICO">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMEFILIAL" HeaderText="FILIAL">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#EEC591" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="10px" ForeColor="Black" />
                                                <AlternatingRowStyle BackColor="#FFE7BA" BorderColor="#8B7355" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9px" />
                                            </asp:GridView>
                                        </td>
                                        <td width="50%" valign="top">
                                            <table class="style13" width="45%">
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:DataList ID="DataList1" runat="server" 
                                                            onitemcommand="DataList1_ItemCommand" onitemdatabound="DataList1_ItemDataBound" 
                                                            RepeatColumns="1">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="100px" 
                                                                    ImageUrl="~/Images/naoDisponivel.jpg" />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 95%" valign="top" width="45%">
                                            <div class="titulo">
                                                </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="45%" colspan='2' align="center">
                                            <asp:Panel ID="pnlCorreios" runat="server" BorderColor="#CCCCCC" 
                                                BorderWidth="1px" Width="605px">
                                              <%--  <iframe id="I1" runat="server" frameborder="0" height="500" name="I1" src="Correios/sro_remoto.htm"
                                                    width="600"></iframe>--%>
                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>   
                                                    
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            <br />

            <div id="dvFoto" runat="server" visible="false" style="border: 1px solid silver; background-color:White; position:absolute; top:15%; left:30%" >
                <table class="style13" width="1%">
                    <tr>
                        <td style="text-align: center">
                            <asp:Image ID="Image2" runat="server" Height="520px" AlternateText="Clique para ampliar" ToolTip="Clique para ampliar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="Button1" runat="server" CssClass="button" 
                                onclick="Button1_Click" Text="Fechar" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmlocazcliente, App_Web_p3uplnwq" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="uplGeral" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </Triggers>
        <ContentTemplate>
            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                        height: 25px">
                        <asp:Label ID="lblTitulo" runat="server" Text="Desempenho de Entrega  Por Cidade"
                            Font-Bold="True" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="width: 100%; height: 100%;">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="2" cellspacing="2">
                                <tr style="height: 10px">
                                    <td nowrap="nowrap" style="font-family: arial; font-size: 8pt; font-weight: bold"
                                        width="100" rowspan="4" valign="top">
                                        <asp:ListBox ID="lstVeiculos" runat="server" CssClass="listbox" AutoPostBack="True"
                                            OnSelectedIndexChanged="lstVeiculos_SelectedIndexChanged" Font-Names="Arial"
                                            Font-Size="8pt" Height="400px" Width="100%"></asp:ListBox>
                                        <br />
                                        <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                        <br />
                                        <table class="grid">
                                            <tr>
                                                <td>
                                                    Atualizado as:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAtualizadoEm" runat="server" Style="color: #000066"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="table">
                                            <tr>
                                                <td class="tdp">
                                                    Veículos:
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblQtdVeiculos" runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdp">
                                                    Serviços:
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblQtdServicos" runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdp">
                                                    Pendentes:
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblQtdPendentes" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdp">
                                                    Entregues:
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblQtdEntregues" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdp">
                                                    Retorno:
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblQtdRetorno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="tdpCabecalhoMenor">
                                                <td class="tdp">
                                                    <strong>%</strong>
                                                </td>
                                                <td class="tdpR">
                                                    <asp:Label ID="lblPerEntregues" runat="server" Style="font-weight: 700"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap" style="font-family: arial; font-size: 10pt; font-weight: bold"
                                        width="5" rowspan="4">
                                        &nbsp;
                                    </td>
                                    <td width="99%" height="10" valign="top">
                                        <table class="table">
                                            <tr>
                                                <td class="tdp">
                                                    <asp:Label ID="lblTipoMapa" runat="server" Text="Mapa:" Visible="False" Font-Bold="True"
                                                        Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                                </td>
                                                <td width="1%" class="tdp" nowrap="nowrap">
                                                    Ordernar Por:
                                                </td>
                                                <td width="1%" class="tdp">
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="cbo"
                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="Numero">Documento</asp:ListItem>
                                                        <asp:ListItem Value="PLACA">Placa</asp:ListItem>
                                                        <asp:ListItem Value="Destinatario">Destinatário</asp:ListItem>
                                                        <asp:ListItem Value="nome">Ocorrência</asp:ListItem>
                                                        <asp:ListItem Value="DataHora">Data / Hora</asp:ListItem>
                                                        <asp:ListItem Value="CEP">Por Rota</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td nowrap="nowrap" class="tdp" width="1%">
                                                    &nbsp;
                                                    <asp:CheckBox ID="chkVerPontos" runat="server" Visible="false" Text="Ver pontos de passagem a partir: "
                                                        OnCheckedChanged="chkVerPontos_CheckedChanged" CssClass="checkbox" />
                                                    <asp:TextBox ID="txtDataHora" runat="server" CssClass="txt" Visible="False" Width="100px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Button ID="btnMultiplosPontos" runat="server" CssClass="button" OnClick="btnMultiplosPontos_Click"
                                                        Text="Atualizar" Visible="False" />
                                                </td>
                                                <td class="tdpR" nowrap="nowrap" style="width: 0%" width="1%">
                                                    Atualizar em:
                                                    <asp:TextBox ID="txtAtualizar" runat="server" AutoPostBack="True" CssClass="txt"
                                                        OnTextChanged="txtAtualizar_TextChanged" Width="40px">2</asp:TextBox>
                                                    &nbsp;min.
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlPxE" runat="server" Visible="False" BorderColor="#CCCCCC" BorderWidth="1px">
                                            <table class="table" border="1" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td nowrap="nowrap" width="1%" class="tdp">
                                                        <span style="font-weight: normal">Nota Fiscal: </span>
                                                    </td>
                                                    <td class="tdp">
                                                        <asp:Label ID="lblPxENota" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" class="tdp">
                                                        Destinatário:
                                                    </td>
                                                    <td class="tdp">
                                                        <asp:Label ID="lblPxEDestinatario" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" class="tdp">
                                                        Endereço de Destino:
                                                    </td>
                                                    <td class="tdp">
                                                        <asp:Label ID="lblPxEEndDestino" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="Panel4" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                            Height="250px" ScrollBars="Vertical">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                Style="text-align: center" OnRowCommand="GridView1_RowCommand" Visible="False"
                                                CssClass="tableVerdana" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                                                CellPadding="1" CellSpacing="1" PageSize="50" AllowSorting="True" EnableSortingAndPagingCallbacks="True"
                                                OnSorting="GridView1_Sorting2">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="DT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridDT" runat="server" Text='<%# Bind("Ndt") %>' Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PLACA" HeaderText="PLACA">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NUMERO" HeaderText="DOCUMENTO">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Destinatario" HeaderText="Destinatário">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Endereço">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPosicaoVEntrega" runat="server" CommandArgument="pxe" CommandName='<% # Eval("IDDOCUMENTO") %>'
                                                                CssClass="link" Text='<% # Eval("ENDERECO") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NOME" HeaderText="Ocorrência / Entrega">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="datahora" HeaderText="Dt. Ocorrência / Entrega">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Canhoto
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbkFoto" runat="server" CommandArgument="VerFoto" CommandName='<% # Eval("IDDOCUMENTO") %>'
                                                                CssClass="link" Font-Bold="True" Text='<%# Bind("FOTO") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ponto de Entrega">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPosicaoEntrega" runat="server" CommandArgument="VerRota" CommandName='<% # Eval("IDDOCUMENTO") %>'
                                                                CssClass="link"><b>VER</b></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="tdpCabecalho" BackColor="#CCCCCC" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <cc1:GMap ID="GMap1" runat="server" enableGoogleBar="True" GZoom="14" Height="100%"
                                            mapType="Normal" Width="100%" CommercialKey="hZ5MC9FZhx8=" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="3">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvFoto" runat="server" style="border: thin solid #666666; position: absolute;
                left: 30%; top: 20%; background-color: White; text-align: center; table-layout: auto;"
                visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 25px">
                        <td width="100%" style="background-color: #808080; font-family: Verdana; font-size: 9px;">
                            <b style="font-family: Verdana">Imagem da Ocorrência / Entrega</b>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td width="100%" style="background-color: #808080; font-family: Verdana; font-size: 9px;">
                            <asp:DataList ID="lstFoto" runat="server" RepeatDirection="Horizontal" OnItemDataBound="lstFoto_ItemDataBound"
                                OnItemCommand="lstFoto_ItemCommand1">
                                <ItemTemplate>
                                    <table class="grid">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblData" runat="server" Font-Size="7pt" ForeColor="White" Text="Fotos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnAmpliarImagem" runat="server" Height="70px" CommandArgument='<% # Bind("IDDOCUMENTOOCORRENCIAARQUIVO") %>'
                                                    CommandName="Ampliar" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="imgFotoGrande" runat="server" Height="350px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="btnFecharImagem" runat="server" CssClass="button" OnClick="btnFecharImagem_Click"
                                Text="FECHAR" /><br />
                            .
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

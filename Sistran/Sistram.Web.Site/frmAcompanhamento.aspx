<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAcompanhamento.aspx.cs"
    Inherits="frmAcompanhamento" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<%--<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<style type="text/css">
.dvtotal
{
    width: 99.8%;
margin: 0 auto;
margin-top: 2px;
}
}
</style>


    <asp:Timer ID="Timer1" runat="server" Interval="50000" OnTick="Timer1_Tick">
    </asp:Timer>
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnPesquisar">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" />
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
            </Triggers>
            <ContentTemplate>
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                            <asp:Label ID="lblTitulo" runat="server" Text="Consultar Entregas" Font-Bold="true"
                                Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                </table>



                <div id="dvTotal" class="dvtotal">
                    <table class="table" cellpadding="1" cellspacing="0">
                        <tr valign="top">
                            <td class="tdp" rowspan="2" style="width: 1%">
                                Filial:
                            </td>
                            <td class="tdp" rowspan="2" valign="top" style="width: 1%">
                                <asp:DropDownList ID="cboFilial" runat="server" CssClass="cbo" Font-Names="Arial"
                                    Font-Size="7pt" Width="150px">
                                </asp:DropDownList>
                                <br />
                                <asp:ListBox ID="lstFilial" runat="server" CssClass="txt" Rows="2" Width="150 
                        " Height="40px"></asp:ListBox>
                            </td>
                            <td class="tdp" style="width: 1%">
                                <asp:Button ID="btnAdicionarFilial" runat="server" BackColor="#990000" BorderStyle="None"
                                    Font-Bold="True" Font-Names="arial" Height="17px" OnClick="btnAdicionarFilial_Click"
                                    Text="+" Width="20px" CssClass="button" />
                            </td>
                            <td class="tdp" style="width: 1%">
                                <asp:Button ID="btnRemoverFilial" runat="server" BackColor="#990000" BorderStyle="None"
                                    Font-Bold="True" Font-Names="arial" Height="17px" OnClick="btnRemoverFilial_Click"
                                    Text="-" Width="20px" CssClass="button" />
                            </td>
                            <td class="tdp" style="width: 1%">
                                <table cellpadding="1" cellspacing="0" class="table" style="width: 100%">
                                    <tr>
                                        <td class="tdp" nowrap="nowrap">
                                            Data de Saída:
                                        </td>
                                        <td class="tdp">
                                            <asp:TextBox ID="txtData" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtData" />
                                            <asp:MaskedEditExtender ID="ssss" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureName="pt-BR"
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txtData" UserDateFormat="DayMonthYear">
                                            </asp:MaskedEditExtender>
                                        </td>
                                        <td class="tdp">
                                            <asp:Button ID="btnPesquisar" runat="server" BackColor="#990000" BorderStyle="None"
                                                CssClass="button" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="17px"
                                                OnClick="btnPesquisar_Click" Text="Pesquisar" Width="55px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp">
                                            Ordenar:
                                        </td>
                                        <td class="tdp" colspan="2">
                                            <b><span style="font-size: 7pt"><span style="font-size: 8pt">
                                                <asp:DropDownList ID="cboOrdem" runat="server" CssClass="cbo" Font-Names="Arial"
                                                    Font-Size="7pt" Width="100%">
                                                    <asp:ListItem Text="Numero DT" Value="Order By Dt.Numero"></asp:ListItem>
                                                    <asp:ListItem Text="Placa" Value="Order By Vei.Placa"></asp:ListItem>
                                                    <asp:ListItem Text="Serviços" Value="Order By TotalDeServicos Desc"></asp:ListItem>
                                                    <asp:ListItem Text="Notas Fiscais" Value="Order By Documentos "></asp:ListItem>
                                                    <asp:ListItem Text="Ocorrências" Value="Order By Ocorrencias Desc"></asp:ListItem>
                                                    <asp:ListItem Text="Realizadas" Value="Order By DocumentosConcluido Desc"></asp:ListItem>
                                                    <asp:ListItem Text="Não Realizadas" Value="Order By DocumentosNaoFinalizado Desc"></asp:ListItem>
                                                    <asp:ListItem Text="Pendentes" Value="Order By Pendentes Desc"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span></span></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdp" bgcolor="White" style="width: 70%">
                                <table id="tbltot" runat="server" border="0" class="table2" cellpadding="1" cellspacing="0"
                                    width="100%">
                                    <tr>
                                        <td class="tdpRVerdana">
                                            Veículos:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblVeiculos" runat="server" Style="font-size: 8pt"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            Notas Fiscais:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <span style="font-size: 8pt">
                                                <asp:Label ID="lblDoc" runat="server" Style="font-size: 8pt"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="tdpRVerdana">
                                            Realizadas:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblRealiz" runat="server" Style="font-size: 8pt"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblRealiz1" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            Pendentes:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblPend" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblPend1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdpRVerdana">
                                            Serviços:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblServ" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            &nbsp;
                                        </td>
                                        <td class="tdpRVerdana">
                                            &nbsp;
                                        </td>
                                        <td class="tdpRVerdana">
                                            Retorno:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblNRealiz" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            <asp:Label ID="lblNRealiz1" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdpRVerdana">
                                            Ocorrências:
                                        </td>
                                        <td class="tdpRVerdana">
                                            <span style="font-size: 7pt">
                                                <asp:Label ID="lblOcorrencia" runat="server" Style="font-size: 8pt"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="tdpRVerdana">
                                            <span style="font-size: 7pt">
                                                <asp:Label ID="lblOcorrencia1" runat="server" Style="font-size: 8pt"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdpRVerdana" colspan="7">
                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" Font-Names="Arial"
                                                Font-Size="6pt" OnCheckedChanged="CheckBox1_CheckedChanged" Style="font-size: 7pt"
                                                Text="Atualização Automática" />
                                        </td>
                                        <td class="tdpRVerdana" colspan="3">
                                            <asp:Label ID="Label1" runat="server" Style="font-size: 7pt"></asp:Label>
                                            &nbsp;<%--<asp:Button ID="btnVerMapa" runat="server" BackColor="#990000" BorderStyle="None"
                                            CssClass="button" Font-Bold="True" Font-Names="Arial" OnClientClick="javascript:window.open('frmLocalizarDTCliente.aspx?opc=Última Posição Do Carro'); return false;"
                                            Font-Size="7pt" Height="17px" Text="Mapa" Width="55px" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    <br />
                    <asp:Panel ID="Panel4" runat="server">
                        <table class="grid" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td nowrap="nowrap" valign="top" width="1%">
                                    <asp:Label ID="lblMapaNota1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                        Text="Última Localização dos Veículos em Rota"></asp:Label>
                                </td>
                                <td style="text-align: left" valign="top" width="99%">
                                    <asp:Label ID="LblListaNf" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                        Text="Notas Fiscais" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left" valign="top" width="1%">
                                    <asp:Label ID="lblMapaNota" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                        Text="Localização NF" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <cc1:GMap ID="GMap1" runat="server" enableGoogleBar="True" GZoom="14" Height="400px"
                                        mapType="Normal" Width="550px" />
                                </td>
                                <td valign="top">
                                    <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" CssClass="listbox"
                                        Font-Names="Arial" Font-Size="8pt" Height="200px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"
                                        Visible="False" Width="99%"></asp:ListBox>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                        Style="text-align: center" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link" Font-Bold="True"
                                                        CommandArgument="ver" CommandName='<% # Eval("IDDOCUMENTO") %>'>Ver</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NUMERO" HeaderText="Nota Fiscal">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SERIE" HeaderText="Série">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOME" HeaderText="Ocorrência / Entrega" />
                                            <asp:BoundField DataField="datahora" HeaderText="Data Ocorrência / Entrega">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Comprovante de Entrega
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbkFoto" runat="server" CssClass="link" Font-Bold="True" CommandArgument="VerFoto"
                                                        CommandName='<% # Eval("IDDOCUMENTO") %>' Text='<%# Bind("FOTO") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td valign="top">
                                    <cc1:GMap ID="GMap2" runat="server" enableGoogleBar="True" GZoom="14" Height="400px"
                                        mapType="Normal" Visible="False" Width="550px" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div id="dvFoto" runat="server" style="position: absolute; left: 30%; top: 20px;
        width: 40%; background-color: White; text-align: center" visible="false">
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
                    <asp:Image ID="imgFotoGrande" runat="server" Height="550px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnFecharImagem" runat="server" CssClass="button" OnClick="btnFecharImagem_Click"
                        Text="FECHAR" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

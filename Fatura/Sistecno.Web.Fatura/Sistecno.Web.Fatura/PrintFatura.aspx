<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="PrintFatura.aspx.cs" Inherits="Sistecno.Web.Fatura.PrintFatura" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:Panel ID="pnl1" runat="server" >
    <table style="width: 100%; font-size:10px" id="tblPrincipal" cellpadding="0" cellspacing="0" >
        <tr>
            <td style="width: 25%">
                <asp:Panel ID="Panel1" runat="server" Font-Size="7pt">
                    <table width="100%" border="1" cellpadding="1" cellspacing="1">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblFilial" runat="server"></asp:Label>
                            </td>
                            <td nowrap="nowrap" width="1%" align="right">
                                <asp:Label ID="lblFilialNumeroFatura" runat="server" Style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblEndereco" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblCep" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblCNPJ" runat="server"></asp:Label>
                                -
                                <asp:Label ID="lblIE" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTelefone" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="right">
                                &nbsp;<asp:Label ID="lblDataDeEmissao" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <br />
                    <table width="100%" border="1" cellpadding="1" cellspacing="1">
                        <tr>
                            <td>
                                <table border="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <table border="1" cellpadding="1" cellspacing="1" width="100%">
                                                        <td align="center">
                                                            FATURA Nº
                                                        </td>
                                                        <td align="center" nowrap="nowrap">
                                                            DUPLICATA - VALOR
                                                        </td>
                                                        <td align="center">
                                                            DUPLICATA - Nº
                                                        </td>
                                                        <td align="center">
                                                            VENCIMENTO
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblFaturaNumero" runat="server" 
                                                                Text='<%  #Eval("NUMERODUPLICATA") %>'></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFaturaValor" runat="server" 
                                                                Text='<%  #Eval("VALORTITULODUP") %>'></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFaturaNumeroDupliCata" runat="server" 
                                                                Text='<%  #Eval("NUMERODUPLICATA") %>'></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFaturaVencimento" runat="server" 
                                                                Text='<%  #Eval("DATADEVENCIMENTO") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Desconto de:
                                            <asp:Label ID="lblDesconto" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp; Sobre
                                            <asp:Label ID="lblValorSobreDesconto" runat="server"></asp:Label>
                                            &nbsp;&nbsp; Até<br />
                                            <br />
                                            Condições Especiais
                                        </td>
                                    </tr>
                                </table>
                                <table border="1" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td nowrap="nowrap" style="height: 19px" width="1%">
                                            <b>NOME DO SACADO:</b>
                                        </td>
                                        <td style="height: 19px" width="75%">
                                            <b>
                                            <asp:Label ID="lblNomeSacado" runat="server"></asp:Label>
                                            </b>
                                        </td>
                                        <td nowrap="nowrap" rowspan="6" valign="top">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="#666666" BorderStyle="Solid" 
                                                BorderWidth="2px" Visible="False" Width="360px">
                                                <table bgcolor="#CCCCCC" cellpadding="1" cellspacing="1" 
                                                    style="border: thin solid #808080">
                                                    <tr>
                                                        <td colspan="2" style="text-align: center; font-size: 14px">
                                                            <strong>PAINEL DE DOWNLOD DE ARQUIVOS</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/IcoBradesco.jpg" 
                                                                Height="30px" />
                                                            <input type="button" onclick="javascript:window.open('GerarBoleto.aspx?idtitulo='); return false;"
                                                                value="Baixar Boleto" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                                font-weight: bold; font-style: normal; font-variant: normal; background-color: #FFFFFF;
                                                                border: thin solid #808080; color: #333333; width: 155px; height: 30px;"  />
                                                        </td>
                                                        <td>
                                                            <input type="button" onclick="javascript:window.open('frmCriarZIP.aspx?idtitulo='); return false;"
                                                                value="Baixar Todos XML`S" style="font-family: Arial, Helvetica, sans-serif;
                                                                font-size: 12px; font-weight: bold; font-style: normal; font-variant: normal;
                                                                background-color: #FFFFFF; border: thin solid #808080; color: #333333; width: 175px; height: 30px;"
                                                                size="100" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="button" onclick="javascript:window.open('frmCriarZIPCompEntrega.aspx'); return false;"
                                                                value="Baixar Comprov. de Entrega" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                                font-weight: bold; font-style: normal; font-variant: normal; background-color: #FFFFFF;
                                                                border: thin solid #808080; color: #333333; width: 175px; height: 30px;" size="100" />
                                                        </td>
                                                        <td>
                                                            <input type="button" onclick="javascript:window.open('frmGerarDocCob.aspx'); return false;"
                                                                value="Baixar DocCob" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                                font-weight: bold; font-style: normal; font-variant: normal; background-color: #FFFFFF;
                                                                border: thin solid #808080; color: #333333; width: 175px; height: 30px;" size="100" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="button" onclick="javascript:window.open('frmGerarConemb.aspx?idtitulo='); return false;"
                                                                value="Baixar Conemb" 
                                                                style="font-family: Arial, Helvetica, sans-serif;
                                                                font-size: 12px; font-weight: bold; font-style: normal; font-variant: normal;
                                                                background-color: #FFFFFF; border: thin solid #808080; color: #333333; width: 175px; height: 30px;" />
                                                        </td>
                                                        <td>
                                                            <input type="button" onclick="javascript:window.open('frmGerarOcoren.aspx?idtitulo='); return false;"
                                                                value="Baixar Ocoren" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;
                                                                font-weight: bold; font-style: normal; font-variant: normal; background-color: #FFFFFF;
                                                                border: thin solid #808080; color: #333333; width: 175px; height: 30px;" size="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" style="height: 19px" width="1%">
                                            ENDEREÇO:
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Label ID="lblSacadoEndereco" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" style="height: 19px" width="1%">
                                            &nbsp;CIDDADE / UF:
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Label ID="lblSacadoCidadeUf" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" style="height: 19px" width="1%">
                                            PRAÇA DE PAGAMENTO:
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Label ID="lblSacadoPracaPagamento" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" style="height: 19px" width="1%">
                                            CNPJ:
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Label ID="lblSacadoCNPJ" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <%-- </table>
                                <table border="1" width="100%">--%>
                                    <tr style="height:30px">
                                        <td nowrap="nowrap" style="width: 4%">
                                            VALOR POR EXTENSO:
                                        </td>
                                        <td bgcolor="#CCCCCC" style="font-weight: 700" width="99%">
                                            <asp:Label ID="lblValorPorExtenso" runat="server" Text="
                        "></asp:Label>
                                            <asp:Label ID="lblValorExt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <br />
                    <asp:Repeater ID="rptNotas" runat="server" OnItemDataBound="rptNotas_ItemDataBound">
                        <HeaderTemplate>
                            <table border="1" cellpadding="1" cellspacing="1" width="100%">
                                <tr style="font-weight: bold; background-color: Silver; text-align: center; font-size:10px">
                                    <td>
                                        CTRC
                                    </td>
                                    <td>
                                        EMISSÃO CTRC
                                    </td>
                                    <td>
                                        NOTA FISCAL
                                    </td>
                                    <td>
                                        VALOR
                                    </td>
                                    <td>
                                        NOME DO ARQUIVO
                                    </td>
                                    <td>
                                        DATA DO ENVIO
                                    </td>
                                    <td>
                                        CHAVE DO DOCUMENTO
                                    </td>
                                    <td>
                                        CODIGO
                                    </td>
                                    <td>
                                        DATA DE ENTREGA
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="font-size:10px">
                                <td style="text-align: right">
                                    <asp:Label ID="lblCtr" runat="server" Text='<% #Eval("CTR") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblCtrcEmissao" runat="server" Text='<% #Eval("EmissaoCtr") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblCtrcNotaFiscal" runat="server" Text='<% #Eval("NotaFiscal") %>'></asp:Label>
                                    <asp:Label ID="lblIdDocumento" runat="server" Text='<% #Eval("IdDocumento") %>' Visible="false" />
                                    <asp:Label ID="lblIdDocumentoOcorrenciaArquivo" runat="server" Text='<% #Eval("COMPROVANTEENTREGA").ToString() %>' Visible="false">
                                    </asp:Label>

                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCtrcFrete" runat="server" Text='<% #Eval("Frete") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCtrcNomeArquivo" runat="server" Text='<% #Eval("NomeDoArquivo") %>'></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:Label ID="lblCtrcDataGeracao" runat="server" Text='<% #Eval("DataGeracaoDoArquivo") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCtrcIdNota" runat="server" Text='<% #Eval("IdNota") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCtrcCodigo" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                </td>
                                <td nowrap="nowrap" style="text-align: center">
                                    <asp:Label ID="lblDataDeEntrega" runat="server" Text='<% #Eval("DataDeEntrega") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:Label ID="lblTotais" runat="server"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <hr /></td>
        </tr>
        <tr>
            <td>
               <center>
                <iframe ID="I1" name="I1" src="GerarBoleto.aspx" 
                    style="border-width:0px; width:700px; height:1000px"></iframe></center>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>

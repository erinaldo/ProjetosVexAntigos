<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmProdutoComFoto.aspx.cs"
    Inherits="frmProdutoComFoto" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pnlteste" runat="server">
    
    
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Produto com Foto" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
            
                <table style="width: 100%" border="0">
                    <tr>
                        <td valign="top" width="1%">
                            <asp:UpdatePanel ID="uplBot" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlMenu" runat="server" BackColor="#EBEBEB" BorderColor="Silver" 
                                        BorderStyle="None" BorderWidth="0px" ScrollBars="Auto">
                                        <asp:PlaceHolder ID="PlaceHolderMenuDivisao" runat="server"></asp:PlaceHolder>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                    <asp:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" 
                                        Corners="All" Radius="6" TargetControlID="pnlMenu">
                                    </asp:RoundedCornersExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlMensagem" runat="server" BackColor="White" 
                                            HorizontalAlign="Center" Visible="false" Width="100%">
                                            <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" 
                                                Corners="All" Radius="6" TargetControlID="pnlMensagem">
                                            </asp:RoundedCornersExtender>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td height="20">
                                                        <asp:Label ID="lblMensagem" runat="server" BorderStyle="None" CssClass="txt" 
                                                            Font-Bold="True" Font-Names="Verdana"></asp:Label>
                                                    </td>
                                                    <td width="30">
                                                        <asp:Button ID="Button3" runat="server" CssClass="button" Text="Imprimir" 
                                                            Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                            <HeaderTemplate>
                                                <table border="0" style="font-family: Verdana; font-style: normal" width="100%">
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center" rowspan="7" style="width:1px">
                                                        <asp:Image ID="ImageButton2" runat="server" Height="150px" />
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Código:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Eval("Codigo") %>'></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Quantidade:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal; text-align:right">
                                                        <asp:Label ID="Label2" runat="server" 
                                                            Text='<% #Eval("SALDODIVISAODISPONIVEL") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Código Cliente:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label3" runat="server" Text='<% #Eval("CODIGODOCLIENTE") %>'></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Unidade Medida:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label4" runat="server" Text='<% #Eval("UNIDADEDEMEDIDA") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Descrição:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label5" runat="server" Text='<% #Eval("DESCRICAO") %>'></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold; ">
                                                        Peso Real:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 50%; background-color: #E5E5E5; font-weight:normal; text-align:right">
                                                        <asp:Label ID="Label6" runat="server" Text='<% #Eval("PESOBRUTO") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Produto Perecível:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label7" runat="server" Text='<% #Eval("PERECIVEL") %>'></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Medida:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="lblMedida" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Sujeito a Validade:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label8" runat="server" Text='<% #Eval("VALIDADE") %>'></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Rodoviário:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal; text-align:right" >
                                                        <asp:Label ID="Label9" runat="server" Text='<% #Eval("RODOVIARIO") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Valor Unitário:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal; text-align:right">
                                                        <asp:Label ID="Label10" runat="server" Text='<% #Eval("VALORUNITARIO") %>' />
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Aéreo:
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal; text-align:right">
                                                        <asp:Label ID="Label11" runat="server" Text='<% #Eval("AERIO") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Conteúdo
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label12" runat="server" Text='<% #Eval("CONTEUDO") %>' />
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color:Silver; font-weight:bold">
                                                        Limite de Uso
                                                    </td>
                                                    <td nowrap="nowrap" 
                                                        style="width: 1%; background-color: #E5E5E5; font-weight:normal">
                                                        <asp:Label ID="Label13" runat="server" Text='<% #Eval("DATALIMITEDEUSO") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <hr />
                                                    </td>
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
                <asp:Label ID="lblMensagem0" runat="server" BorderStyle="None" CssClass="txt" 
                    Font-Bold="True" Font-Names="Verdana" Visible="False"></asp:Label>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

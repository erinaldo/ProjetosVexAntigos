<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmDivisoesCliente, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >  

    <asp:Panel ID="pnlteste1" runat="server" >
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Text="Divisões" Font-Bold="True" 
            Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" runat="server" cellpadding="1" cellspacing="0" class="table">
    
    
    <tr valign="bottom" >
    <td  nowrap="nowrap" width="1%" class="tdp">CNPJ:&nbsp; </td>
        <td  nowrap="nowrap" class="tdp" width="10%">
            &nbsp;<asp:Label ID="lblCnpj" runat="server"></asp:Label>
        </td>
    <td  nowrap="nowrap" width="1%" class="tdp" >Nome:&nbsp; </td>
        <td nowrap="nowrap" class="tdp" width="60%" >
            &nbsp;<asp:Label ID="lblNome" runat="server"></asp:Label>
        </td>
    </tr>
    
        <tr valign="bottom">
            <td colspan="4" nowrap="nowrap" bgcolor="White">
                <asp:Panel ID="pnlContetudo" runat="server" Width="75%">
                    <table ID="tblDetalhes" runat="server" border="0" cellpadding="0" 
                        cellspacing="0" visible="false" width="100%" class="table" bgcolor="White" 
                        style="background-color: #FFFFFF">
                        <tr>
                            <td style="font-family: Verdana; font-size: 8pt">
                                <asp:Label ID="lblNomeAdcItem" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                &nbsp;|
                                <asp:TextBox ID="txtTextSelecionado" runat="server" CssClass="txt" 
                                    Visible="False"></asp:TextBox>
                                <asp:Label ID="lblCodDiviSel" runat="server" Text="" Visible="true"></asp:Label>
                                &nbsp;<asp:Button ID="btnAdicionar" runat="server" CssClass="button" 
                                    OnClick="btnAdicionar_Click" Text="Novo" Visible="False" />
                                &nbsp;<asp:Button ID="btnOK" runat="server" CssClass="button" onclick="btnOK_Click" 
                                    Text="Confirmar" Visible="False" />
                                &nbsp;<asp:Button ID="btnExcluir" runat="server" CssClass="button" 
                                    onclick="btnExcluir_Click" Text="Excluir" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Verdana; font-size: 8pt">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="font-family: vERDAna; font-size: 8pt;">
                                DIVISÕES:</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="Repeater1" runat="server" 
                                    onitemcommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                                    <HeaderTemplate>
                                        <table border="0">
                                   
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIDClienteDivisao" runat="server" Font-Names="Verdana" 
                                                    Font-Size="7pt" Text='<% #Eval("IDCLIENTEDIVISAO") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lblNome0" runat="server" Font-Bold="false" 
                                                    Font-Names="Verdana" Font-Size="7pt" Font-Underline="false" ForeColor="Black" 
                                                    Text='<% #Eval("Nome") %>' />
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
                </asp:Panel>
            </td>
        </tr>
    
    </table>   
    </asp:Panel> 
</asp:Content>

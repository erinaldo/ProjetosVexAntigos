<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="frmPerfisDetalhe.aspx.cs"
    Inherits="frmPerfisDetalhe" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnSalvar" 
        Height="600px">
    <table style="width: 100%;" >
    <tr>
    <td colspan="4" style="background-image:url('Images/skins/primeiro/img/menu_3_2.jpg'); height:20px">
    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="14px"></asp:Label>
    </td>
    </tr>
    </table>
    
    <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" 
            width="100%">
    <tr valign="bottom" >
    <td class="tdp" width="5%" nowrap="nowrap">Perfil:</td>
        <td class="tdp" nowrap="nowrap" width="90%">
            <asp:TextBox ID="txtNf0" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtNf0" ErrorMessage="Informe o nome do perfil">*</asp:RequiredFieldValidator>
            <asp:Button ID="btnSalvar" runat="server" CssClass="button" Font-Names="arial" 
                Font-Size="7pt" OnClick="Button1_Click" Text="Salvar" />
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    
        <tr valign="bottom">
            <td class="tdp" nowrap="nowrap" width="5%" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="White">
                    <tr align="center">
                        <td align="center" style="width: 100%; text-align: left;">
                            <br />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center" style="width: 100%; text-align: left;">
                            Clique sobre as imagens
                            <asp:Image ID="Image8" runat="server"   Height="15px"
                                ImageUrl="~/Images/Botao_ConfirmarVista.ico" />
                            <asp:Image ID="Image9" runat="server" Height="15px" 
                                ImageUrl="~/Images/Botao_ConfirmarVistaDisabled.ico" />
                            &nbsp;para habilidar ou desabilitar acesso.</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        <br />
                            <asp:Repeater ID="rptMenu" runat="server" onitemcommand="rptMenu_ItemCommand" 
                                onitemdatabound="rptMenu_ItemDataBound">
                                <HeaderTemplate>
                                    <table border="0">
                                        <!--<tr>
                                                <td >
                                                    Tipo
                                                </td>
                                                <td >
                                                    Hab.
                                                </td>
                                                <td  >
                                                    Descrição
                                                </td>
                                            </tr>-->
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Image ID="imgTipo" runat="server" 
                                                CommandName='<% #Eval("IDMODULOOPCAO") %>' Height="15px" 
                                                ImageUrl="images/Usuarios_32x32.png" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgHab" runat="server" CommandArgument="edit" 
                                                CommandName='<% #Eval("IDMODULOOPCAO")  %>' Height="15px" 
                                                ImageUrl="images/Botao_ConfirmarVistaDisabled.ico" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTexto" runat="server" Text='<% #Eval("Descricao") %>' />
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
                        <td style="text-align: left">
                            <table style="width: 20%">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image6" runat="server" 
                                            ImageUrl="~/Images/Botao_ConfirmarVista.ico" Height="15px"/>
                                    </td>
                                    <td>
                                        Uso Permitido</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image5" runat="server" Height="15px" 
                                            ImageUrl="~/Images/Usuarios_32x32.png" />
                                    </td>
                                    <td>
                                        Atribuído no Perfil</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image7" runat="server" Height="15px" 
                                            ImageUrl="~/Images/user_32x32.bmp" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            ShowMessageBox="True" ShowSummary="False" />
                                    </td>
                                    <td>
                                        Atribuído no Usuário</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
    </table>
    
    
    
    </asp:Panel>
</asp:Content>

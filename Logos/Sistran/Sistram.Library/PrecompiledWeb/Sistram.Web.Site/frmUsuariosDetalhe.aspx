<%@ page language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="frmUsuariosDetalhe, App_Web_k1oyg1pl" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;
            if ((tecla > 47 && tecla < 58)) return true;
            else {
                if (tecla != 8) return false;
                else return true;
            }
        }
    </script>

    <asp:Panel ID="pnlteste" runat="server" >
        <table style="width: 100%;">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 20px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Usuários" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="2" cellspacing="2" 
            width="100%">
            <tr valign="baseline">
                <td class="tdp" valign="middle" width="1%">
                    CNPJ/CPF:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" width="48%">
                    <asp:TextBox ID="txtCPF" runat="server" AutoPostBack="True" CssClass="txt" MaxLength="18"
                        OnTextChanged="txtCPF_TextChanged" Width="300px"></asp:TextBox>
                    <asp:Label ID="lblIdUsuario" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" width="1%">
                    Login:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" width="48%">
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="txt" MaxLength="20" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin"
                        ErrorMessage="Informe o login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    Nome:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNome"
                        ErrorMessage="Informe o nome" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    E-mail:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" MaxLength="50" Width="300px"
                        OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Informe o e-mail" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="E-mail inválido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    Senha:</td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtSenha" runat="server" CssClass="txt" MaxLength="10" 
                        OnTextChanged="txtCPF_TextChanged" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtSenha" ErrorMessage="Informe a senha">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    Perfil:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:DropDownList ID="cboPerfil" runat="server" AutoPostBack="True" 
                        CssClass="cbo" onselectedindexchanged="cboPerfil_SelectedIndexChanged" 
                        Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    Divisões:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;</td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;</td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;</td>
            </tr>
            <tr valign="baseline" align="center">
                <td bgcolor="White" colspan="4" valign="middle">
                    <table width="100%" border="0">
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblSelecionado" runat="server" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="8pt" Text="Item Selecionado:" Visible="False" />
                            </td>
                            <td rowspan="2" valign="top" width="30%">
                                <table bgcolor="White" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="center">
                                        <td align="center" style="width: 100px; text-align: left;">
                                            &nbsp;</td>
                                        <td align="center" style="width: 100%; text-align: left;">
                                            Clique sobre as imagens
                                            <asp:Image ID="Image8" runat="server" Height="15px" 
                                                ImageUrl="~/Images/Botao_ConfirmarVista.ico" />
                                            <asp:Image ID="Image9" runat="server" Height="15px" 
                                                ImageUrl="~/Images/Botao_ConfirmarVistaDisabled.ico" />
                                            &nbsp;para habilidar ou desabilitar acesso.</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            <br />
                                            <asp:Repeater ID="rptMenu" runat="server" onitemcommand="rptMenu_ItemCommand" 
                                                onitemdatabound="rptMenu_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table border="0">                                                  
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
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            <table style="width: 60%">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image6" runat="server" Height="15px" 
                                                            ImageUrl="~/Images/Botao_ConfirmarVista.ico" />
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
                        <tr>
                            <td width="15%" valign="top">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand"
                                    OnItemDataBound="Repeater1_ItemDataBound">
                                    <HeaderTemplate>
                                        <table border="0">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblIDClienteDivisao" runat="server" Font-Names="Verdana" Font-Size="7pt"
                                                    Text='<% #Eval("IDCLIENTEDIVISAO") %>' Visible="false"></asp:Label><asp:LinkButton
                                                        ID="lblNome0" runat="server" Font-Bold="false" Font-Names="Verdana" Font-Size="7pt"
                                                        Font-Underline="false" ForeColor="Black" Text='<% #Eval("Nome") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                            <td width="1%">
                                <table align="center" style="width: 100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnAnterior" runat="server" ImageUrl="~/Images/setaEsqerda.png"
                                                OnClick="btnAnterior_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnPosterior" runat="server" ImageUrl="~/Images/SetaDireita.png"
                                                OnClick="btnPosterior_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td  valign="top" style="text-align: left" width="15%">
                                <asp:ListBox ID="lstSelecionados" runat="server" Font-Names="Verdana" 
                                    Font-Size="7pt" Rows="20" Width="99%"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="baseline">
                <td bgcolor="White" class="tdpR" colspan="4" valign="middle">
                    <asp:HiddenField ID="hdCodigoDivisaoCliente0" runat="server" />
                    <asp:HiddenField ID="hdCodigoDivisaoCliente" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                    <asp:Button ID="btnSalvar" runat="server" CssClass="button" OnClick="btnSalvar_Click"
                        Text="Salvar" Visible="True" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

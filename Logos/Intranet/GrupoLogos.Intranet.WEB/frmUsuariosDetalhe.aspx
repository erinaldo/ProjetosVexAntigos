<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true"
    CodeFile="frmUsuariosDetalhe.aspx.cs" Inherits="frmUsuariosDetalhe" Theme="Adm"
    EnableTheming="true" %>

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

        function ExibirDiv() {

            //if (document.getElementById('dvEscolherCliente').style.display == 'block')
            // document.getElementById('dvEscolherCliente').style.display = 'none';
            // else
            document.getElementById('dvEscolherCliente').style.display = 'block';
        }

    </script>
    <asp:Panel ID="pnlteste" runat="server">
        <left>
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px; " align="left">
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
                <td class="tdp" nowrap="nowrap" valign="middle" width="40%">
                    <asp:TextBox ID="txtCPF" runat="server" AutoPostBack="True" CssClass="txt" MaxLength="18"
                        OnTextChanged="txtCPF_TextChanged" Width="300px" ReadOnly="True"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/lupa.gif" 
                        ToolTip="Pesquisar" onclick="ImageButton1_Click" 
                        CausesValidation="False" />
                    <asp:Label ID="lblIdUsuario" runat="server" Text="0" Visible="False"></asp:Label>

                  
                    <asp:Label ID="lblIdCadastro" runat="server" Text="0" Visible="False"></asp:Label>

                  
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" width="1%">
                    Nome:
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle" width="48%">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" MaxLength="50" 
                        Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtNome" ErrorMessage="Informe o nome" 
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" colspan="4" valign="middle">
                     <hr /></td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    Login:</td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="txt" MaxLength="20" 
                        Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtLogin" ErrorMessage="Informe o login">*</asp:RequiredFieldValidator>
                </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    Senha:</td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtSenha" runat="server" CssClass="txt" MaxLength="10" 
                        OnTextChanged="txtCPF_TextChanged" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtSenha" ErrorMessage="Informe a senha">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" valign="middle">
                    E-mail: </td>
                <td class="tdp" nowrap="nowrap" valign="middle">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" MaxLength="50" 
                        Width="300px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="E-mail inválido" 
                        SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="Informe o e-mail" 
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
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
                <td class="tdp" colspan="4" valign="middle">
                    <hr /></td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" colspan="2" valign="middle">
                    Clique sobre as imagens
                    <asp:Image ID="Image8" runat="server" Height="15px" 
                        ImageUrl="~/Images/Botao_ConfirmarVista.ico" />
                    <asp:Image ID="Image9" runat="server" Height="15px" 
                        ImageUrl="~/Images/Botao_ConfirmarVistaDisabled.ico" />
                    &nbsp;para habilidar ou desabilitar acesso.</td>
                <td class="tdpR" nowrap="nowrap" valign="middle" colspan="2">
                    <asp:Button ID="btnAbriPesquisarCliente" runat="server" CssClass="button" 
                        onclick="Button2_Click" Text="+ Clientes" />
                </td>
            </tr>
            <tr valign="baseline">
                <td valign="top" colspan="2" align="left" bgcolor="White">
                    <asp:Repeater ID="rptMenu" runat="server" onitemcommand="rptMenu_ItemCommand" 
                        onitemdatabound="rptMenu_ItemDataBound">
                        <HeaderTemplate>
                            <table border="0">                           
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td nowrap="nowrap">
                                    <asp:Image ID="imgTipo" runat="server" 
                                        CommandName='<% #Eval("IDMODULOOPCAO") %>' Height="15px" 
                                        ImageUrl="images/Usuarios_32x32.png" />
                                </td>
                                <td nowrap="nowrap">
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
                <td  nowrap="nowrap" valign="top"  colspan="2" bgcolor="White">
                    <asp:UpdatePanel ID="uplDivi" runat="server">
                        <ContentTemplate>
                            <div ID="dvEscolherCliente" runat="server" visible="false">
                                <table class="table">
                                    <tr align="left" 
                                        style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px;">
                                        <td colspan="3" style="font-family: Verdana; font-size: 9px; font-weight: bold">
                                            Pesquisa de Cliente</td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" nowrap="nowrap" style="width:1%">
                                            Iniciais:
                                        </td>
                                        <td class="tdp" nowrap="nowrap" style="width:90%">
                                            <asp:TextBox ID="txtFiltroNome" runat="server" CssClass="txt" Width="350px"></asp:TextBox>
                                        </td>
                                        <td class="tdp" nowrap="nowrap" style="width:1%">
                                            <asp:Button ID="btnPesquisarFiltro" runat="server" CssClass="button" 
                                                onclick="btnPesquisarFiltro_Click" Text="Pesquisar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" colspan="3">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" colspan="3" 
                                            style="font-family: Verdana; font-size: 8pt; font-weight: bold">
                                            <table class="table" id="tblEscolherClientes" runat="server" visible="false">
                                                <tr>
                                                    <td class="tdpCenter" width="50%">
                                                        Selecione para Incluir</td>
                                                    <td class="tdpCenter">
                                                        Clientes Selecionados</td>
                                                </tr>
                                                <tr>
                                                    <td class="tdp">
                                                        <asp:Panel ID="Panel6" runat="server" Height="200px" ScrollBars="Auto" 
                                                            Width="100%">
                                                            <asp:CheckBoxList ID="rdListClientes" runat="server" AutoPostBack="True" 
                                                                Font-Bold="False" Font-Names="Verdana" 
                                                                onselectedindexchanged="rdListClientes_SelectedIndexChanged1">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                    <td class="tdp">
                                                        <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto">
                                                            <asp:CheckBoxList ID="rdListClientesSelecionados" runat="server" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdpR" colspan="3">
                                            <asp:Button ID="btnConfirmar" runat="server" CssClass="button" 
                                                onclick="btnConfirmar_Click" Text="Adicionar Selecionados" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td valign="top" align="left" 
                                        style="font-family: verdana; font-size: 8pt; font-weight: bold">
                                        Clientes:</td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" 
                                                            Width="100%">
                                        <asp:CheckBoxList ID="chkEscolhidosFinal" runat="server" AutoPostBack="True" 
                                            Font-Bold="False" Font-Names="Verdana" Font-Size="7pt" 
                                            onselectedindexchanged="chkEscolhidosFinal_SelectedIndexChanged">
                                            <asp:ListItem>Nenhum Cliente</asp:ListItem>
                                        </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr valign="baseline">
                <td class="tdp" colspan="2" valign="middle" align="left" bgcolor="White">
                    &nbsp;</td>
                <td bgcolor="White" class="tdp" nowrap="nowrap" valign="middle">
                    &nbsp;</td>
                <td class="tdpR" nowrap="nowrap" valign="middle" bgcolor="White">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="button" 
                        OnClick="btnSalvar_Click" Text="Salvar" Visible="True" Font-Size="9pt" />
                </td>
            </tr>
            <tr valign="baseline">
                <td bgcolor="White" class="tdpR" colspan="4" valign="middle">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
        </table>
    </left>
    </asp:Panel>


                            <div ID="dvEscolherUsuario" runat="server" 
        visible="false" style="position:absolute; top:10%; left:33%; width:400px">
                                <table class="table">
                                    <tr align="left" 
                                        style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px;">
                                        <td colspan="3">
                                            Pesquisar Usuário:</td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" nowrap="nowrap" style="width:1%">
                                            Iniciais:
                                        </td>
                                        <td class="tdp" nowrap="nowrap" style="width:90%">
                                            <asp:TextBox ID="txtFiltroNomeCliente" runat="server" CssClass="txt" 
                                                Width="99%"></asp:TextBox>
                                        </td>
                                        <td class="tdp" nowrap="nowrap" style="width:1%">
                                            <asp:Button ID="btnPesquisarFiltroUsuario" runat="server" CssClass="button" 
                                                Font-Size="9pt" onclick="btnPesquisrFiltro_Click" Text="Pesquisar" 
                                                Width="100px" CausesValidation="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" colspan="3">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdp" align="left" colspan="3">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdpR" colspan="3">
                                            <asp:Button ID="btnConfirmarUsuario" runat="server" CssClass="button" Font-Size="9pt" 
                                                onclick="btnConfirmarUsuario_Click" Text="Confirmar" Width="100px" 
                                                CausesValidation="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

</asp:Content>

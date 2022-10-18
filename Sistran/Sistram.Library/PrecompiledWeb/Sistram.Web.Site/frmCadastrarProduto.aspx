<%@ page language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="frmCadastrarProduto, App_Web_y4x4wfpf" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">

 function limpa_string(S){

    //'S' é o valor que o usuário escreveu na TextBox  

     var Digitos = "0123456789,"; //Você escreve aqui o caractéres permitidos

     var temp = ""; //Essa variavel vai ser resultante da comparação

     var digito = ""; //Essa variavel vai servir de auxilio para a comparação
   
        //Aqui vai ser loop de comparação 
        for (var i=0; i<S.length; i++)
         {
             //'digito' recebe o caracter da posição 'i' da variavel 'S'
              digito = S.charAt(i);

            //Compara se o caracter da variavel 'digito' têm na variavel 'Digito'
             if (Digitos.indexOf(digito)>=0){temp=temp+digito;}
         }

     //Retorna o resultado da comparação  
     return temp;
  }

</script>


    <asp:UpdatePanel ID="UpdatePanel55" runat="server">

        <ContentTemplate>
            <asp:Panel ID="pnlteste" runat="server" DefaultButton="btnConfirmar" HorizontalAlign="Left"
                Style="text-align: left" Height="600px">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                            height: 25px">
                            <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Produtos" Font-Bold="True"
                                Font-Size="14px"></asp:Label>
                            <asp:Label ID="lblIdProduto" runat="server" Text="0" Visible="False"></asp:Label>
                            <asp:Label ID="lblIdProdutoCliente" runat="server" Text="0" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>

                <table id="novatb" runat="server" cellpadding="1" cellspacing="0" class="table" width="100%">
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap" width="1%" style="height: 4px">
                            Código:
                        </td>
                        <td class="tdp" width="45%" style="height: 4px">
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" OnTextChanged="txtCodigo_TextChanged"
                                Width="95%" MaxLength="20" AutoPostBack="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCodigo" ErrorMessage="Informe o Código" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" width="1%" style="height: 4px">
                            Descrição:
                        </td>
                        <td class="tdp" width="45%" style="height: 4px">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="txt" Width="95%" 
                                MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDesc" ErrorMessage="Informe a Descrição" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Método de Movimentação:
                        </td>
                        <td class="tdp">
                            <asp:DropDownList ID="cboMovimentacao" runat="server" CssClass="cbo" 
                                Width="96%">
                                <asp:ListItem>FIFO</asp:ListItem>
                                <asp:ListItem>LIFO</asp:ListItem>
                                <asp:ListItem>FEFO</asp:ListItem>
                                <asp:ListItem>LOTE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            Unidade de Medida:
                        </td>
                        <td class="tdp" width="25%">
                            <asp:DropDownList ID="cboMedida" runat="server" CssClass="cbo" Width="96%">
                                <asp:ListItem Value="1">UNIDADE</asp:ListItem>
                                <asp:ListItem Value="2">METRO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Ativo:
                        </td>
                        <td class="tdp">
                            <asp:DropDownList ID="cboAtivo" runat="server" CssClass="cbo" Width="96%">
                                <asp:ListItem>SIM</asp:ListItem>
                                <asp:ListItem>NAO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                           </td>
                        <td class="tdp" width="25%">
                           </td>
                    </tr>
                    <tr valign="bottom">
                        <td bgcolor="#666666" colspan="4" nowrap="nowrap">
                            </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Código de Barras:
                        </td>
                        <td class="tdp">
                            <asp:TextBox ID="txtCodBarras" runat="server" 
                                CssClass="txt" MaxLength="20" OnTextChanged="txtCódigo_TextChanged" 
                                Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtCodBarras" ErrorMessage="Informe o Código de Barras" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            Conteúdo:
                        </td>
                        <td class="tdp" width="25%">
                            <asp:TextBox ID="txtConteudo" runat="server" CssClass="txt" MaxLength="60" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtConteudo" ErrorMessage="Informe o Conteúdo" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Unidade Cliente:
                        </td>
                        <td class="tdp">
                            <asp:TextBox ID="txtUnidadeCliente" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtUnidadeCliente" 
                                ErrorMessage="Informe a Unidade do Cliente" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            Valor Unitário:
                        </td>
                        <td class="tdp" width="25%">
                            <asp:TextBox ID="txtValorUnitario" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtValorUnitario" ErrorMessage="Informe o Valor Unitário" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Unidade Logística:
                        </td>
                        <td class="tdp">
                            <asp:TextBox ID="txtUnidadeLogistica" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="txtUnidadeLogistica" 
                                ErrorMessage="Informe  a Unidade da Logistica" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                           </td>
                        <td class="tdp" width="25%">
                           </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            </td>
                        <td class="tdp">
                            </td>
                        <td class="tdp" nowrap="nowrap">
                            </td>
                        <td class="tdp" width="25%">
                            </td>
                    </tr>
                    <tr valign="bottom">
                        <td bgcolor="#666666" colspan="4" nowrap="nowrap">
                            </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Peso Líquido:
                        </td>
                        <td class="tdp">
                            <asp:TextBox ID="txtPesoLiquido" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="txtPesoLiquido" ErrorMessage="Informe o Peso Líquido" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            Peso Bruto:
                        </td>
                        <td class="tdp" width="25%">
                            <asp:TextBox ID="txtPesoBruto" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                ControlToValidate="txtPesoBruto" ErrorMessage="Informe o Peso Bruto" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr valign="bottom">
                        <td bgcolor="#666666" colspan="4" nowrap="nowrap">
                            </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Altura:</td>
                        <td class="tdp">
                            <asp:TextBox ID="txtAltura" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                ControlToValidate="txtAltura" ErrorMessage="Informe a Altura" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            Comprimento:
                        </td>
                        <td class="tdp" width="25%">
                            <asp:TextBox ID="txtComprimento" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                ControlToValidate="txtComprimento" ErrorMessage="Informe o Comprimento" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" nowrap="nowrap">
                            Largura:
                        </td>
                        <td class="tdp">
                            <asp:TextBox ID="txtLargura" runat="server" CssClass="txtValor" 
                                OnTextChanged="txtCódigo_TextChanged" Width="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                ControlToValidate="txtLargura" ErrorMessage="Informe a Largura" 
                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="tdp" nowrap="nowrap">
                            </td>
                        <td class="tdp" width="25%">
                            </td>
                    </tr>
                    <tr valign="bottom">
                        <td nowrap="nowrap" id="tdSep1" colspan="4" bgcolor="#666666">
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdp" colspan="3" nowrap="nowrap">
                            <table width="99%">
                                <tr>
                                    <td align="left" style="font-weight: 700">
                                    
                                        Divisões:</td>
                                        <td></td>
                                        <td></td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblSelecionado" runat="server" Font-Bold="True" 
                                            Font-Names="Verdana" Font-Size="8pt" Text="Item Selecionado:" Visible="False" />
                                        <asp:HiddenField ID="hdCodigoDivisaoCliente" runat="server" />
                                        <asp:HiddenField ID="hdCodigoDivisaoCliente0" runat="server" />
                                        
                                    </td>      <td></td>
                                        <td></td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                    <asp:UpdatePanel ID="uplDivisoes" runat="server">
                                    <ContentTemplate>
                                    
                                   
                                    
                                    <asp:Panel ID="pnlDivisoes" runat="server" Height="300px" ScrollBars="Auto">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand"
                                            OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <table border="0">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblIDClienteDivisao" runat="server" Font-Names="Verdana" Font-Size="7pt"
                                                            Text='<% #Eval("IDCLIENTEDIVISAO") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lblNome0" runat="server" Font-Bold="false" Font-Names="Verdana"
                                                            Font-Size="7pt" Font-Underline="false" ForeColor="Black" Text='<% #Eval("Nome") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        </asp:Panel>
                                     
                                      </ContentTemplate>                                    
                                    </asp:UpdatePanel>
                                    </td>
                                    <td>
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
                                    <td align="right" valign="top" width="50%">
                                        <asp:ListBox ID="ListBox1" runat="server" CssClass="listbox2" Height="300px" Rows="10"
                                            Width="60%"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdp" style="text-align: center" valign="top" width="2%">
                            <table class="table" style="width: 100%">
                                <tr>
                                    <td class="tdp">
                                        Image:</td>
                                    <td class="tdp" style="text-align: left">
                                       <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
<Triggers> 
<asp:PostBackTrigger ControlID="brnCarregar" /> 
</Triggers> 
<ContentTemplate> 
<asp:FileUpload ID="fileUploadArquivo" runat="server" CssClass="fileUpload" /> 
    &nbsp;<asp:Button ID="brnCarregar" runat="server" CssClass="button" 
        onclick="brnCarregar_Click" Text="Carregar" />
</ContentTemplate> 
</asp:UpdatePanel> 

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    <asp:UpdatePanel ID="ooooo" runat="server">
                                    <ContentTemplate >
                                        <asp:Image ID="imgProd" runat="server" Height="160px" ImageAlign="Top" 
                                            ImageUrl="~/Images/naoDisponivel.jpg" 
                                            ToolTip="Clique aqui para inserir uma foto" />
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        <br />
                        <br />
                        <br />
                        <br />
                                
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td class="tdpR" colspan="4" nowrap="nowrap">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                            <asp:Button ID="btnConfirmar" runat="server" CssClass="button" 
                                OnClick="btnConfirmar_Click" Text="Confirmar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmCadAviso, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <script language="javascript" type="text/javascript">

 function limpa_string(S){
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
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Avisos" Font-Bold="True"
                        Font-Size="14px"></asp:Label>
                    <asp:Label ID="lblIdUsuario" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        
        <table id="novatb" class="table" runat="server" cellpadding="1" cellspacing="0" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    Nome:
                </td>
                <td class="tdp" nowrap="nowrap" width="30%">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="200"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Login:
                </td>
                <td class="tdp" nowrap="nowrap" width="30%">
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="txt" Width="200px" 
                        Wrap="False"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Operação:</td>
                <td class="tdp" nowrap="nowrap" width="30%">
                    <asp:TextBox ID="txtOperacao" runat="server" CssClass="txt" Width="200px" 
                        Wrap="False"></asp:TextBox>
                </td>
                <td class="tdpR" nowrap="nowrap" width="60%">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                        OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" Text="Novo" />
                </td>
            </tr>
        </table>
        
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
                    AllowSorting="True" GridLines="None" 
                    OnPageIndexChanged="RadGrid1_PageIndexChanged" 
                    OnSortCommand="RadGrid1_SortCommand" onitemcommand="RadGrid1_ItemCommand">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="IdAviso" HeaderText="Código" 
                                UniqueName="column1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="CODIGOTextBox" runat="server" Text='<%# Bind("IdAviso") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl='<% # "frmDetalheAviso.aspx?Codigo=" + Eval("IdAviso") %>' 
                                        Target="_blank" Text='<%# Bind("IdAviso") %>' 
                                        ToolTip="Clique aqui para ver o detalhe."></asp:HyperLink>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                           
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Usuário"  DataField="NOME"
                                UniqueName="column31">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                              <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Login"  DataField="Login"
                                UniqueName="column3">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Divisao" DataField="Divisao"
                                UniqueName="column41">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Operação" DataField="Operacao"
                                UniqueName="column42">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                             <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Canal de Venda" DataField="Representante"
                                UniqueName="column4">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="Deletar" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkDeletar" runat="server" CommandName='<% #Eval("IDAviso") %>' CommandArgument="Deletar" >Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

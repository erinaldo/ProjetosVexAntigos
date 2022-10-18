<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="frmListKPI_IRWIN, App_Web_p3uplnwq" theme="Adm" enabletheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
        function Expandir(div) {
            if (document.getElementById(div).style.display == 'block') {
                document.getElementById(div).style.display = 'none';
            }
            else {
                document.getElementById(div).style.display = 'block';
            }
        }
    
    </script>

    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastrar KPI" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="novatb" class="table" runat="server" cellpadding="2" cellspacing="1" width="100%">
            <tr valign="bottom">
                <td class="tdp" width="1%" nowrap="nowrap">
                    Grupo Produto:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:DropDownList ID="cboLinha" runat="server" CssClass="cbo" Font-Names="Arial"
                        Font-Size="7pt" Height="17px" Width="120px">
                    </asp:DropDownList>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    Mês Ano:
                </td>
                <td class="tdp" width="1%">
                    <asp:TextBox ID="txtMesAno" runat="server" CssClass="txt" MaxLength="7" Width="100px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="25%">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                        OnClick="Button1_Click" Text="Pesquisar" />
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="txt" Visible="False" Width="200px"
                        Wrap="False"></asp:TextBox>
                        
                    <input ID="Button3" type="button" value="Gerar Excel" 
                        onclick="window.open('popUpExcelGridView.aspx', 'myname', 'status=yes')"; 
                        class="button" /></td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" OnRowDataBound="grdResultado_RowDataBound" 
                    OnRowCommand="grdResultado_RowCommand" EnableModelValidation="True" 
                    GridLines="Vertical">
                    <RowStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                        Width="99%" BackColor="#F7F7DE" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
         
               
                        <asp:BoundField DataField="CHAVE" HeaderText="KEY" />
         
               
                        <asp:BoundField DataField="Descricao" HeaderText="DESCRIÇÃO">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DescricaoUnidadeDeMedida" HeaderText="UNIDADE">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DescricaoTarguet" HeaderText="TARGET">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="01" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="links" Text='<%#  Eval("DIA1") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA1") %>' CommandArgument='<% #Eval("DIA1") %>' ></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="02" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="links" Text='<%#Eval("DIA2") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA2") %>' CommandArgument='<% #Eval("DIA2") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="03" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="links" Text='<%#Eval("DIA3") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA3") %>' CommandArgument='<% #Eval("DIA3") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="04" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="links" Text='<%#Eval("DIA4") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA4") %>' CommandArgument='<% #Eval("DIA4") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="05" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="links" Text='<%#Eval("DIA5") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA5") %>' CommandArgument='<% #Eval("DIA5") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="06" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton6" runat="server" CssClass="links" Text='<%#Eval("DIA6") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA6") %>' CommandArgument='<% #Eval("DIA6") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="07" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton7" runat="server" CssClass="links" Text='<%#Eval("DIA7") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA7") %>' CommandArgument='<% #Eval("DIA7") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="08" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton8" runat="server" CssClass="links" Text='<%#Eval("DIA8") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA8") %>' CommandArgument='<% #Eval("DIA8") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="09" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton9" runat="server" CssClass="links" Text='<%#Eval("DIA9") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA9") %>' CommandArgument='<% #Eval("DIA9") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton10" runat="server" CssClass="links" Text='<%#Eval("DIA10") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA10") %>' CommandArgument='<% #Eval("DIA10") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="11" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton11" runat="server" CssClass="links" Text='<%#Eval("DIA11") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA11") %>' CommandArgument='<% #Eval("DIA11") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton12" runat="server" CssClass="links" Text='<%#Eval("DIA12") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA12") %>' CommandArgument='<% #Eval("DIA12") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="13" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton13" runat="server" CssClass="links" Text='<%#Eval("DIA13") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA13") %>' CommandArgument='<% #Eval("DIA13") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="14" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton14" runat="server" CssClass="links" Text='<%#Eval("DIA14") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA14") %>' CommandArgument='<% #Eval("DIA14") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="15" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton15" runat="server" CssClass="links" Text='<%#Eval("DIA15") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA15") %>' CommandArgument='<% #Eval("DIA15") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="16" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton16" runat="server" CssClass="links" Text='<%#Eval("DIA16") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA16") %>' CommandArgument='<% #Eval("DIA16") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="17" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton17" runat="server" CssClass="links" Text='<%#Eval("DIA17") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA17") %>' CommandArgument='<% #Eval("DIA17") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton18" runat="server" CssClass="links" Text='<%#Eval("DIA18") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA18") %>' CommandArgument='<% #Eval("DIA18") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton19" runat="server" CssClass="links" Text='<%#Eval("DIA19") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA19") %>' CommandArgument='<% #Eval("DIA19") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="20" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton20" runat="server" CssClass="links" Text='<%#Eval("DIA20") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA20") %>' CommandArgument='<% #Eval("DIA20") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="21" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton21" runat="server" CssClass="links" Text='<%#Eval("DIA21") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA21") %>' CommandArgument='<% #Eval("DIA21") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="22" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton22" runat="server" CssClass="links" Text='<%#Eval("DIA22") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA22") %>' CommandArgument='<% #Eval("DIA22") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="23" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton23" runat="server" CssClass="links" Text='<%#Eval("DIA23") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA23") %>' CommandArgument='<% #Eval("DIA23") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="24" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton24" runat="server" CssClass="links" Text='<%#Eval("DIA24") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA24") %>' CommandArgument='<% #Eval("DIA24") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="25" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton25" runat="server" CssClass="links" Text='<%#Eval("DIA25") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA25") %>' CommandArgument='<% #Eval("DIA25") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="26" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton26" runat="server" CssClass="links" Text='<%#Eval("DIA26") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA26") %>' CommandArgument='<% #Eval("DIA26") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="27" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton27" runat="server" CssClass="links" Text='<%#Eval("DIA27") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA27") %>' CommandArgument='<% #Eval("DIA27") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="28" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton28" runat="server" CssClass="links" Text='<%#Eval("DIA28") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA28") %>' CommandArgument='<% #Eval("DIA28") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="29" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton29" runat="server" CssClass="links" Text='<%#Eval("DIA29") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA29") %>' CommandArgument='<% #Eval("DIA29") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="30" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton30" runat="server" CssClass="links" Text='<%#Eval("DIA30") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA30") %>' CommandArgument='<% #Eval("DIA30") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="31" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton31" runat="server" CssClass="links" Text='<%#Eval("DIA31") %>'
                                    CommandName='<% #Eval("CODIGO_CELULA31") %>' CommandArgument='<% #Eval("DIA31") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" Visible="False">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:UpdatePanel ID="upldv" runat="server">
                    <ContentTemplate>
                        <div id="divDetalhes" runat="server" style="width: 300px; position: absolute; top: 40%;
                            left: 40%;">
                            <asp:Label ID="lblCodigoMov" runat="server" Visible="false" Text=""></asp:Label>
                            <table class="table">
                                <tr style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px">
                                    <td colspan="2">
                                        Informe os campos abaixo:
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdp" style="width: 1%" nowrap="nowrap">
                                        Valor:
                                    </td>
                                    <td class="tdp">
                                        <asp:TextBox ID="txtValor" runat="server" CssClass="txtValor" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdpR" colspan="2" nowrap="nowrap">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="button" OnClick="btnConfirmar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

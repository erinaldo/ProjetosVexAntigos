<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmControleArquivos.aspx.cs"
    Inherits="frmControleArquivos" Theme="Adm" EnableTheming="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function exibir() {
            obj = document.getElementById('dvArquivo');
            obj.style.display = 'block';
        }

        function esconder() {
            obj = document.getElementById('dvArquivo');
            obj.style.display = 'none';
        }

        function OnChange(dropdown, txt) {
            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;

            obj = document.getElementById(txt);

            if (SelValue == "DIGITAR NOVO...") 
            {
                obj.style.display = 'block';
                obj.focus();
            }
            else 
            {

                obj.style.display = 'none';
            }

            //alert(obj);
        }


     
    </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfirma" />
        </Triggers>
        </asp:UpdatePanel>

    <asp:UpdatePanel ID="pnlup" runat="server">
       
        <ContentTemplate>
            <div id="dvArquivo" style="position: absolute; border: solid:1px:silver; display: none;
                top: 35%; left: 40%; background-color: Silver">
                <asp:Panel ID="pnldiv" runat="server" Style="width: 400; border: solid 1px black">
                    <table border="1" cellpadding="2" cellspacing="2" width="400">
                        <tr>
                            <td colspan="2" style="text-align: center; font-size: 9pt; font-weight: 700;">
                                Escolha o Arquivo e Clique em Confirmar
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" style="width: 1%">
                                Tipo de Arquivo:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cmbTipo" runat="server" CssClass="cbo" Height="17px" >
                                </asp:DropDownList>

                                <asp:TextBox ID="txtNovoNome" runat="server" CssClass="txt" Width="250px" style="display: none;" ></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" style="width: 1%">
                                Selecione o Arquivo:
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txt" Width="99%" Height="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnConfirma" runat="server" Text="Confirmar" CssClass="button" 
                                    onclick="btnConfirma_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClientClick="esconder();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width: 100%;" __designer:mapid="13af" border="0" cellpadding="0" cellspacing="0">
        <tr __designer:mapid="13b0">
            <td style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg'); height: 25px"
                __designer:mapid="13b1">
                <asp:Label ID="lblTitulo" runat="server" Text="Controle de Arquivos" Font-Bold="True"
                    Font-Size="14px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdpR">
                <table class="grid">
                    <tr>
                        <td>
                            <div class="button" style="height: 18px; width: 90px; text-align: center; vertical-align: middle;
                                cursor: hand;" onclick="exibir();">
                                <table>
                                    <tr valign="middle">
                                        <td>
                                            Novo Arquivo
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGridUsuarios" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="0" GridLines="None" OnItemCommand="RadGrid16_ItemCommand" OnItemDataBound="RadGrid16_ItemDataBound"
                                Skin="Default2006" Width="100%" PageSize="20" OnNeedDataSource="RadGridUsuarios_NeedDataSource">
                                <MasterTableView BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="0" GridLines="Both">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="IDARQUIVOITEM" HeaderText="CÓDIGO" UniqueName="column0011">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% # "GerarDownloadArquivo.aspx?i=" + Eval("IDARQUIVOITEM") %>'
                                                    Text='<%# Bind("IDARQUIVOITEM") %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" Width="1%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NomeDoArquivo" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="NOME" ItemStyle-HorizontalAlign="Left" UniqueName="column11" 
                                            Visible="true">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="DATA" ItemStyle-HorizontalAlign="Left" UniqueName="column1" Visible="true">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="TIPO" ItemStyle-HorizontalAlign="Left" UniqueName="column2" 
                                            Visible="true">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="USUARIO" EmptyDataText="&amp;nbsp;" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="USUÁRIO" ItemStyle-HorizontalAlign="Left" UniqueName="column3" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <AlternatingItemStyle Font-Size="7pt" Height="8px" />
                                <ItemStyle BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                    Font-Size="7pt" Height="7px" />
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <HeaderStyle Font-Bold="False" Font-Size="7pt" />
                                <FilterMenu EnableTheming="True" Skin="Default2006">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </FilterMenu>
                                <StatusBarSettings LoadingText="Carregando..." />
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

﻿<%@ page language="C#" masterpagefile="~/IntranetMasterBlank.master" autoeventwireup="true" inherits="Intranet_frmPOPListMotorista, App_Web_p3uplnwq" enabletheming="True" theme="Adm" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="pnlteste" runat="server" DefaultButton="Button1">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                    height: 25px">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro de Motorista" Font-Bold="True"
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
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="250px"></asp:TextBox>
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    CPF/CNPJ:
                </td>
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:TextBox ID="txtCpf" runat="server" CssClass="txt" Width="200px" 
                        Wrap="False"></asp:TextBox>
                </td>
                
                
                <td class="tdp" nowrap="nowrap" width="1%">
                    <asp:CheckBox ID="chkativo" runat="server" Text="Ativo" Checked="True" Visible="false" />
                </td>
               
                <td class="tdpR" nowrap="nowrap" width="60%">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Font-Names="arial" Font-Size="7pt"
                        OnClick="Button1_Click" Text="Pesquisar" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="button" Font-Names="arial" 
                        Font-Size="7pt" Text="Novo" onclick="Button2_Click" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="xxwewx" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid17" runat="server" GridLines="None" 
                    Skin="Default2006" AllowPaging="True" AllowSorting="True" 
                    onpageindexchanged="RadGrid17_PageIndexChanged" 
                    onsortcommand="RadGrid17_SortCommand" 
                    onitemdatabound="RadGrid17_ItemDataBound" Visible="False">
                    <MasterTableView AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Código" UniqueName="TemplateColumn">
                                
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpCodigo" runat="server" Text='<%# Bind("IDMOTORISTA") %>' CssClass="link"  ></asp:HyperLink>
                                </ItemTemplate>
                                
                                
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Width="1%" Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="True" />
                            </telerik:GridTemplateColumn>
                            
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Código"  DataField="IDMotorista"
                                UniqueName="column1" Visible="False">
                            </telerik:GridBoundColumn>
                            
                            
                             <telerik:GridTemplateColumn HeaderText="CPF/CNPJ" UniqueName="TemplateColumn">
                                
                                <ItemTemplate>
                                    <asp:Label ID="CnpjCpfLabel" runat="server" Text='<%# Bind("CnpjCpf") %>'  ></asp:Label>
                                </ItemTemplate>
                                
                                
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" 
                                     Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" 
                                    Wrap="True" />
                            </telerik:GridTemplateColumn>
                            
                            
                            <telerik:GridTemplateColumn HeaderText="Nome" UniqueName="TemplateColumn">
                                
                                <ItemTemplate>
                                    <asp:Label ID="RazaoSocialNomeLabel" runat="server" Text='<%# Bind("RazaoSocialNome") %>'  ></asp:Label>
                                </ItemTemplate>
                                
                                
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" 
                                    Wrap="True" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" 
                                    Wrap="True" />
                            </telerik:GridTemplateColumn>
                            
                            
                            
                            
                          <%--  <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Nome"  DataField="RazaoSocialNome"
                                UniqueName="column2">
                            </telerik:GridBoundColumn>--%>
                            
                            
                           <%-- <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="CPF/CNPJ" DataField="CnpjCpf"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            --%>
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Ativo" DataField="Ativo"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn EmptyDataText="&amp;nbsp;" HeaderText="Liberado" DataField="liberado"
                                UniqueName="column">
                            </telerik:GridBoundColumn>
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

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CagasDisponiveis.aspx.cs"
    Inherits="CagasDisponiveis" Theme="Adm" EnableTheming="true" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    
            <asp:Panel ID="pnlteste" runat="server" >
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" style="background-image: url('Images/skins/primeiro/img/menu_3_2.jpg');
                            height: 25px">
                            <asp:Label ID="lblTitulo" runat="server"
                                Font-Bold="True" Font-Size="14px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvAjudaTransitTime" runat="server" style="position: absolute; top: 30%;
                    left: 45%; text-align: center; display: none; width:300px; border-color:Silver; border-style:solid; border-width:1px">
                    <table cellpadding="2" cellspacing="2" border="0" style="background-color:#FFFFDD" width="100%" >
                        <tr>
                            <td>                                
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <asp:Label ID="lbltituloAjuda" runat="server" ></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        
                                          <tr>
                                            <td style="text-align: center; font-size: 12px; font-family: Verdana; font-weight: bold">
                                            <hr />
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr  align="left">
                                            <td>
                                                <asp:Label ID="lblAjuda" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="vv" runat="server"  Text="FECHAR [X]" CssClass="link"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" style="height: 16px">
                               
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" Text="Passo 1 - Selecione Filial / Tipo"></asp:Label>
                                
                               </td>
                            <td style="height: 16px" valign="top" width="30px">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                <asp:Label ID="lblPasso2" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" Text="Passo 2 - Selecione o Cliente" Visible="False"></asp:Label>
                            </td>
                            <td style="height: 16px" valign="top" width="30px">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                <asp:Label ID="lblPasso3" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" Text="Passo 3 - Gerar " Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 16px" valign="top">
                                <hr />
                                </td>
                        </tr>
                        <tr>
                            <td style="height: 16px" valign="top" nowrap="nowrap" width="1%">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                    CellPadding="3" EnableModelValidation="True" Font-Names="Arial" Font-Size="9pt" 
                                    ForeColor="Black" GridLines="Vertical" onrowdatabound="GridView1_RowDataBound" 
                                    style="font-size: 8pt">
                                    <AlternatingRowStyle BackColor="#F2F2F2" />
                                    <Columns>
                                        <asp:BoundField DataField="NUMERODAFILIAL" HeaderText="Número">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Filial" HeaderText="Filial">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Pedido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGridPedido" runat="server" AutoPostBack="True" 
                                                    Text='<% # Eval("PEDIDOS") %>' 
                                                    oncheckedchanged="chkGridPedido_CheckedChanged"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nota Fiscal">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGridNF" runat="server" AutoPostBack="True" 
                                                    Text='<% # Eval("NF") %>' oncheckedchanged="chkGridNF_CheckedChanged" />
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdFilial" runat="server" Text='<%# Eval("idfilial") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#E9E9E9" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                                <br />
                            </td>
                            <td nowrap="nowrap" 
                                style="border-style: none solid none none; border-width: thin; height: 16px; border-top-color: #FFFFFF; border-right-color: #C0C0C0;" 
                                valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                <asp:Panel ID="Panel9" runat="server">
                                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                        CellPadding="3" EnableModelValidation="True" Font-Names="Arial" Font-Size="9pt" 
                                        ForeColor="Black" GridLines="Vertical"  
                                        style="font-size: 8pt" Visible="False">
                                        <AlternatingRowStyle BackColor="#F2F2F2" />
                                        <Columns>
                                            <asp:BoundField DataField="NUMERODAFILIAL" HeaderText="Número">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Filial" HeaderText="Filial">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="RazaoSocialNome" HeaderText="Cliente" >
                                            <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pedidos" HeaderText="Pedido" />
                                            <asp:BoundField DataField="NF" HeaderText="Nota Fiscal" />
                                            

                                             <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdcliente" runat="server" Text='<%# Eval("IDCLIENTE") %>'></asp:Label>
                                                <asp:Label ID="lblIdFilial" runat="server" Text='<%# Eval("IDFILIAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGridSel" runat="server" Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#E9E9E9" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                    <br />
                                    <asp:ListBox ID="lstEscolhidos" runat="server" Width="150px" Visible="False"></asp:ListBox>
                                </asp:Panel>
                            </td>
                            <td style="border-style: none solid none none; border-width: thin; height: 16px; border-top-color: #FFFFFF; border-right-color: #C0C0C0;" 
                                valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                <asp:Button ID="btnGerar" runat="server" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="2px" Height="50px" onclick="Button4_Click" Text="Gerar" 
                                    Width="100px" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px" valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                &nbsp;</td>
                            <td style="height: 16px" valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 16px" valign="top">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
</asp:Content>

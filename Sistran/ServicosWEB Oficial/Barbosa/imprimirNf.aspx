<%@ Page Title="" Language="C#" MasterPageFile="~/Barbosa/Site1.Master" AutoEventWireup="true" CodeBehind="imprimirNf.aspx.cs" Inherits="ServicosWEB.Barbosa.imprimirNf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    .auto-style2 {
        text-align: center;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr style="background-color:darkblue">
            <td colspan="3">
                <asp:Image ID="Image1" runat="server" ImageUrl="https://www.barbosasupermercados.com.br/wp-content/themes/mplsagc/assets/img/main-brand.png" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="white-space:nowrap">
                <table class="auto-style1">
                    <tr>
                        <td style="width:1%; white-space:nowrap">Dias:
                <asp:TextBox ID="txtDias" runat="server" Width="50px">2</asp:TextBox>
                &nbsp;&nbsp;</td>
                        <td style="width:1%; white-space:nowrap"><asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Ambos</asp:ListItem>
                    <asp:ListItem Value="NaoImpresso">Não Impresso</asp:ListItem>
                    <asp:ListItem>Impresso</asp:ListItem>
                </asp:RadioButtonList>
                        </td>
                        <td><asp:Button ID="btnPesquisar" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Pesquisar" OnClick="btnPesquisar_Click" BackColor="White" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="3">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Font-Names="Verdana" Font-Size="8pt" OnRowCommand="GridView1_RowCommand" Width="100%">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />

                       <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/Img/Impressora.png"
                            CommandName='<%# Eval("chave") %>' CommandArgument="imprimir" ToolTip='<% # Eval("IMPRESSO") %>' />                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MaisBrasil/Site1.Master" AutoEventWireup="true" CodeBehind="NfeEntrada.aspx.cs" Inherits="ServicosWEB.MaisBrasil.NfeEntrada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        body
        {
            font-size: 12px;
            margin: 0 0 0 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            white-space: nowrap;
            width: 100%;
        }
        table
        {
            width: 100%
        }
    </style>
    <table style="width:200px">
        <tr>
            <td>N. NFe: </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td> 
           

            <td>
                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" BackColor="White" BorderStyle="Solid" /></td>
             <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>

        </tr>
    </table>
    <hr />


    <asp:GridView ID="GridView1" runat="server" HeaderStyle-HorizontalAlign="Left" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" OnClick="Button1_Click" Text="Ver" CommandArgument="ver"  CommandName='<%# Eval("Id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
<HeaderStyle HorizontalAlign="Left" BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Girotrade/Site1.Master" AutoEventWireup="true" CodeBehind="Ocorrencias.aspx.cs" Inherits="ServicosWEB.ViaVarejo.Ocorrencias" %>
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
         .auto-style1 {
             width: 1px;
         }
    </style>
    <table style="width:200px">
        <tr>           
             <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>

        </tr>
    </table>
    <hr />


    <asp:GridView ID="GridView1" runat="server" HeaderStyle-HorizontalAlign="Left" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand">
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

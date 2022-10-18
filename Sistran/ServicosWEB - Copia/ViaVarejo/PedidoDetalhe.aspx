<%@ Page Title="" Language="C#" MasterPageFile="~/Girotrade/Site1.Master" AutoEventWireup="true" CodeBehind="PedidoDetalhe.aspx.cs" Inherits="ServicosWEB.Girotrade.PedidoDetalhe" %>
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

    <div>
        <div style="text-align:right; margin-top:-15px">
        <asp:Button ID="btnVoltar" runat="server" Text="<< Voltar" PostBackUrl="~/Girotrade/Pedidos.aspx" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
       </div>
        <br />

        <div style="float:left; width:48%">

            <h3>Pedido Enviado Para Vex</h3>    
            <p><b>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b></p>
            <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

        </div>
        

        <div style="float:right; width:48%">
            <%--<p style="color:white">.</p>--%><%--<p style="color:white">.</p>--%>
           
            
            <h3>Detalhe do Liberado para Faturamento</h3>
            <br />

              <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" HeaderStyle-HorizontalAlign="Left" DataSourceID="SqlDataSource2" style="margin-top:43px">
                <Columns>
                    <asp:BoundField DataField="Código" HeaderText="Código" SortExpression="Código" />
                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />                   

                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

           





            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GrupoLogosConnectionString %>" SelectCommand=" select CodigoDoProduto [Código],  Quantidade 
 from YandehStatusPedido a
 Left Join YandehStatusPedidoItem b on b.IdStatusPedido= a.id
 where NrPedido=@NrPedido
and Status='Liberado para Faturamento'
 order by CodigoDoProduto">
                <SelectParameters>
                    <asp:QueryStringParameter Name="NrPedido" QueryStringField="i" />
                </SelectParameters>
            </asp:SqlDataSource>

           
            <br />
             <H3>Retorno do Pedido</H3>            
            <br />

            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" HeaderStyle-HorizontalAlign="Left" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="NrPedido" HeaderText="NrPedido" SortExpression="NrPedido" />                   

                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:BoundField DataField="Consumido" HeaderText="Consumido" SortExpression="Consumido" />
                    <asp:BoundField DataField="DataHora" HeaderText="DataHora" SortExpression="DataHora" />

                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GrupoLogosConnectionString %>" SelectCommand=" select cast(consumido as datetime) Consumido,* from YandehStatusPedido a
 where NrPedido=@NrPedido
 order by DataHora">
                <SelectParameters>
                    <asp:QueryStringParameter Name="NrPedido" QueryStringField="i" />
                </SelectParameters>
            </asp:SqlDataSource>



        </div>

    </div>


</asp:Content>

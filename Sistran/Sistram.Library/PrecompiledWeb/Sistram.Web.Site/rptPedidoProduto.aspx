<%@ page language="C#" masterpagefile="~/SiteDetalhe.master" autoeventwireup="true" inherits="rptPedidoProduto, App_Web_k1oyg1pl" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="580px" Width="100%" BackColor="White">
        <LocalReport ReportPath="Reports\rptPedidoProduto.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="PedidoProduto_DataTable1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="ListarRptPedidos" TypeName="SistranBLL.Produto">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="CODIGO" 
                QueryStringField="codigo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </asp:Content>


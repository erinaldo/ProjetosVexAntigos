<%@ Page Language="C#" MasterPageFile="~/SiteDetalhe.master" AutoEventWireup="true" CodeFile="rptFoto.aspx.cs" Inherits="rptFoto"  %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
   <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="580px" Width="100%" BackColor="White">
        <LocalReport ReportPath="Reports\rptProdutoComFoto.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DsProdutoComFoto_ProdutoFoto" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>--%>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="ConsultarProdutoComFoto" TypeName="SistranDAO.Produto">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="idClienteDivisao" 
                QueryStringField="IdClienteDivisao" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


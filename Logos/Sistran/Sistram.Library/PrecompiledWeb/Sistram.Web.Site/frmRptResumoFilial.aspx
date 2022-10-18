<%@ page language="C#" masterpagefile="~/SiteDetalheFull.master" autoeventwireup="true" inherits="frmRptResumoFilial, App_Web_k1oyg1pl" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <rsweb:ReportViewer ID="reportViewer1" runat="server" Height="600px" 
        Width="100%" BackColor="White" Font-Names="Verdana" Font-Size="8pt">
        <LocalReport DisplayName="Teste" EnableExternalImages="True" 
            EnableHyperlinks="True" ReportPath="Report.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_DataTable1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetData" TypeName="DataSet1TableAdapters.DataTable1TableAdapter">
    </asp:ObjectDataSource>
</asp:Content>


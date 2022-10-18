<%@ Page Language="C#" MasterPageFile="~/SiteDetalheFull.master" AutoEventWireup="true" CodeFile="frmRptLista.aspx.cs" Inherits="frmRptLista" Title="NOTAS FISCAIS AGUADANDO EMBARQUE" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="White" 
        Font-Names="Verdana" Font-Size="8pt" Height="95%" Width="100%">
        <LocalReport ReportPath="rptNFLista.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" 
                    Name="DSNFAguardandoEmbarque_DataTable1" />
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" 
                    Name="DSNFAguardandoEmbarque_DataTable2" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
        SelectMethod="GetData" 
        TypeName="DSNFAguardandoEmbarqueTableAdapters.DataTable2TableAdapter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DSNFAguardandoEmbarqueTableAdapters.DataTable1TableAdapter">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="&quot;01/08/2011&quot;" Name="data" 
                QueryStringField="data" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


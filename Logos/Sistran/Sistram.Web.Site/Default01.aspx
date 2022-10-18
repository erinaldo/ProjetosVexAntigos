<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Default01.aspx.cs" Inherits="_Default01" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"--%>
    <%--Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server" >
    <table class="grid" align="center" width="50%">
        <tr>
            <td width="10%" style="text-align: right">
                Nota Fiscal:</td>
            <td width="25%">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="45%"></asp:TextBox>
            </td>
            <td width="10%" style="text-align: right">
                Nome Fantasia:</td>
            <td width="25%">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="99%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
            <td width="25%" style="text-align: right">
                <asp:Button ID="Button1" runat="server" CssClass="button" 
                    onclick="Button1_Click" Text="Gerar" />
            </td>
        </tr>
      
        <tr>
            <td colspan="4" height="0" >
            <hr style="background-color:Silver; height:1px" />
                </td>
        </tr>

          <tr>
            <td colspan="4">
               
             <%--   <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="98%" 
                    ZoomMode="PageWidth">
                    <LocalReport ReportPath="Report1.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>--%>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                    TypeName="GrupoLogosDataSetTableAdapters.NotaFiscalSaidaConsultarTableAdapter">
                </asp:ObjectDataSource>
              </td>
        </tr>
       
    </table>
</asp:Content>


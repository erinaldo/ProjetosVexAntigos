using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

public partial class frmRptDetalheNF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        rw.Width = 1024;
        rw.Height = 650;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;

        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;

        DataTable D = (DataTable)Session["dtNF"];
        ReportDataSource datasource = new ReportDataSource("DetalheItens_NF", D);
        rw.LocalReport.DataSources.Add(datasource);

        DataTable D2 = (DataTable)Session["dtUltimaOcorrencia"];
        ReportDataSource datasource2 = new ReportDataSource("DetalheItens_UltimaOcorrencia", D2);
        rw.LocalReport.DataSources.Add(datasource2);

        DataTable D3 = (DataTable)Session["dtOcorrencia"];
        ReportDataSource datasource3 = new ReportDataSource("DetalheItens_Ocorrencias", D3);
        rw.LocalReport.DataSources.Add(datasource3);

        DataTable D4 = (DataTable)Session["dtItens"];
        ReportDataSource datasource4 = new ReportDataSource("DetalheItens_ItensNF", D4);
        rw.LocalReport.DataSources.Add(datasource4);

        //decimal qtd = Convert.ToDecimal(D4.Compute("SUM(Quantidade)", ""));
        //ReportParameter par = new ReportParameter("qtd", qtd.ToString("N2"));

        //rw.LocalReport.SetParameters(new ReportParameter[] { par });        
        rw.LocalReport.ReportPath = @"reports\DetalheNF.rdlc";
        rw.LocalReport.Refresh();
        Panel1.Controls.Add(rw);
    }
}

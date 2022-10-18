using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Threading;
using System.Globalization;

/// <summary>
/// Summary description for cReport
/// </summary>
public class cReport
{
    public cReport()
    {
    }
    public Panel gerarReportKpi01(string NomeReport, DataTable dt, Panel pnlReport)
    {
        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        rw.Width = new Unit("100%");
        rw.Height = 600;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;
        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;
        ReportDataSource datasource = new ReportDataSource("dsKpi01_Portaria", dt);
        rw.LocalReport.DataSources.Add(datasource);
        rw.LocalReport.ReportPath = @"kpi\reports\" + NomeReport + ".rdlc";
        rw.LocalReport.EnableExternalImages = true;
        rw.LocalReport.Refresh();
        pnlReport.Controls.Add(rw);
        pnlReport.ScrollBars = ScrollBars.None;
        return pnlReport;

    }

    public Panel gerarPalletsRecebidos(string NomeReport, DataTable dt, Panel pnlReport)
    {
        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        rw.Width = new Unit("100%");
        rw.Height = 600;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;
        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;
        ReportDataSource datasource = new ReportDataSource("dsFaturamento_FAT", dt);
        rw.LocalReport.DataSources.Add(datasource);
        rw.LocalReport.ReportPath = @"Reports\rptFaturamentoPallets.rdlc";
        rw.LocalReport.EnableExternalImages = true;
        rw.LocalReport.Refresh();
        pnlReport.Controls.Add(rw);
        pnlReport.ScrollBars = ScrollBars.None;
        return pnlReport;

    }

    public Panel gerarLinhasExpedidas(string NomeReport, DataTable dt, Panel pnlReport)
    {
        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        rw.Width = new Unit("100%");
        rw.Height = 600;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;
        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;
        ReportDataSource datasource = new ReportDataSource("dsFaturamento_LINHAS_EXPEDIDAS", dt);
        rw.LocalReport.DataSources.Add(datasource);
        rw.LocalReport.ReportPath = @"Reports\rptFaturamentoLinhasExpedidas.rdlc";
        rw.LocalReport.EnableExternalImages = true;
        rw.LocalReport.Refresh();
        pnlReport.Controls.Add(rw);
        pnlReport.ScrollBars = ScrollBars.None;
        return pnlReport;

    }
}
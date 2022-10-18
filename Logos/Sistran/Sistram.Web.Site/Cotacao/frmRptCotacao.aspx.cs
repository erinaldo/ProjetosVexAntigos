using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using  Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class frmRptCotacao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GerarReport();

        }
    }

    protected void GerarReport()
    {
        //Panel1.Controls.Clear();
        //ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        //Panel1.Width = new Unit("100%");
        //rw.Width = new Unit("100%");
        //rw.Height = 600;
        //rw.BackColor = System.Drawing.Color.White;
        //rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //rw.BorderWidth = 0;
        //rw.ZoomMode = ZoomMode.PageWidth;
        //rw.LocalReport.DataSources.Clear();
        //rw.LocalReport.EnableHyperlinks = true;

        //DataTable dt = (DataTable)Session["dtContacao"];
        //DataTable dt2 = (DataTable)Session["dtcCondPagto"];

        //ReportDataSource datasource = new ReportDataSource("DScotacao_DataTable1", dt);
        //rw.LocalReport.DataSources.Add(datasource);

        //ReportDataSource datasource2 = new ReportDataSource("DScotacao_CotacaoFornecedorCondicaoPagamento", dt2);
        //rw.LocalReport.DataSources.Add(datasource2);

        //rw.LocalReport.ReportPath = @"reports\rptCotacao.rdlc";
        

        ////ReportParameter par = new ReportParameter("Titulo", "Relatório de Motoristas");
        ////rw.LocalReport.SetParameters(new ReportParameter[] { par });

        //rw.LocalReport.EnableExternalImages = true;
        //rw.LocalReport.Refresh();

        //////impressao pdf
        ////if (Request.QueryString["tipo"] == "PDF")
        ////{
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    Warning[] warnings;
        //    string[] streamids;
        //    byte[] exportBytes = rw.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = mimeType;
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Cotacao " + DateTime.Now.ToString() + "." + fileNameExtension);
        //    HttpContext.Current.Response.BinaryWrite(exportBytes);
        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();
        ////}
        ////else
        ////{
        ////    Panel1.Controls.Add(rw);
        ////}

    }


    protected void GerarReportVeiculo()
    {
        //Panel1.Controls.Clear();
        //ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        //Panel1.Width = new Unit("100%");
        //rw.Width = new Unit("100%");
        //rw.Height = 600;
        //rw.BackColor = System.Drawing.Color.White;
        //rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //rw.BorderWidth = 0;
        
        //rw.LocalReport.DataSources.Clear();
        //rw.LocalReport.EnableHyperlinks = true;

        //DataTable dt = (DataTable)Session["dtResult"];

        //ReportDataSource datasource = new ReportDataSource("DsVeiculo_Veiculo", dt);
        //rw.LocalReport.DataSources.Add(datasource);

        //rw.LocalReport.ReportPath = @"reports\rptVeiculos.rdlc";


        //ReportParameter par = new ReportParameter("Titulo", "Relatório de Veículos");
        //rw.LocalReport.SetParameters(new ReportParameter[] { par });

        //rw.LocalReport.EnableExternalImages = true;
        //rw.LocalReport.Refresh();

        //////impressao pdf
        //if (Request.QueryString["tipo"] == "PDF")
        //{
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    Warning[] warnings;
        //    string[] streamids;
        //    byte[] exportBytes = rw.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = mimeType;
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
        //    HttpContext.Current.Response.BinaryWrite(exportBytes);
        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();
        //}
        //else
        //{
        //    Panel1.Controls.Add(rw);
        //}

    }
}
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

public partial class frmRptDetalheNFS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lblTitulo.Text = Request.QueryString["titulo"];

        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
        rw.Width = new Unit("100%");
        rw.Height = 650;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;
        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;

        DataTable D = new DataTable();
        D = (DataTable)Session["dtReport"];

        ReportDataSource datasource = new ReportDataSource("dsdetalhenota_DataTable1", D);

        rw.LocalReport.DataSources.Add(datasource);
        ReportParameter par = new ReportParameter("nomeDoReports", Server.UrlDecode(Request.QueryString["titulo"].ToString()));
        rw.LocalReport.ReportPath = @"reports\rptDetalheNota.rdlc";
        rw.LocalReport.EnableExternalImages = true;
        rw.LocalReport.SetParameters(new ReportParameter[] { par });
        rw.LocalReport.Refresh();

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        string filename;

        byte[] bytes = rw.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
        filename = string.Format("{0}.{1}", "notas", "xls");
        Response.ClearHeaders();
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        Response.ContentType = mimeType;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End(); 
        //Panel1.Controls.Add(rw);
    }
}
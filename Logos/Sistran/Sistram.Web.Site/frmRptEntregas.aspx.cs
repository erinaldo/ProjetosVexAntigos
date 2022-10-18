using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using  Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class frmRptEntregas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
			SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO: " + Server.UrlDecode(Request.QueryString["tit"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
			ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
			rw.Width = new Unit("99%");
			rw.Height = 600;
			rw.BackColor = System.Drawing.Color.WhiteSmoke;
			rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
			rw.BorderWidth = 0;

			rw.LocalReport.DataSources.Clear();
			rw.LocalReport.EnableHyperlinks = true;
			DataTable D = (DataTable)Session["dt"];
			ReportDataSource datasource = new ReportDataSource("RptEntrega_NotaFiscalSaidaConsultar", D);


			rw.LocalReport.DataSources.Add(datasource);
			ReportParameter par = new ReportParameter("tit", Server.UrlDecode(Request.QueryString["tit"].ToString()));
			ReportParameter par1 = new ReportParameter("marca", ConfigurationSettings.AppSettings["marca"].ToString());
			rw.LocalReport.ReportPath = @"reports\rptEntrega.rdlc";
			rw.LocalReport.SetParameters(new ReportParameter[] { par, par1 });
			rw.LocalReport.Refresh();

			////impressao pdf
			if (Request.QueryString["tipo"] == "PDF")
			{
				string mimeType;
				string encoding;
				string fileNameExtension = ".xls";
				Warning[] warnings;
				string[] streamids;
				byte[] exportBytes = rw.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);


				HttpContext.Current.Response.Buffer = true;
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ContentType = mimeType;
				HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
				HttpContext.Current.Response.BinaryWrite(exportBytes);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			else
			{
			//anel1.Controls.Add(rw);

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
			}
		}
	}
}
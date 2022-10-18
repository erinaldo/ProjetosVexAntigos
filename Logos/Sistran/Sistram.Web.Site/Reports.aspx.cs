using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
//using Microsoft.Reporting.WebForms;

public partial class Reports : System.Web.UI.Page
{
    //protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    PnlReport.Controls.Clear();
    //    ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();

    //    rw.Width = new Unit("99%");
    //    rw.Height = 550;
    //    rw.BackColor = System.Drawing.Color.White;
    //    rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
    //    rw.BorderWidth = 0;               

    //    rw.LocalReport.DataSources.Clear();
    //    rw.LocalReport.EnableHyperlinks = true;

    //    DataTable D = new SistranBLL.Reports().ListaSimplesUsuario();

    //    ReportDataSource datasource = new ReportDataSource("DataSet3_DataTable1", D);
    //    rw.LocalReport.DataSources.Add(datasource);

    //    //rw.ZoomMode = ZoomMode.FullPage;
    //    //rw.ZoomPercent = new Unit("100%");
 
    //    switch (RadioButtonList2.SelectedValue)
    //    {
    //        case "1":
    //            rw.LocalReport.ReportPath = @"reports\rptListaUsuariordlc.rdlc";
    //            break;

    //        case "2":
    //            rw.LocalReport.ReportPath = @"reports\rptListaUsuarioDivisao.rdlc";
    //            break;

    //        case "3":
    //            rw.LocalReport.ReportPath = @"reports\rptListaUsuarioDivisaoProduto.rdlc";
    //            break;
    //    }

        
    //    rw.LocalReport.EnableExternalImages = true;       
    //    rw.LocalReport.Refresh();

    //    ////impressao pdf
    //    if (Request.QueryString["tipo"] == "PDF")
    //    {
    //        string mimeType;
    //        string encoding;
    //        string fileNameExtension;
    //        Warning[] warnings;
    //        string[] streamids;
    //        byte[] exportBytes = rw.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
    //        HttpContext.Current.Response.Buffer = true;
    //        HttpContext.Current.Response.Clear();
    //        HttpContext.Current.Response.ContentType = mimeType;
    //        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
    //        HttpContext.Current.Response.BinaryWrite(exportBytes);
    //        HttpContext.Current.Response.Flush();
    //        HttpContext.Current.Response.End();
    //    }
    //    else
    //    {
    //        PnlReport.Controls.Add(rw);
    //    }
            
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

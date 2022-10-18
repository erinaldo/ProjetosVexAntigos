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
////using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

public partial class frmRptImagem : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "VISUALIZOU A IMAGEM DA OCORRÊNCIA ", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

    //    ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();

    //    rw.Width = new Unit("100%");
    //    rw.Height = 650;
    //    rw.BackColor = System.Drawing.Color.WhiteSmoke;
    //    rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
    //    rw.BorderWidth = 0;

    //    //rw.ZoomMode = ZoomMode.Percent;

    //    rw.LocalReport.DataSources.Clear();
    //    rw.LocalReport.EnableHyperlinks = true;
    //    rw.LocalReport.ReportPath = @"reports\rptImagem.rdlc";


    //    ReportParameter parImage = new ReportParameter("parImage", String.Concat("file:///", Session["caminho"].ToString()));

    //    rw.LocalReport.EnableExternalImages = true;
    //    rw.LocalReport.SetParameters(new ReportParameter[] { parImage });

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
    //        Panel1.Controls.Add(rw);
    //    }
    //}
}
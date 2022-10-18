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
using System.Collections.Generic;
using SistranBLL;
using ChartDirector;
using System.IO;
using System.Drawing;

public partial class frmRptListDt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO DE DT", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
        
        //ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();

        //rw.Width = new Unit("100%");
        //rw.Height = 600;
        //rw.BackColor = System.Drawing.Color.WhiteSmoke;
        //rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //rw.BorderWidth = 0;
        
        //rw.LocalReport.DataSources.Clear();
        //rw.LocalReport.EnableHyperlinks = true;

        //DataTable dt_DT = (DataTable)Session["dt_DT"];

        //ReportDataSource datasource = new ReportDataSource("DT_DT", dt_DT);
        //rw.LocalReport.DataSources.Add(datasource);       
       
        //rw.LocalReport.ReportPath = @"reports\ListDt.rdlc";
        //rw.LocalReport.EnableExternalImages = true;
        //rw.LocalReport.Refresh();
        //Panel1.Controls.Add(rw);
    }
}

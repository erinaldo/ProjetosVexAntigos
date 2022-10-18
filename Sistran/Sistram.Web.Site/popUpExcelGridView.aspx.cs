using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;


public partial class popUpExcelGridView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        grdResultado.Enabled = true;
        grdResultado.DataSource = new SistranBLL.KPI_Irwin().GerarPlanilha(Session["mesano"].ToString(), Session["linha"].ToString(), "V2");
        grdResultado.DataBind();
        gerarExcel();
    }

    private void gerarExcel()
    {
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=kpi_irwin.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        form.Controls.Add(grdResultado);
        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }

}

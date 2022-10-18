using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class frmgdrExcelSobras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable DS = (DataTable)Session["dt"];


        RadGridUsuarios.DataSource = DS;
        RadGridUsuarios.DataBind();
        gerarExcel();
    }

    private void gerarExcel()
    {
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=evoCli.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        form.Controls.Add(RadGridUsuarios);
        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
}
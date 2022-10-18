using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
//using Microsoft.Reporting.WebForms;

public partial class ReportsVeic : System.Web.UI.Page
{
    //protected void GerarReport(string NomeDataTable, DataTable dt, string titulo)
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

    //    ReportDataSource datasource = new ReportDataSource("DsVeiculo_"+ NomeDataTable, dt);
    //    rw.LocalReport.DataSources.Add(datasource);

    //    switch (RadioButtonList2.SelectedIndex.ToString())
    //    {
    //        case "0":
    //            rw.LocalReport.ReportPath = @"reports\rptVeiculos.rdlc";
    //            break;

    //        case "1":
    //            rw.LocalReport.ReportPath = @"reports\rptMotoristas.rdlc";
    //            break;


    //        case "2":
    //            rw.LocalReport.ReportPath = @"reports\rptProprietario.rdlc";
    //            break;
    //    }

    //    ReportParameter par = new ReportParameter("Titulo", titulo);
    //    rw.LocalReport.SetParameters(new ReportParameter[] { par });

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
        if (!IsPostBack)
        {
            
        }

    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedIndex == 0)
            rblVeiculo.Visible = true;
        else
            rblVeiculo.Visible = false;

        if (RadioButtonList2.SelectedIndex == 1)
            rbMotorista.Visible = true;
        else
            rbMotorista.Visible = false;

        if (RadioButtonList2.SelectedIndex == 2)
        {
            {
                DataTable dt = new SistranBLL.Cadastro.Motorista().CarregarReportMotorista(rbMotorista.Items[0].Selected, rbMotorista.Items[2].Selected, rbMotorista.Items[4].Selected);
              //  GerarReport("Proprietario", dt, "RELATÓRIO DE PROPRIETÁRIOS");
            }

        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ////veiculo antt
        //if (RadioButtonList2.SelectedIndex == 0 && rblVeiculo.SelectedIndex==0)
        //{
        //    DataTable dt = new SistranBLL.Veiculo().GerarReportVeiculo(rblVeiculo.SelectedIndex == 0 ? true : false);
        //    GerarReport("Veiculo", dt, "RELATÓRIO DE VEÍCULOS COM ANTT VENCIDO");
        //}

        //// Motoristas
        //if (RadioButtonList2.SelectedIndex == 1 )
        //{
        //    DataTable dt = new SistranBLL.Cadastro.Motorista().CarregarReportMotorista(rbMotorista.Items[0].Selected, rbMotorista.Items[2].Selected, rbMotorista.Items[4].Selected);
        //    GerarReport("Motorista", dt, "RELATÓRIO DE MOTORISTAS");
        //}


    }
    protected void rblVeiculo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
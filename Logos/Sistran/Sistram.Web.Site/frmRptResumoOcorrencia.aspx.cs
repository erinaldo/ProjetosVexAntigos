using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using  Microsoft.Reporting.WebForms;
using System.Configuration;
using ChartDirector;
using System.IO;
using System.Drawing;

public partial class frmRptDesempenhoFilial : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO RESUMO POR OCORRÊNCIAS", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

    //        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
            
    //        rw.Width = new Unit("100%");
    //        rw.Height = 650;
    //        rw.BackColor = System.Drawing.Color.WhiteSmoke;
    //        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
    //        rw.BorderWidth = 0;
    //        rw.LocalReport.DataSources.Clear();
    //        rw.LocalReport.EnableHyperlinks = true;

    //        DataTable dtOcoGeral = new DataTable();
    //        dtOcoGeral = (DataTable)Session["dtOcoGeral"];

    //        DataTable dtOcoFilial = new DataTable();
    //        dtOcoFilial = (DataTable)Session["dtOcoFilial"];

    //        DataTable dtResp = new DataTable();
    //        dtResp = (DataTable)Session["dtResp"];
                 

    //        DataTable dtRespCliente = new DataTable();
    //        DataTable dtRespTransporte = new DataTable();

    //        dtRespCliente.Columns.Add(new DataColumn("CODIGO"));
    //        dtRespCliente.Columns.Add(new DataColumn("RESPONSABILIDADE"));
    //        dtRespCliente.Columns.Add(new DataColumn("NOME"));
    //        dtRespCliente.Columns.Add(new DataColumn("NOTAS"));

    //        dtRespTransporte.Columns.Add(new DataColumn("CODIGO"));
    //        dtRespTransporte.Columns.Add(new DataColumn("RESPONSABILIDADE"));
    //        dtRespTransporte.Columns.Add(new DataColumn("NOME"));
    //        dtRespTransporte.Columns.Add(new DataColumn("NOTAS"));
                        

    //        foreach (DataRow item in dtResp.Rows)
    //        {
    //            DataRow orw;
    //            if (item["RESPONSABILIDADE"].ToString() == "CLIENTE")
    //            {
    //                orw = dtRespCliente.NewRow();
    //                orw["CODIGO"] = item["CODIGO"];
    //                orw["RESPONSABILIDADE"] = item["RESPONSABILIDADE"];
    //                orw["NOME"] = item["NOME"];
    //                orw["NOTAS"] = Convert.ToInt32(item["NOTAS"]);
    //                dtRespCliente.Rows.Add(orw);

    //            }
    //            else
    //            {
    //                orw = dtRespTransporte.NewRow();
    //                orw["CODIGO"] = item["CODIGO"];
    //                orw["RESPONSABILIDADE"] = item["RESPONSABILIDADE"];
    //                orw["NOME"] = item["NOME"];
    //                orw["NOTAS"] = Convert.ToInt32(item["NOTAS"]);
    //                dtRespTransporte.Rows.Add(orw);
    //            }
                
    //        }

    //        DataTable dtRespFiliais = new DataTable();
    //        dtRespFiliais = (DataTable)Session["dtRespFiliais"];

    //        DataTable dtRespClienteFilial = new DataTable();
    //        DataTable dtRespTransporteFilial = new DataTable();

    //        dtRespClienteFilial.Columns.Add(new DataColumn("IDFILIAL"));
    //        dtRespClienteFilial.Columns.Add(new DataColumn("RESPONSABILIDADE"));
    //        dtRespClienteFilial.Columns.Add(new DataColumn("NOME"));
    //        dtRespClienteFilial.Columns.Add(new DataColumn("NOTAS"));

    //        dtRespTransporteFilial.Columns.Add(new DataColumn("IDFILIAL"));
    //        dtRespTransporteFilial.Columns.Add(new DataColumn("RESPONSABILIDADE"));
    //        dtRespTransporteFilial.Columns.Add(new DataColumn("NOME"));
    //        dtRespTransporteFilial.Columns.Add(new DataColumn("NOTAS"));

    //        foreach (DataRow item in dtRespFiliais.Rows)
    //        {
    //            DataRow orw;
    //            if (item["RESPONSABILIDADE"].ToString() == "CLIENTE")
    //            {
    //                orw = dtRespClienteFilial.NewRow();
    //                orw["IDFILIAL"] = item["IDFILIAL"];
    //                orw["RESPONSABILIDADE"] = item["RESPONSABILIDADE"];
    //                orw["NOME"] = item["NOME"];
    //                orw["NOTAS"] = Convert.ToInt32(item["NOTAS"]);
    //                dtRespClienteFilial.Rows.Add(orw);

    //            }
    //            else
    //            {
    //                orw = dtRespTransporteFilial.NewRow();
    //                orw["IDFILIAL"] = item["IDFILIAL"];
    //                orw["RESPONSABILIDADE"] = item["RESPONSABILIDADE"];
    //                orw["NOME"] = item["NOME"];
    //                orw["NOTAS"] = Convert.ToInt32(item["NOTAS"]);
    //                dtRespTransporteFilial.Rows.Add(orw);
    //            }

    //        }



    //        ReportDataSource datasourcedtOcoGeral = new ReportDataSource("ResumoOcorrencia_oco", dtOcoGeral);
    //        ReportDataSource datasourcedtOcoFilial = new ReportDataSource("ResumoOcorrencia_Filial", dtOcoFilial);

    //        ReportDataSource datasourcedtOcoRespCliente = new ReportDataSource("ResumoOcorrencia_FilialResponsavelCliente", dtRespCliente);
    //        ReportDataSource datasourcedtOcoRespTransporte = new ReportDataSource("ResumoOcorrencia_FilialResponsavelTransporte", dtRespTransporte);

    //        ReportDataSource datasourcedtOcoRespClienteFilial = new ReportDataSource("ResumoOcorrencia_FilialResponsavelClienteFilial", dtRespClienteFilial);
    //        ReportDataSource datasourcedtOcoRespTransporteFilial = new ReportDataSource("ResumoOcorrencia_FilialResponsavelTransporteFilial", dtRespTransporteFilial);
            

    //        rw.LocalReport.DataSources.Add(datasourcedtOcoGeral);
    //        rw.LocalReport.DataSources.Add(datasourcedtOcoFilial);
    //        rw.LocalReport.DataSources.Add(datasourcedtOcoRespCliente);
    //        rw.LocalReport.DataSources.Add(datasourcedtOcoRespTransporte);

    //        rw.LocalReport.DataSources.Add(datasourcedtOcoRespClienteFilial);
    //        rw.LocalReport.DataSources.Add(datasourcedtOcoRespTransporteFilial);

    //        ReportParameter par = new ReportParameter("tit", Server.UrlDecode(Request.QueryString["tit"].ToString()));
    //        ReportParameter par1 = new ReportParameter("marca", ConfigurationSettings.AppSettings["marca"].ToString());
    //        rw.LocalReport.ReportPath = @"reports\ResumoOcorrencia.rdlc";

    //        string caminhoGeral = PrepararGraficoGeral(Guid.NewGuid().ToString(), dtOcoGeral);
    //        ReportParameter parImage = new ReportParameter("parImage", String.Concat("file:///", caminhoGeral));

    //        string caminhoGeralFilial = PrepararGeralFilial(Guid.NewGuid().ToString(), dtOcoFilial);
    //        ReportParameter parImageGeralFilial = new ReportParameter("parImageGeralFilial", String.Concat("file:///", caminhoGeralFilial));

    //        string caminhoOcoRespCliente = PrepararRespCliente(Guid.NewGuid().ToString(), dtRespCliente);
    //        ReportParameter parImageOcoRespCliente = new ReportParameter("parImageOcoRespCliente", String.Concat("file:///", caminhoOcoRespCliente));

    //        string caminhoOcoRespTranporte = PrepararRespTranporte(Guid.NewGuid().ToString(), dtRespTransporte);
    //        ReportParameter parImageOcoRespTransporte = new ReportParameter("parImageOcoRespTransporte", String.Concat("file:///", caminhoOcoRespTranporte));

    //        string caminhodtRespClienteFilial = PrepararRespClienteFilial(Guid.NewGuid().ToString(), dtRespClienteFilial);
    //        ReportParameter parImageRespClienteFilial = new ReportParameter("parImageRespClienteFilial", String.Concat("file:///", caminhodtRespClienteFilial));

    //        string caminhodtRespTransporteFilial = PrepararRespTransporteFilial(Guid.NewGuid().ToString(), dtRespTransporteFilial);
    //        ReportParameter parImageRespTransporteFilial = new ReportParameter("parImageRespTransporteFilial", String.Concat("file:///", caminhodtRespTransporteFilial));

           

    //        rw.LocalReport.EnableExternalImages = true;
    //        rw.LocalReport.SetParameters(new ReportParameter[] { par, par1, parImage, parImageGeralFilial, parImageOcoRespCliente, parImageOcoRespTransporte, parImageRespClienteFilial, parImageRespTransporteFilial });

    //        rw.LocalReport.Refresh();

    //        ////impressao pdf
    //        if (Request.QueryString["tipo"] == "PDF")
    //        {
    //            string mimeType;
    //            string encoding;
    //            string fileNameExtension;
    //            Warning[] warnings;
    //            string[] streamids;
    //            byte[] exportBytes = rw.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
    //            HttpContext.Current.Response.Buffer = true;
    //            HttpContext.Current.Response.Clear();
    //            HttpContext.Current.Response.ContentType = mimeType;
    //            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
    //            HttpContext.Current.Response.BinaryWrite(exportBytes);
    //            HttpContext.Current.Response.Flush();
    //            HttpContext.Current.Response.End();
    //        }
    //        else
    //        {
    //            Panel1.Controls.Add(rw);
    //        }
            
    //    }
    //}

    public string PrepararGraficoGeral(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.GerarGraficosGeral(dt);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }   

    public string PrepararGeralFilial(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.GerarGraficosGeralFilial(dt);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string PrepararRespCliente(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.GraficoGeralResponsabilidade(dt, "CLIENTE");
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string PrepararRespTranporte(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.GraficoGeralResponsabilidade(dt, "TRANSPORTE");
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string PrepararRespClienteFilial(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.MontarGraficoFilialResponsavel(dt, "CLIENTE");
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string PrepararRespTransporteFilial(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        wv = cGraficos.MontarGraficoFilialResponsavel(dt, "TRANSPORTE");
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }
}
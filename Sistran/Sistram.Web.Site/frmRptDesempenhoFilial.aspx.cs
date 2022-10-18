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
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        //    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO DESEMPENHO POR FILIAL", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        //       ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();
            
        //    rw.Width = new Unit("100%");
        //    rw.Height = 650;
        //    rw.BackColor = System.Drawing.Color.WhiteSmoke;
        //    rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //    rw.BorderWidth = 0;            
        //    rw.LocalReport.DataSources.Clear();
        //    rw.LocalReport.EnableHyperlinks = true;
        //    DataTable D = new DataTable();
        //    D = (DataTable)Session["dt"];
                        
        //    ReportDataSource datasource = new ReportDataSource("DesempenhoFilial_Filial", D);

        //    rw.LocalReport.DataSources.Add(datasource);
        //    ReportParameter par = new ReportParameter("tit", Server.UrlDecode(Request.QueryString["tit"].ToString()));
        //    ReportParameter par1 = new ReportParameter("marca", ConfigurationSettings.AppSettings["marca"].ToString());
        //    rw.LocalReport.ReportPath = @"reports\DesempenhoFilial.rdlc";

        //    string caminho = f(DateTime.Now.ToString("yyyymmyyMMss"), D) ;
        //    ReportParameter parImage = new ReportParameter("parImage", String.Concat("file:///", caminho));

        //    string caminho2 = f2(DateTime.Now.ToString("yyyymmyyMMss") +"linhas" , D) ;
        //    ReportParameter parImage2 = new ReportParameter("parImage2", String.Concat("file:///", caminho2));


        //    rw.LocalReport.EnableExternalImages = true;
        //    rw.LocalReport.SetParameters(new ReportParameter[] { par, par1, parImage, parImage2 });



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
        //        //Panel1.Controls.Add(rw);

        //        Warning[] warnings;
        //        string[] streamids;
        //        string mimeType;
        //        string encoding;
        //        string extension;
        //        string filename;

        //        byte[] bytes = rw.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
        //        filename = string.Format("{0}.{1}", "notas", "xls");
        //        Response.ClearHeaders();
        //        Response.Clear();
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        //        Response.ContentType = mimeType;
        //        Response.BinaryWrite(bytes);
        //        Response.Flush();
        //        Response.End(); 
        //    }
            
        //}
    }

    public string f(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();

        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);

       double totalEntregue = Convert.ToDouble(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));
       double totalNaoEntregue = Convert.ToDouble(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", ""));

        wv = Grafico(dt, totalEntregue, totalNaoEntregue) ;
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string f2(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();
        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);
        //Graficos graf = new Graficos();
        //int evolucao = Convert.ToInt32(new EvolucaoBLL().SelectLastEvolucaoCliente(obra)[0]);
        wv = Grafico2(dt);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    //pizza
    private WebChartViewer Grafico(DataTable table, double totalEntregue, double totalNaoEntregue)
    {
        MultiChart chart = new MultiChart(400, 400);
        //        MultiChart chart = new MultiChart(300, 300);
        double[] numArray = new double[table.Rows.Count];
        double[] data = new double[table.Rows.Count];
        double[] numArray3 = new double[table.Rows.Count];
        string[] labels = new string[table.Rows.Count];
        string[] texts = new string[table.Rows.Count];
        int[] colors = new int[table.Rows.Count];
        int sectorNo = 0;
        int index = 0;




        foreach (DataRow row in table.Rows)
        {
            double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAIS_ENTREGUE"]) + Convert.ToDouble(row["NOTASFISCAIS_NAO_ENTREGUE"]));
            double b = totalEntregue + totalNaoEntregue;
            double c = a / b;
            numArray3[index] = c;
            labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
            colors[index] = 0x33868e;
            index++;
        }

        //pizza
        //PieChart chart3 = new PieChart(300, 300);
        PieChart chart3 = new PieChart(520, 540);


        chart3.setPieSize(160, 80, 70);
        ///// chart3.setPieSize(160, 135, 110);

        chart3.set3D(20);
        chart3.setStartAngle(160.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("arialb.ttf", 6.5);

        chart3.setColors(Chart.transparentPalette);

        chart3.setExplode(sectorNo);
        chart.addChart(5, 30, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    //barras
    private WebChartViewer Grafico2(DataTable table)
    {
        MultiChart chart = new MultiChart(600, 600);
        double[] numArray = new double[table.Rows.Count];
        double[] data = new double[table.Rows.Count];
        double[] numArray3 = new double[table.Rows.Count];
        string[] labels = new string[table.Rows.Count];
        string[] texts = new string[table.Rows.Count];
        int[] colors = new int[table.Rows.Count];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(table.Compute("Max(LEADTIME)", ""));
        int index = 0;
        decimal itac = Convert.ToDecimal("0.00");

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["LEADTIME"]);
            data[index] = Convert.ToDouble(row["LEADTIME"].ToString());
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = row["NOMEDAFILIAL"].ToString();
            texts[index] = row["LEADTIME"].ToString();
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }


        XYChart c = new XYChart(500, 540);
        c.addExtraField(texts);
        c.setPlotArea(100, 5, 340, 320, 0xf8f8f8, 0xffffff);

        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("arial.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("", "arialb.ttf", 7.0);
        c.xAxis().setTitle("", "arialb.ttf", 6.0);
        c.xAxis().setLabelStyle("arialb.ttf", 5.0, 0);
        chart.addChart(5, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }
}
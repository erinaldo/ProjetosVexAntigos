using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using  Microsoft.Reporting.WebForms;
using System.Configuration;
using ChartDirector;
using System.IO;
using System.Drawing;

public partial class frmRptDesempenhoPorDia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "GEROU O RELATÓRIO DESEMPENHO POR DIA", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

            ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();

            rw.Width = new Unit("100%");
            rw.Height = 650;
            rw.BackColor = System.Drawing.Color.WhiteSmoke;
            rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            rw.BorderWidth = 0;
            rw.LocalReport.DataSources.Clear();
            rw.LocalReport.EnableHyperlinks = true;

            decimal total = Convert.ToDecimal(0);
            decimal itac = Convert.ToDecimal("0.00");
            decimal calc = Convert.ToDecimal("0.00");

            DataTable D = new DataTable();
            D = (DataTable)Session["dt"];

            total = Convert.ToDecimal(D.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDecimal(D.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""));


            //total = Convert.ToDecimal(0);
            itac = Convert.ToDecimal("0.00");
            calc = Convert.ToDecimal("0.00");
            for (int i = 0; i < D.Rows.Count; i++)
            {
                decimal it = Convert.ToDecimal(D.Rows[i]["NOTASFISCAISENTREGUE"]);

                if (total > 0)
                    calc = (it / total) * 100;

                itac += calc;
                D.Rows[i]["perc"] = calc.ToString("N2");
                D.Rows[i]["ACUMUL"] = itac.ToString("N2");
            }

            DataTable dtNaoEntregues = (DataTable)Session["dtNaoEntregues"];

            try
            {
                dtNaoEntregues.Columns.Add(new DataColumn("perc"));
                dtNaoEntregues.Columns.Add(new DataColumn("acumul"));
            }
            catch (Exception)
            {
            }

            //total = Convert.ToDecimal(0);
            //itac = Convert.ToDecimal("0.00");
            //calc = Convert.ToDecimal("0.00");
            for (int i = 0; i < dtNaoEntregues.Rows.Count; i++)
            {
                decimal it = Convert.ToDecimal(dtNaoEntregues.Rows[i]["NOTAS"]);

                if (total > 0)
                    calc = (it / total) * 100;

                itac += calc;
                dtNaoEntregues.Rows[i]["perc"] = calc.ToString("N2");
                dtNaoEntregues.Rows[i]["acumul"] = itac.ToString("N2");
            }

            ReportDataSource datasource = new ReportDataSource("DesempenhoDia_Dia", D);
            ReportDataSource datasourceNaoEntregue = new ReportDataSource("DesempenhoDia_DocumentoFilial", dtNaoEntregues);

            rw.LocalReport.DataSources.Add(datasource);
            rw.LocalReport.DataSources.Add(datasourceNaoEntregue);

            ReportParameter par = new ReportParameter("tit", Server.UrlDecode(Request.QueryString["tit"].ToString()));
            ReportParameter par1 = new ReportParameter("marca", ConfigurationSettings.AppSettings["marca"].ToString());
            rw.LocalReport.ReportPath = @"reports\DesempenhoDia.rdlc";

            string caminho = f(DateTime.Now.ToString("yyyymmyyMMss"), D);
            ReportParameter parImage = new ReportParameter("parImage", String.Concat("file:///", caminho));

            string caminho2 = f2(DateTime.Now.ToString("yyyymmyyMMss") + "linhas", D);
            ReportParameter parImage2 = new ReportParameter("parImage2", String.Concat("file:///", caminho2));

            string caminho3 = f3(DateTime.Now.ToString("yyyymmyyMMss") + "9linhas", dtNaoEntregues);
            ReportParameter parImage3 = new ReportParameter("parImage3", String.Concat("file:///", caminho3));


            rw.LocalReport.EnableExternalImages = true;
            rw.LocalReport.SetParameters(new ReportParameter[] { par, par1, parImage, parImage2, parImage3 });


            rw.LocalReport.Refresh();

            ////impressao pdf
            if (Request.QueryString["tipo"] == "PDF")
            {
                string mimeType;
                string encoding;
                string fileNameExtension;
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
                Panel1.Controls.Add(rw);
            }

        }
    }

    private string f3(string nomeArk, DataTable dtNaoEntregues)
    {
        WebChartViewer wv = new WebChartViewer();

        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);
        wv = GraficoNaoEntregue(dtNaoEntregues);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }
    private WebChartViewer GraficoNaoEntregue(DataTable table)
    {
        MultiChart chart = new MultiChart(400, 250);
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(Convert.ToDouble(table.Compute("SUM(notas)", "")));

        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        decimal total;

        total = Convert.ToDecimal(Convert.ToDouble(table.Compute("SUM(notas)", "")));

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["notas"]);

            data[index] = Convert.ToDouble(it);
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = (row["SITUACAO"].ToString() == "AGUARDANDO SOLUCAO" ? "COM OCORRÊNCIA" : row["SITUACAO"].ToString()).ToString().Replace(" ", "<*BR*>");
            texts[index] = it.ToString();
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }

            index++;
        }

        XYChart c = new XYChart(400, 300);
        c.addExtraField(texts);
        c.setPlotArea(130, 5, 250, 220, 0xf8f8f8, 0xffffff);
        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("%", "verdana.ttf", 7.0);
        c.xAxis().setTitle("", "verdana.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);
        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;

    }

    public string f(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();

        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);
        wv = Grafico(dt) ;
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string f2(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();     
        wv = Grafico2(dt);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }


    //pizza
    private WebChartViewer Grafico(DataTable table)
    {
        MultiChart chart = new MultiChart(400, 400);        
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        int index = 0;

        foreach (DataRow row in table.Rows)
        {
            numArray3[index] = (Convert.ToDouble(row["NOTASFISCAISENTREGUE"]) / (Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""))) * 100);
            labels[index] = row["PRAZOUTILIZADO"].ToString();
            colors[index] = 0x33868e;
            index++;
        }

        numArray3[index] = (Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")) / (Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", ""))) * 100);
        labels[index] = "N.F. Não Entregue<*BR*>";
        colors[index] = 0x33868e;

        //pizza
        //PieChart chart3 = new PieChart(300, 300);
        PieChart chart3 = new PieChart(530, 540);


        chart3.setPieSize(185, 70, 80);
        ///// chart3.setPieSize(160, 135, 110);

        chart3.set3D(20);
        chart3.setStartAngle(160.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);

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
        MultiChart chart = new MultiChart(400, 400);
        double[] numArray = new double[table.Rows.Count + 1];
        double[] data = new double[table.Rows.Count + 1];
        double[] numArray3 = new double[table.Rows.Count + 1];
        string[] labels = new string[table.Rows.Count + 1];
        string[] texts = new string[table.Rows.Count + 1];
        int[] colors = new int[table.Rows.Count + 1];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")));

        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        double calc = 0.0;
        decimal total;

        total = Convert.ToDecimal(Convert.ToDouble(table.Compute("SUM(NOTASFISCAISENTREGUE)", "")) + Convert.ToDouble(table.Compute("SUM(NOTASNAOFISCAISENTREGUE)", "")));

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["NOTASFISCAISENTREGUE"]);

            if (total > 0)
                calc = Convert.ToDouble((it / total) * 100);

            itac += Convert.ToDecimal(calc);
            data[index] = Convert.ToDouble(itac);
            numArray3[index] = Convert.ToDouble(itac);
            labels[index] = row["PRAZOUTILIZADO"].ToString();
            texts[index] = itac.ToString("N2") + "%";
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }

            index++;
        }

        itac += Convert.ToDecimal(calc);
        data[index] = Convert.ToDouble((100 - itac));
        numArray3[index] = Convert.ToDouble(itac);
        labels[index] = "N.F. Não <*br*>Entregue";
        texts[index] = (100 - itac).ToString("N2") + "%";
        //colors[index] = 0x33868e;        
        num2 = numArray3[index];
        sectorNo = index;


        XYChart c = new XYChart(400, 440);
        c.addExtraField(texts);
        c.setPlotArea(50, 5, 300, 340, 0xf8f8f8, 0xffffff);
        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("%", "verdana.ttf", 7.0);
        c.xAxis().setTitle("TRANSIT TIME", "verdana.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);
        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }
  
}
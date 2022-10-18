using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using SistranBLL;
using ChartDirector;
using System.IO;
using System.Drawing;

public partial class frmRptHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SistranBLL.Usuario.LogBDBLL.GravarLog(1, "PHP", "Gerou Relatorio", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

     
        Label1.Text = Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]);

        ReportViewer rw = new Microsoft.Reporting.WebForms.ReportViewer();

        rw.Width =new Unit("100%");
        rw.Height =600;
        rw.BackColor = System.Drawing.Color.WhiteSmoke;
        rw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        rw.BorderWidth = 0;


        DateTime? ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime? fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        rw.LocalReport.DataSources.Clear();
        rw.LocalReport.EnableHyperlinks = true;
        DataTable dtAgEmb = NotasFiscais.ListarHomeAguardandoEmbarque(Label1.Text, "", FuncoesGerais.LoadDataSetConstantes("ConstDcoFilSitAguardEmbarque"), ini, fim);
        DataTable dtAgEmbFilial = NotasFiscais.ListarHomeAguardandoEmbarqueFilial(Label1.Text, "", FuncoesGerais.LoadDataSetConstantes("ConstDcoFilSitAguardEmbarque"), ini, fim);
        DataTable dtEmb = NotasFiscais.ListarHomeNotasFiscaisEmbarcadas(Label1.Text, "", FuncoesGerais.LoadDataSetConstantes("ConstDcoFilSitMercadoriaEmbarcada"), ini, fim);
        DataTable dtOcorr = NotasFiscais.ListarHomeNotasFiscaisComOcorrencias(Label1.Text, "", ini, fim, false, false);



        ReportDataSource datasource = new ReportDataSource("Home_AguardandoEmbarque", dtAgEmb);
        rw.LocalReport.DataSources.Add(datasource);

        ReportDataSource datasource2 = new ReportDataSource("Home_MercadoriaEmbarcada", dtEmb);
        rw.LocalReport.DataSources.Add(datasource2);

        ReportDataSource datasource3 = new ReportDataSource("Home_Ocorrencia", dtOcorr);
        rw.LocalReport.DataSources.Add(datasource3);

        ReportDataSource datasource4 = new ReportDataSource("Home_Filial", dtAgEmbFilial);
        rw.LocalReport.DataSources.Add(datasource4);

        
        rw.LocalReport.ReportPath = @"reports\home.rdlc";

        string caminho = f(DateTime.Now.ToString("yyyymmyyMMss"), dtAgEmbFilial);
        ReportParameter parImage = new ReportParameter("parImage", String.Concat("file:///", caminho));

        string caminho2 = f2(DateTime.Now.ToString("yyyymmyyMMss") + "linhas", dtAgEmb, dtEmb, dtOcorr);
        ReportParameter parImage2 = new ReportParameter("parImage2", String.Concat("file:///", caminho2));


        rw.LocalReport.EnableExternalImages = true;
        rw.LocalReport.SetParameters(new ReportParameter[] {parImage, parImage2});
        
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

    private WebChartViewer GrafAguardandoEmbarque(DataTable table)
    {        
        int quantidade = 0;
        string nomeFilial = "";

        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOME"].ToString())
            {
                quantidade++;
                nomeFilial = row["NOME"].ToString();
            }
        }


        MultiChart chart = new MultiChart(500, 220);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;
        int index = 0;
        nomeFilial = "";

        double b = Convert.ToDouble(table.Compute("SUM(Notas)", ""));
        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOME"].ToString())
            {
                double a = Convert.ToDouble(Convert.ToDouble(row["Notas"]));
                double c = a / b;
                numArray3[index] = c;
                labels[index] = row["NOME"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOME"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(500, 450);
        chart3.setPieSize(240, 90, 90);

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);
        chart3.setColors(Chart.transparentPalette);
        chart3.setExplode(sectorNo);
        chart.addChart(5, 5, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    private WebChartViewer GrafGeral(DataTable dtAgEmb, DataTable dtEmbarc, DataTable dtOcorrencia)
    {
        int  totalAguardEmbarque , totalComOcorrencia, totalEmbarcadas=0;

        if (dtAgEmb.Rows.Count > 0)
            totalAguardEmbarque = Convert.ToInt32(dtAgEmb.Compute("sum(Notas)", ""));
        else
            totalAguardEmbarque = 0;

        if (dtOcorrencia.Rows.Count > 0)
            totalComOcorrencia = Convert.ToInt32(dtOcorrencia.Compute("sum(Notas)", ""));
        else
            totalComOcorrencia = 0;

        if (dtEmbarc.Rows.Count > 0)
            totalEmbarcadas = Convert.ToInt32(dtEmbarc.Compute("sum(Notas)", ""));
        else
            totalEmbarcadas = 0;


        int quantidade = 3;

        MultiChart chart = new MultiChart(500, 220);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;    


        double b = Convert.ToDouble(totalAguardEmbarque + totalComOcorrencia + totalEmbarcadas);
        double a = Convert.ToDouble(totalAguardEmbarque);
        double c = a / b;
        numArray3[0] = c;
        labels[0] = "Aguardando" + "<*BR*>" + "Embarque" + "<*BR*>";
        colors[0] = 0x33868e;

        // b = Convert.ToDouble(totalAguardEmbarque + totalComOcorrencia + totalEmbarcadas);
        a = Convert.ToDouble(totalComOcorrencia);
        c = a / b;
        numArray3[1] = c;
        labels[1] = "Com" + "<*BR*>" + "Ocorrência" + "<*BR*>";
        colors[1] = 0x33868e;

        a = Convert.ToDouble(totalEmbarcadas);
        c = a / b;
        numArray3[2] = c;
        labels[2] = "Embarcadas" + "<*BR*>";
        colors[2] = 0x33868e;



        //pizza        
        PieChart chart3 = new PieChart(500, 450);
        chart3.setPieSize(240, 90, 90);

        chart3.set3D(20);
        chart3.setStartAngle(170.0);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setData(numArray3, labels);
        chart3.setLabelStyle("verdana.ttf", 6.5);
        chart3.setColors(Chart.transparentPalette);
        chart3.setExplode(sectorNo);
        chart.addChart(15, 30, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    public string f(string nomeArk, DataTable dt)
    {
        WebChartViewer wv = new WebChartViewer();

        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);

        //double totalEntregue = Convert.ToDouble(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));
        //double totalNaoEntregue = Convert.ToDouble(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", ""));

        wv = GrafAguardandoEmbarque(dt);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

    public string f2(string nomeArk, DataTable dtAgEmb, DataTable dtEmbarc, DataTable dtOcorrencia)
    {
        WebChartViewer wv = new WebChartViewer();
        int obra = 0;
        obra = Convert.ToInt32(Request["pob_cod"]);
        //Graficos graf = new Graficos();
        //int evolucao = Convert.ToInt32(new EvolucaoBLL().SelectLastEvolucaoCliente(obra)[0]);
        wv = GrafGeral(dtAgEmb, dtEmbarc, dtOcorrencia);
        byte[] v = wv.Image.image;
        MemoryStream ms = new MemoryStream(v);
        Bitmap m = new Bitmap(ms);
        m.Save(Server.MapPath("~\\imgReport\\" + nomeArk + ".png"));
        return Server.MapPath("~\\imgReport\\" + nomeArk + ".png");
    }

}

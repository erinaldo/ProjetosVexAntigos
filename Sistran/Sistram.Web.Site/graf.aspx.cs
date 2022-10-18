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
using ChartDirector;

public partial class graf : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Panel1.Controls.Add((WebChartViewer)Grafico());
    }

    private WebChartViewer Grafico()
    {
        MultiChart chart = new MultiChart(960, 350);
        double[] numArray = new double[5];
        double[] data = new double[5];
        double[] numArray3 = new double[5];
        string[] labels = new string[5];
        string[] texts = new string[5];
        int[] colors = new int[5];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(200);
        double num4 = Convert.ToDouble(10);
        int index = 0;

        //foreach (DataRow row in table.Rows)
        //{

        for (int i = 0; i < 5; i++)
        {
            numArray[index] = i ;
            data[index] = Convert.ToDouble(i);
            numArray3[index] = (i + DateTime.Now.Millisecond);
            labels[index] = "Elem." + i.ToString();
            texts[index] = (i + DateTime.Now.Millisecond).ToString();
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }
        //}

        XYChart c = new XYChart(400, 440);
        c.addExtraField(texts);
        c.addTitle(7, "Grafico Teste", "arialbi.ttf", 16.0);
        c.setPlotArea(120, 40, 250, 240, 0xf8f8f8, 0xffffff);
        
        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("arialbi.ttf", 9.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("x", "arialb.ttf", 9.0);
        c.xAxis().setTitle("y", "arialb.ttf", 9.0);
        c.xAxis().setLabelStyle("arialb.ttf", 9.0, 0);
        chart.addChart(0, 0, c);


        //pizza
        PieChart chart3 = new PieChart(520, 270);
        chart3.addTitle("PIZZA", "arialbi.ttf", 15.0);
        chart3.setPieSize(250, 0x87, 110);
        chart3.set3D(20);
        chart3.setLabelLayout(0);
        chart3.setLabelStyle().setBackground(-65529, -16777216, Chart.glassEffect());
        chart3.setLineColor(-65529, 0);
        chart3.setStartAngle(135.0);
        chart3.setData(numArray3, labels);
        chart3.setColors(Chart.transparentPalette);
        chart3.setExplode(sectorNo);
        chart.addChart(420, 30, chart3);
       // chart.addTitle(1, "Total: " + "100000" + " Empreendimentos ou " + "200000" + " UHs.", "arialbi.ttf", 9.0);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }
}
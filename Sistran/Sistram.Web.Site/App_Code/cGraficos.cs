using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChartDirector;
using System.Data;


public static class cGraficos
{  
    public static WebChartViewer GerarGraficosGeralFilial(DataTable table)
    {
        //definir as quantidades
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


        MultiChart chart = new MultiChart(500, 500);
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
                labels[index] = row["NOME"].ToString() + "<*BR*>" + row["Notas"].ToString() + " N.F's " + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOME"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(500, 500);
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
        
    public static WebChartViewer GerarGraficosGeral(DataTable table)
    {
        MultiChart chart = new MultiChart(600, (table.Rows.Count>40?1000:700));
        double[] numArray = new double[table.Rows.Count];
        double[] data = new double[table.Rows.Count];
        double[] numArray3 = new double[table.Rows.Count];
        string[] labels = new string[table.Rows.Count];
        string[] texts = new string[table.Rows.Count];
        int[] colors = new int[table.Rows.Count];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(table.Compute("SUM(Notas)", ""));
        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        foreach (DataRow row in table.Rows)
        {
            string x = "Sem Descrição";
            if (( row["NOME"].ToString().Trim()).Length > 5)
            {

                x = row["CODIGO"].ToString().Trim() + "-" + row["NOME"].ToString().Trim().Replace("TRANSPORTADORA", "TRANSP.").Substring(0, 4).Trim();
            }
            x = x.Replace(" ", "<*BR*>");
            decimal it = Convert.ToDecimal(row["Notas"]);
            data[index] = Convert.ToDouble(row["Notas"].ToString());
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = x;
            texts[index] = Convert.ToDouble(row["Notas"].ToString()).ToString("#0");
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }


        XYChart c = new XYChart(600, (table.Rows.Count > 40 ? 1000 : 700));
        c.addExtraField(texts);

        if (table.Rows.Count > 40)
        {
            c.setPlotArea(150, 5, 400, 700, 0xf8f8f8, 0xffffff);
        }
        else
        {
            c.setPlotArea(150, 5, 400, 450, 0xf8f8f8, 0xffffff);
        }

        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("Quantidade de Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setTitle("Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);

        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    public static WebChartViewer GraficoGeral(DataTable table)
    {
        MultiChart chart = new MultiChart(600, 700);
        double[] numArray = new double[table.Rows.Count];
        double[] data = new double[table.Rows.Count];
        double[] numArray3 = new double[table.Rows.Count];
        string[] labels = new string[table.Rows.Count];
        string[] texts = new string[table.Rows.Count];
        int[] colors = new int[table.Rows.Count];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(table.Compute("SUM(Notas)", ""));
        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
        foreach (DataRow row in table.Rows)
        {
            string x = row["CODIGO"].ToString().Trim() + "-" + row["NOME"].ToString().Trim().Replace("TRANSPORTADORA", "TRANSP.").Substring(0, 4).Trim();

            //string x = row["CODIGO"].ToString().Trim() + "-" + row["NOME"].ToString().Trim().Replace("TRANSPORTADORA", "TRANSP.").Substring(0, 4).Trim();


            //if (x.Length > 20)
            //    x = x.Substring(0, 19) + "...";

            x = x.Replace(" ", "<*BR*>");

            decimal it = Convert.ToDecimal(row["Notas"]);
            data[index] = Convert.ToDouble(row["Notas"].ToString());
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = x;
            texts[index] = Convert.ToDouble(row["Notas"].ToString()).ToString("#0");
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }


        XYChart c = new XYChart(600, 800);
        c.addExtraField(texts);
        c.setPlotArea(150, 5, 430, 500, 0xf8f8f8, 0xffffff);

        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("Quantidade de Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setTitle("Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);

        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    public static WebChartViewer GraficoGeralResponsabilidade(DataTable table, string tipo)
    {
        DataRow[] row = table.Select("RESPONSABILIDADE='" + tipo + "'");
        int tot = 0;
        for (int i = 0; i < row.Length; i++)
        {
            tot += Convert.ToInt32(row[i]["Notas"]);
        }


        MultiChart chart = new MultiChart(460, 400);
        double[] numArray = new double[row.Length];
        double[] data = new double[row.Length];
        double[] numArray3 = new double[row.Length];
        string[] labels = new string[row.Length];
        string[] texts = new string[row.Length];
        int[] colors = new int[row.Length];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(tot);
        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");

        foreach (DataRow rows in row)
        {
            string x = rows["CODIGO"].ToString().Trim() + "-" + rows["NOME"].ToString().Trim().Replace("TRANSPORTADORA", "TRANSP.").Substring(0, 4).Trim();
            x = x.Replace(" ", "<*BR*>");
            decimal it = Convert.ToDecimal(rows["Notas"]);
            data[index] = Convert.ToDouble(rows["Notas"].ToString());
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = x;
            texts[index] = Convert.ToDouble(rows["Notas"].ToString()).ToString("#0");
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }


        XYChart c = new XYChart(430, 600);
        c.addExtraField(texts);
        c.setPlotArea(100, 5, 300, 340, 0xf8f8f8, 0xffffff);

        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("Quantidade de Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setTitle("Ocorrências", "verdanab.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);

        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    public static WebChartViewer MontarGraficoFilialResponsavel(DataTable table, string Tipo)
    {
        //definir as quantidades
        DataRow[] row = table.Select("RESPONSABILIDADE='" + Tipo + "'");

        int tot = 0;
        for (int i = 0; i < row.Length; i++)
        {
            tot += Convert.ToInt32(row[i]["Notas"]);
        }


        MultiChart chart = new MultiChart(500, 400);
        double[] numArray = new double[row.Length];
        double[] data = new double[row.Length];
        double[] numArray3 = new double[row.Length];
        string[] labels = new string[row.Length];
        string[] texts = new string[row.Length];
        int[] colors = new int[row.Length];
        int sectorNo = 0;
        int index = 0;
        string nomeFilial = "";

        //double b = Convert.ToDouble(table.Compute("SUM(Notas)", ""));
        foreach (DataRow rows in row)
        {
            if (nomeFilial != rows["NOME"].ToString())
            {
                double a = Convert.ToDouble(Convert.ToDouble(rows["Notas"]));
                double c = a / tot;
//                double c = a / b;
                numArray3[index] = c;
                labels[index] = rows["NOME"].ToString() + "<*BR*>" + rows["Notas"].ToString() + " N.F's " + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = rows["NOME"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(500, 480);
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

    public static WebChartViewer GraficoInventarioXRua(DataTable table, int ruas)
    {
       
        MultiChart chart = new MultiChart(460, 400);
        double[] numArray = new double[ruas ];
        double[] data = new double[ruas];
        double[] numArray3 = new double[ruas];
        string[] labels = new string[ruas];
        string[] texts = new string[ruas];
        int[] colors = new int[ruas];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(100);
        int index = 0;

        decimal itac = Convert.ToDecimal(table.Compute("SUM(QUANTIDADE)", "")==DBNull.Value?0:table.Compute("SUM(QUANTIDADE)", ""));

        for (int i = 1; i <= ruas; i++)
        {
            //string x = "Rua " + (i.ToString().Length < 10 ? "0" + i.ToString() : i.ToString());
            string srua = (i.ToString().Length < 10 ? "0" + i.ToString() : i.ToString());
            
            decimal it = Convert.ToDecimal(table.Compute("SUM(QUANTIDADE)","RUA='"+srua+"'")==DBNull.Value?0:table.Compute("SUM(QUANTIDADE)","RUA='"+srua+"'"));

            decimal calc = Convert.ToDecimal(0);

            if (it == 0 || itac == 0)
                calc = 0;
            else
                calc = (it / itac)*100;

            data[index] = Convert.ToDouble(it);
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = srua;
            texts[index] = calc.ToString("#0.00")+"%"; 
            colors[index] = 0x33868e;
            if (num2 < numArray3[index])
            {
                num2 = numArray3[index];
                sectorNo = index;
            }
            index++;
        }       

        XYChart c = new XYChart(430, 600);
        c.addExtraField(texts);
        c.setPlotArea(100, 5, 300, 340, 0xf8f8f8, 0xffffff);

        BarLayer layer = c.addBarLayer3(data, colors);
        c.swapXY();
        layer.setBorderColor(-16777216, Chart.glassEffect(3, 8));
        layer.setAggregateLabelFormat("{field0|0}");
        //layer.setAggregateLabelStyle("verdana.ttf", 8.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("%", "verdanab.ttf", 8.0);
        c.xAxis().setTitle("Ruas", "verdanab.ttf", 7.0);
        c.xAxis().setLabelStyle("verdana.ttf", 7.0, 0);

        chart.addChart(0, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    public static WebChartViewer GraficoInventarioXRuaPizza(DataTable table, int ruas)
    {
        //definir as quantidades
        int quantidade = 0;
        string nomeFilial = "";

        quantidade = ruas;

        MultiChart chart = new MultiChart(500, 500);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;
        int index = 0;
        nomeFilial = "";

        decimal b = Convert.ToDecimal(table.Compute("SUM(QUANTIDADE)", "") == DBNull.Value ? 0 : table.Compute("SUM(QUANTIDADE)", ""));

        for (int i = 1; i <= ruas; i++)
        {
            string srua = (i.ToString().Length < 10 ? "0" + i.ToString() : i.ToString());
            decimal it = Convert.ToDecimal(table.Compute("SUM(QUANTIDADE)", "RUA='" + srua + "'") == DBNull.Value ? 0 : table.Compute("SUM(QUANTIDADE)", "RUA='" + srua + "'"));

            decimal calc;

            if (it == 0 || b == 0)
                calc = 0;
            else
                calc = (it / b) * 100;

            numArray3[index] = Convert.ToDouble(calc);
            labels[index] = srua;
            colors[index] = 0x33868e;
            index++;
            nomeFilial =srua;
        }


        //pizza        
        PieChart chart3 = new PieChart(500, 500);
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

        
}

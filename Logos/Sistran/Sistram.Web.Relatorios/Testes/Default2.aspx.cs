using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
public partial class Testes_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        // Criando dois arrays, um com os nomes dos vendedores, e 
        // Outro com os valores das vendas
        string[] datas = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "10", "11", "12", "13", "05", "06", "07", "08", "09", "10", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" };
        double[] Valores = { 1000, 2300, 1220.33, 2222.40, 100, 100, 100, 100, 100, 100, 1000, 2300, 1220.33, 2222.40, 100, 100, 100, 100, 100, 100, 1000, 2300, 1220.33, 2222.40, 100, 100, 100, 100, 100, 100 };

        string[] Vendedores1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        double[] Totais1 = { 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115, 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115, 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115 };

        string[] linga = { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" };
        double[] lingaV = { 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115, 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115, 100, 230, 122.33, 222.40, 10, 12, 15, 17, 12, 115 };


        // Adicionando Título
        Chart1.Titles.Add("Relatório de Vendas");

        // Setando tipo de Grafico ( Colunas Verticais )
        Chart1.Series[0].ChartType = SeriesChartType.Column;
        Chart1.Series[1].ChartType = SeriesChartType.Column;
        Chart1.Series[2].ChartType = SeriesChartType.Line;
        

        // Vinculando os arrays ao Controle para que ele gere o
        // Gráfico
        Chart1.Series[0].Points.DataBindXY(datas, Valores);
        Chart1.Series[1].Points.DataBindXY(Vendedores1, Totais1);
        Chart1.Series[2].Points.DataBindXY(linga, lingaV);
        

        // Mostrando os Gráficos com valores
        //Chart1.Series[0].IsValueShownAsLabel = true;
        //Chart1.Series[1].IsValueShownAsLabel = true;
        Chart1.Series[2].IsValueShownAsLabel = true;

        foreach (Series s in Chart1.Series)
        {
            for (int i = 0; i < s.Points.Count; i++)
            {
                // Mostra o valor como tooltip            
                s.Points[i].ToolTip = "#VAL{C}";
            }
        }


        // Create a new legend called "Legend2".
        Chart1.Legends.Add(new Legend("Legend2"));

        // Set Docking of the Legend chart to the Default Chart Area.
        //Chart1.Legends["Legend2"].DockToChartArea = "Default";

        // Assign the legend to Series1.
        Chart1.Series[0].Legend = "Legend2";
        Chart1.Series[0].IsVisibleInLegend = true;
        Chart1.Series[0].LegendText = "Barra1";

        // Assign the legend to Series1.
        Chart1.Series[1].Legend = "Legend2";
        Chart1.Series[1].IsVisibleInLegend = true;
        Chart1.Series[0].LegendText = "Barra2";


        // Assign the legend to Series1.
        Chart1.Series[2].Legend = "Legend2";
        Chart1.Series[2].IsVisibleInLegend = true;
        Chart1.Series[2].LegendText = "Linha";

       */


 
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        ChartArea ca = new ChartArea();        
        Chart cr = new Chart();
        cr.Palette = ChartColorPalette.SeaGreen;
        cr.ChartAreas.Add(ca);
        cr.Width = 800;
        cr.Height = 400;
        cr.BackGradientStyle = GradientStyle.VerticalCenter;
        //cr.BackColor = System.Drawing.Color.GreenYellow;

        ca.BackGradientStyle = GradientStyle.Center;
        ca.BackColor = System.Drawing.Color.SkyBlue;
        cr.Titles.Add("Teste Run-Time");

        string[] dias = new string[31];
        double[] valores = new double[31];
        double[] linha = new double[31];

        
        for (int i = 0; i <= 30; i++)
        {
            dias[i] = (i+1).ToString();
            valores[i] = double.Parse((100 * i).ToString());

            if(i==14 || i==5)
                linha[i] = double.Parse((11350).ToString());           
            else
                linha[i] = double.Parse((i + 100).ToString());           
        }

        

        cr.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisX.Interval=1;
        
        cr.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

        cr.Series.Add("Valor");
        cr.Series.Add("Valor1");
        cr.Series.Add("Valor2");

        cr.Series[0].ChartType = SeriesChartType.Column;

        cr.Series[1].ChartType = SeriesChartType.Line;
        cr.Series[1].BorderDashStyle = ChartDashStyle.Solid;
        cr.Series[1].BorderWidth=3;
        cr.Series[1].SmartLabelStyle.CalloutLineWidth = 0;
        cr.Series[1].MarkerSize = 5;
        
        cr.Series[1].LabelForeColor = System.Drawing.Color.Black;
        cr.Series[2].ChartType = SeriesChartType.Column;

        cr.Series[0].Points.DataBindXY(dias, valores);
        cr.Series[1].Points.DataBindXY(dias, linha);
        cr.Series[2].Points.DataBindXY(dias, valores);


        // Mostrando os Gráficos com valores
        //cr.Series[0].IsValueShownAsLabel = true;
        cr.Series[1].IsValueShownAsLabel = true;
        //cr.Series[2].IsValueShownAsLabel = true;

        
        foreach (Series s in cr.Series)
        {
            for (int i = 0; i < s.Points.Count; i++)
            {
                // Mostra o valor como tooltip            
                s.Points[i].ToolTip = "#VAL{C}";
            }
        }


        // Create a new legend called "Legend2".
        Legend Legend2 = new Legend();
        Legend2.Name = "Legend2";
        Legend2.Docking = Docking.Bottom;

        cr.Legends.Add(Legend2);


        // Assign the legend to Series1.
        cr.Series[0].Legend = "Legend2";
        cr.Series[0].IsVisibleInLegend = true;
        cr.Series[0].LegendText = "Valor";

        // Assign the legend to Series1.
        cr.Series[1].Legend = "Legend2";
        cr.Series[1].IsVisibleInLegend = true;
        cr.Series[0].LegendText = "Linha";


        // Assign the legend to Series1.
        cr.Series[2].Legend = "Legend2";
        cr.Series[2].IsVisibleInLegend = true;
        cr.Series[2].LegendText = "valor 2";



        
        Panel1.Controls.Add(cr);
        

    }
}
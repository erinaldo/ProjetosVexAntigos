using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using ChartDirector;
using System.Web.UI.WebControls;

public partial class DesempenhoEntregaFilial : System.Web.UI.Page
{

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = 30;
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);
                        
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                btnGerarReport.Visible = false;
                btnPDF.Visible = false;                
                btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?n="+  Guid.NewGuid() +"&tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");

                
                //DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtF.Text = DateTime.Now.ToShortDateString();
                //txtI.Text = primeiroDiaMes.ToShortDateString();

                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" +txtF.Text;

            if (txtFoi.Text == "S")
            {
                MontarTable();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'","´") + "')", true);
            return;

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
        txtFoi.Text = "S";
    }
   
    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

//        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

        //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));



        decimal totalEntregue = Convert.ToDecimal(0);
        decimal totalNaoEntregue = Convert.ToDecimal(0);
        decimal totalNaoEntregueAtraso = Convert.ToDecimal(0);
        decimal totalNaoEntregueNoPrazo = Convert.ToDecimal(0);
        decimal totalTrasnsittime = Convert.ToDecimal(0);


        TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
        if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
            return;
        }

        if (ts.Days > intervalo)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
            return;
        }

               
        DataTable dt = NotasFiscais.ListarDesempenhoEntregaFilial(Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(true), Session["Conn"].ToString());
        PlaceHolder1.Controls.Clear();

        if (dt.Rows.Count > 0)
            pnlGrafico.Visible = true;
        else
            pnlGrafico.Visible = false;


        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' >"));
       
        if (dt.Rows.Count > 0)
        {

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;' rowspan='2' valign='top' >FILIAL"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top' >N.F. ENTREGUE"));
            
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top'>"));
            PlaceHolder1.Controls.Add(CriarBoatoAjuda("LEAD TIME"));
            PlaceHolder1.Controls.Add(new LiteralControl(@" LEAD TIME</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;' rowspan='2' valign='top'>"));
            PlaceHolder1.Controls.Add(CriarBoatoAjuda("TRANSITTIME"));
            PlaceHolder1.Controls.Add(new LiteralControl(@" TRANSIT TIME</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center'  style='font-size:7pt;' COLSPAN='3' >"));
            PlaceHolder1.Controls.Add(CriarBoatoAjuda("NAOENTREGUE"));
            PlaceHolder1.Controls.Add(new LiteralControl(@" N.F. NÃO ENTREGUE</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center'  style='font-size:7pt;' COLSPAN='3' >"));
            PlaceHolder1.Controls.Add(CriarBoatoAjuda("MEDIASNAOENTREGUE"));            
            PlaceHolder1.Controls.Add(new LiteralControl(@"MEDIA DIAS NÃO ENTREGUE</td>"));

  
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;' rowspan='2' valign='top' >% N.F."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            //----------------------------

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

          


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>N.F. ATRASO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>N.F. NO PRAZO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>TOTAL N.F."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

    
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>DIAS ATRASO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>DIAS NO PRAZO"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>TOTAL DIAS"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'"));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            totalEntregue = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));
            totalNaoEntregue = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", ""));

            totalNaoEntregueAtraso = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE_ATRASO)", ""));
            totalNaoEntregueNoPrazo = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE_NO_PRAZO)", ""));
            totalTrasnsittime = Convert.ToDecimal(dt.Compute("SUM(TRANSITTIME)", ""));


            decimal totlead = Convert.ToDecimal(dt.Compute("SUM(LEADTIME)", ""));
            decimal totMediaNaoEntregue = Convert.ToDecimal(dt.Compute("SUM(MEDIA_DOS_DIAS_NAO_ENTREGUE)", ""));
            decimal totMediaNaoEntregueAtraso = Convert.ToDecimal(dt.Compute("SUM(MEDIA_DOS_DIAS_NAO_ENTREGUE_ATRASO)", ""));
            decimal totMediaNaoEntreguenoPrazo = Convert.ToDecimal(dt.Compute("SUM(MEDIA_DOS_DIAS_NAO_ENTREGUE_NO_PRAZO)", ""));
            


            decimal itac = Convert.ToDecimal("0.00");
            decimal calc = Convert.ToDecimal("0.00");

            foreach (DataRow item in dt.Rows)
            {

                decimal itEntregue = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                decimal itNaoEntregue = Convert.ToDecimal(item["NOTASFISCAIS_NAO_ENTREGUE"]);

                if (totalEntregue > 0)
                    calc = ((itEntregue+itNaoEntregue) / (totalEntregue+totalNaoEntregue)) * 100;

                itac += calc;

                

                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'  align='left' style='font-size:7pt;height:10px'>" + item["NOMEDAFILIAL"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["LEADTIME"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='right' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["TRANSITTIME"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_NAO_ENTREGUE_ATRASO"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_NAO_ENTREGUE_NO_PRAZO"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_NAO_ENTREGUE"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["MEDIA_DOS_DIAS_NAO_ENTREGUE_ATRASO"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["MEDIA_DOS_DIAS_NAO_ENTREGUE_NO_PRAZO"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["MEDIA_DOS_DIAS_NAO_ENTREGUE"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'  align='right' style='font-size:7pt;height:10px'>" + calc.ToString("#0.00").ToString() + "%"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left'  style='font-size:7pt;'>TOTAL"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + totalEntregue.ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + (totlead / dt.Rows.Count).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + Convert.ToDecimal(totalTrasnsittime / dt.Rows.Count).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>")); 

            

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + Convert.ToDecimal(totalNaoEntregueAtraso).ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + Convert.ToDecimal(totalNaoEntregueNoPrazo).ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + totalNaoEntregue.ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + (totMediaNaoEntregueAtraso / dt.Rows.Count).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + (totMediaNaoEntreguenoPrazo / dt.Rows.Count).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right'  style='font-size:7pt;'>" + (totMediaNaoEntregue / dt.Rows.Count).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>100,00%"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));            

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }
        else
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

       
        if (dt.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
            btnPDF.Visible = true;
            Session["dt"] = dt;

            tbTotais.Visible = true;
            Label2.Text = totalEntregue.ToString("#0");
            Label3.Text = totalNaoEntregue.ToString("#0");
            Label1.Text = (totalEntregue + totalNaoEntregue).ToString();
        }

        if (dt.Rows.Count > 0)
            GerarGraficos(dt, Convert.ToDouble(totalEntregue), Convert.ToDouble(totalNaoEntregue)); 
    }

    private ImageButton CriarBoatoAjuda(string p)
    {
        ImageButton bot = new ImageButton();
        
        bot.ImageUrl = "ajuda.jpg";

        if(p=="TRANSITTIME")
            bot.ToolTip = "Clique aqui para ajuda de Transit Time";

        if (p == "LEAD TIME")
            bot.ToolTip = "Clique aqui para ajuda de Lead Time";
             
        if(p=="NAOENTREGUE")
            bot.ToolTip = "Clique aqui para ajuda de Notas Fiscais não Entregue";

        if (p == "MEDIASNAOENTREGUE")
            bot.ToolTip = "Clique aqui para ajuda Média de Dias não Entregue";
        

        bot.Click += ImageButton_Click;
        bot.CommandArgument = p;
        return bot;        
    }

    private void GerarGraficos(DataTable dt, double totalEntregue, double totalNaoEntregue)
    {

        pnlGrafPerc.Controls.Add((WebChartViewer)Grafico(dt, totalEntregue, totalNaoEntregue));
        pnlGrafPerc.Visible = true;

        pnlGrafAcum.Controls.Add((WebChartViewer)Grafico2(dt));
    }
  
    //pizza
    private WebChartViewer Grafico(DataTable table, double totalEntregue, double totalNaoEntregue)
    {
        MultiChart chart = new MultiChart(400, 400);
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
            double b = totalEntregue + totalNaoEntregue ;
            double c = a/b;
            numArray3[index] = c;
            labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
            colors[index] = 0x33868e;
            index++;
        }
       
        //pizza        
        PieChart chart3 = new PieChart(520, 540);
        chart3.setPieSize(160, 80, 70);
               
        chart3.set3D(20);
        chart3.setStartAngle(170.0);
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
        MultiChart chart = new MultiChart(450, 450);
//        MultiChart chart = new MultiChart(300, 300);

        double[] numArray = new double[table.Rows.Count];
        double[] data = new double[table.Rows.Count];
        double[] numArray3 = new double[table.Rows.Count];
        string[] labels = new string[table.Rows.Count];
        string[] texts = new string[table.Rows.Count];
        int[] colors = new int[table.Rows.Count];
        int sectorNo = 0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(table.Compute("Max(TRANSITTIME)", ""));
//        double num3 = Convert.ToDouble(table.Compute("SUM(NOTASFISCAIS)", ""));
        
        int index = 0;

        decimal itac = Convert.ToDecimal("0.00");
       // double calc = 0.0;
        //decimal total;

        //total = Convert.ToDecimal(table.Compute("SUM(media)", ""));

        foreach (DataRow row in table.Rows)
        {
            decimal it = Convert.ToDecimal(row["TRANSITTIME"]);

            //if (total > 0)
            //    calc = Convert.ToDouble((it / total) * 100);

            //itac += Convert.ToDecimal(calc);            
            data[index] = Convert.ToDouble(row["TRANSITTIME"].ToString());
            numArray3[index] = Convert.ToDouble(it);
            labels[index] = row["NOMEDAFILIAL"].ToString();
            texts[index] = Convert.ToDouble(row["TRANSITTIME"].ToString()).ToString("#0.00");
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
        layer.setAggregateLabelStyle("verdana.ttf", 7.0);
        c.xAxis().setLabels(labels);
        c.yAxis().setTitle("", "verdana.ttf", 7.0);
        c.xAxis().setTitle("", "verdana.ttf", 6.0);
        c.xAxis().setLabelStyle("verdana.ttf", 6.0, 0);
        chart.addChart(5, 0, c);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);
        viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}'");
        return viewer;
    }

    protected void btnGerarReport_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        MontarTable();

    }

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton x = (ImageButton)sender;

        switch (x.CommandArgument.ToString().ToUpper())
        {
            case "TRANSITTIME":
                lbltituloAjuda.Text = "AJUDA: TRANSIT TIME";
                lblAjuda.Text = "CÁLCULO DA MÉDIA DE DIAS ENTRE A DATA DE ENTREGA MENOS A DATA DE RECEBIMENTO (DIAS ÚTEIS).";
                break;

            case "LEAD TIME":
                lbltituloAjuda.Text = "AJUDA: LEAD TIME";
                lblAjuda.Text = "CÁLCULO DA MÉDIA DE DIAS ENTRE A DATA DE ENTREGA MENOS A DATA DE EMISSÃO.";
                break;

            case "NAOENTREGUE":
                lbltituloAjuda.Text = "AJUDA: NOTAS FISCAIS NÃO ENTREGUE";
                lblAjuda.Text = "<B>NOTAS FISCAIS EM ATRASO:(A) </B></BR>";
                lblAjuda.Text += "NÚMERO DE NOTAS NÃO ENTREGUES ONDE A DATA ATUAL SUPERA A DATA PLANEJADA PARA ENTREGA. </BR></BR>";

                lblAjuda.Text += "<B>NOTAS FISCAIS NO PRAZO:(B)</B></BR>";
                lblAjuda.Text += "NÚMERO DE NOTAS NÃO ENTREGUES ONDE A DATA ATUAL NÃO SUPERA A DATA PLANEJADA.</BR></BR>";

                lblAjuda.Text += "<B>TOTAL DE NOTAS FISCAIS:</B></BR>";
                lblAjuda.Text += "SOMA DE A E B.</BR></BR>";
                break;


            case "MEDIASNAOENTREGUE":
                lbltituloAjuda.Text = "AJUDA: MEDIA DIAS NÃO ENTREGUE";
                lblAjuda.Text = "<B>DIAS ATRASO:(A) </B></BR>";
                lblAjuda.Text += "MÉDIA DE DIAS EM ATRASO  ONDE A DATA ATUAL SUPERA A DATA PLANEJADA PARA ENTREGA. </BR></BR>";

                lblAjuda.Text += "<B>DIAS NO PRAZO:(B)</B></BR>";
                lblAjuda.Text += "MÉDIA DE DIAS EM ATRASO  ONDE A DATA ATUAL NÃO SUPERA A DATA PLANEJADA PARA ENTREGA.</BR></BR>";

                lblAjuda.Text += "<B>TOTAL DE DIAS:</B></BR>";
                lblAjuda.Text += "MÉDIA TOTAL A E B</BR></BR>";
                break;

        }

        
        dvAjudaTransitTime.Style.Add("display", "block");      
    }

    protected void vv_Click(object sender, EventArgs e)
    {   
        dvAjudaTransitTime.Style.Add("display", "none");     
    }
}

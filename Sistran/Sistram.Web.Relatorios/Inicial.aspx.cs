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

public partial class Inicial : System.Web.UI.Page
{
    int faltamin = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]);
    int totalAguardEmbarque, totalComOcorrencia, totalEmbarcadas = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Title = "#SistranWeb " + Request.QueryString["nu"].ToString();
            Session["ConnLogin"] = "Data Source=192.168.10.10;Initial Catalog=GrupoLogos;User ID=site_ASP;Password=asp7998;";

            if ((Request.QueryString["cliente"] == null || Request.QueryString["cliente"] == "") || (Request.QueryString["data2"] == null || Request.QueryString["data2"] == null))
            {
                throw new Exception("Informações Incorretas. Favor contactar o administrador.");
            }

            DateTime inicio = Convert.ToDateTime(Request.QueryString["data"].ToString());

            Session["DataConf"] = inicio + "|" + Convert.ToDateTime(Request.QueryString["data2"].ToString()).ToShortDateString();

            ChartDirector.WebChartViewer.OnPageInit(Page);
            if (!IsPostBack)
            {                
//               SistranBLL.Usuario.LogBDBLL.GravarLog(1, "PHP", "Acesso feito pelo PHP.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                Button2.Attributes.Add("onClick", "FullWindow('frmRpthome.aspx?cliente=" + Request.QueryString["cliente"] + "&tipo=TELA&tit=Resumo', 'NovaJanela2', 'yes')");
                Button3.Attributes.Add("onClick", "FullWindow('frmRpthome.aspx?cliente=" + Request.QueryString["cliente"] + "&tipo=PDF&tit=Resumo', 'NovaJanela2', 'yes')");

                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]) * 60000;

               string[] DataConf = FuncoesGerais.DataConf();
               lblPeriodo.Text = inicio.ToShortDateString() + " à " + DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();



               lblqtdEmitidas.Text = NotasFiscais.RetornarTotalNotasFiscaisEmitidas(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]), inicio.ToShortDateString(), Request.QueryString["data2"].ToString()).ToString();                

            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            montarall();
            lblTempo.Text = "Última Atualização: " + DateTime.Now.ToShortTimeString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
    
    protected void MontarTableAguardandoEmbarque(bool exibir, DateTime? ini, DateTime? fim)
    {
        try
        {


            const string Situacao = "ConstDcoFilSitAguardEmbarque";
            DataTable dtAgEmb = NotasFiscais.ListarHomeAguardandoEmbarque(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]), "", FuncoesGerais.LoadDataSetConstantes(Situacao), ini, fim);
            PlaceHolder1.Controls.Clear();
            int tot = 0;
            #region geral
            PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
            if (dtAgEmb.Rows.Count > 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>Data"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>Nota Fiscal"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


                if (exibir == true)
                {
                    foreach (DataRow item in dtAgEmb.Rows)
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='NotasFiscaisAguardEmbarqueFiltro.aspx?cliente="+Request.QueryString["cliente"]+"&data=" + Server.UrlEncode(item["DataDeEntrada"].ToString()) + "' class='link' >" + item["DataDeEntrada"].ToString() + "</a>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                        tot += Convert.ToInt32(item["Notas"]);
                    }
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                }
            }
            else
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

            #endregion

            #region Por Filial
            DataTable dtAgEmbFilial = NotasFiscais.ListarHomeAguardandoEmbarqueFilial(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"].ToString()).ToString(),"", FuncoesGerais.LoadDataSetConstantes(Situacao), ini, fim);

            PlaceHolder4.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
            if (dtAgEmbFilial.Rows.Count > 0)
            {
                PlaceHolder4.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

                PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>Filial"));
                PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                //PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;'>Data"));
                //PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>Nota Fiscal"));
                PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder4.Controls.Add(new LiteralControl(@"</tr>"));

                tot = 0;
                if (exibir == true)
                {
                    foreach (DataRow item in dtAgEmbFilial.Rows)
                    {
                        PlaceHolder4.Controls.Add(new LiteralControl(@"<tr>"));

                        PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><a href='NotasFiscaisAguardEmbarqueFiltro.aspx?cliente="+Request.QueryString["cliente"].ToString()+"&idfilial=" + Server.UrlEncode(item["idfilial"].ToString()) + "' class='link'  >" + item["nome"].ToString() + "</a>"));
                        PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                        //PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='NotasFiscaisAguardEmbarqueFiltro.aspx?data=" + Server.UrlEncode(item["DataDeEntrada"].ToString()) + "' class='link'>" + item["DataDeEntrada"].ToString() + "</a>"));
                        //PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                        PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolder4.Controls.Add(new LiteralControl(@"</tr>"));
                        tot += Convert.ToInt32(item["Notas"]);
                    }
                    PlaceHolder4.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                    PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
                    PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                    //PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" ));
                    //PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
                    PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder4.Controls.Add(new LiteralControl(@"</tr>"));
                }
            }
            else
            {
                PlaceHolder4.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder4.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
                PlaceHolder4.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder4.Controls.Add(new LiteralControl(@"</tr>"));
                totalAguardEmbarque = tot;

            }
            PlaceHolder4.Controls.Add(new LiteralControl(@"</table>"));


            if (exibir == true)
            {
                pnlGrafAguardandoEmbarque.Controls.Add((WebChartViewer)GrafAguardandoEmbarque(dtAgEmbFilial));
            }
            totalAguardEmbarque = tot;
            #endregion
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private WebChartViewer GrafAguardandoEmbarque(DataTable table)
    {
        if (table.Rows.Count == 0)
            return null;

        pnlGrafAguardandoEmbarque.Visible = true;
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


        MultiChart chart = new MultiChart(500, 300);
        double[] numArray = new double[quantidade];
        double[] data = new double[quantidade];
        double[] numArray3 = new double[quantidade];
        string[] labels = new string[quantidade];
        string[] texts = new string[quantidade];
        int[] colors = new int[quantidade];
        int sectorNo = 0;
        int index = 0;
        nomeFilial = "";

        double b = Convert.ToDouble(0);
        if(table.Rows.Count>0)
             b = Convert.ToDouble(table.Compute("SUM(Notas)", ""));
        
        
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

    private WebChartViewer GrafGeral()
    {
        pnlGrafGeral.Visible = true;

        int quantidade = 3;


        MultiChart chart = new MultiChart(500, 500);
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

        if (b == 0)
            c = 0;
        else
            c = a / b;

        numArray3[1] = c;
        labels[1] = "Com" + "<*BR*>" + "Ocorrência" + "<*BR*>";
        colors[1] = 0x33868e;

        a = Convert.ToDouble(totalEmbarcadas);

        if (b == 0)
            c = 0;
        else
            c = a / b;

        numArray3[2] = c;
        labels[2] = "Embarcadas" + "<*BR*>";
        colors[2] = 0x33868e;
        WebChartViewer viewer = new WebChartViewer();

       
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
            chart.addChart(15, 30, chart3);
        
        if(c>0)
            viewer.Image = chart.makeWebImage(0);
        
        return viewer;
    }

    protected void MontarTableEmbarcadas(bool exibir, DateTime? ini, DateTime? fim)
    {
        const string Situacao = "ConstDcoFilSitMercadoriaEmbarcada";

        DataTable dtAgEmb = NotasFiscais.ListarHomeNotasFiscaisEmbarcadas(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]), "", FuncoesGerais.LoadDataSetConstantes(Situacao), ini, fim);
        PlaceHolder2.Controls.Clear();
        int tot = 0;
        PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtAgEmb.Rows.Count > 0)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>Data"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>Nota Fiscal"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


            if (exibir == true)
            {
                foreach (DataRow item in dtAgEmb.Rows)
                {
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap align='center' style='font-size:8pt;height:10px'><a href='NotasFiscaisMercadoriaEmbarcadaFiltro.aspx?cliente="+Request.QueryString["cliente"]+"&data=" + Server.UrlEncode(item["DataDeEntrada"].ToString()) + "' class='link'>" + item["DataDeEntrada"].ToString() + "</a>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
                    tot += Convert.ToInt32(item["Notas"]);
                }
                PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
            }
        }
        else
        {
            totalEmbarcadas = tot;
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));
        totalEmbarcadas = tot;
    }

    protected void MontarTableComOcorrencias(bool exibir, DateTime? ini, DateTime? fim)
    {
        DataTable dtAgEmb = NotasFiscais.ListarHomeNotasFiscaisComOcorrencias(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]), "", ini, fim, false, false);
        PlaceHolder3.Controls.Clear();
        int tot = 0;
        PlaceHolder3.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtAgEmb.Rows.Count > 0)
        {
            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>Ocorrência"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>Nota Fiscal"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));


            if (exibir == true)
            {
                foreach (DataRow item in dtAgEmb.Rows)
                {
                    PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='left' style='font-size:8pt;height:10px'><a class='link' href='#' >" + item["Codigo"].ToString() + "-" + item["Nome"].ToString() + "</a>"));
                    PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

                    PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToDecimal(item["Notas"]).ToString("#0")));
                    PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));
                    tot += Convert.ToInt32(item["Notas"]);
                }
                PlaceHolder3.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + tot));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));
            }
        }
        else
        {
            PlaceHolder3.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder3.Controls.Add(new LiteralControl(@"</tr>"));
            totalComOcorrencia = tot;

        }
        PlaceHolder3.Controls.Add(new LiteralControl(@"</table>"));
        totalComOcorrencia = tot;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //montarall();
        Response.Redirect("frmBaterDatas.aspx");
    }

    private void montarall()
    {
        try
        {
            string[] DataConf = FuncoesGerais.DataConf();

            DateTime? ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime? fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            PlaceHolder1.Controls.Clear();
            PlaceHolder2.Controls.Clear();
            PlaceHolder3.Controls.Clear();
            PlaceHolder4.Controls.Clear();
            pnlGrafAguardandoEmbarque.Controls.Clear();
            pnlGrafGeral.Controls.Clear();


            MontarTableAguardandoEmbarque(true, ini, fim);
            MontarTableEmbarcadas(true, ini, fim);
            MontarTableComOcorrencias(true, ini, fim);

            if(totalEmbarcadas>0)
                pnlGrafGeral.Controls.Add((WebChartViewer)GrafGeral());

            lblPeriodo.Text = Convert.ToDateTime(DataConf[0]).ToShortDateString() + " à " + DateTime.Parse(DataConf[1]).ToShortDateString();
            lblqtdEmitidas.Text = NotasFiscais.RetornarTotalNotasFiscaisEmitidas(Sistran.Library.FuncoesUteis.retornarClientes(Request.QueryString["cliente"]), Convert.ToDateTime(DataConf[0]).ToShortDateString(), Convert.ToDateTime(DataConf[1]).ToShortDateString()).ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }


}
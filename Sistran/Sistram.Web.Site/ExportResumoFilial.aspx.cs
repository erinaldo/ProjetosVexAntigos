using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using SistranBLL;
using ChartDirector;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ExportResumoFilial : System.Web.UI.Page
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

            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
               
            }
            //lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
        MontarTable();
        GerarExcel();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        MontarTable();
    }

    protected void MontarTable()
    {
        string[] DataConf = FuncoesGerais.DataConf();
        Sistran.Library.Enuns.tipoReportResumoFilial en = new Sistran.Library.Enuns.tipoReportResumoFilial();
        en = Sistran.Library.Enuns.tipoReportResumoFilial.filial;
        DataTable dt = NotasFiscais.ListarResumoPorFilial(Convert.ToDateTime(DataConf[0]), Convert.ToDateTime(DataConf[1]), Sistran.Library.FuncoesUteis.retornarClientes(), Session["Conn"].ToString(), en,true, true, true);

        if (dt.Rows.Count == 0)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            return;
        }
        PlaceHolder1.Controls.Clear();


        //acerta as colunas

        int qtdColunas = (Convert.ToInt32(dt.Compute("MAX(PRAZOUTILIZADO)", "")) * 3) + 4;
        decimal qtdTotalNf = (Convert.ToDecimal(dt.Compute("MAX(TOTALDENOTAS)", "")));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99% id='teste' runat='server'  >"));
        int contDefColun = 0;
        string NomeColuna = "";

        #region Cabeçalho

        //cabeçalho
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        for (int i = 0; i < qtdColunas; i++)
        {
            if (i == 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>Filial"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            }
            else
            {
                switch (contDefColun)
                {
                    case 0:
                        NomeColuna = "&nbsp;&nbsp;&nbsp;TRANSIT TIME";
                        contDefColun += 1;
                        break;

                    case 1:
                        NomeColuna = "&nbsp;&nbsp;&nbsp;NF ENTREGUES";
                        contDefColun += 1;

                        break;

                    case 2:
                        NomeColuna = "&nbsp;&nbsp;&nbsp;% NF";
                        contDefColun = 0;
                        break;


                }
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            }
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL NF ENTREGUES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NF NÃO ENTREGUES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL DE NOTAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion

        int qtdColunasTemp = (Convert.ToInt32(dt.Compute("MAX(PRAZOUTILIZADO)", "")));
        decimal[] tot = new decimal[qtdColunasTemp + 1];

        #region Itens
        for (int i = 0; i < qtdColunas; i++)
        {
            string NomeFilial = "";
            for (int ii = 0; ii < dt.Rows.Count; ii++)
            {
                if (i == 0)
                {

                    if (NomeFilial != dt.Rows[ii]["NOMEDAFILIAL"].ToString())
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Rows[ii]["NOMEDAFILIAL"].ToString()));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                        DataRow[] orw = dt.Select("NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'");

                        int contador = 0;
                        int row = 0;
                        decimal porcAtualAcmulado = Convert.ToDecimal("0.00");

                        foreach (DataRow item in orw)
                        {
                            if (contador < Convert.ToInt32(item["PRAZOUTILIZADO"]))
                            {
                                for (int kkkk = contador; kkkk < Convert.ToInt32(item["PRAZOUTILIZADO"]); kkkk++)
                                {
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>&nbsp;&nbsp;" + contador.ToString()));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>&nbsp;&nbsp;0"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>&nbsp;&nbsp;0"));
                                    //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> &nbsp;&nbsp;" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                    contador += 1;
                                    row += 1;

                                }
                            }


                            if (contador == Convert.ToInt32(item["PRAZOUTILIZADO"]))
                            {
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> &nbsp;&nbsp;" + item["PRAZOUTILIZADO"].ToString()));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> &nbsp;&nbsp;" + item["NOTASFISCAIS_ENTREGUE"].ToString()));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                
                                decimal qtdTotal = (Convert.ToDecimal(dt.Compute("sum(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                                decimal qtdItemAtual = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);

                                decimal porcAtual = Convert.ToDecimal(0);

                                if (qtdTotal > 0)
                                {
                                    porcAtual = (qtdItemAtual / qtdTotal) * 100;
                                }
                                porcAtualAcmulado += porcAtual;

                                tot[row] += Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'> &nbsp;&nbsp;" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                            }
                            row += 1;
                            contador += 1;
                        }


                        if (contador <= qtdColunasTemp)
                        {
                            for (int iii = contador; iii <= qtdColunasTemp; iii++)
                            {
                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + contador.ToString()));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>0"));
                                //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + porcAtualAcmulado.ToString("#0.00") + "%"));
                                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                                contador += 1;
                            }
                        }

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + dt.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + dt.Rows[ii]["NOMEDAFILIAL"].ToString() + "'")));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


                        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

                        NomeFilial = dt.Rows[ii]["NOMEDAFILIAL"].ToString();
                    }
                }
            }
        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion

        #region Rodape


        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        int cont2 = 0;

        decimal totporcAcm = Convert.ToDecimal("0.00");

        for (int i = 0; i < qtdColunas; i++)
        {
            if (i == 0)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>Total de Notas"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            }
            else
            {
                switch (contDefColun)
                {
                    case 0:
                        NomeColuna = "";
                        contDefColun += 1;
                        break;

                    case 1:
                        NomeColuna = tot[cont2].ToString();
                        contDefColun += 1;

                        break;

                    case 2:
                        decimal AtualPorcent = ((Convert.ToDecimal(tot[cont2])) / Convert.ToDecimal(dt.Compute("sum(NOTASFISCAIS_ENTREGUE)", "")) * 100);
                        totporcAcm += AtualPorcent;
                        //NomeColuna = AtualPorcent.ToString("#0.00") + "% - " + totporcAcm.ToString("#0.00") + "%";
                        NomeColuna = totporcAcm.ToString("#0.00") + "%";
                        contDefColun = 0;
                        cont2 += 1;
                        break;
                }
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NomeColuna));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            }

        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(NOTASFISCAISNAOENTREGUE)", "")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpRVerdana' nowrap=nowrap align='right' style='font-size:7pt;font-weight:bold'>" + dt.Compute("SUM(TOTALDENOTAS)", "")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion


        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        if (dt.Rows.Count > 0)
        {            
            //GerarGraficos(dt);
        }
    }

    private void GerarGraficos(DataTable dt)
    {
        pnlEntregues.Controls.Add((WebChartViewer)GraficoEntregues(dt));
        pnlEntregues.Visible = true;
        pnlNaoEntregues.Visible = true;
        tbGraf.Visible = true;
        pnlNaoEntregues.Controls.Add((WebChartViewer)GraficoNaoEntregues(dt));
    }

    protected void btnGerarReport_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        MontarTable();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=myexcel.xls");
        Response.ContentType = "application/ms-excel";
        string k = GetGridViewHtml(PlaceHolder1);
        Response.Write(k);
        Response.End();
    }

    public string GetGridViewHtml(Control c)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        c.RenderControl(hw);
        return sw.ToString();

    }

    public void GerarExcel()
    {
        StringWriter tw = new StringWriter();
        System.Web.UI.Html32TextWriter hw = new Html32TextWriter(tw);
        HtmlForm frm = new HtmlForm();
        //Response.ContentType = "application/vnd.ms-excel";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename="+DateTime.Now.ToString().Replace(":","").Replace("/","")+".xls");
        Response.Charset = "";
        EnableViewState = false;
        Controls.Add(frm);
        frm.Controls.Add(PlaceHolder1);
        frm.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
    }

    private WebChartViewer GraficoEntregues(DataTable table)
    {

        //definir as quantidades
        int quantidade = 0;
        string nomeFilial = "";

        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                quantidade++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
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
        double b = Convert.ToDouble(table.Compute("sum(NOTASFISCAIS_ENTREGUE)", ""));
        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAIS_ENTREGUE"]));
                double c = a / b;
                numArray3[index] = c;
                labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(750, 790);
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
        chart.addChart(5, 30, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }

    private WebChartViewer GraficoNaoEntregues(DataTable table)
    {
        //definir as quantidades
        int quantidade = 0;
        string nomeFilial = "";

        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                quantidade++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
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
        double b = Convert.ToDouble(table.Compute("sum(NOTASFISCAISNAOENTREGUE)", ""));
        foreach (DataRow row in table.Rows)
        {
            if (nomeFilial != row["NOMEDAFILIAL"].ToString())
            {
                double a = Convert.ToDouble(Convert.ToDouble(row["NOTASFISCAISNAOENTREGUE"]));
                double c = a / b;
                numArray3[index] = c;
                labels[index] = row["NOMEDAFILIAL"].ToString() + "<*BR*>";
                colors[index] = 0x33868e;
                index++;
                nomeFilial = row["NOMEDAFILIAL"].ToString();
            }
        }

        //pizza        
        PieChart chart3 = new PieChart(750, 790);
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
        chart.addChart(5, 30, chart3);
        WebChartViewer viewer = new WebChartViewer();
        viewer.Image = chart.makeWebImage(0);

        return viewer;
    }
}
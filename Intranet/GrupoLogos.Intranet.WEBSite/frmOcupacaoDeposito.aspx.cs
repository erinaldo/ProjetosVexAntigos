﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;


public partial class frmOcupacaoDeposito : System.Web.UI.Page
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

//                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));


       
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                CarregarCboFilial();
                cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;

            //txtI.Text = "01/01/2012";
            //MontarTable();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
            return;
        }
    }

    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();
        cboFilial.Items.Insert(0, new ListItem("SELECIONE"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

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



        string strsql = "";

        strsql += " SELECT DATA, PALLETSARMAZENAGEM,  ";
        strsql += " ( ";
        strsql += " SELECT COUNT(DISTINCT CODIGO)  FROM DEPOSITO D ";
        strsql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITO = D.IDDEPOSITO ";
        strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA ";
        strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAOREGRA DPLR ON DPLR.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO";
        strsql += " WHERE IDFILIAL=" + cboFilial.SelectedValue;
        strsql += " AND D.ATIVO = 'SIM' ";
        strsql += " AND DP.ATIVO='SIM' ";
        strsql += " AND DPL.ATIVO = 'SIM' ";
        strsql += " AND DPLR.IDCLIENTE=" + cboCliente.SelectedValue;

        strsql += " )POSICOES,  ";
        strsql += " ( ";
        strsql += " cast(PALLETSARMAZENAGEM as Numeric(10,4)) /  ";
        strsql += " cast( ";
        strsql += " ( ";
        strsql += " SELECT CASE COUNT(DISTINCT CODIGO)  WHEN 0  THEN 1 ELSE COUNT(DISTINCT CODIGO) end FROM DEPOSITO D ";
        strsql += " INNER JOIN DEPOSITOPLANTA DP ON DP.IDDEPOSITO = D.IDDEPOSITO ";
        strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA ";
        strsql += "INNER JOIN DEPOSITOPLANTALOCALIZACAOREGRA DPLR ON DPLR.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO";
        strsql += " WHERE IDFILIAL=" + cboFilial.SelectedValue;
        strsql += " AND D.ATIVO = 'SIM' ";
        strsql += " AND DP.ATIVO='SIM' ";
        strsql += " AND DPL.ATIVO = 'SIM' ";
        strsql += " AND DPLR.IDCLIENTE=" + cboCliente.SelectedValue;

        strsql += " ) as numeric(10,4))*100 ";
        strsql += " )perc ";
        strsql += " FROM MOVIMENTACAOCLIENTECONSOLIDADO WHERE DATA BETWEEN CONVERT(DATETIME, '" + txtI.Text + "',103)  AND CONVERT(DATETIME, '" + txtF.Text + "',103)   ";
        strsql += " AND IDCLIENTE="+cboCliente.SelectedValue;
        strsql += " Order by data";

        DataTable dt = new DataTable();


        try
        {
             dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }
        catch (Exception ex)
        {
            if (ex.ToString() == "")
                ex.ToString();
            
        }
        

        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99%  runat='server' >"));
        if (dt.Rows.Count == 0)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
            return;
        }

        #region D A T A
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + cboCliente.SelectedItem.Text));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:7pt;height:10px'>" + DateTime.Parse(item["DATA"].ToString()).ToString("dd/MM/yyyy")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion


        #region P O S I Ç Õ E S
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>QUANTIDADE DE POSIÇÕES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + item["POSICOES"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region P O S I Ç Õ E S   O C U P A D A S
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>POSIÇÕES OCUPADAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + item["PALLETSARMAZENAGEM"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
                #endregion
                

        #region % % % % % % 

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>% OCUPAÇÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["PERC"].ToString()).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion


        #region Rodape
        //PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td COLSPAN=2 class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:7pt;height:10px'>"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL DE POSIÇÕES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'><B>" + dt.Compute("sum(posicoes)","").ToString()));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL DE POSIÇÕES OCUPADAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'><B>" + dt.Compute("sum(PALLETSARMAZENAGEM)", "").ToString()));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>MÉDIA DE OCUPAÇÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'><B>" + decimal.Parse(dt.Compute("AVG(PERC)", "").ToString()).ToString("#0.00") + "%"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));


        gerarGrafico(dt);
    }

    private void gerarGrafico(DataTable dt)
    {

        if (dt.Rows.Count == 0)
            return;

        ChartArea ca = new ChartArea();
        System.Web.UI.DataVisualization.Charting.Chart  cr = new System.Web.UI.DataVisualization.Charting.Chart();
        cr.Palette = ChartColorPalette.None;
        cr.ChartAreas.Add(ca);
        cr.Width = 1000;
        cr.Height = 500;
        cr.BackGradientStyle = GradientStyle.VerticalCenter;
        

        ca.BackGradientStyle = GradientStyle.Center;
        //ca.BackColor = System.Drawing.Color.SkyBlue;
        cr.Titles.Add("ARMAZENAGEM - % DE OCUPAÇÃO");

        string[] dias = new string[dt.Rows.Count];
        double[] posicoes = new double[dt.Rows.Count];
        double[] ocupadas = new double[dt.Rows.Count];
        double[] linha = new double[dt.Rows.Count];


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dias[i] = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy");
            posicoes[i] = double.Parse(dt.Rows[i]["POSICOES"].ToString());
            ocupadas[i] = double.Parse(dt.Rows[i]["PALLETSARMAZENAGEM"].ToString());
            linha[i] = double.Parse( double.Parse(dt.Rows[i]["PERC"].ToString()).ToString("#0.00"));           
        }


        cr.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisX.Interval = 1;

        cr.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 7;

        cr.ChartAreas[0].AxisY2.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisY2.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisY2.Minimum = 0;
        cr.ChartAreas[0].AxisY2.Maximum = 100;

        cr.ChartAreas[0].AxisY2.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;


        cr.ChartAreas[0].AxisY.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

        cr.ChartAreas[0].AxisX.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;


        int x = int.Parse(dt.Rows[0]["POSICOES"].ToString()) /10;

        cr.ChartAreas[0].AxisY.Interval = Convert.ToDouble(x);        
        cr.Series.Add("Posições");
        cr.Series.Add("Ocupadas");
        cr.Series.Add("Percentual");


        //cr.Series[0].ChartType = SeriesChartType.Column;
        //cr.Series[0].BackGradientStyle = GradientStyle.VerticalCenter;
        //cr.Series[0].BackSecondaryColor = System.Drawing.Color.DarkRed;
        //cr.Series[0].Color = System.Drawing.Color.Azure;
        //cr.Series[0].BorderColor = System.Drawing.Color.DarkRed;

        cr.Series[0].ChartType = SeriesChartType.Column;
        cr.Series[0].BackGradientStyle = GradientStyle.VerticalCenter;
        cr.Series[0].BackSecondaryColor = System.Drawing.Color.DarkSeaGreen;
        cr.Series[0].Color = System.Drawing.Color.Azure;
        cr.Series[0].BorderColor = System.Drawing.Color.DarkSeaGreen;

        cr.Series[1].ChartType = SeriesChartType.Column;
        cr.Series[1].BackGradientStyle = GradientStyle.VerticalCenter;
        cr.Series[1].BackSecondaryColor = System.Drawing.Color.DarkRed;
        cr.Series[1].Color = System.Drawing.Color.Red;
        cr.Series[1].BorderColor = System.Drawing.Color.DarkRed;



        cr.Series[2].ChartType = SeriesChartType.Line;
        cr.Series[2].BorderDashStyle = ChartDashStyle.Solid;
        cr.Series[2].BorderWidth = 3;
        cr.Series[2].SmartLabelStyle.CalloutLineWidth = 0;
        cr.Series[2].MarkerSize = 5;
        cr.Series[2].LabelForeColor = System.Drawing.Color.Red;
        cr.Series[2].Color = System.Drawing.Color.Red;
        

        cr.Series[0].Points.DataBindXY(dias, posicoes);
        cr.Series[2].Points.DataBindXY(dias, linha);
        cr.Series[1].Points.DataBindXY(dias, ocupadas);

        
        cr.Series[2].YAxisType = AxisType.Secondary;

        

        // Mostrando os Gráficos com valores
        //cr.Series[0].IsValueShownAsLabel = true;
        cr.Series[2].IsValueShownAsLabel = true;
        //cr.Series[2].IsValueShownAsLabel = true;


        foreach (Series s in cr.Series)
        {
            for (int i = 0; i < s.Points.Count; i++)
            {
                // Mostra o valor como tooltip            
                s.Points[i].ToolTip = "#VAL{D}";
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
        cr.Series[0].LegendText = "Posições";

        // Assign the legend to Series1.
        cr.Series[1].Legend = "Legend2";
        cr.Series[1].IsVisibleInLegend = true;
        cr.Series[0].LegendText = "Posições";


        // Assign the legend to Series1.
        cr.Series[2].Legend = "Legend2";
        cr.Series[2].IsVisibleInLegend = true;
        cr.Series[2].LegendText = "% Ocupação";




        Panel9.Controls.Add(cr);
    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        MontarTable();
    }

    protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregarComboClientes();
    }

    private void carregarComboClientes()
    {

    }
    protected void cboFilial_SelectedIndexChanged1(object sender, EventArgs e)
    {
        cboCliente.Items.Clear();

        if (cboFilial.SelectedIndex > 0)
        {
            string strsql = "";

            strsql += " SELECT C.IDCLIENTE, CNPJCPF+ ' - ' +  FANTASIAAPELIDO FANTASIAAPELIDO";
            strsql += " FROM CLIENTEFILIAL CF ";
            strsql += " INNER JOIN CLIENTE C ON C.IDCLIENTE = CF.IDCLIENTE ";
            strsql += " INNER JOIN CADASTRO CD ON CD.IDCADASTRO = C.IDCLIENTE ";
            strsql += " WHERE IDFILIAL="+ cboFilial.SelectedValue;
            strsql += " AND CF.CLIENTELOGISTICA = 'SIM' ORDER BY 2";

            cboCliente.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            cboCliente.DataTextField = "FANTASIAAPELIDO";
            cboCliente.DataValueField = "IDCLIENTE";
            cboCliente.DataBind();

            cboCliente.Items.Insert(0, new ListItem("SELECIONE"));

        }
        else
        {
            cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));
        }
    }
}

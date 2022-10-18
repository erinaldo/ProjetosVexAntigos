using System;
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


public partial class frmKPIExpedicao : System.Web.UI.Page
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
//               SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));


                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                CarregarCboFilial();
                cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;           

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
//       SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

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
        strsql += " SELECT ";
        strsql += " CAST(DATADEEMISSAO AS DATE) DATA, ";
        strsql += " COUNT(DISTINCT DOC.IDDOCUMENTO) QTDNOTAS, ";
        strsql += " COUNT(DISTINCT PE.IDPRODUTOCLIENTE) QTDSKU, ";
        strsql += " AVG(DISTINCT DOC.PESOBRUTO) PESO, ";
        strsql += " CAST(COUNT(DISTINCT PE.IDPRODUTOCLIENTE) AS DECIMAL)  / CAST(COUNT(DISTINCT DOC.IDDOCUMENTO) AS DECIMAL) SKUNF ";
        strsql += " FROM DOCUMENTO DOC ";
        strsql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTO = DOC.IDDOCUMENTO ";
        strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
        strsql += " WHERE DOC.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + txtI.Text + "',103)  AND CONVERT(DATETIME, '" + txtF.Text + "',103) "; ;
        strsql += " AND TIPODEDOCUMENTO='NOTA FISCAL' ";
        strsql += " AND DOC.ENTRADASAIDA='SAIDA' ";
        strsql += " AND DOC.ATIVO='SIM' ";
        strsql += " AND IDCLIENTE= " + cboCliente.SelectedValue;
        strsql += " GROUP BY CAST(DATADEEMISSAO AS DATE) ";
        strsql += " ORDER BY CAST(DATADEEMISSAO AS DATE); ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
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

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99%  runat='server' >"));

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
        

        #region QUANTIDADE DE NOTAS
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>QTDE DE NOTAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["QTDNOTAS"].ToString()).ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region QUANTIDADE DE SKU
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>QTDE DE SKU "));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["QTDSKU"].ToString()).ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region MEDIA DE PESO POR NOTA FISCAL
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PESO POR NOTA "));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["PESO"].ToString()).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        #region SKU POR NOTA FISCAL
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>SKU POR NOTA "));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        foreach (DataRow item in dt.Rows)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:7pt;height:10px'>" + decimal.Parse(item["SKUNF"].ToString()).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        #endregion

        
        gerarGrafico(dt);
         
    }

    private void gerarGrafico(DataTable dt)
    {

        if (dt.Rows.Count == 0)
            return;

        ChartArea ca = new ChartArea();
        System.Web.UI.DataVisualization.Charting.Chart cr = new System.Web.UI.DataVisualization.Charting.Chart();
        cr.Palette = ChartColorPalette.None;
        cr.ChartAreas.Add(ca);
        cr.Width = 800;
        cr.Height = 400;
        cr.BackGradientStyle = GradientStyle.VerticalCenter;


        ca.BackGradientStyle = GradientStyle.Center;
        cr.Titles.Add("EXPEDIÇÃO - NOTAS FISCAIS");

        string[] dias = new string[dt.Rows.Count];
        double[] qtdNotas = new double[dt.Rows.Count];
        double[] qtdSKU = new double[dt.Rows.Count];
        double[] SkuPorNota = new double[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dias[i] = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy");
            qtdNotas[i] = double.Parse(double.Parse(dt.Rows[i]["QTDNOTAS"].ToString()).ToString("#0.00"));
            qtdSKU[i] = double.Parse(double.Parse(dt.Rows[i]["QTDSKU"].ToString()).ToString("#0.00"));
            SkuPorNota[i] = double.Parse(double.Parse(dt.Rows[i]["SKUNF"].ToString()).ToString("#0.00"));
        }



        cr.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisX.Interval = 1;

        cr.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;


        cr.ChartAreas[0].AxisY2.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisY2.LabelAutoFitMaxFontSize = 7;



        cr.ChartAreas[0].AxisY2.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;


        cr.ChartAreas[0].AxisY.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

        cr.ChartAreas[0].AxisX.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

        cr.Series.Add("Armazenagem");
       // cr.Series.Add("SKU");
        cr.Series.Add("Saídas");
        

        cr.Series[0].ChartType = SeriesChartType.Column;
        cr.Series[0].BackGradientStyle = GradientStyle.VerticalCenter;
        cr.Series[0].BackSecondaryColor = System.Drawing.Color.DarkSeaGreen;
        cr.Series[0].Color = System.Drawing.Color.Azure;
        cr.Series[0].BorderColor = System.Drawing.Color.DarkSeaGreen;
        cr.Series[0].LabelForeColor = System.Drawing.Color.Green;


        cr.Series[1].ChartType = SeriesChartType.Line;
        cr.Series[1].BorderDashStyle = ChartDashStyle.Solid;
        cr.Series[1].BorderWidth = 3;
        cr.Series[1].SmartLabelStyle.CalloutLineWidth = 0;
        cr.Series[1].MarkerSize = 5;
        cr.Series[1].LabelForeColor = System.Drawing.Color.DarkRed;
        cr.Series[1].Color = System.Drawing.Color.DarkRed;


        //cr.Series[1].ChartType = SeriesChartType.Line;
        //cr.Series[1].BorderDashStyle = ChartDashStyle.Solid;
        //cr.Series[1].BorderWidth = 3;
        //cr.Series[1].SmartLabelStyle.CalloutLineWidth = 0;
        //cr.Series[1].MarkerSize = 5;
        //cr.Series[1].LabelForeColor = cr.Series[2].Color;
        //cr.Series[1].LabelForeColor = System.Drawing.Color.DarkBlue;
        //cr.Series[1].Color = System.Drawing.Color.DarkBlue;



        cr.Series[0].Points.DataBindXY(dias, qtdNotas);
       // cr.Series[1].Points.DataBindXY(dias, qtdSKU);
        cr.Series[1].Points.DataBindXY(dias, SkuPorNota);


        //cr.Series[1].YAxisType = AxisType.Secondary;
        cr.Series[1].YAxisType = AxisType.Secondary;

        cr.Series[1].IsValueShownAsLabel = true;
      //  cr.Series[1].IsValueShownAsLabel = true;
        cr.Series[0].IsValueShownAsLabel = true;


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
        cr.Series[0].LegendText = "Quantidade de Notas";

        // Assign the legend to Series1.
        cr.Series[1].Legend = "Legend2";
        cr.Series[1].IsVisibleInLegend = true;
        cr.Series[0].LegendText = "Quantidade de Notas";


        // Assign the legend to Series1.
        //cr.Series[2].Legend = "Legend2";
        //cr.Series[2].IsVisibleInLegend = true;
        //cr.Series[2].LegendText = "SKU Por Nota";




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

            strsql += " SELECT C.IDCLIENTE, cnpjcpf+ ' - ' +  FANTASIAAPELIDO FANTASIAAPELIDO";
            strsql += " FROM CLIENTEFILIAL CF ";
            strsql += " INNER JOIN CLIENTE C ON C.IDCLIENTE = CF.IDCLIENTE ";
            strsql += " INNER JOIN CADASTRO CD ON CD.IDCADASTRO = C.IDCLIENTE ";
            strsql += " WHERE IDFILIAL=" + cboFilial.SelectedValue;
            strsql += " AND CF.CLIENTELOGISTICA = 'SIM' order by 2";

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
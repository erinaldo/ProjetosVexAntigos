using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
//using ChartDirector;

public partial class frmPopInv : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper().Substring(1, 20)), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            lblTitulo.Text = "ACOMPANHAMENTO DO INVENTARIO  |  " + Request.QueryString["idinventariocontagem"].ToString();
        }

        if (!IsPostBack)
            CheckBox1.Checked = true;

        Atualizar();
    }

    private void Atualizar()
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            Carregar();
            CarregarRakuinUsuarios();
            Label1.Text = "Última Atualização: " + DateTime.Now;
            
            

            if (CheckBox1.Checked)
            {
                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(1) * 60000;

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarRakuinUsuarios()
    {
        PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=99% runat='server' border='0'>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpCenter' colspan='2'><b>RAKING DE USUÁRIOS</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap><b>USUÁRIO</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'><b>QUANTIDADE</b>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));

        string[] dados = Request.QueryString["idinventariocontagem"].ToString().Split('-');


        DataTable duser = new SistranBLL.Deposito().RakingUsers(int.Parse(dados[0]));

        for (int i = 0; i < duser.Rows.Count; i++)
        {
            PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap>" + duser.Rows[i]["NOME"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdpR'>" + duser.Rows[i]["QUANTIDADE"].ToString()));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));
        }
        PlaceHolder2.Controls.Add(new LiteralControl(@"</table >"));
    }

    private void Carregar()
    {
        string[] dados = Request.QueryString["idinventariocontagem"].ToString().Split('-');

        string strsqlRegra = "";
        strsqlRegra += " SELECT DISTINCT CODIGO, DPL.IDDEPOSITOPLANTALOCALIZACAO, ATIVO,  ";
        strsqlRegra += " SUBSTRING(DPL.CODIGO, 1,2) RUA,  ";
        strsqlRegra += " SUBSTRING(DPL.CODIGO, 3,2) COLUNA,   ";
        strsqlRegra += " SUBSTRING(DPL.CODIGO, 5,2) PREDIO,";
        strsqlRegra += " ISNULL(( ";
        strsqlRegra += " SELECT SUM(ISNULL(QUANTIDADE,0)) FROM INVENTARIOCONTAGEMPRODUTO ICPI  ";
        strsqlRegra += " INNER JOIN INVENTARIOCONTAGEM ICI ON ICI.IDINVENTARIOCONTAGEM = ICPI.IDINVENTARIOCONTAGEM ";
        strsqlRegra += " WHERE ICI.IDINVENTARIOCONTAGEM = " + dados[0];
        strsqlRegra += " AND DPL.IDDEPOSITOPLANTALOCALIZACAO = ICPI.IDDEPOSITOPLANTALOCALIZACAO ";
        strsqlRegra += " ),0) QTD ";
        strsqlRegra += " FROM DEPOSITOPLANTALOCALIZACAOREGRA DPLR ";
        strsqlRegra += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO=DPLR.IDDEPOSITOPLANTALOCALIZACAO ";
        strsqlRegra += " WHERE DPLR.IDCLIENTE in (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsqlRegra += " ORDER BY 1 ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsqlRegra, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        lblTotalEnderecos.Text = dt.Compute("count(codigo)", "ATIVO='SIM'").ToString();
        lblPosicoesContadas.Text = dt.Compute("count(codigo)", "ATIVO='SIM' AND QTD>0").ToString();

        if (lblTotalEnderecos.Text == "0")
            lblPercInv.Text = "0";
        else
            lblPercInv.Text = ((decimal.Parse(lblPosicoesContadas.Text) / decimal.Parse(lblTotalEnderecos.Text)) * decimal.Parse("100")).ToString("#0.000") + "%";

        tblResumo.Visible = true;

        int MinimoDeRuas = Convert.ToInt32(dt.Compute("MIN(rua)", ""));
        int ruas = Convert.ToInt32(dt.Compute("max(rua)", ""));
        int colunas = Convert.ToInt32(dt.Compute("max(coluna)", ""));
        int andar = Convert.ToInt32(dt.Compute("max(PREDIO)", ""));

        string tempI = (MinimoDeRuas.ToString().Length < 2 ? "0" + MinimoDeRuas.ToString() : MinimoDeRuas.ToString());
        string tempF = (ruas.ToString().Length < 2 ? "0" + ruas.ToString() : ruas.ToString());

        PlaceHolder1.Controls.Clear();

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing='1' celpanding='1' width=90% runat='server' border='2'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' width='1%' ><b>RUA</b>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' colspan='" + (colunas + 1).ToString() + "'><b>COLUNAS</b>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir("", true, tempI, tempF, andar.ToString())));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        //        string colunaAtual = "";

        for (int i = 0; i < colunas; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' ><b><CENTER>" + (Convert.ToInt32(i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString()).ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<CENTER></b></td>"));
        }
        //coluna de total
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' ><b>TOTAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        for (int ii = MinimoDeRuas; ii <= ruas; ii++)
        {
            int totContadosNaRua = 0;
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px' align='CENTER'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCenter'><b>"));
            PlaceHolder1.Controls.Add(new LiteralControl(CriarBotaoExpandir(ii.ToString(), false, "", "", andar.ToString())));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));

            for (int ic = 1; ic <= colunas; ic++)
            {
                string r = (ii < 10 ? "0" + ii.ToString() : ii.ToString());
                string c = (ic < 10 ? "0" + ic.ToString() : ic.ToString());

                int qtdEnd = int.Parse(dt.Compute("count(codigo)", "rua='" + r + "' and coluna='" + c + "'").ToString());
                int qtdEenContados = int.Parse(dt.Compute("count(codigo)", "rua='" + r + "' and coluna='" + c + "' and QTD >0").ToString());
                int qtdEenCDesabilitados = int.Parse(dt.Compute("count(codigo)", "rua='" + r + "' and coluna='" + c + "' and ativo ='NAO'").ToString());

                if (qtdEenContados > 0)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='contados'><b>" + qtdEenContados));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));
                }
                else
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='naocontados'><b>" + (qtdEnd - qtdEenCDesabilitados)));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));
                }

                totContadosNaRua += qtdEenContados;

            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR'><b>" + totContadosNaRua));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</b></td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            string rua = (ii < 10 ? "0" + ii.ToString() : ii.ToString());


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='tr" + rua + "1' style='display:none'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='" + (colunas + 2) + "' class='Divisoria2'><B>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr VALIGN='TOP' ALIGN='CENTER' id='tr" + rua + "4' style='display:none'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='" + (colunas + 2) + "' class='tdp' valign='middle' align='right'>"));

            for (int iandar = andar; iandar > 0; iandar--)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr  id='trAndar" + rua + iandar + "' style='font-size:7pt;height;16px; display:none' valign='top' align='center'>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td>" + iandar + "º"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                for (int icoluna = 1; icoluna <= colunas; icoluna++)
                {
                    string srua = (ii < 10 ? "0" + ii.ToString() : ii.ToString());
                    string sandar = (iandar < 10 ? "0" + iandar.ToString() : iandar.ToString());
                    string scoluna = (icoluna < 10 ? "0" + icoluna.ToString() : icoluna.ToString());

                    DataRow[] calculo = dt.Select("CODIGO='" + srua + scoluna + sandar + "'", "");

                    string estilo = "tdpR";

                    if (calculo == null || calculo.Length == 0 || calculo[0]["Ativo"].ToString() == "NAO")
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + estilo + "'>"));
                        PlaceHolder1.Controls.Add(criarImagemBloqueado());
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    }
                    else
                    {
                        if (double.Parse(calculo[0]["QTD"].ToString()) > 0)
                        {
                            estilo = "contados_pequeno";
                        }
                        else
                        {
                            estilo = "naocontados_pequeno";
                        }

                        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='" + estilo + "'> " + double.Parse(calculo[0]["QTD"].ToString())));
                        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                    }

                }

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            }
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr id='tr" + rua + "3' style='display:none'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan='" + (colunas + 2) + "' class='Divisoria2'><B>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        }

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        gerarGrafico(dt);
    }

    private Image criarImagemBloqueado()
    {
        Image i = new Image();
        i.ImageUrl = "http://www.grupologos.com.br/sistranWeb.NET/Images/proibidoX.bmp";
        return i;

    }

    public string CriarBotaoExpandir(string rua, bool expandAll, string RI, string RF, string maxAndar)
    {
        string m = "";
        if (expandAll)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<center><div id='dvExpandir' style='font-size:11px;cursor:pointer;background-image:url(Images/seta.jpg); height:12px; width:14px;' OnClick=ExpandirAll('" + RI + "','" + RF + "','" + maxAndar + "');>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</div></center>"));
            m = "";
        }
        else
        {
            m = "<b><div style='font-size:11px;cursor:pointer' OnClick=Expandir('" + rua + "','" + maxAndar + "');>";
            m += rua + "</div></b>";
        }
        return m;
    }

    private void gerarGrafico(DataTable dt)
    {

        if (dt.Rows.Count == 0)
            return;


        int MinimoDeRuas = Convert.ToInt32(dt.Compute("MIN(rua)", ""));
        int iruas = Convert.ToInt32(dt.Compute("max(rua)", ""));
        int colunas = Convert.ToInt32(dt.Compute("max(coluna)", ""));
        int andar = Convert.ToInt32(dt.Compute("max(PREDIO)", ""));

        string tempI = (MinimoDeRuas.ToString().Length < 2 ? "0" + MinimoDeRuas.ToString() : MinimoDeRuas.ToString());
        string tempF = (iruas.ToString().Length < 2 ? "0" + iruas.ToString() : iruas.ToString());


        ChartArea ca = new ChartArea();
        System.Web.UI.DataVisualization.Charting.Chart cr = new System.Web.UI.DataVisualization.Charting.Chart();
        cr.Palette = ChartColorPalette.None;
        cr.ChartAreas.Add(ca);
        cr.Width = 500;
        cr.Height = 400;
        cr.BackGradientStyle = GradientStyle.VerticalCenter;


        ca.BackGradientStyle = GradientStyle.Center;
        cr.Titles.Add("Andamento do Inventario (% Contado Por Rua)");

        string[] ruas = new string[iruas - MinimoDeRuas + 1];
        double[] perc = new double[iruas - MinimoDeRuas + 1];


        for (int i = 0; i < ruas.Length; i++)
        {
            ruas[i] = "Rua " + (iruas).ToString();

            decimal totend = decimal.Parse((dt.Compute("count(codigo)", "rua='" + iruas + "' and ativo='SIM'").ToString()));
            decimal endContados = decimal.Parse((dt.Compute("count(codigo)", "rua='" + iruas + "' and ativo='SIM' and qtd>0").ToString()));

            if (totend == 0)
                perc[i] = double.Parse("0");
            else
            {
                decimal n = endContados / totend;
                n = n * 100;

                string sn = n.ToString("#0.000");

                perc[i] = double.Parse(sn);
            }
            iruas--;

        }



        cr.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 7;
        cr.ChartAreas[0].AxisX.Interval = 1;
        cr.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 5;
        cr.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 7;

        cr.ChartAreas[0].AxisY.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

        cr.ChartAreas[0].AxisX.MinorGrid.LineWidth = 0;
        cr.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

        cr.ChartAreas[0].AxisY.Minimum = 0;
        cr.ChartAreas[0].AxisY.Maximum = 100;
        cr.ChartAreas[0].AxisY.Interval = 10;


        cr.Series.Add("Rua");

        cr.Series[0].ChartType = SeriesChartType.Bar;
        cr.Series[0].BackGradientStyle = GradientStyle.None;


        cr.Series[0].Color = System.Drawing.Color.Green;
        cr.Series[0].Points.DataBindXY(ruas, perc);
        cr.Series[0].IsValueShownAsLabel = true;
        
        Panel9.Controls.Add(cr);
        Panel9.Visible = true;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        Timer1.Enabled = CheckBox1.Checked;
    }
}
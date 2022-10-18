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
using System.Linq;
public partial class RelatorioProducaoRE : System.Web.UI.Page
{
    DataTable dtTodosOsDados;
    DataTable distinctDataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Button1.Attributes.Add("OnClick", "window.open('frmRptLista.aspx?data="+ DateTime.Now.ToShortDateString()+"', '','fullscreen=yes, scrollbars=auto'); return false;");
        //Gerar();
        this.Title = "Relatório de Produção por Relação de Entrega";

        if (!IsPostBack)
        {
            //string[] DataConf = FuncoesGerais.DataConf();
            txtI.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtF.Text = DataConf[1];
        }
        //Session["DataConf"] = txtI.Text + "|" + txtF.Text;

    }

    private void Gerar()
    {
        /*
        dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC GERAR_PRODUCAO_RE '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + DateTime.Now.ToString("dd/MM/yyyy")  + "'", Sistran.Library.Robo.Robo.RetornarStringBaseAntiga()).Tables[0];
        //dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("select * from Temp", Sistran.Library.Robo.Robo.RetornarStringBaseAntiga()).Tables[0];


        dtTodosOsDados.Columns.Add("FreteMotorista", Type.GetType("System.Decimal"));
        dtTodosOsDados.Columns.Add("Lucro", Type.GetType("System.Decimal"));
        dtTodosOsDados.Columns.Add("PercLucro", Type.GetType("System.Decimal"));
        
        for (int i = 0; i < dtTodosOsDados.Rows.Count; i++)
        {
            //calcula freteMotorista
            if (decimal.Parse(dtTodosOsDados.Rows[i]["creditos"].ToString()) > decimal.Parse("0"))
            {
                dtTodosOsDados.Rows[i]["FreteMotorista"] = decimal.Parse(dtTodosOsDados.Rows[i]["creditos"].ToString()) - decimal.Parse(dtTodosOsDados.Rows[i]["debitos"].ToString());
            }
            else
            {
                decimal entregas = decimal.Parse(dtTodosOsDados.Rows[i]["entregas"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporentrega"].ToString());
                decimal coleta = decimal.Parse(dtTodosOsDados.Rows[i]["coletas"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporcoleta"].ToString());
                decimal kkm = decimal.Parse(dtTodosOsDados.Rows[i]["distanciapercorrida"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporkm"].ToString());
                decimal kilo = decimal.Parse(dtTodosOsDados.Rows[i]["pesototalre"].ToString()) * decimal.Parse(dtTodosOsDados.Rows[i]["valorporkilo"].ToString());

                if (kilo < decimal.Parse(dtTodosOsDados.Rows[i]["valorminimopeso"].ToString()))
                {
                    kilo = decimal.Parse(dtTodosOsDados.Rows[i]["valorminimopeso"].ToString());
                }

                decimal diaria = decimal.Parse(dtTodosOsDados.Rows[i]["diaria"].ToString());
                decimal adicionalPercurso = decimal.Parse(dtTodosOsDados.Rows[i]["ad_percurso"].ToString());
                decimal debitos = decimal.Parse(dtTodosOsDados.Rows[i]["debitos"].ToString());

                dtTodosOsDados.Rows[i]["FreteMotorista"] = entregas + coleta + kkm + kilo + diaria + adicionalPercurso - debitos;
            }

            //lucro
            DataRow[] rw = dtTodosOsDados.Select("filial='" + (dtTodosOsDados.Rows[i]["filial"].ToString() + "'"));
            decimal impostoH = decimal.Parse(dtTodosOsDados.Rows[i]["Imposto"].ToString()) / 100;
            decimal SeguroH = decimal.Parse(dtTodosOsDados.Rows[i]["Seguro"].ToString()) / 100;
            decimal AdmH = decimal.Parse(dtTodosOsDados.Rows[i]["TaxaAdministrativa"].ToString()) / 100;
            decimal TranfH = decimal.Parse(dtTodosOsDados.Rows[i]["TaxaDeTranferencia"].ToString()) / 100;

            impostoH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * impostoH);
            SeguroH = (decimal.Parse(dtTodosOsDados.Rows[i]["valordanota"].ToString()) * SeguroH);
            AdmH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * AdmH);
            TranfH = (decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) * TranfH);


            dtTodosOsDados.Rows[i]["lucro"] = decimal.Parse(dtTodosOsDados.Rows[i]["Frete"].ToString()) - decimal.Parse(dtTodosOsDados.Rows[i]["FreteMotorista"].ToString()) - impostoH - SeguroH - AdmH - TranfH;

            if (Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString()) == 0)
                dtTodosOsDados.Rows[i]["perclucro"] = 0;
            else
                dtTodosOsDados.Rows[i]["perclucro"] = (Convert.ToDecimal(dtTodosOsDados.Rows[i]["lucro"]) / Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString())) * 100;

        }
         * */

        MontarTotais();
    }

    DataTable dtApurarTotal = new DataTable();
    private void MontarTotais()
    {
        bool mensal = false;

        DataTable dtGeral = null;

        if (mensal == false)
            dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RESULTADO_RE null, '" + DateTime.Parse(txtI.Text).ToString("yyyy-MM-dd") + "', '" + DateTime.Parse(txtI.Text).ToString("yyyy-MM-dd") + "', 1,  'dt'  ", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];
       
        #region Styles

        string html = "<html><head>";
        html += " <STYLE type='text/css'>";
        html += " body ";
        html += " { ";
        html += " margin: 0px; ";
        html += " background-color: #f8f8f8; ";
        html += " font-family: Verdana; ";
        html += " text-align: left; ";
        html += " font-size: 12PX; }";


        html += " .table  ";
        html += " { ";
        html += " background-color: #E0E0E0; ";
        html += " width: 50%; ";
        html += " font-family: Arial, Helvetica, sans-serif; ";
        html += " font-size: 7pt; ";
        html += " font-weight: bold; ";
        html += " } ";

        html += " .tableFundoClaro ";
        html += " { ";
        html += " background-color: #F8F8F8; ";
        html += " width: 100%; ";
        html += " font-family: Arial, Helvetica, sans-serif; ";
        html += " font-size: 7pt; ";
        html += " font-weight: bold; ";
        html += " } ";

        html += " .tableSemCorFundo ";
        html += " {	 ";
        html += " width: 50%; ";
        html += " font-family: Arial, Helvetica, sans-serif; ";
        html += " font-size: 7pt; ";
        html += " font-weight: bold; ";
        html += " } ";

        html += " .table2 ";
        html += " { ";
        html += " background-color:#E0E0E0 ;  ";
        html += " font-family: Arial, Helvetica, sans- ";
        html += " } ";

        html += " .tdpCenter ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " text-align: center ; ";
        html += " nowrap:nowrap; ";
        html += " font-weight:normal; ";
        html += " } ";

        html += " .tdp ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " nowrap:nowrap; ";
        html += " font-weight:normal; ";
        html += " text-align: left; ";
        html += " vertical-align:middle; ";

        html += " } ";
        html += " .tdpSemAlign ";
        html += " { ";
        html += " border: 0.5pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " nowrap:nowrap; ";
        html += " font-weight:normal; ";
        html += " } ";

        html += " .tdpSemAlignGray ";
        html += " { ";
        html += " border: 0.5pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " nowrap:nowrap; ";
        html += " font-weight:normal; ";
        html += " background-color:GrayText; ";
        html += " } ";


        html += " .tdpCenter ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " text-align: center ; ";
        html += " nowrap:now ";
        html += " } ";
        html += " .tdpR ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " text-align:right; ";
        html += " nowrap:nowrap; ";
        html += " font-weight:normal;	 ";
        html += " } ";

        html += "  .tdpVerdana ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        //html += " text-align: left; ";
        html += " nowrap:nowrap; ";
        html += " } ";

        html += " .tdpCabecalho ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " height: 13pt; ";
        html += " font-size:9pt; ";
        html += " font-family:Verdana; ";
        html += " font-weight:bold; ";
        html += " text-transform: uppercase;	 ";
        html += " } ";

        html += " .tdpRVerdanaVerde ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " text-align: right;	 ";
        html += " nowrap:nowrap; ";
        html += " background-color:#20AE3F; ";
        html += " } ";

        html += " .tdpRVerdanaAmarelo ";
        html += " { ";
        html += " border: 0.1pt solid #FFFFFF; ";
        html += " font-size:8pt; ";
        html += " font-family:Verdana; ";
        html += " text-align: right;	 ";
        html += " nowrap:nowrap; ";
        html += " background-color:#DEDE40; ";
        html += " } ";

        html += " .tdpRVerdanaVermelho ";
        html += " { ";
        html += " 	border: 0.1pt solid #FFFFFF; ";
        html += " 	font-size:8pt; ";
        html += " 	font-family:Verdana; ";
        html += " 	text-align: right;	 ";
        html += " 	nowrap:nowrap; ";
        html += " 	background-color:#DE4040; ";
        html += "} ";

        html += " </STYLE> ";

        html += "</HEAD>";

        html += "<BODY>";

     
        html += "<TABLE>";

        #endregion

        #region Cabecalho
        html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
        html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE MOT";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANSF";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>REENTREGA";
        html += "</td>";


        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>GERENTES";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ASSISTENTES";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>LIDERES OP.";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>CONFERENTES";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>SEPARADORES";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>LIMPEZA";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>EMPILHADOR";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>OUTROS";
        html += "</td>";

        html += "<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL DE FUNC.";
        html += "</td>";


        html += "</tr>";
        #endregion

        DataView view = new DataView(dtGeral);
        DataTable dt = view.ToTable(true, "FLNOME", "IMPOSTO", "TAXAADMINISTRATIVA", "TAXADETRANFERENCIA", "SEGURO", "GERENTE", "ASSISTENTE", "LIDEROPERACIONAL", "CONFERENTE", "SEPARADOR", "LIMPEZA", "OUTROS", "EMPILHADOR");
        dt.Columns.Add("LucroTotal", typeof(decimal));

        float tot_reentrega = 0;
        float tot_imposto = 0;
        float tot_seguro = 0;
        float tot_txadm = 0;
        float tot_txTransf = 0;
        float tot_lucro = 0;
        float lucro = 0;
        float vl_fret = 0;
        float vl_nota = 0;


        for (int i = 0; i < dt.Rows.Count; i++)
        {

            lucro = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
            vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            lucro = lucro - float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            float tx = (float.Parse(dt.Rows[i]["Imposto"].ToString()) / 100);
            float vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

            lucro = lucro - vlcalc;

            tx = (float.Parse(dt.Rows[i]["SEGURO"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

            lucro = lucro - vlcalc;

            tx = (float.Parse(dt.Rows[i]["TaxaAdministrativa"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

            lucro = lucro - vlcalc;

            tx = (float.Parse(dt.Rows[i]["TaxaDeTranferencia"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;

            lucro = lucro - vlcalc;

            vlcalc = float.Parse(dtGeral.Compute("SUM(REENTREGA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            lucro = lucro - vlcalc;

            //lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
            float xx = 0;

            if (vl_fret > 0)
                xx = (lucro / vl_fret) * 100;

            dt.Rows[i][13] = xx;
        }




        dt.DefaultView.Sort = "LucroTotal desc";
        dt = dt.DefaultView.ToTable(true);


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            vl_nota = 0;
            vl_fret = 0;
            lucro = 0;

            html += "<tr>";
            html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i]["FLNOME"].ToString() + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "FLNOME='" + dt.Rows[i]["FLNOME"].ToString() + "'") + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESOBRUTO)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";

            lucro = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            if (vl_nota == 0)
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
            else
            {
                float perc_frete = (vl_fret / vl_nota);
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
            }

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</td>";
            lucro = lucro - float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            float calcPerc = float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

            if (vl_fret > 0)
                calcPerc = (calcPerc / vl_fret) * 100;
            else
                calcPerc = 0;

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", calcPerc) + "%</td>";


            float tx = (float.Parse(dt.Rows[i]["Imposto"].ToString()) / 100);
            float vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
            lucro = lucro - vlcalc;
            tot_imposto += vlcalc;

            tx = (float.Parse(dt.Rows[i]["SEGURO"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
            lucro = lucro - vlcalc;
            tot_seguro += vlcalc;

            tx = (float.Parse(dt.Rows[i]["TaxaAdministrativa"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
            lucro = lucro - vlcalc;
            tot_txadm += vlcalc;

            tx = (float.Parse(dt.Rows[i]["TaxaDeTranferencia"].ToString()) / 100);
            vlcalc = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString()) * tx;
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
            lucro = lucro - vlcalc;
            tot_txTransf += vlcalc;


            vlcalc = float.Parse(dtGeral.Compute("SUM(REENTREGA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc) + "</td>";
            lucro = lucro - vlcalc;
            tot_reentrega += vlcalc;


            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucro) + "</td>";
            tot_lucro += lucro;

            if (vl_fret > 0)
            {
                float x = (lucro / vl_fret) * 100;


                string css = "tdpRVerdanaVermelho";

                if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                    css = "tdpRVerdanaVerde";
                else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(10)))
                    css = "tdpRVerdanaAmarelo";
                else
                    css = "tdpRVerdanaVermelho";


                html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (x).ToString("#0.00") + "%</td>";
            }

            else
            {
                html += "<td class='tdpRVerdanaVermelho' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
            }


            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["GERENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["GERENTE"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["ASSISTENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["ASSISTENTE"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["LIDEROPERACIONAL"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["LIDEROPERACIONAL"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["CONFERENTE"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["CONFERENTE"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["SEPARADOR"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["SEPARADOR"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["LIMPEZA"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["LIMPEZA"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["EMPILHADOR"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["EMPILHADOR"].ToString()) + "</td>";
            html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dt.Rows[i]["OUTROS"].ToString() == "" ? "S/ CAD" : dt.Rows[i]["OUTROS"].ToString()) + "</td>";


            int totFuncFilial = (dt.Rows[i]["GERENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["GERENTE"].ToString()));
            totFuncFilial += (dt.Rows[i]["ASSISTENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["ASSISTENTE"].ToString()));
            totFuncFilial += (dt.Rows[i]["LIDEROPERACIONAL"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["LIDEROPERACIONAL"].ToString()));
            totFuncFilial += (dt.Rows[i]["CONFERENTE"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["CONFERENTE"].ToString()));
            totFuncFilial += (dt.Rows[i]["SEPARADOR"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["SEPARADOR"].ToString()));
            totFuncFilial += (dt.Rows[i]["LIMPEZA"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["LIMPEZA"].ToString()));
            totFuncFilial += (dt.Rows[i]["OUTROS"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["OUTROS"].ToString()));
            totFuncFilial += (dt.Rows[i]["EMPILHADOR"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["EMPILHADOR"].ToString()));

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px; backgroud-color:#CCC'><b>" + totFuncFilial + "</b></td>";




        }


        #region Totaliza



        html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b>TOTAL</td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dtGeral.Compute("SUM(ENTREGAS)", "") + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESOBRUTO)", "").ToString())) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "").ToString())) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "").ToString())) + "</b></td>";

        vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "").ToString());
        vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "").ToString());

        if (vl_nota == 0)
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", 0) + "</b></td>";
        else
        {
            float perc_frete = (vl_fret / vl_nota);
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + (perc_frete * 100).ToString("#0.00") + "%</b></td>";
        }

        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "").ToString())) + "</b></td>";


        float totPercmot = float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "").ToString());
        if (vl_fret > 0)
            totPercmot = (totPercmot / vl_fret) * 100;
        else
            totPercmot = 0;

        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", totPercmot) + "%</b></td>";


        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_imposto) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_seguro) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_txadm) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_txTransf) + "</b></td>";
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_reentrega) + "</b></td>";

        // tot_lucro = tot_lucro - tot_imposto - tot_seguro - tot_txTransf - tot_txadm - tot_reentrega;
        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_lucro) + "</b></td>";


        lucro = tot_lucro;

        if (vl_fret > 0)
        {
            float x = (lucro / vl_fret) * 100;


            string css = "tdpRVerdanaVermelho";

            if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                css = "tdpRVerdanaVerde";
            else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(10)))
                css = "tdpRVerdanaAmarelo";
            else
                css = "tdpRVerdanaVermelho";


            html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + (x).ToString("#0.00") + "%</b></td>";
        }

        else
        {
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + 0.ToString("#0.00") + "%</b></td>";
        }



        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(GERENTE)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(ASSISTENTE)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(CONFERENTE)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(SEPARADOR)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIMPEZA)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(EMPILHADOR)", "").ToString() + "</b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(OUTROS)", "").ToString() + "</b></td>";


        int totTotFunc = int.Parse(dt.Compute("SUM(GERENTE)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(ASSISTENTE)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(CONFERENTE)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(SEPARADOR)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(LIMPEZA)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(EMPILHADOR)", "").ToString());
        totTotFunc += int.Parse(dt.Compute("SUM(OUTROS)", "").ToString());

        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + totTotFunc + "</b></td>";







        #endregion


        html += "</TABLE>";

        if (mensal == false)
        {

            html += "<hr>";
            html += "<br>";

            dtGeral.Columns.Add("PercOrdem", System.Type.GetType("System.Decimal"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow[] linhas = dtGeral.Select("FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'", "NUMERO");

                //calcula a ordenacao

                for (int ic = 0; ic < linhas.Length; ic++)
                {
                    float lucrMotReg = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString());
                    lucrMotReg -= float.Parse(linhas[ic]["fretemotoristaRateado"].ToString());

                    float tx = (float.Parse(linhas[ic]["seguro"].ToString()) / 100);
                    float vlcalc = float.Parse(linhas[ic]["VALORDANOTA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;


                    tx = (float.Parse(linhas[ic]["TaxaAdministrativa"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;


                    tx = (float.Parse(linhas[ic]["TaxaDeTranferencia"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[ic]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;

                    lucrMotReg -= float.Parse(linhas[ic]["REENTREGA"].ToString());

                    if(float.Parse(linhas[ic]["FRETEEMPRESA"].ToString()) >0)
                        lucrMotReg = (lucrMotReg / float.Parse(linhas[ic]["FRETEEMPRESA"].ToString())) * 100;
                    else
                        lucrMotReg = 0;

                    linhas[ic]["PercOrdem"] = lucrMotReg;

                }


                html += "<table border='0'>";
                html += "<tr>";
                html += " <TD COLSPAN=4 > <B>FILIAL: " + dt.Rows[i]["FLNOME"].ToString().ToUpper() + " <B> <TD>";
                html += "</tr>";

                html += "<tr>";

                html += "<td>";
                html += "IMPOSTOS: " + linhas[0]["Imposto"].ToString() + " %";
                html += "</td>";


                html += "<td>";
                html += "SEGURO: " + linhas[0]["Seguro"].ToString() + " %";
                html += "</td>";

                html += "<td>";
                html += "TAXA ADMINISTRATIVA: " + linhas[0]["TaxaAdministrativa"].ToString() + " %";
                html += "</td>";

                html += "<td>";
                html += "TAXA TRANFERÊNCIA: " + linhas[0]["TaxaDeTranferencia"].ToString() + " %";
                html += "</td>";

                html += "</tr>";
                html += "</TABLE>";

                html += "<hr>";


                ///////////////////////////////////////////////////////////////
                #region Cabecalho
                html += "<table class='table' cellspacing=1 celpanding=1 >";
                html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";
                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>DT";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>REGIAO";
                html += "</td>";

                html += "<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA";
                html += "</td>";



                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRASNF";
                html += "</td>";


                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>REENTREGA";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                html += "</td>";


                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                html += "</td>";

                html += "</tr>";


                var result = from row in linhas
                             orderby row["PercOrdem"] descending
                             //                             orderby row["PercOrdem"], row["NUMERO"] descending
                             select row;

                linhas = result.ToArray();
                tot_imposto = 0;
                tot_lucro = 0;
                tot_reentrega = 0;
                tot_seguro = 0;
                tot_txadm = 0;
                tot_txTransf = 0;
                
                for (int io = 0; io < linhas.Length; io++)
                {

                    html += "<tr>";
                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["NUMERO"].ToString() + "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px;^text-align:center'>" + DateTime.Parse(linhas[io]["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    html += "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["REGIAO"].ToString() + "</td>";
                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["PMNOME"].ToString() + "</td>";
                    html += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + linhas[io]["ENTREGAS"].ToString() + "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PESOBRUTO"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALORDANOTA"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["FRETEEMPRESA"].ToString()));
                    html += "</td>";

                    decimal vl = (decimal.Parse(linhas[io]["FRETEEMPRESA"].ToString()) / decimal.Parse(linhas[io]["VALORDANOTA"].ToString())) * 100;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vl) + "%";
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["fretemotoristaRateado"].ToString()));
                    html += "</td>";


                    float tx = (float.Parse(linhas[io]["Imposto"].ToString()) / 100);

                    float vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;

                    tot_imposto += vlcalc;

                    float lucrMotReg = float.Parse(linhas[io]["FRETEEMPRESA"].ToString());
                    lucrMotReg -= float.Parse(linhas[io]["fretemotoristaRateado"].ToString());

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";
                    lucrMotReg -= vlcalc;


                    tx = (float.Parse(linhas[io]["seguro"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["VALORDANOTA"].ToString()) * tx;
                    tot_seguro += vlcalc;


                    lucrMotReg -= vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";

                    tx = (float.Parse(linhas[io]["TaxaAdministrativa"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;

                    tot_txadm += vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";


                    tx = (float.Parse(linhas[io]["TaxaDeTranferencia"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;

                    tot_txTransf += vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";


                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(linhas[io]["REENTREGA"].ToString()));
                    html += "</td>";
                    lucrMotReg -= float.Parse(linhas[io]["REENTREGA"].ToString());

                    tot_reentrega += float.Parse(linhas[io]["REENTREGA"].ToString());

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucrMotReg);
                    html += "</td>";

                    tot_lucro += lucrMotReg;

                    if (float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) <= 0)
                    {
                        lucrMotReg = -100;
                    }
                    else
                    lucrMotReg = (lucrMotReg / float.Parse(linhas[io]["FRETEEMPRESA"].ToString())) * 100;

                    string cs = "";
                    if (lucrMotReg >= float.Parse("12"))
                        cs = "tdpRVerdanaVerde";
                    else if (lucrMotReg < float.Parse("12") && lucrMotReg >= float.Parse("10"))
                        cs = "tdpRVerdanaAmarelo";
                    else
                        cs = "tdpRVerdanaVermelho";

                    html += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucrMotReg) + "%";
                    html += "</td>";

                    html += "</tr>";
                }

                #region Totaliza iTEMS



                html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b></td>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b></td>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b></td>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b>TOTAL</td>";

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dtGeral.Compute("SUM(ENTREGAS)","FILIAL='" + dt.Rows[i]["FLNOME"].ToString() +"'") + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESOBRUTO)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</b></td>";

                vl_nota = float.Parse(dtGeral.Compute("SUM(VALORDANOTA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(FRETEEMPRESA)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());

                if (vl_nota == 0)
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", 0) + "</b></td>";
                else
                {
                    float perc_frete = (vl_fret / vl_nota);
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + (perc_frete * 100).ToString("#0.00") + "%</b></td>";
                }

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString())) + "</b></td>";


                totPercmot = float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                if (vl_fret > 0)
                    totPercmot = (totPercmot / vl_fret) * 100;
                else
                    totPercmot = 0;

                //html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", totPercmot) + "%</b></td>";


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_imposto) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_seguro) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_txadm) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_txTransf) + "</b></td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_reentrega) + "</b></td>";

                tot_lucro = vl_fret - tot_imposto - tot_seguro - tot_txTransf - tot_txadm - tot_reentrega - float.Parse(dtGeral.Compute("SUM(fretemotoristaRateado)", "FILIAL='" + dt.Rows[i]["FLNOME"].ToString() + "'").ToString());
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_lucro) + "</b></td>";


                lucro = tot_lucro;

                if (vl_fret > 0)
                {
                    float x = (lucro / vl_fret) * 100;


                    string css = "tdpRVerdanaVermelho";

                    if ((decimal.Parse(x.ToString())) >= Convert.ToDecimal(12))
                        css = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(x.ToString()) < Convert.ToDecimal(12) && (decimal.Parse(x.ToString()) * 100) >= Convert.ToDecimal(10)))
                        css = "tdpRVerdanaAmarelo";
                    else
                        css = "tdpRVerdanaVermelho";


                    html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + (x).ToString("#0.00") + "%</b></td>";
                }

                else
                {
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + 0.ToString("#0.00") + "%</b></td>";
                }



                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(GERENTE)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(ASSISTENTE)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(CONFERENTE)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(SEPARADOR)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIMPEZA)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(EMPILHADOR)", "").ToString() + "</b></td>";
                //html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(OUTROS)", "").ToString() + "</b></td>";


                //int totTotFunc = int.Parse(dt.Compute("SUM(GERENTE)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(ASSISTENTE)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(CONFERENTE)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(SEPARADOR)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(LIMPEZA)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(EMPILHADOR)", "").ToString());
                //totTotFunc += int.Parse(dt.Compute("SUM(OUTROS)", "").ToString());

                //html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + totTotFunc + "</b></td>";
                

                #endregion


                #endregion
                html += "</table>";
                html += "<hr>";
                html += "<br>";
            }
        }


        html += "</BODY>";
        html += "</html>";

        PhTotais.Controls.Add(new LiteralControl(html));
        //#region resumido
        //PhTotais.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));

        //if (dtTodosOsDados.Rows.Count > 0)
        //{
        //    #region Cabecalho
        //    PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
        //    #endregion
        //}

        ////pega as filiais

     

        //DataView dw = dtTodosOsDados.DefaultView;
        //distinctDataTable = dw.ToTable(true, "NomeFilial");
        //distinctDataTable.Columns.Add("Ordem", Type.GetType("System.Decimal"));

        //for (int i1 = 0; i1 < distinctDataTable.Rows.Count; i1++)
        //{
        //    decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());
        //    decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());

        //    if (dcalcfrete > 0)
        //        distinctDataTable.Rows[i1]["Ordem"] = Convert.ToDecimal((lucro / dcalcfrete).ToString());
        //    else
        //        distinctDataTable.Rows[i1]["Ordem"] = 0;

        //}

        //DataView dv = distinctDataTable.DefaultView;
        //dv.Sort = "Ordem Desc";
        //distinctDataTable = dv.ToTable();


        //decimal perFrete = 0;
        //decimal perFretetot = 0, impostptot = 0, segutotot = 0, admtot = 0, tranftot = 0, freteMottot = 0, lucrotot = 0, totPerLucro = 0;

        
        //dtApurarTotal.Columns.Add("FILIAL");
        //dtApurarTotal.Columns.Add("ENTREGAS");
        //dtApurarTotal.Columns.Add("PESO");
        //dtApurarTotal.Columns.Add("VALORDANOTA");
        //dtApurarTotal.Columns.Add("FRETE");
        //dtApurarTotal.Columns.Add("PERCFRETE");
        //dtApurarTotal.Columns.Add("FRETEMOT");
        //dtApurarTotal.Columns.Add("IMPOSTOS");
        //dtApurarTotal.Columns.Add("SEGURO");
        //dtApurarTotal.Columns.Add("ADM");        
        //dtApurarTotal.Columns.Add("TRANF");
        //dtApurarTotal.Columns.Add("LUCRO");
        //dtApurarTotal.Columns.Add("PERCLUCRO");       											


        //foreach (DataRow item in distinctDataTable.Rows)
        //{
        //    DataRow rwdtApurarTotal = dtApurarTotal.NewRow();


        //    PhTotais.Controls.Add(new LiteralControl(@"<tr>"));

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NomeFilial"].ToString()));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["FILIAL"] = item["NomeFilial"].ToString();

        //    int calc = int.Parse(dtTodosOsDados.Compute("SUM(entregas)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + calc.ToString()));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["ENTREGAS"] = calc;

        //    decimal dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(peso)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["PESO"] = dcalc;

        //    dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["VALORDANOTA"] = dcalc;

        //    decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfrete.ToString("###,###.##")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["FRETE"] = dcalcfrete;

        //    if (dcalc > 0)
        //        perFrete = ((dcalcfrete / dcalc) * 100);
        //    else
        //        perFrete = 0;


        //    perFretetot += perFrete;
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + perFrete.ToString("#0.000") + "%"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["PERCFRETE"] = perFrete;


        //    decimal dcalcfreteMot = decimal.Parse(dtTodosOsDados.Compute("SUM(FreteMotorista)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    freteMottot += dcalcfreteMot;
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfreteMot.ToString("#0.00")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["FRETEMOT"] = dcalcfreteMot;



        //    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + item["NomeFilial"].ToString() + "'");

        //    decimal imposto = decimal.Parse(rw[0]["Imposto"].ToString()) / 100;
        //    impostptot += (dcalcfrete * imposto);
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * imposto).ToString("###,###.##")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["IMPOSTOS"] = (dcalcfrete * imposto);


        //    decimal seguro = decimal.Parse(rw[0]["seguro"].ToString()) / 100;

        //    segutotot += decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro;
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro).ToString("#0.00")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["SEGURO"] = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());


        //    decimal adm = decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()) / 100;
        //    admtot += (dcalcfrete * adm);
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * adm).ToString("#0.00")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["ADM"] = (dcalcfrete * adm).ToString("#0.00");


        //    decimal transf = decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()) / 100;
        //    tranftot += (dcalcfrete * transf);
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * transf).ToString("#0.00")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["TRANF"] = (dcalcfrete * transf).ToString("#0.00");


        //    decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
        //    lucrotot += lucro;
        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + lucro.ToString("#0.00")));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["LUCRO"] = lucro;

        //    if(dcalcfrete>0)
        //        totPerLucro += ((lucro / dcalcfrete) * 100);

        //    string cs = "tdpRVerdanaVermelho";
        //    decimal calcPercLucro = (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100) : 0);

        //    if (calcPercLucro >= Convert.ToDecimal(12))
        //        cs = "tdpRVerdanaVerde";
        //    else if (calcPercLucro < Convert.ToDecimal(12) && calcPercLucro >= Convert.ToDecimal(10))
        //        cs = "tdpRVerdanaAmarelo";
        //    else
        //        cs = "tdpRVerdanaVermelho";

        //    PhTotais.Controls.Add(new LiteralControl(@"<td class='"+cs+"' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100).ToString("#0.000") : "0.00") + "%"));
        //    PhTotais.Controls.Add(new LiteralControl(@"</td>"));
        //    rwdtApurarTotal["PERCLUCRO"] = (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100) : 0);

        //    dtApurarTotal.Rows.Add(rwdtApurarTotal);
        //    PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
        //}


        //#region Totais
        //PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dtTodosOsDados.Compute("SUM(entregas)", "").ToString()));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "").ToString()).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString()).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (perFretetot / distinctDataTable.Rows.Count).ToString("#0.000") + "%"));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));


        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (freteMottot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (impostptot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (segutotot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (admtot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (tranftot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (lucrotot).ToString("###,###.##")));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //decimal calxc = lucrotot / decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString());
        //calxc = calxc * 100;

        //string css = "tdpRVerdanaVermelho";        



        //if (calxc >= Convert.ToDecimal(12))
        //    css = "tdpRVerdanaVerde";
        //else if (calxc < Convert.ToDecimal(12) && calxc >= Convert.ToDecimal(10))
        //    css = "tdpRVerdanaAmarelo";
        //else
        //    css = "tdpRVerdanaVermelho";


        //PhTotais.Controls.Add(new LiteralControl(@"<td class='"+ css+"' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + calxc.ToString("#0.000") + "%"));
        //PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        //PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
        //#endregion

        //PhTotais.Controls.Add(new LiteralControl(@"</table>"));

        //#endregion

        //for (int i = 0; i < distinctDataTable.Rows.Count; i++)
        //{
        //    PhTotais.Controls.Add(new LiteralControl(@"<br>"));
        //    PhTotais.Controls.Add(new LiteralControl(@"<hr>"));

        //    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "'");

        //    PhTotais.Controls.Add(new LiteralControl(@"<b><font size='2pt'>FILIAL: " + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp |   IMPOSTO: " + decimal.Parse(rw[0]["Imposto"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | SEGURO: " + decimal.Parse(rw[0]["Seguro"].ToString()).ToString("#0.00") + "%    &nbsp &nbsp &nbsp | TX. ADM: " + decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | TX. TRANFERENCIA: " + decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()).ToString("#0.00") + "% </font> </b>"));
        //    ExpandirPorFilial(distinctDataTable.Rows[i]["NomeFilial"].ToString());

        //}

    }

    private void ExpandirPorFilial(string CodFilial)
    {
        #region ExpandirPorFilial
        PhTotais.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));

        if (dtTodosOsDados.Rows.Count > 0)
        {
            #region Cabecalho
            PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>RE"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>EMISSAO"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>MOTORISTA"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ENTREGAS"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>PESO"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>VALOR DA NOTA"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% FRETE"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>FRETE MOT"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion
        }

        //pega as filiais

       // DataRow[] orw = dtTodosOsDados.Select("NomeFilial='" + CodFilial + "'");
        DataRow[] orw = dtTodosOsDados.Select("NomeFilial='" + CodFilial + "'", "PercLucro desc");


        decimal perFrete = 0;
        decimal perFretetot = 0, impostptot = 0, segutotot = 0, admtot = 0, tranftot = 0, freteMottot = 0, lucrotot = 0, totPerLucro = 0;

        foreach (DataRow item in orw)
        {

            PhTotais.Controls.Add(new LiteralControl(@"<tr>"));
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["relacaodeentrega"].ToString()));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + DateTime.Parse(item["emissao"].ToString()).ToString("dd/MM/yyyy")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["nomemotorista"].ToString()));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            int calc = int.Parse(dtTodosOsDados.Compute("SUM(entregas)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + calc.ToString()));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            decimal dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(peso)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));


            dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfrete.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            if (dcalc > 0)
                perFrete = ((dcalcfrete / dcalc) * 100);
            else
                perFrete = 0;

            perFretetot += perFrete;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalc>0?((dcalcfrete / dcalc) * 100).ToString("#0.000"):"0.00") + "%"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));



            decimal dcalcfreteMot = decimal.Parse(dtTodosOsDados.Compute("SUM(FreteMotorista)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            freteMottot += dcalcfreteMot;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfreteMot.ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));


            DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + item["NomeFilial"].ToString() + "'");

            decimal imposto = decimal.Parse(rw[0]["Imposto"].ToString()) / 100;
            impostptot += (dcalcfrete * imposto);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * imposto).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            decimal seguro = decimal.Parse(rw[0]["seguro"].ToString()) / 100;

            segutotot += decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString()) * seguro;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString()) * seguro).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            decimal adm = decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()) / 100;
            admtot += (dcalcfrete * adm);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * adm).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            decimal transf = decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()) / 100;
            tranftot += (dcalcfrete * transf);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * transf).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));


            decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "relacaodeentrega='" + item["relacaodeentrega"].ToString() + "'").ToString());
            lucrotot += lucro;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + lucro.ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            if(dcalcfrete>0)
                totPerLucro += ((lucro / dcalcfrete) * 100);

            string cs = "tdpRVerdanaVermelho";
            decimal dlcr = 0;

            if (dcalcfrete > 0)
                dlcr = ((lucro / dcalcfrete) * 100);

            if (dlcr >= Convert.ToDecimal(12))
                cs = "tdpRVerdanaVerde";
            else if (dlcr < Convert.ToDecimal(12) && dlcr >= Convert.ToDecimal(10))
                cs = "tdpRVerdanaAmarelo";
            else
                cs = "tdpRVerdanaVermelho";

            PhTotais.Controls.Add(new LiteralControl(@"<td class='"+cs+"' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100).ToString("#0.000") : "0.00") + "%"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"</tr>"));

          
        }

  #region Totais

            DataRow[] orew = dtApurarTotal.Select("filial='" + CodFilial + "'", "");
            PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + orew[0]["ENTREGAS"]));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PESO"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["VALORDANOTA"].ToString().ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["FRETE"].ToString().ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PERCFRETE"].ToString()).ToString("#0.000") + "%"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));


            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["FRETEMOT"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["IMPOSTOS"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["SEGURO"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["ADM"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["TRANF"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["LUCRO"].ToString()).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            string scs = "tdpRVerdanaVermelho";
            if (decimal.Parse(orew[0]["PERCLUCRO"].ToString()) >= Convert.ToDecimal(12))
                scs = "tdpRVerdanaVerde";
            else if (decimal.Parse(orew[0]["PERCLUCRO"].ToString()) < Convert.ToDecimal(12) && decimal.Parse(orew[0]["PERCLUCRO"].ToString()) >= Convert.ToDecimal(10))
                scs = "tdpRVerdanaAmarelo";
            else
                scs = "tdpRVerdanaVermelho";

            PhTotais.Controls.Add(new LiteralControl(@"<td class='" + scs + "' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(orew[0]["PERCLUCRO"].ToString()).ToString("#0.000")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));

            PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion
      

        PhTotais.Controls.Add(new LiteralControl(@"</table>"));

        #endregion
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Gerar();
    }
}
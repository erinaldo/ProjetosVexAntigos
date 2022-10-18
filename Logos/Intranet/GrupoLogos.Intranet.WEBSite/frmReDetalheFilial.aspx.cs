using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using ChartDirector;
using System.IO;
using System.Drawing;

public partial class frmReDetalheFilial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Di = Session["txtI"].ToString();
            string Df = Session["txtf"].ToString();
            DataTable dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC PRC_RE_MENSAL_LISTAR '" + DateTime.Parse(Di ).ToString("yyyy-MM-dd") + "', '" + DateTime.Parse(Df).ToString("yyyy-MM-dd") + "'", Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];



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

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>IMPOSTOS";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>SEGURO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>ADM";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
            html += "</td>";

            html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
            html += "</td>";

            html += "</tr>";
            #endregion

            DataView view = new DataView(dtGeral);
            DataTable dt = view.ToTable(true, "FILIAL");
            dt.Columns.Add("LucroTotal", typeof(decimal));

            float tot_despesas = 0;
            float tot_frete_mot = 0;
            float tot_imposto = 0;
            float tot_seguro = 0;
            float tot_txadm = 0;
            float tot_txTransf = 0;
            float lucro = 0;
            float vl_fret = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                float xx = 0;

                if (vl_fret > 0)
                    xx = (lucro / vl_fret) * 100;

                dt.Rows[i][1] = xx;
            }

            dt.DefaultView.Sort = "LucroTotal desc";
            dt = dt.DefaultView.ToTable(true);

            float vl_nota = 0;
            // float vl_fret = 0;

            //float tot_despesas = 0;
            //float tot_frete_mot = 0;
            //float tot_imposto = 0;
            //float tot_seguro = 0;
            //float tot_txadm = 0;
            //float tot_txTransf = 0;
            //float lucro = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vl_nota = 0;
                vl_fret = 0;
                tot_despesas = 0;
                tot_frete_mot = 0;
                tot_imposto = 0;
                tot_seguro = 0;
                tot_txadm = 0;
                tot_txTransf = 0;
                lucro = 0;

                html += "<tr>";
                html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dt.Rows[i]["FILIAL"].ToString() + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'") + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                if (vl_nota == 0)
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
                else
                {
                    float perc_frete = (vl_fret / vl_nota);
                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
                }

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";

                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString())) + "</td>";


                tot_despesas = 0;
                tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_imposto = float.Parse(dtGeral.Compute("SUM(VLIMPOSTOS)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_seguro = float.Parse(dtGeral.Compute("SUM(VLSEGURO)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txadm = float.Parse(dtGeral.Compute("SUM(ADM)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());
                tot_txTransf = float.Parse(dtGeral.Compute("SUM(TRANSF)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

                tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

                lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'").ToString());

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

            }


            #region Totaliza



            html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;  font-weight: bold;'>";

            html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>TOTAL</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dtGeral.Compute("SUM(ENTREGAS)", "") + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(PESO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString())) + "</td>";

            vl_nota = float.Parse(dtGeral.Compute("SUM(VALOR_DA_NOTA)", "").ToString());
            vl_fret = float.Parse(dtGeral.Compute("SUM(VALOR_DO_FRETE)", "").ToString());

            if (vl_nota == 0)
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", 0) + "</td>";
            else
            {
                float perc_frete = (vl_fret / vl_nota);
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (perc_frete * 100).ToString("#0.00") + "%</td>";
            }

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString())) + "</td>";

            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(adm)", "").ToString())) + "</td>";
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(transf)", "").ToString())) + "</td>";


            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString())) + "</td>";


            tot_despesas = 0;
            tot_frete_mot = float.Parse(dtGeral.Compute("SUM(FRETE_MOTORISTA)", "").ToString());
            tot_imposto = float.Parse(dtGeral.Compute("SUM(vlIMPOSTOS)", "").ToString());
            tot_seguro = float.Parse(dtGeral.Compute("SUM(vlSEGURO)", "").ToString());
            tot_txadm = float.Parse(dtGeral.Compute("SUM(adm)", "").ToString());
            tot_txTransf = float.Parse(dtGeral.Compute("SUM(transf)", "").ToString());

            tot_despesas = tot_frete_mot + tot_imposto + tot_seguro + tot_txadm + tot_txTransf;

            lucro = float.Parse(dtGeral.Compute("SUM(lucro)", "").ToString());

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
                html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + 0.ToString("#0.00") + "%</td>";
            }

            #endregion


            html += "</TABLE>";

            html += "<hr>";
            html += "<br>";


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow[] linhas = dtGeral.Select("FILIAL='" + dt.Rows[i]["FILIAL"].ToString() + "'", "PER_LUCRO DESC");
                html += "<table border='0'>";
                html += "<tr>";
                html += " <TD COLSPAN=4 > <B>FILIAL: " + dt.Rows[i]["FILIAL"].ToString().ToUpper() + " <B> <TD>";
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

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>TRANF";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LUCRO";
                html += "</td>";

                html += "<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>% LUCRO";
                html += "</td>";

                html += "</tr>";


                for (int io = 0; io < linhas.Length; io++)
                {

                    html += "<tr>";
                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["NUMERO"].ToString() + "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px;^text-align:center'>" + DateTime.Parse(linhas[io]["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                    html += "</td>";

                    html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + linhas[io]["MOTORISTA"].ToString() + "</td>";
                    html += "<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + linhas[io]["ENTREGAS"].ToString() + "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PESO"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALOR_DA_NOTA"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VALOR_DO_FRETE"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PERC_FRETE"].ToString())) + "%";
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["FRETE_MOTORISTA"].ToString()));
                    html += "</td>";


                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VLIMPOSTOS"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["VLSEGURO"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["ADM"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["TRANSF"].ToString()));
                    html += "</td>";

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["LUCRO"].ToString()));
                    html += "</td>";


                    string cs = "";
                    if ((decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) >= Convert.ToDecimal(12))
                        cs = "tdpRVerdanaVerde";
                    else if ((decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) < Convert.ToDecimal(12) && (decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) >= Convert.ToDecimal(10))
                        cs = "tdpRVerdanaAmarelo";
                    else
                        cs = "tdpRVerdanaVermelho";

                    html += "<td class='" + cs + "' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", decimal.Parse(linhas[io]["PER_LUCRO"].ToString())) + "%";
                    html += "</td>";

                    html += "</tr>";
                }
                #endregion

                html += "</table>";
                html += "<hr>";
                html += "<br>";



            }

            html += "</BODY>";
            html += "</html>";
            
        }
    }
}
            
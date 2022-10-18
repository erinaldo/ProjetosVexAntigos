using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class frmgdrExcelSobras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["t"] == null)
        {
            DataTable DS = (DataTable)Session["dt"];
            GridView1.DataSource = DS;
            GridView1.DataBind();
            gerarExcel();
        }
        else
        {
            GerarRe();
            gerarExcel();
        }
    }

    private void GerarRe()
    {

        bool mensal = true;

        DataTable dtGeral;
     

        string S = "SELECT IDDT, IDROMANEIO,REGIAO,NUMERO,EMISSAO,PLACA,PMNOME,TIPODEDT,FLNUMERO,FLNOME,FILIAL,TRNOME,CAPACIDADEDECARGAKG,VOLUMES,VALORDANOTA,PBRUTOTOTAL,PESOBRUTO,NOTASFISCAIS,ENTREGAS,FRETEMOTORISTARATEADO,FRETEMOTORISTA,FRETEEMPRESA,REENTREGA,QTD_REENTREGA,GERENTE,ASSISTENTE,LIDEROPERACIONAL,CONFERENTE,SEPARADOR,LIMPEZA,OUTROS, isnull(EMPILHADOR,0) EMPILHADOR,F.IMPOSTO,F.SEGURO,F.TAXAADMINISTRATIVA,F.TAXADETRANFERENCIA FROM FACE_RE  RE WITH (NOLOCK) INNER JOIN FILIAL   F WITH (NOLOCK)   ON F.NOME = FLNOME ";
        S += " WHERE EMISSAO BETWEEN '" + DateTime.Parse(Session["ini"].ToString()).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(Session["fim"].ToString()).ToString("yyyy-MM-dd") + "' AND FLNOME <> 'VEX TRANSFERENCIA'  AND F.ATIVO='SIM'";
        dtGeral = Sistran.Library.GetDataTables.RetornarDataSetWS(S, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos()).Tables[0];

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



        html += "<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px;'>";

        html += "<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'><b>TOTAL</b></td>";
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

        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", totPercmot) + "%</b></td>";


        html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + string.Format("{0:0,0.00}", tot_imposto) + "<</b>/td>";
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


            html += "<td class='" + css + "' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + (x).ToString("#0.00") + "%</td>";
        }

        else
        {
            html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + 0.ToString("#0.00") + "%</td>";
        }



        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(GERENTE)", "").ToString() + "</b></b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(ASSISTENTE)", "").ToString() + "</b></b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIDEROPERACIONAL)", "").ToString() + "</b></b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(CONFERENTE)", "").ToString() + "</b></b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(SEPARADOR)", "").ToString() + "</b></b></td>";
        html += " <td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><b>" + dt.Compute("SUM(LIMPEZA)", "").ToString() + "</b></b></td>";
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

                    lucrMotReg = (lucrMotReg / float.Parse(linhas[ic]["FRETEEMPRESA"].ToString())) * 100;

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
                             orderby row["NUMERO"] descending
                             select row;

                linhas = result.ToArray();

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

                    float lucrMotReg = float.Parse(linhas[io]["FRETEEMPRESA"].ToString());
                    lucrMotReg -= float.Parse(linhas[io]["fretemotoristaRateado"].ToString());

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";


                    tx = (float.Parse(linhas[io]["seguro"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["VALORDANOTA"].ToString()) * tx;

                    lucrMotReg -= vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";

                    tx = (float.Parse(linhas[io]["TaxaAdministrativa"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";


                    tx = (float.Parse(linhas[io]["TaxaDeTranferencia"].ToString()) / 100);
                    vlcalc = float.Parse(linhas[io]["FRETEEMPRESA"].ToString()) * tx;
                    lucrMotReg -= vlcalc;

                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", vlcalc);
                    html += "</td>";


                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", float.Parse(linhas[io]["REENTREGA"].ToString()));
                    html += "</td>";
                    lucrMotReg -= float.Parse(linhas[io]["REENTREGA"].ToString());


                    html += "<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + string.Format("{0:0,0.00}", lucrMotReg);
                    html += "</td>";


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
                #endregion
                html += "</table>";
                html += "<hr>";
                html += "<br>";



            }


        }


        html += "</BODY>";
        html += "</html>";

        PhTotais.Controls.Add(new LiteralControl(html));
    }

    private void gerarExcel()
    {
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=excel" + Guid.NewGuid() + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);

        if (Request.QueryString["t"] == null)
            form.Controls.Add(GridView1);
        else
            form.Controls.Add(Panel1);


        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
}
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

public partial class RelatorioProducaoRE_Resumo : System.Web.UI.Page
{
    DataTable dtTodosOsDados;
    DataTable distinctDataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {
            string[] DataConf = FuncoesGerais.DataConf();
            txtI.Text = DataConf[0];
            txtF.Text = DataConf[1];
        }

        //Gerar();
        this.Title = "Relatório de Produção por Relação de Entrega";
    }

    private void Gerar()
    {
        dtTodosOsDados = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC GERAR_PRODUCAO_RE_V2 '" + txtI.Text + "', '" + txtF.Text + "'", Sistran.Library.Robo.Robo.RetornarStringBaseAntiga()).Tables[0];


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

            if (Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString()) > 0)
                dtTodosOsDados.Rows[i]["perclucro"] = (Convert.ToDecimal(dtTodosOsDados.Rows[i]["lucro"]) / Convert.ToDecimal(dtTodosOsDados.Rows[i]["Frete"].ToString())) * 100;
            else
                dtTodosOsDados.Rows[i]["perclucro"] = 0;

        }

        MontarTotais();
    }

    DataTable dtApurarTotal = new DataTable();
    private void MontarTotais()
    {
        #region resumido
        PhTotais.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));

        if (dtTodosOsDados.Rows.Count > 0)
        {
            #region Cabecalho
            PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL"));
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

     

        DataView dw = dtTodosOsDados.DefaultView;
        distinctDataTable = dw.ToTable(true, "NomeFilial");
        distinctDataTable.Columns.Add("Ordem", Type.GetType("System.Decimal"));

        for (int i1 = 0; i1 < distinctDataTable.Rows.Count; i1++)
        {
            decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());
            decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + distinctDataTable.Rows[i1]["NomeFilial"].ToString() + "'").ToString());

            if (dcalcfrete > 0)
                distinctDataTable.Rows[i1]["Ordem"] = Convert.ToDecimal((lucro / dcalcfrete).ToString());
            else
                distinctDataTable.Rows[i1]["Ordem"] = 0;

        }

        DataView dv = distinctDataTable.DefaultView;
        dv.Sort = "Ordem Desc";
        distinctDataTable = dv.ToTable();


        decimal perFrete = 0;
        decimal perFretetot = 0, impostptot = 0, segutotot = 0, admtot = 0, tranftot = 0, freteMottot = 0, lucrotot = 0, totPerLucro = 0;

        
        dtApurarTotal.Columns.Add("FILIAL");
        dtApurarTotal.Columns.Add("ENTREGAS");
        dtApurarTotal.Columns.Add("PESO");
        dtApurarTotal.Columns.Add("VALORDANOTA");
        dtApurarTotal.Columns.Add("FRETE");
        dtApurarTotal.Columns.Add("PERCFRETE");
        dtApurarTotal.Columns.Add("FRETEMOT");
        dtApurarTotal.Columns.Add("IMPOSTOS");
        dtApurarTotal.Columns.Add("SEGURO");
        dtApurarTotal.Columns.Add("ADM");        
        dtApurarTotal.Columns.Add("TRANF");
        dtApurarTotal.Columns.Add("LUCRO");
        dtApurarTotal.Columns.Add("PERCLUCRO");       											


        foreach (DataRow item in distinctDataTable.Rows)
        {
            DataRow rwdtApurarTotal = dtApurarTotal.NewRow();


            PhTotais.Controls.Add(new LiteralControl(@"<tr>"));

            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NomeFilial"].ToString()));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["FILIAL"] = item["NomeFilial"].ToString();

            int calc = int.Parse(dtTodosOsDados.Compute("SUM(entregas)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:8pt;height:10px'>" + calc.ToString()));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["ENTREGAS"] = calc;

            decimal dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(peso)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["PESO"] = dcalc;

            dcalc = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalc.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["VALORDANOTA"] = dcalc;

            decimal dcalcfrete = decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfrete.ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["FRETE"] = dcalcfrete;

            if (dcalc > 0)
                perFrete = ((dcalcfrete / dcalc) * 100);
            else
                perFrete = 0;


            perFretetot += perFrete;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + perFrete.ToString("#0.000") + "%"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["PERCFRETE"] = perFrete;


            decimal dcalcfreteMot = decimal.Parse(dtTodosOsDados.Compute("SUM(FreteMotorista)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            freteMottot += dcalcfreteMot;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + dcalcfreteMot.ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["FRETEMOT"] = dcalcfreteMot;



            DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + item["NomeFilial"].ToString() + "'");

            decimal imposto = decimal.Parse(rw[0]["Imposto"].ToString()) / 100;
            impostptot += (dcalcfrete * imposto);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * imposto).ToString("###,###.##")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["IMPOSTOS"] = (dcalcfrete * imposto);


            decimal seguro = decimal.Parse(rw[0]["seguro"].ToString()) / 100;

            segutotot += decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString()) * seguro).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["SEGURO"] = decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());


            decimal adm = decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()) / 100;
            admtot += (dcalcfrete * adm);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * adm).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["ADM"] = (dcalcfrete * adm).ToString("#0.00");


            decimal transf = decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()) / 100;
            tranftot += (dcalcfrete * transf);
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete * transf).ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["TRANF"] = (dcalcfrete * transf).ToString("#0.00");


            decimal lucro = decimal.Parse(dtTodosOsDados.Compute("SUM(LUCRO)", "NomeFilial='" + item["NomeFilial"].ToString() + "'").ToString());
            lucrotot += lucro;
            PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + lucro.ToString("#0.00")));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["LUCRO"] = lucro;

            if(dcalcfrete>0)
                totPerLucro += ((lucro / dcalcfrete) * 100);

            string cs = "tdpRVerdanaVermelho";
            decimal calcPercLucro = (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100) : 0);

            if (calcPercLucro >= Convert.ToDecimal(12))
                cs = "tdpRVerdanaVerde";
            else if (calcPercLucro < Convert.ToDecimal(12) && calcPercLucro >= Convert.ToDecimal(10))
                cs = "tdpRVerdanaAmarelo";
            else
                cs = "tdpRVerdanaVermelho";

            PhTotais.Controls.Add(new LiteralControl(@"<td class='"+cs+"' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100).ToString("#0.000") : "0.00") + "%"));
            PhTotais.Controls.Add(new LiteralControl(@"</td>"));
            rwdtApurarTotal["PERCLUCRO"] = (dcalcfrete > 0 ? ((lucro / dcalcfrete) * 100) : 0);

            dtApurarTotal.Rows.Add(rwdtApurarTotal);
            PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
        }


        #region Totais
        PhTotais.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + dtTodosOsDados.Compute("SUM(entregas)", "").ToString()));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(valordanota)", "").ToString()).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString()).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (perFretetot / distinctDataTable.Rows.Count).ToString("#0.000") + "%"));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));


        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (freteMottot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (impostptot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (segutotot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (admtot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (tranftot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + (lucrotot).ToString("###,###.##")));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        decimal calxc = lucrotot / decimal.Parse(dtTodosOsDados.Compute("SUM(frete)", "").ToString());
        calxc = calxc * 100;

        string css = "tdpRVerdanaVermelho";        



        if (calxc >= Convert.ToDecimal(12))
            css = "tdpRVerdanaVerde";
        else if (calxc < Convert.ToDecimal(12) && calxc >= Convert.ToDecimal(10))
            css = "tdpRVerdanaAmarelo";
        else
            css = "tdpRVerdanaVermelho";


        PhTotais.Controls.Add(new LiteralControl(@"<td class='"+ css+"' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + calxc.ToString("#0.000") + "%"));
        PhTotais.Controls.Add(new LiteralControl(@"</td>"));

        PhTotais.Controls.Add(new LiteralControl(@"</tr>"));
        #endregion

        PhTotais.Controls.Add(new LiteralControl(@"</table>"));

        #endregion

        //for (int i = 0; i < distinctDataTable.Rows.Count; i++)
        //{
        //    PhTotais.Controls.Add(new LiteralControl(@"<br>"));
        //    PhTotais.Controls.Add(new LiteralControl(@"<hr>"));

        //    DataRow[] rw = dtTodosOsDados.Select("NomeFilial='" + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "'");

        //    PhTotais.Controls.Add(new LiteralControl(@"<b><font size='2pt'>FILIAL: " + distinctDataTable.Rows[i]["NomeFilial"].ToString() + "   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp |   IMPOSTO: " + decimal.Parse(rw[0]["Imposto"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | SEGURO: " + decimal.Parse(rw[0]["Seguro"].ToString()).ToString("#0.00") + "%    &nbsp &nbsp &nbsp | TX. ADM: " + decimal.Parse(rw[0]["TaxaAdministrativa"].ToString()).ToString("#0.00") + "%   &nbsp &nbsp &nbsp | TX. TRANFERENCIA: " + decimal.Parse(rw[0]["TaxaDeTranferencia"].ToString()).ToString("#0.00") + "% </font> </b>"));
        //   // ExpandirPorFilial(distinctDataTable.Rows[i]["NomeFilial"].ToString());

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
        TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
        if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
            return;
        }

        if (ts.Days > 30)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + 30 + " dias.')", true);
            return;
        }

        Gerar();
    }
}
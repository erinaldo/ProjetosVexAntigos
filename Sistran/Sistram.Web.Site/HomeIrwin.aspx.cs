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

public partial class HomeIrwin : System.Web.UI.Page
{

    DateTime? ini;
    DateTime? fim;

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        Session["DataConf"] = inicio + "|" + DateTime.Now.ToShortDateString();
        lblPeriodo.Text = inicio.ToShortDateString() + " à " + DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString() + "  -|- @@ultima";

        string[] DataConf = FuncoesGerais.DataConf();
         ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
         fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


        if (!IsPostBack)
        {
            //Timer1.Enabled = true;
            Timer1.Interval = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervaloRefresh"]) * (60000/2);
            Recaregar();
        }

    }


    private void Recaregar()
    {
        Timer1.Enabled = false;
        carregarNotaFiscalDeEntrada(ini, fim);
        carregarNotaFiscalDeSaida(ini, fim);
        carregarPedidoDeVenda(ini, fim);
        carregarPedidoMontagemKit(ini, fim);
        carregarPedidoEmSeparacao(ini, fim);
        carregarPedidoDeKiteEmSeparacao(ini, fim);
        carregarEntradaDeKit(ini, fim);
        carregarEntradaParaArmazenagem(ini, fim);
        carregarNotasFiscaisAguardandoEmbarque(ini, fim);
        carregarPedidosEmAndamento(ini, fim);

        DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        lblPeriodo.Text = inicio.ToShortDateString() + " à " + DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString() + "  -|- Última Atualização: " + DateTime.Now.ToShortTimeString();
        
        Timer1.Enabled = true;
    }

    private void carregarPedidosEmAndamento(DateTime? ini, DateTime? fim)
    {
        phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidoEmAndamento(ini, fim);
        int qtdMinut01 = 0;
        int qtdMinut02 = 0;
        int qtdMinut03 = 0;
        int qtdMinut04 = 0;


        DataRow[] orw = dt.Select("INICIODASEPARACAO IS NULL OR DATAHORATERMINOCONFERENCIA IS NULL OR DATAHORABAIXADOESTOQUE IS NULL OR  DATAHORAPEDIDONOTAFISCAL IS NULL OR NOMEDOARQUIVO IS NULL");
        //DataRow[] orw = dt.Select("1=1");

        if (orw.Length == 0)
        {
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>PEDIDO"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>RECEBIMENTO INTERFACE"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>INÍCIO SEPARAÇÃO"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>T(1)"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;PALET CONFERIDO &nbsp;&nbsp;"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;TÉRMINO DA CONFERÊNCIA"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;T(2)"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;BAIXA DO ESTOQUE"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;T(3)"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;SOLICITAÇÃO DA NOTA FISCAL"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;T(4)"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;ARQUIVO"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;NOTA FISCAL"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;DATA NOTA FISCAL"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in orw)
            {
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;" + item["EMISSAOPEDIDO"].ToString() + "&nbsp;&nbsp;"));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
                
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NUMERO"].ToString() ));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORARECEBIMENTOINTERFACE"].ToString()));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;&nbsp;" + item["INICIODASEPARACAO"].ToString() + "&nbsp;&nbsp;"));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                TimeSpan ts = DateTime.Now - DateTime.Now;

                
                if (item["DATAHORARECEBIMENTOINTERFACE"].ToString() != "" && item["INICIODASEPARACAO"].ToString() != "")
                {
                    ts = Convert.ToDateTime(item["DATAHORARECEBIMENTOINTERFACE"].ToString()) - Convert.ToDateTime(item["INICIODASEPARACAO"].ToString());
                    qtdMinut01 += Math.Abs((ts.Hours*60)  + ts.Minutes);
                }

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpR'  nowrap=nowrap  style='font-size:8pt;height:10px' align='rigth'>" + (Math.Abs((ts.Hours * 60) + ts.Minutes) == 0 ? "-" : Math.Abs((ts.Hours * 60) + ts.Minutes).ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


                if(item["CONCLUSAOPALETS"].ToString()=="" || item["CONCLUSAOPALETS"].ToString()=="0")
                    phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;&nbsp;SIM" ));
                else
                    phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;&nbsp;NAO" ));


                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORATERMINOCONFERENCIA"].ToString()));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));



                ts = DateTime.Now - DateTime.Now;

                if (item["INICIODASEPARACAO"].ToString() != "" && item["DATAHORATERMINOCONFERENCIA"].ToString() != "")
                {
                    ts = Convert.ToDateTime(item["INICIODASEPARACAO"].ToString()) - Convert.ToDateTime(item["DATAHORATERMINOCONFERENCIA"].ToString());
                    qtdMinut02 += Math.Abs((ts.Hours * 60) + ts.Minutes);
                }


                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Math.Abs((ts.Hours * 60) + ts.Minutes) == 0 ? "-" : Math.Abs((ts.Hours * 60) + ts.Minutes).ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORABAIXADOESTOQUE"].ToString()));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                ts = DateTime.Now - DateTime.Now;

                if (item["DATAHORATERMINOCONFERENCIA"].ToString() != "" && item["DATAHORABAIXADOESTOQUE"].ToString() != "")
                {
                    ts = Convert.ToDateTime(item["DATAHORATERMINOCONFERENCIA"].ToString()) - Convert.ToDateTime(item["DATAHORABAIXADOESTOQUE"].ToString());
                    qtdMinut03 += Math.Abs((ts.Hours * 60) + ts.Minutes);        
                }

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Math.Abs((ts.Hours * 60) + ts.Minutes) == 0 ? "-" : Math.Abs((ts.Hours * 60) + ts.Minutes).ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));



                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORAPEDIDONOTAFISCAL"].ToString()));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                ts = DateTime.Now - DateTime.Now;

                if (item["DATAHORABAIXADOESTOQUE"].ToString() != "" && item["DATAHORAPEDIDONOTAFISCAL"].ToString() != "")
                {
                    ts = Convert.ToDateTime(item["DATAHORABAIXADOESTOQUE"].ToString()) - Convert.ToDateTime(item["DATAHORAPEDIDONOTAFISCAL"].ToString());
                    qtdMinut04 += Math.Abs((ts.Hours * 60) + ts.Minutes);
                }

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (ts.Minutes == 0 ? "-" : Math.Abs(ts.Minutes).ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NOMEDOARQUIVO"].ToString()));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;&nbsp;" + (item["NF"].ToString() == "" ? "-" : item["NF"].ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>&nbsp;&nbsp;" + (item["HORANF"].ToString() == "" ? "-" :item["HORANF"].ToString())));
                phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
                
            }


            //--------------------------------medias----------------------------------
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='4' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>Médias de Tempo em Minutos: "));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>" + Convert.ToDecimal((qtdMinut01 / orw.Length)).ToString("#0.00")));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='3' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>" + Convert.ToDecimal((qtdMinut02 / orw.Length)).ToString("#0.00")));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>" + Convert.ToDecimal((qtdMinut03 / orw.Length)).ToString("#0.00")));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='2' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>" + Convert.ToDecimal((qtdMinut04 / orw.Length)).ToString("#0.00")));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='1' class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));

            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));

            //--------------------------------medias----------------------------------


            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='15'class='tdpCabecalhoMenor' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + orw.Length));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));


            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td td colspan='13'class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;'>T(1) = Tempo em minutos do Recebimento de Interface até Início da Separação"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));


            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td td colspan='13'class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;'>T(2) = Tempo em minutos da Separação até o término da conferencia"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));

            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td td colspan='13'class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;'>T(3) = Tempo em minutos da baixa no estoque e o termino da conferencia"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));


            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<td colspan='13'class='tdpCabecalhoMenor' align='left' nowrap=nowrap style='font-size:8pt;'>T(4) = Tempo em minutos da baixa no estoque ate a nota fiscal"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidosEmAndamento.Controls.Add(new LiteralControl(@"</table>"));

        //carregarPedidosEmAndamentoConcluido(dt);
    }

    //private void carregarPedidosEmAndamentoConcluido(DataTable dt)
    //{
    //    DataRow[] orw = dt.Select("INICIODASEPARACAO IS not NULL and DATAHORATERMINOCONFERENCIA IS not NULL and DATAHORABAIXADOESTOQUE IS not NULL and  DATAHORAPEDIDONOTAFISCAL IS not NULL and NOMEDOARQUIVO IS not NULL");
    //    phPedidosEmAndamento.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));

    //    if (orw.Length == 0)
    //    {
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<tr>"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</tr>"));
    //    }
    //    else
    //    {
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>PEDIDO"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>RECEBIMENTO INTERFACE"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>INÍCIO SEPARAÇÃO"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>TÉMINO DA CONFERENCIA"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>BAIXA DO ESTOQUE"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL / PEDIDO"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:8pt;width:1%'>ARQUIVO"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</tr>"));

    //        foreach (DataRow item in orw)
    //        {
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<tr>"));

    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["EMISSAOPEDIDO"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NUMERO"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORARECEBIMENTOINTERFACE"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["INICIODASEPARACAO"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));


    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORATERMINOCONFERENCIA"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));

    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORABAIXADOESTOQUE"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));


    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DATAHORAPEDIDONOTAFISCAL"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));


    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["NOMEDOARQUIVO"].ToString()));
    //            phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));
    //        }

    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"<td colspan='8'class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + orw.Length));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</td>"));
    //        phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</tr>"));
    //    }

    //    phPedidosEmAndamentoConcluido.Controls.Add(new LiteralControl(@"</table>"));
    //}

   private void carregarNotaFiscalDeSaida(DateTime? DataInicio, DateTime? DataFim)
    {
        //phNFSaida.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        //DataTable dt = sqlNfSaida(DataInicio, DataFim);

        //if (dt.Rows.Count == 0)
        //{
        //    phNFSaida.Controls.Add(new LiteralControl(@"<tr>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));
        //}
        //else
        //{

        //    phNFSaida.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LINHA"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

        //    int totNotas = 0, totLinhas = 0;
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        phNFSaida.Controls.Add(new LiteralControl(@"<tr>"));

        //        phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + Convert.ToDateTime(item["DATADEEMISSAO"]).ToShortDateString() + "</A>"));
        //        phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //        //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

        //        phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["NOTASEMITIDAS"])));
        //        phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //        //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

        //        phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["LINHAS"])));
        //        phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //        phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

        //        totNotas += Convert.ToInt32(item["NOTASEMITIDAS"]);
        //        totLinhas += Convert.ToInt32(item["LINHAS"]);
        //    }

        //    phNFSaida.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totNotas));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //    //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

        //    phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totLinhas));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
        //    phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));
        //}

        //phNFSaida.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarNotaFiscalDeEntrada(DateTime? DataInicio, DateTime? DataFim)
    {
       /* phNFEntrada.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlNfEntrada(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phNFEntrada.Controls.Add(new LiteralControl(@"<tr>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {

            phNFEntrada.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));

            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));

            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LINHA"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));

            int totNotas = 0, totLinhas = 0;
            foreach (DataRow item in dt.Rows)
            {
                phNFEntrada.Controls.Add(new LiteralControl(@"<tr>"));

                phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + Convert.ToDateTime(item["DATADEENTRADA"]).ToShortDateString() + "</A>"));
                phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
                //phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));

                phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["NOTASDEENTRADA"])));
                phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
                //phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));

                phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["LINHAS"])));
                phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
                phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));

                totNotas += Convert.ToInt32(item["NOTASDEENTRADA"]);
                totLinhas += Convert.ToInt32(item["LINHAS"]);
            }

            phNFEntrada.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));

            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totNotas));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
            //phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));

            phNFEntrada.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totLinhas));
            phNFEntrada.Controls.Add(new LiteralControl(@"</td>"));
            phNFEntrada.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phNFEntrada.Controls.Add(new LiteralControl(@"</table>"));      */  
    }

    private void carregarPedidoMontagemKit(DateTime? DataInicio, DateTime? DataFim)
    {
       /* phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidoMontagemDeKit(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LINHA"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));

            int totNotas = 0, totLinhas = 0;
            foreach (DataRow item in dt.Rows)
            {
                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + Convert.ToDateTime(item["DataDeEmissao"]).ToShortDateString() + "</A>"));
                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
                //phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));

                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["NotasEmitidas"])));
                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
                //phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));

                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["LINHAS"])));
                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
                phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));

                totNotas += Convert.ToInt32(item["NotasEmitidas"]);
                totLinhas += Convert.ToInt32(item["LINHAS"]);
            }

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totNotas));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));

            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totLinhas));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</table>"));*/
    }

    private void carregarPedidoDeVenda(DateTime? DataInicio, DateTime? DataFim)
    {
        /*
        phPedidoDeVenda.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidoDeVenda(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LINHA"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));

            int totNotas = 0, totLinhas = 0;
            foreach (DataRow item in dt.Rows)
            {
                phPedidoDeVenda.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + Convert.ToDateTime(item["DataDeEmissao"]).ToShortDateString() + "</A>"));
                phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
                //phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));

                phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["NotasEmitidas"])));
                phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
                //phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));

                phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["LINHAS"])));
                phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
                phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));

                totNotas += Convert.ToInt32(item["NotasEmitidas"]);
                totLinhas += Convert.ToInt32(item["LINHAS"]);
            }

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totNotas));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
            //phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));

            phPedidoDeVenda.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totLinhas));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoDeVenda.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoDeVenda.Controls.Add(new LiteralControl(@"</table>"));*/
    }
    
    private void carregarPedidoEmSeparacao(DateTime? DataInicio, DateTime? DataFim)
    {
        phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidoEmSeparacao(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>PEDIDO"));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</td>"));            
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
            
            foreach (DataRow item in dt.Rows)
            {
                phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["PEDIDOSEMSEPARACAO"].ToString() + "</A>"));
                phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</td>"));                
            }

            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + dt.Rows.Count));            
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</td>"));            
            phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoEmSeparacao.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarPedidoDeKiteEmSeparacao(DateTime? DataInicio, DateTime? DataFim)
    {
        phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidodeKitEmSeparacao(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>PEDIDO"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {
                phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["PedidosDeKitEmSeparacao"].ToString() + "</A>"));
                phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            }
            

            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>TOTAL: " + dt.Rows.Count));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</table>"));
        carregarPedidoDeKiteEmSeparacaoPorData(DataInicio, DataFim);
    }

    private void carregarPedidoDeKiteEmSeparacaoPorData(DateTime? DataInicio, DateTime? DataFim)
    {
        phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlPedidodeKitEmSeparacaoPorData(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<tr>"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='rigth' nowrap=nowrap style='font-size:8pt;width:1%'>PEDIDO"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</tr>"));

            int m = 0;
            foreach (DataRow item in dt.Rows)
            {
                phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<tr>"));

                phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["DATADEEMISSAO"].ToString() + "</A>"));
                phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));

                phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["PEDIDOSEMSEPARACAO"].ToString() ));
                phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));

                m += Convert.ToInt32(item["PEDIDOSEMSEPARACAO"]);
            }

            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: "));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));

            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + m));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoEmSeparacaoPorData.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarEntradaDeKit(DateTime? DataInicio, DateTime? DataFim)
    {
        phEntradaDeKit.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlEntradaDeKit(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phEntradaDeKit.Controls.Add(new LiteralControl(@"<tr>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phEntradaDeKit.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>NÚMERO"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {
                phEntradaDeKit.Controls.Add(new LiteralControl(@"<tr>"));
                phEntradaDeKit.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["Numero"].ToString() + "</A>"));
                phEntradaDeKit.Controls.Add(new LiteralControl(@"</td>"));
            }

            phEntradaDeKit.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + dt.Rows.Count));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaDeKit.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phEntradaDeKit.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarNotasFiscaisAguardandoEmbarque(DateTime? DataInicio, DateTime? DataFim)
    {
        phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlNotasFiscaisAguardandoEmbarque(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<tr>"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:8pt;width:1%'>NOTAS"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));


            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='RIGHT' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp;&nbsp;&nbsp;DIAS AGUARDANDO EMBARQUE"));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));


            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {
                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<tr>"));
                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["DATADEEMISSAO"].ToString() + "</A>"));
                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));


                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["QUANTIDADEDENOTAS"].ToString()));
                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["DIASAGUARDANDOEMBARQUE"].ToString()));
                phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));
            }

            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total "));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));            


            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + dt.Compute("SUM(QUANTIDADEDENOTAS)", "")));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + dt.Compute("AVG(DIASAGUARDANDOEMBARQUE)", "")));
            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));


            phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phAguardandoEmbarque.Controls.Add(new LiteralControl(@"</table>"));
    }
    
    private void carregarEntradaParaArmazenagem(DateTime? DataInicio, DateTime? DataFim)
    {
        phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlEntradaParaArmazenagem(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<tr>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>NÚMERO"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {
                phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<tr>"));
                phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + item["Numero"].ToString() + "</A>"));
                phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</td>"));
            }

            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + dt.Rows.Count));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</td>"));
            phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phEntradaParaArmazenagem1.Controls.Add(new LiteralControl(@"</table>"));
    }


    #region Selects

    public DataTable sqlNfSaida(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";

        strsql += " SELECT  ";
        strsql += "   CONVERT(VARCHAR(10),NF.DATADEEMISSAO,103) DATADEEMISSAO, COUNT(*) NOTASEMITIDAS,  ";
        strsql += " (  ";
        strsql += " SELECT COUNT(*)  ";
        strsql += " FROM DOCUMENTO N ";
        strsql += " INNER JOIN DOCUMENTOITEM NI ON (NI.IDDOCUMENTO = N.IDDOCUMENTO) ";
        strsql += " WHERE  ";
        strsql += " N.DATADEEMISSAO = NF.DATADEEMISSAO ";
        strsql += " AND N.IDCLIENTE = NF.IDCLIENTE ";
        strsql += " AND N.ATIVO = 'SIM' ";
        strsql += " AND N.ENTRADASAIDA = 'SAIDA' ";
        strsql += " GROUP BY N.DATADEEMISSAO ";
        strsql += " ) LINHAS ";
        strsql += " FROM DOCUMENTO NF ";
        strsql += " WHERE  ";
        strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
        strsql += " AND NF.ATIVO = 'SIM' ";
        strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";
        strsql += " GROUP BY  ";
        strsql += " NF.DATADEEMISSAO, ";
        strsql += " NF.IDCLIENTE ";
        strsql += " ORDER BY 1 DESC ";

        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlNfEntrada(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";

        strsql += " SELECT ";
        strsql += " CONVERT(VARCHAR(10),NF.DATADEENTRADA,103) DATADEENTRADA, COUNT(*) NOTASDEENTRADA,  ";
        strsql += " (  ";
        strsql += " SELECT COUNT(*)  ";
        strsql += " FROM DOCUMENTO N ";
        strsql += " INNER JOIN DOCUMENTOITEM NI ON (NI.IDDOCUMENTO = N.IDDOCUMENTO) ";
        strsql += " WHERE  ";
        strsql += " N.DATADEENTRADA = NF.DATADEENTRADA ";
        strsql += " AND N.IDCLIENTE = NF.IDCLIENTE ";
        strsql += " AND N.ATIVO = 'SIM' ";
        strsql += " AND N.ENTRADASAIDA = 'ENTRADA' ";
        strsql += " GROUP BY N.DATADEENTRADA ";
        strsql += " ) LINHAS ";
        strsql += " FROM DOCUMENTO NF ";
        strsql += " WHERE  ";
        strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
        strsql += " AND NF.ATIVO = 'SIM' ";
        strsql += " AND NF.ENTRADASAIDA = 'ENTRADA' ";
        strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";
        strsql += " AND NF.DATADEENTRADA IS NOT NULL ";
        strsql += " GROUP BY  ";
        strsql += " NF.DATADEENTRADA, ";
        strsql += " NF.IDCLIENTE ";
        strsql += " ORDER BY 1 DESC ";

        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlPedidoMontagemDeKit(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";

        strsql += " SELECT  ";
        strsql += "   CONVERT(VARCHAR(10),NF.DATADEEMISSAO,103) DATADEEMISSAO, COUNT(*) NOTASEMITIDAS,  ";
        strsql += " (  ";
        strsql += " SELECT COUNT(*)  ";
        strsql += " FROM DOCUMENTO N ";
        strsql += " INNER JOIN DOCUMENTOITEM NI ON (NI.IDDOCUMENTO = N.IDDOCUMENTO) ";
        strsql += " WHERE  ";
        strsql += " N.DATADEEMISSAO = NF.DATADEEMISSAO ";
        strsql += " AND N.IDCLIENTE = NF.IDCLIENTE ";
        strsql += " AND N.ATIVO = 'SIM' ";
        strsql += " AND N.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND N.SERIE = 'MK' ";
        strsql += " GROUP BY N.DATADEEMISSAO ";
        strsql += " ) LINHAS ";

        strsql += " FROM DOCUMENTO NF ";
        strsql += " WHERE  ";
        strsql += " NF.IDCLIENTE = 6915   ";
        strsql += " AND NF.TIPODEDOCUMENTO = 'PEDIDO' ";
        strsql += " AND NF.ATIVO = 'SIM' ";
        strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND NF.DATADEEMISSAO BETWEEN '01/AUG/2011' AND '10/AUG/2011' ";
        strsql += " AND NF.SERIE = 'MK' ";
        strsql += " GROUP BY  ";
        strsql += " NF.DATADEEMISSAO, ";
        strsql += " NF.IDCLIENTE ";
        strsql += " ORDER BY 1 DESC ";

        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlPedidoDeVenda(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";

        strsql += " SELECT  ";
        strsql += "   CONVERT(VARCHAR(10),NF.DATADEEMISSAO,103) DATADEEMISSAO, COUNT(*) NOTASEMITIDAS,  ";
        strsql += " (  ";
        strsql += " SELECT COUNT(*)  ";
        strsql += " FROM DOCUMENTO N ";
        strsql += " INNER JOIN DOCUMENTOITEM NI ON (NI.IDDOCUMENTO = N.IDDOCUMENTO) ";
        strsql += " WHERE  ";
        strsql += " N.DATADEEMISSAO = NF.DATADEEMISSAO ";
        strsql += " AND N.IDCLIENTE = NF.IDCLIENTE ";
        strsql += " AND N.ATIVO = 'SIM' ";
        strsql += " AND N.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND N.SERIE = 'VD' ";
        strsql += " GROUP BY N.DATADEEMISSAO ";
        strsql += " ) LINHAS ";

        strsql += " FROM DOCUMENTO NF ";
        strsql += " WHERE  ";
        strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " AND NF.TIPODEDOCUMENTO = 'PEDIDO' ";
        strsql += " AND NF.ATIVO = 'SIM' ";
        strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";
        strsql += " AND NF.SERIE = 'VD' ";
        strsql += " GROUP BY  ";
        strsql += " NF.DATADEEMISSAO, ";
        strsql += " NF.IDCLIENTE ";
        strsql += " ORDER BY 1 DESC ";

        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlPedidoEmSeparacao(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " SELECT DISTINCT ";
        strsql += "   PD.NUMERO PEDIDOSEMSEPARACAO ";
        strsql += " FROM MAPA MP  ";
        strsql += " INNER JOIN MOVIMENTACAOITEM MI ON (MI.IDMAPA = MP.IDMAPA)  ";
        strsql += " INNER JOIN DOCUMENTO PD ON (PD.IDDOCUMENTO = MI.IDDOCUMENTO)  ";
        strsql += " WHERE  ";
        strsql += " PD.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " AND MP.ESTOQUEPROCESSADO = 'NAO' ";
        strsql += " AND MP.TIPO = 'SAIDA' ";
        strsql += " AND PD.ATIVO = 'SIM' ";
        strsql += " AND PD.SERIE = 'VD'";
        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlPedidodeKitEmSeparacao(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " Select Distinct ";
        strsql += "   PD.Numero PedidosDeKitEmSeparacao ";
        strsql += " From Mapa MP  ";
        strsql += " Inner Join MovimentacaoItem MI on (MI.IDMapa = MP.IDMapa)  ";
        strsql += " Inner Join Documento PD on (PD.IDDocumento = MI.IDDocumento)  ";
        strsql += " where  ";
        strsql += " PD.IDCliente IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " and MP.EstoqueProcessado = 'NAO' ";
        strsql += " and MP.TIPO = 'SAIDA' ";
        strsql += " and PD.Ativo = 'SIM' ";
        strsql += " and PD.Serie = 'MK' ";
        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlPedidodeKitEmSeparacaoPorData(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " SELECT  ";
        strsql += "  CONVERT(VARCHAR(10),DATADEEMISSAO,103) DATADEEMISSAO, ";
         strsql += " COUNT(DISTINCT PD.NUMERO) PEDIDOSEMSEPARACAO    ";
         strsql += " FROM MAPA MP    ";
         strsql += " INNER JOIN MOVIMENTACAOITEM MI ON (MI.IDMAPA = MP.IDMAPA)    ";
         strsql += " INNER JOIN DOCUMENTO PD ON (PD.IDDOCUMENTO = MI.IDDOCUMENTO)    ";
         strsql += " WHERE   PD.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ")  AND MP.ESTOQUEPROCESSADO = 'NAO'  AND MP.TIPO = 'SAIDA'  AND PD.ATIVO = 'SIM'  AND PD.SERIE = 'VD' ";
         strsql += " GROUP BY PD.DATADEEMISSAO ";
        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlEntradaDeKit(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " Select Distinct ";
        strsql += " PD.IdDocumento, ";
        strsql += " PD.Numero  ";
        strsql += " From Mapa MP  ";
        strsql += " Inner Join MovimentacaoItem MI on (MI.IDMapa = MP.IDMapa)  ";
        strsql += " Inner Join Documento PD on (PD.IDDocumento = MI.IDDocumento)  ";
        strsql += " where  ";
        strsql += " PD.IDCliente IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " and MP.EstoqueProcessado = 'NAO' ";
        strsql += " and MP.TIPO = 'ENTRADA' ";
        strsql += " and PD.Ativo = 'SIM' ";
        strsql += " and PD.Serie = 'MK1' ";
        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlEntradaParaArmazenagem(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " Select Distinct ";
        strsql += " PD.IdDocumento, ";
        strsql += " PD.Numero  ";
        strsql += " From Mapa MP  ";
        strsql += " Inner Join MovimentacaoItem MI on (MI.IDMapa = MP.IDMapa)  ";
        strsql += " Inner Join Documento PD on (PD.IDDocumento = MI.IDDocumento)  ";
        strsql += " where  ";
        strsql += " PD.IDCliente IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " and MP.EstoqueProcessado = 'NAO' ";
        strsql += " and MP.TIPO = 'ENTRADA' ";
        strsql += " and PD.Ativo = 'SIM' ";
        strsql += " and PD.Serie <> 'MK1' ";
        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    public DataTable sqlNotasFiscaisAguardandoEmbarque(DateTime? DataInicio, DateTime? DataFim)
    {
        string strsql = "";
        strsql += " SELECT ";
        strsql += "   CONVERT(VARCHAR(10), NF.DATADEEMISSAO, 103) DATADEEMISSAO, ";
        strsql += " COUNT(*) QUANTIDADEDENOTAS, ";
        strsql += " DATEDIFF(DAY, NF.DATADEEMISSAO, GETDATE()) DIASAGUARDANDOEMBARQUE FROM DOCUMENTO NF ";
        strsql += " INNER JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = NF.IDDOCUMENTO) WHERE ";
        strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
        strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
        strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
        strsql += " AND DF.SITUACAO = 'AGUARDANDO EMBARQUE' ";
        if (DataInicio != null)
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";

        strsql += " GROUP BY NF.DATADEEMISSAO ";
        strsql += " ORDER BY NF.DATADEEMISSAO ";       

        return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    }

    private DataTable sqlPedidoEmAndamento(DateTime? ini, DateTime? fim)
    {
        string  STRSQL = "";
        STRSQL += " SELECT ";
        STRSQL += "   CONVERT(VARCHAR(10),PD.DATADEEMISSAO, 103) EMISSAOPEDIDO, ";
        STRSQL += " PD.IDDOCUMENTO, ";
        STRSQL += " PD.NUMERO,  ";
        STRSQL += " PD.DATADOMOVIMENTO DATAHORARECEBIMENTOINTERFACE, ";
        STRSQL += " ( ";
        STRSQL += " SELECT TOP 1 M.DATADECADASTRO ";
        STRSQL += " FROM MOVIMENTACAOITEM MI ";
        STRSQL += " LEFT JOIN MOVIMENTACAO M ON (M.IDMOVIMENTACAO = MI.IDMOVIMENTACAO) ";
        STRSQL += " WHERE MI.IDDOCUMENTO = PD.IDDOCUMENTO ";
        STRSQL += " ) INICIODASEPARACAO, ";

        STRSQL += " ( ";
        STRSQL += " SELECT TOP 1 P.DATADEFECHAMENTO ";
        STRSQL += " FROM PALLET P ";
        STRSQL += " LEFT JOIN PALLETDOCUMENTO PLD ON (PLD.IDPALLET = P.IDPALLET)  ";
        STRSQL += " WHERE PLD.IDDOCUMENTO = PD.IDDOCUMENTO ";
        STRSQL += " ) DATAHORATERMINOCONFERENCIA, ";

        STRSQL += "  (SELECT   COUNT(*) ";
        STRSQL += " FROM    ";
        STRSQL += " DOCUMENTO NF    ";
        STRSQL += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO)    ";
        STRSQL += " LEFT JOIN PALLETDOCUMENTO PDX ON (PDX.IDDOCUMENTO = NFI.IDDOCUMENTO)    ";
        STRSQL += " LEFT JOIN PALLETDOCUMENTOITEM PDI ON (PDI.IDDOCUMENTOITEM = NFI.IDDOCUMENTOITEM AND PDX.IDPALLETDOCUMENTO = PDI.IDPALLETDOCUMENTO)    ";
        STRSQL += " LEFT JOIN PRODUTO P ON (P.IDPRODUTO = PDI.IDPRODUTO)    ";
        STRSQL += " LEFT JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM)    ";
        STRSQL += " LEFT JOIN PRODUTOEMBALAGEM PPE ON (PPE.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE AND PPE.IDPRODUTO = PDI.IDPRODUTO)    ";
        STRSQL += " LEFT JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE)    ";
        STRSQL += " LEFT JOIN PRODUTO PR ON (PR.IDPRODUTO = PE.IDPRODUTO)    ";
        STRSQL += " WHERE    ";
        STRSQL += " NF.IDDOCUMENTO =  PD.IDDOCUMENTO ";
        STRSQL += "  HAVING CAST(MAX(NFI.QUANTIDADE) - SUM(COALESCE(PDI.QUANTIDADE,0) * COALESCE(PPE.UNIDADEDOCLIENTE,0)) AS NUMERIC(10,0)) > 0 ) CONCLUSAOPALETS, ";

        STRSQL += " ( ";
        STRSQL += " SELECT TOP 1 MI.DATADEEXECUCAO ";
        STRSQL += " FROM MOVIMENTACAOITEM MI ";
        STRSQL += " WHERE MI.IDDOCUMENTO = PD.IDDOCUMENTO ";
        STRSQL += " ) DATAHORABAIXADOESTOQUE, ";
        STRSQL += " ( ";
        STRSQL += " SELECT TOP 1 MI.DATAHORAPEDIDONOTAFISCAL ";
        STRSQL += " FROM MOVIMENTACAOITEM MI ";
        STRSQL += " WHERE MI.IDDOCUMENTO = PD.IDDOCUMENTO ";
        STRSQL += " ) DATAHORAPEDIDONOTAFISCAL, ";

        STRSQL += " ( ";
        STRSQL += " SELECT TOP 1 MI.PEDIDONOTAFISCAL ";
        STRSQL += " FROM MOVIMENTACAOITEM MI ";
        STRSQL += " WHERE MI.IDDOCUMENTO = PD.IDDOCUMENTO ";
        STRSQL += " ) NOMEDOARQUIVO,  ";

        STRSQL += " (SELECT TOP 1 DF.DATA 	";
		STRSQL += " FROM  DOCUMENTORELACIONADO DR  	";
		STRSQL += " LEFT JOIN DOCUMENTO DOCNF ON DOCNF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO ";
		STRSQL += " INNER JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = DOCNF.IDDOCUMENTO";
		STRSQL += " WHERE   DR.IDDOCUMENTOPAI = PD.IDDOCUMENTO  )  HORANF, ";

               
        STRSQL += " (SELECT TOP 1 DOCNF.NUMERO 	FROM  DOCUMENTORELACIONADO DR  	LEFT JOIN DOCUMENTO DOCNF ON DOCNF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO WHERE   DR.IDDOCUMENTOPAI = PD.IDDOCUMENTO ";
        STRSQL += " )  NF ";
        STRSQL += " FROM DOCUMENTO PD ";
        STRSQL += " WHERE  ";
        STRSQL += " PD.IDCLIENTE = 6915  ";
        STRSQL += " AND PD.TIPODEDOCUMENTO =  'PEDIDO'  ";
        STRSQL += " AND PD.ENTRADASAIDA =  'SAIDA'  ";
        STRSQL += " AND PD.ATIVO = 'SIM'  AND PD.DATADEEMISSAO>= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "'";

        return Sistran.Library.GetDataTables.RetornarDataTable(STRSQL, "");
    }

    //public DataTable sqlEntradaParaArmazenagem(DateTime? DataInicio, DateTime? DataFim)
    //{
    //    string strsql = "";
    //    strsql += " Select Distinct ";
    //    strsql += " PD.IdDocumento, ";
    //    strsql += " PD.Numero  ";
    //    strsql += " From Mapa MP  ";
    //    strsql += " Inner Join MovimentacaoItem MI on (MI.IDMapa = MP.IDMapa)  ";
    //    strsql += " Inner Join Documento PD on (PD.IDDocumento = MI.IDDocumento)  ";
    //    strsql += " where  ";
    //    strsql += " PD.IDCliente IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
    //    strsql += " and MP.EstoqueProcessado = 'NAO' ";
    //    strsql += " and MP.TIPO = 'ENTRADA' ";
    //    strsql += " and PD.Ativo = 'SIM' ";
    //    strsql += " and PD.Serie <> 'MK1' ";
    //    return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
    //}

#endregion

    protected void Button2_Click(object sender, EventArgs e)
    {
        //Sistran.Library.GetDataTables.RetornarIdTabela("A");
    }
    
    
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Recaregar();

    }
}
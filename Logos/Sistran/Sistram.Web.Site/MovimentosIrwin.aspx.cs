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

public partial class MovimentosIrwin : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        Session["DataConf"] = inicio + "|" + DateTime.Now.ToShortDateString();
        lblPeriodo.Text = inicio.ToShortDateString() + " à " + DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();

        string[] DataConf = FuncoesGerais.DataConf();
        DateTime? ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime? fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        carregarNotaFiscalDeEntrada(ini, fim);
        carregarNotaFiscalDeSaida(ini, fim);
        carregarPedidoDeVenda(ini, fim);
        carregarPedidoMontagemKit(ini, fim);
        carregarPedidoEmSeparacao(ini, fim);
        carregarPedidoDeKiteEmSeparacao(ini, fim);
        carregarEntradaDeKit(ini, fim);
        carregarEntradaParaArmazenagem(ini, fim);
    }

    private void carregarNotaFiscalDeSaida(DateTime? DataInicio, DateTime? DataFim)
    {
        phNFSaida.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
        DataTable dt = sqlNfSaida(DataInicio, DataFim);

        if (dt.Rows.Count == 0)
        {
            phNFSaida.Controls.Add(new LiteralControl(@"<tr>"));
            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
            phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {

            phNFSaida.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:8pt;width:1%'>DATA"));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>NOTA FISCAL"));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>LINHA"));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
            phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

            int totNotas = 0, totLinhas = 0;
            foreach (DataRow item in dt.Rows)
            {
                phNFSaida.Controls.Add(new LiteralControl(@"<tr>"));

                phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap  style='font-size:8pt;height:10px'><a class='link' href='#'>" + Convert.ToDateTime(item["DATADEEMISSAO"]).ToShortDateString() + "</A>"));
                phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
                //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

                phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["NOTASEMITIDAS"])));
                phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
                //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

                phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + Convert.ToInt32(item["LINHAS"])));
                phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
                phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

                totNotas += Convert.ToInt32(item["NOTASEMITIDAS"]);
                totLinhas += Convert.ToInt32(item["LINHAS"]);
            }

            phNFSaida.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total:"));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));

            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totNotas));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
            //phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));

            phNFSaida.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>" + totLinhas));
            phNFSaida.Controls.Add(new LiteralControl(@"</td>"));
            phNFSaida.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phNFSaida.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarNotaFiscalDeEntrada(DateTime? DataInicio, DateTime? DataFim)
    {
        phNFEntrada.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
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

        phNFEntrada.Controls.Add(new LiteralControl(@"</table>"));        
    }

    private void carregarPedidoMontagemKit(DateTime? DataInicio, DateTime? DataFim)
    {
        phPedidoMontagemKit.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=0 celpanding=0 >"));
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

        phPedidoMontagemKit.Controls.Add(new LiteralControl(@"</table>"));
    }

    private void carregarPedidoDeVenda(DateTime? DataInicio, DateTime? DataFim)
    {
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

        phPedidoDeVenda.Controls.Add(new LiteralControl(@"</table>"));
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
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;'>Total: " + dt.Rows.Count));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</td>"));
            phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phPedidoMontagemDeKitSeparacao.Controls.Add(new LiteralControl(@"</table>"));
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
        strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";
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
}
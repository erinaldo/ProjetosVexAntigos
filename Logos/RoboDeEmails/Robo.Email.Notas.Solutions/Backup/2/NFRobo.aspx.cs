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

public partial class NFRobo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        Button1.Attributes.Add("OnClick", "window.open('frmRptLista.aspx?data="+ DateTime.Now.ToShortDateString()+"', '','fullscreen=yes, scrollbars=auto'); return false;");
        Gerar();
        this.Title = "Acompanhamento de NF Aguardando Embarque";
    }

    private void Gerar()
    {
        MontarAguardandoEmbarque();
        MontarAguardandoEmbarqueAtraso(">=3", PhEmAtraso);
        MontarAguardandoEmbarqueAtraso("=2", phEmAtraso2dias);
        MontarAguardandoEmbarqueAtraso("<=1", phEmAtraso1dia);
    }

    public void MontarAguardandoEmbarque()
    {
        string strsql = " Select ";
        strsql += " CEMB.FilialAtual filial,  ";
        strsql += " FL.Sigla nome, ";
        strsql += " COUNT(*) Notas,  ";
        strsql += " Coalesce(Count(Distinct CNH.CnpjDestinatario),0) As Entregas,    ";
        strsql += " SUM(CNH.Volumes) Volumes, ";
        strsql += " Cast(SUM(CNH.Peso) As Numeric(12,0)) Peso, ";

        strsql += " Cast(SUM(CNH.ValorDaNota) As Numeric(12,2)) ValorDaNota, ";
        strsql += "             ( ";
        strsql += " Select ";
        strsql += " Count(*) RE ";
        strsql += " From RelacaoDeEntrega RE ";

        strsql += "     where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Emissao =  CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "',103) ";
        strsql += " ) RE_Emitidas, ";

        strsql += "   ( ";
        strsql += " Select ";
        strsql += " Count(*) RE ";
        strsql += " From RelacaoDeEntrega RE ";

        strsql += " where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO') ";
        strsql += " ) RE_NaoLiberadas, ";

        strsql += " ( ";
        strsql += " Select ";
        strsql += " Count(*)  ";
        strsql += " From RelacaoDeEntrega RE ";
        strsql += " Inner Join RelacaoDeEntregaConhecimento REC on (REC.Filial = RE.Filial and REC.Serie = RE.Serie and REC.RelacaoDeEntrega = RE.RelacaoDeEntrega) ";
        strsql += " where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO') ";
        strsql += " ) NotasFiscaisNasRENaoLiberadas, ";

        strsql += "    ( ";
        strsql += " Select ";
        strsql += " COUNT(*) ";
        strsql += " From Conhecimento CNH ";
        strsql += " where ";
        strsql += " CNH.FilialDestino = CEMB.FilialAtual  ";
        strsql += " and CNH.DataDeEmissao >= '01/aug/2011' ";
        strsql += " and CNH.DataDeEntrega is null ";
        strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 ";
        strsql += " ) SemDataDeEntrega ";

        strsql += " From ConhecimentoAEmbarcar CEMB ";
        strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
        strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00' Group By ";
        strsql += " CEMB.FilialAtual, FL.Sigla ";
        strsql += " Order By  ";
        strsql += " CEMB.FilialAtual  ";
        DataTable dtAgEmb = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());

        #region Tabela
        PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtAgEmb.Rows.Count > 0)
        {
            #region Cabecalho
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>NOME"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;NOTAS"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;ENTREGAS"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;VOLUMES"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;&nbsp; &nbsp;PESO"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;VALOR DA NOTA"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;RE EMITIDAS"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;RE NAO LIBERADAS"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;NF EM RE'S NAO LIBERADAS"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;NF S/ DATA DE ENTREGA"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));

            #endregion

            #region itens

            foreach (DataRow item in dtAgEmb.Rows)
            {
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<tr>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["FILIAL"].ToString()));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:8pt;height:10px'><a href='nfLista.aspx?nome=" + item["NOME"].ToString() + "&tipo=normal&fl=" + item["FILIAL"].ToString() + "' class='link' >" + item["NOME"].ToString() + "</a>"));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["Notas"]));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["ENTREGAS"]));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["VOLUMES"]) > 0 ? Convert.ToDecimal(item["VOLUMES"]).ToString("###,###.##") : "0,00")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["PESO"]) > 0 ? Convert.ToDecimal(item["PESO"]).ToString("###,###.##") : "0,00")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["VALORDANOTA"]) > 0 ? Convert.ToDecimal(item["VALORDANOTA"]).ToString("###,###.##") : "0,00")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["RE_EMITIDAS"]));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["RE_NAOLIBERADAS"]) > 0 ? Convert.ToDecimal(item["RE_NAOLIBERADAS"]).ToString("###,###.##") : "0")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["NOTASFISCAISNASRENAOLIBERADAS"]) > 0 ? Convert.ToDecimal(item["NOTASFISCAISNASRENAOLIBERADAS"]).ToString("###,###.##") : "0")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'><a href='nfLista.aspx?r=s&nome=" + item["NOME"].ToString() + "&tipo=normal&fl=" + item["FILIAL"].ToString() + "' class='link' >" + (Convert.ToDecimal(item["SEMDATADEENTREGA"]) > 0 ? Convert.ToDecimal(item["SEMDATADEENTREGA"]).ToString("###,###.##") : "0</a>")));
                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

                PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));

            }

            #endregion

            #region Rodape
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  nowrap=nowrap style='font-size:8pt;width:1%'>"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(Notas)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(Entregas)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(VOLUMES)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;&nbsp; &nbsp;" + int.Parse(dtAgEmb.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtAgEmb.Compute("SUM(VALORDANOTA)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(RE_Emitidas)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(RE_NaoLiberadas)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(NotasFiscaisNasRENaoLiberadas)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAgEmb.Compute("SUM(SemDataDeEntrega)", "").ToString()).ToString("###,###.##")));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));

            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion

        }
        else
        {
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<tr>"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</td>"));
            PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</tr>"));
        }

        PhAguradandoEmbarque.Controls.Add(new LiteralControl(@"</table>"));
        #endregion
    }

    public void MontarAguardandoEmbarqueAtraso( string condicao, PlaceHolder controle)
    {
        string strsql = "";
        strsql = " Select ";
        strsql += " CEMB.FilialAtual filial,  ";
        strsql += " FL.Sigla nome, ";
        strsql += " COUNT(*) Notas,  ";
        strsql += " Coalesce(Count(Distinct CNH.CnpjDestinatario),0) As Entregas,    ";
        strsql += " SUM(isnull(CNH.Volumes,0)) Volumes, ";
        strsql += " Cast(SUM(isnull(CNH.Peso,0)) As Numeric(12,0)) Peso, ";

        strsql += " Cast(SUM(isnull(CNH.ValorDaNota,0)) As Numeric(12,2)) ValorDaNota, ";
        strsql += "             ( ";
        strsql += " Select ";
        strsql += " Count(*) RE ";
        strsql += " From RelacaoDeEntrega RE ";
        strsql += "     where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Emissao  =  CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "',103) ";
        strsql += " ) RE_Emitidas, ";

        strsql += "   ( ";
        strsql += " Select ";
        strsql += " Count(*) RE ";
        strsql += " From RelacaoDeEntrega RE ";
        strsql += " where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO') ";
        strsql += " ) RE_NaoLiberadas, ";

        strsql += " ( ";
        strsql += " Select ";
        strsql += " Count(*)  ";
        strsql += " From RelacaoDeEntrega RE ";
        strsql += " Inner Join RelacaoDeEntregaConhecimento REC on (REC.Filial = RE.Filial and REC.Serie = RE.Serie and REC.RelacaoDeEntrega = RE.RelacaoDeEntrega) ";
        strsql += " where  ";
        strsql += " RE.Filial = CEMB.FilialAtual ";
        strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO') ";
        strsql += " ) NotasFiscaisNasRENaoLiberadas, ";

        strsql += "    ( ";
        strsql += " Select ";
        strsql += " COUNT(*) ";
        strsql += " From Conhecimento CNH ";
        strsql += " where ";
        strsql += " CNH.FilialDestino = CEMB.FilialAtual  ";
        strsql += " and CNH.DataDeEmissao >= '01/aug/2011' ";
        strsql += " and CNH.DataDeEntrega is null ";
        strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 ";
        strsql += " ) SemDataDeEntrega ";

        strsql += " From ConhecimentoAEmbarcar CEMB ";
        strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
        strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') ";
        strsql += " and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) "+ condicao +" and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00' ";
        strsql += " Group By ";
        strsql += " CEMB.FilialAtual, FL.Sigla ";
        strsql += " Order By  ";
        strsql += " CEMB.FilialAtual  ";
        DataTable dtAEAtraso = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseAntiga());

        #region Tabela
        controle.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 >"));
        if (dtAEAtraso.Rows.Count > 0)
        {
            #region Cabecalho
            controle.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:8pt;width:1%'>FILIAL"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  align='LEFT' nowrap=nowrap style='font-size:8pt;width:1%'>NOME"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;NOTAS"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;ENTREGAS"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;VOLUMES"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;&nbsp; &nbsp;PESO"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;VALOR DA NOTA"));
            controle.Controls.Add(new LiteralControl(@"</td>"));
           

            controle.Controls.Add(new LiteralControl(@"</tr>"));

            #endregion

            #region itens

            foreach (DataRow item in dtAEAtraso.Rows)
            {
                controle.Controls.Add(new LiteralControl(@"<tr>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["FILIAL"].ToString()));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:8pt;height:10px'><a href='nfLista.aspx?nome=" + item["NOME"].ToString() + "&tipo=atraso&dias="+condicao.Replace(">","").Replace("<","").Replace("=","")+"&fl=" + item["FILIAL"].ToString() + "' class='link'>" + item["NOME"].ToString() + "</a>"));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["Notas"]));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + item["ENTREGAS"]));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["VOLUMES"]) > 0 ? Convert.ToDecimal(item["VOLUMES"]).ToString("###,###.##") : "0,00")));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["PESO"]) > 0 ? Convert.ToDecimal(item["PESO"]).ToString("###,###.##") : "0,00")));
                controle.Controls.Add(new LiteralControl(@"</td>"));

                controle.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap  style='font-size:8pt;height:10px'>" + (Convert.ToDecimal(item["VALORDANOTA"]) > 0 ? Convert.ToDecimal(item["VALORDANOTA"]).ToString("###,###.##") : "0,00")));
                controle.Controls.Add(new LiteralControl(@"</td>"));          

                controle.Controls.Add(new LiteralControl(@"</tr>"));

            }

            #endregion

            #region Rodape
            controle.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap=nowrap style='font-size:8pt;width:1%'>TOTAL"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho'  nowrap=nowrap style='font-size:8pt;width:1%'>"));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAEAtraso.Compute("SUM(Notas)", "").ToString()).ToString("###,###.##")));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAEAtraso.Compute("SUM(Entregas)", "").ToString()).ToString("###,###.##")));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + int.Parse(dtAEAtraso.Compute("SUM(VOLUMES)", "").ToString()).ToString("###,###.##")));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>&nbsp; &nbsp;&nbsp; &nbsp;" + int.Parse(dtAEAtraso.Compute("SUM(PESO)", "").ToString()).ToString("###,###.##")));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:8pt;width:1%'>" + decimal.Parse(dtAEAtraso.Compute("SUM(VALORDANOTA)", "").ToString()).ToString("###,###.##")));
            controle.Controls.Add(new LiteralControl(@"</td>"));

            controle.Controls.Add(new LiteralControl(@"</tr>"));
            #endregion

        }
        else
        {
            controle.Controls.Add(new LiteralControl(@"<tr>"));
            controle.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            controle.Controls.Add(new LiteralControl(@"</td>"));
            controle.Controls.Add(new LiteralControl(@"</tr>"));
        }

        controle.Controls.Add(new LiteralControl(@"</table>"));
        #endregion

    }
}
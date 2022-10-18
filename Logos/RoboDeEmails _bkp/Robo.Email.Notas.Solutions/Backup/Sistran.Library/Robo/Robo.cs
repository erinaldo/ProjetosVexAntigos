using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;


namespace Sistran.Library.Robo
{
    public class Robo
    {
        public void Iniciar(string email)
        {
            Gerar(email);
        }

        public void Gerar(string email)
        {
            try
            {

                string strsql = " Select ";
                strsql += " CEMB.FilialAtual filial,  ";
                strsql += " FL.Sigla nome, ";
                strsql += " COUNT(*) Notas,  ";
                strsql += " Coalesce(Count(Distinct CNH.CnpjDestinatario),0) As Entregas,    ";
                strsql += " SUM(ISNULL(CNH.Volumes,0)) Volumes, ";
                strsql += " Cast(SUM(ISNULL(CNH.Peso,0)) As Numeric(12,0)) Peso, ";

                strsql += " Cast(SUM(ISNULL(CNH.ValorDaNota,0)) As Numeric(12,2)) ValorDaNota, ";
                strsql += "             ( ";
                strsql += " Select ";
                strsql += " Count(*) RE ";
                strsql += " From RelacaoDeEntrega RE ";

                strsql += "     where  ";
                strsql += " RE.Filial = CEMB.FilialAtual ";
                strsql += " and RE.Emissao = CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "',103) ";
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
                strsql += " and CNH.DataDeEmissao >= '01/aug/2011' "; //fixo
                strsql += " and CNH.DataDeEntrega is null ";
                strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 ";
                strsql += " ) SemDataDeEntrega ";

                strsql += " From ConhecimentoAEmbarcar CEMB ";
                strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
                strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00'  Group By ";
                strsql += " CEMB.FilialAtual, FL.Sigla ";
                strsql += " Order By  ";
                strsql += " CEMB.FilialAtual  ";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDAntigo);


                strsql = " Select ";
                strsql += " CEMB.FilialAtual filial,  ";
                strsql += " FL.Sigla nome, ";
                strsql += " COUNT(*) Notas,  ";
                strsql += " Coalesce(Count(Distinct CNH.CnpjDestinatario),0) As Entregas,    ";
                strsql += " SUM(ISNULL(CNH.Volumes,0)) Volumes, ";
                strsql += " Cast(SUM(ISNULL(CNH.Peso,0)) As Numeric(12,0)) Peso, ";

                strsql += " Cast(SUM(ISNULL(CNH.ValorDaNota,0)) As Numeric(12,2)) ValorDaNota, ";
                strsql += "             ( ";
                strsql += " Select ";
                strsql += " Count(*) RE ";
                strsql += " From RelacaoDeEntrega RE ";

                strsql += "     where  ";
                strsql += " RE.Filial = CEMB.FilialAtual ";
                strsql += " and RE.Emissao = CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "',103) ";
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
                strsql += " and CNH.DataDeEmissao >= '01/aug/2011' "; // fixa
                strsql += " and CNH.DataDeEntrega is null ";
                strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 ";
                strsql += " ) SemDataDeEntrega ";

                strsql += " From ConhecimentoAEmbarcar CEMB ";
                strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
                strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') ";
                strsql += " and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) @DIAS  and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00' ";
                strsql += " Group By ";
                strsql += " CEMB.FilialAtual, FL.Sigla ";
                strsql += " Order By  ";
                strsql += " CEMB.FilialAtual  ";

                DataTable dt3dias = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", ">=3"), BaseAntigaConfig.Default.BDAntigo);
                DataTable dt2dias = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", "=2"), BaseAntigaConfig.Default.BDAntigo);
                DataTable dt1dia = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", "<=1"), BaseAntigaConfig.Default.BDAntigo);


                string S1 = MontarHtmlParaEmail(dt, "Notas Fiscais Aguardando Embarque", false);

                string S2 = MontarHtmlParaEmail(dt3dias, "NOTAS FISCAIS AGUARDANDO EMBARQUE 72 HS OU MAIS", true);
                string S3 = MontarHtmlParaEmail(dt2dias, "NOTAS FISCAIS AGUARDANDO EMBARQUE 48 HS ", true);
                string S4 = MontarHtmlParaEmail(dt1dia, "NOTAS FISCAIS AGUARDANDO EMBARQUE 24 HS ", true);

                string Tba = "<html><head>";
                Tba += " <STYLE type='text/css'>";
                Tba += " body ";
                Tba += " { ";
                Tba += " margin: 0px; ";
                Tba += " background-color: #f8f8f8; ";
                Tba += " font-family: Verdana; ";
                Tba += " text-align: center; ";
                Tba += " font-size: 12PX; }";
                Tba += " </STYLE></HEAD> ";


                Tba += " <body style='font-size: 12px;'> <B>Para maiores <a href='http://www.grupologos.com.br/reports.net/nfrobo.aspx'>detalhes</a> ou se não conseguir visualizar este e-mail clique <a href='http://www.grupologos.com.br/reports.net/nfrobo.aspx'>aqui</B></a>  </body></html>";
                string juncao = Tba + "<BR><BR><BR>" + S1 + "<BR><BR><BR>" + S2 + "<BR><BR><BR>" + S3 + "<BR><BR><BR>" + S4;

                Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Aviso: Notas Fiscais Aguardando Embarque", juncao, "mail.grupologos.com.br", "logos0902");
                //this.enviarEmail("Aviso: Notas Fiscais Aguardando Embarque", juncao, email);
            }
            catch (Exception)
            {
            }

        }

        public string[] GerarParaHTML()
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
            strsql += " and RE.Emissao =  getdate() ";
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

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDAntigo);


            strsql = " Select ";
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
            strsql += " and RE.Emissao =  getdate() ";
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
            strsql += " and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) > 3  and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00' ";
            strsql += " Group By ";
            strsql += " CEMB.FilialAtual, FL.Sigla ";
            strsql += " Order By  ";
            strsql += " CEMB.FilialAtual  ";
            DataTable dt2 = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDAntigo);

            string[] ret = new string[2];

            ret[0] = MontarHtml(dt, "Notas Fiscais Aguardando Embarque");
            ret[1] = MontarHtml(dt2, "Notas Fiscais Aguardando Embarque em Atraso");

            return ret;
        }

        public string MontarHtml(DataTable dt, string Titulo)
        {

            string Tba = "<HTML>";
            Tba += "<HEAD>";
            /*Tba += " <STYLE type='text/css'>";
            Tba += " body ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " background-color: #f8f8f8; ";
            Tba += " font-family: Verdana; ";
            Tba += " text-align: center; ";
            Tba += " font-size: 7pt; ";

            Tba += " } ";
            Tba += " form ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " text-align: center; ";
            Tba += " } ";


            Tba += " .table  ";
            Tba += " { ";
            Tba += " background-color: #E0E0E0; ";
            Tba += " width: 50%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .tableFundoClaro ";
            Tba += " { ";
            Tba += " background-color: #F8F8F8; ";
            Tba += " width: 100%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .tableSemCorFundo ";
            Tba += " {	 ";
            Tba += " width: 50%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .table2 ";
            Tba += " { ";
            Tba += " background-color:#E0E0E0 ;  ";
            Tba += " font-family: Arial, Helvetica, sans- ";
            Tba += " } ";

            Tba += " .tdpCenter ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align: center ; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " } ";

            Tba += " .tdp ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " text-align: left; ";
            Tba += " vertical-align:middle; ";

            Tba += " } ";
            Tba += " .tdpSemAlign ";
            Tba += " { ";
            Tba += " border: 0.5pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " } ";

            Tba += " .tdpSemAlignGray ";
            Tba += " { ";
            Tba += " border: 0.5pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " background-color:GrayText; ";
            Tba += " } ";


            Tba += " .tdpCenter ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align: center ; ";
            Tba += " nowrap:now ";
            Tba += " } ";
            Tba += " .tdpR ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align:right; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal;	 ";
            Tba += " } ";

            Tba += "  .tdpVerdana ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            //Tba += " text-align: left; ";
            Tba += " nowrap:nowrap; ";
            Tba += " } ";

            Tba += " .tdpCabecalho ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " height: 13pt; ";
            Tba += " font-size:9pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " font-weight:bold; ";
            Tba += " text-transform: uppercase;	 ";
            Tba += " } ";

            Tba += " </STYLE> ";*/

            Tba += "</HEAD>";


            Tba += "<BODY>";

            /*Tba += "<table cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=0>";
            Tba += "<tr>";

            Tba += "<td> <image src='http://www.grupologos.com.br/SistranWeb.NET/Imagens/LOGOS-LOGTRANSP-03.jpg' height='60px' />";
            Tba += "</td>";            
            Tba += "<td>" + Titulo;
            Tba += "</td>";


            Tba += "</tr>";
            Tba += "</table>";
             * */



            Tba += "<table class='table' cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=0>";

            Tba += "<TR style='background-image:url(http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>FILIAL</TD>";
            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NOME</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>NOTAS</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>ENTREGAS</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VOLUMES</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>PESO</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>VALOR DA NOTA</TD>";
            Tba += "</TR>";

            int NOTAS = 0;
            int ENTREGAS = 0;
            int VOLUMES = 0;
            int PESO = 0;
            decimal ValorDaNota = Convert.ToDecimal(0);
            foreach (DataRow item in dt.Rows)
            {

                Tba += "<TR>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["filial"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["nome"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["notas"].ToString() + "</TD>";

                NOTAS += Convert.ToInt32(item["notas"].ToString());

                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["entregas"].ToString() + "</TD>";
                ENTREGAS += Convert.ToInt32(item["entregas"].ToString());


                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["volumes"].ToString() + "</TD>";
                VOLUMES += Convert.ToInt32(item["volumes"].ToString());

                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["peso"].ToString() + "</TD>";
                PESO += Convert.ToInt32(item["peso"].ToString());


                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["ValorDaNota"].ToString() + "</TD>";
                ValorDaNota += Convert.ToDecimal(item["ValorDaNota"].ToString());


                Tba += "</TR>";
            }

            Tba += "<TR style='background-image:url(http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL</TD>";
            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'></TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + NOTAS + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + ENTREGAS + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + VOLUMES + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + PESO + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(ValorDaNota).ToString("#0.00") + "</TD>";
            Tba += "</TR>";

            Tba += "</TABLE>";
            Tba += "<BODY>";
            Tba += "</HTML>";

            return Tba;
            // Sistran.Library.EnviarEmails.EnviarEmail(emails, "moises@sistecno.com.br", Titulo.ToUpper(), Tba, "mail.sistecno.com.br", "mo2404");
            //enviarEmail(Titulo.ToUpper(), Tba, emails);

            //EscreverLogHtml(Tba);
        }

        public string MontarHtmlParaEmail(DataTable dt, string Titulo, bool ATRASO)
        {

            string Tba = "<HTML>";
            Tba += "<HEAD>";
            Tba += " <STYLE type='text/css'>";
            Tba += " body ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " background-color: #f8f8f8; ";
            Tba += " font-family: Verdana; ";
            Tba += " text-align: center; ";
            Tba += " font-size: 7pt; ";

            Tba += " } ";
            Tba += " form ";
            Tba += " { ";
            Tba += " margin: 0px; ";
            Tba += " text-align: center; ";
            Tba += " } ";


            Tba += " .table  ";
            Tba += " { ";
            Tba += " background-color: #E0E0E0; ";
            Tba += " width: 50%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .tableFundoClaro ";
            Tba += " { ";
            Tba += " background-color: #F8F8F8; ";
            Tba += " width: 100%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .tableSemCorFundo ";
            Tba += " {	 ";
            Tba += " width: 50%; ";
            Tba += " font-family: Arial, Helvetica, sans-serif; ";
            Tba += " font-size: 7pt; ";
            Tba += " font-weight: bold; ";
            Tba += " } ";

            Tba += " .table2 ";
            Tba += " { ";
            Tba += " background-color:#E0E0E0 ;  ";
            Tba += " font-family: Arial, Helvetica, sans- ";
            Tba += " } ";

            Tba += " .tdpCenter ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align: center ; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " } ";

            Tba += " .tdp ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " text-align: left; ";
            Tba += " vertical-align:middle; ";

            Tba += " } ";
            Tba += " .tdpSemAlign ";
            Tba += " { ";
            Tba += " border: 0.5pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " } ";

            Tba += " .tdpSemAlignGray ";
            Tba += " { ";
            Tba += " border: 0.5pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal; ";
            Tba += " background-color:GrayText; ";
            Tba += " } ";


            Tba += " .tdpCenter ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align: center ; ";
            Tba += " nowrap:now ";
            Tba += " } ";
            Tba += " .tdpR ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " text-align:right; ";
            Tba += " nowrap:nowrap; ";
            Tba += " font-weight:normal;	 ";
            Tba += " } ";

            Tba += "  .tdpVerdana ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " font-size:8pt; ";
            Tba += " font-family:Verdana; ";
            //Tba += " text-align: left; ";
            Tba += " nowrap:nowrap; ";
            Tba += " } ";

            Tba += " .tdpCabecalho ";
            Tba += " { ";
            Tba += " border: 0.1pt solid #FFFFFF; ";
            Tba += " height: 13pt; ";
            Tba += " font-size:9pt; ";
            Tba += " font-family:Verdana; ";
            Tba += " font-weight:bold; ";
            Tba += " text-transform: uppercase;	 ";
            Tba += " } ";

            Tba += " </STYLE> ";

            Tba += "</HEAD>";


            Tba += "<BODY>";

            Tba += "<table cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=0>";
            Tba += "<tr>";

            Tba += "<td> <image src='http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg' height='60px'></image>";
            //            Tba += "<td> <image src='http://www.grupologos.com.br/SistranWeb.NET/Imagens/LOGOS-LOGTRANSP-03.jpg' height='60px'></image>";
            Tba += "</td>";
            Tba += "<td class='tdpCabecalho'>" + Titulo;
            Tba += "</td>";


            Tba += "</tr>";
            Tba += "</table>";




            Tba += "<table class='table' cellspacing='1' celpanding='1' width=50% id='teste' runat='server' border=1>";

            Tba += "<TR style='background-image:url(http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>FILIAL</TD>";
            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;NOME</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;NOTAS</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;ENTREGAS</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;VOLUMES</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PESO</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;VALOR DA NOTA</TD>";

            if (ATRASO == false)
            {
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;RE EMITIDAS</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;RE NAO LIBERADAS</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;NF EM RE'S NAO LIBERADAS</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>&nbsp;&nbsp;NF S/ DATA DE ENTREGA(Acima de 5 Dias)</TD>";
            }
            Tba += "</TR>";

            int NOTAS = 0;
            int ENTREGAS = 0;
            int VOLUMES = 0;

            int eitidas = 0;
            int naoliberadas = 0;
            int nfs_naoLiberadas = 0;
            int semData = 0;

            int PESO = 0;
            decimal ValorDaNota = Convert.ToDecimal(0);
            foreach (DataRow item in dt.Rows)
            {

                Tba += "<TR>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["filial"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["nome"].ToString() + "</TD>";


                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["notas"]) > 0 ? Convert.ToDecimal(item["notas"]).ToString("###,###.##") : "0,00") + "</TD>";
                //                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["notas"].ToString() + "</TD>";

                NOTAS += Convert.ToInt32(item["notas"].ToString());

                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["entregas"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["entregas"]) > 0 ? Convert.ToDecimal(item["entregas"]).ToString("###,###.##") : "0,00") + "</TD>";

                ENTREGAS += Convert.ToInt32(item["entregas"].ToString());

                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["volumes"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["volumes"]) > 0 ? Convert.ToDecimal(item["volumes"]).ToString("###,###.##") : "0,00") + "</TD>";

                VOLUMES += Convert.ToInt32(item["volumes"].ToString());


                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["peso"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["peso"]) > 0 ? Convert.ToDecimal(item["peso"]).ToString("###,###.##") : "0,00") + "</TD>";

                PESO += Convert.ToInt32(item["peso"].ToString());

                //                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["ValorDaNota"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["ValorDaNota"]) > 0 ? Convert.ToDecimal(item["ValorDaNota"]).ToString("###,###.##") : "0,00") + "</TD>";
                ValorDaNota += Convert.ToDecimal(item["ValorDaNota"].ToString());

                if (ATRASO == false)
                {
                    Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["RE_Emitidas"].ToString() + "</TD>";
                    //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["RE_Emitidas"]) > 0 ? Convert.ToDecimal(item["RE_Emitidas"]).ToString("###,###.##") : "0,00") + "</TD>";

                    eitidas += Convert.ToInt32(item["RE_Emitidas"]);

                    Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["RE_NaoLiberadas"].ToString() + "</TD>";
                    //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["RE_NaoLiberadas"]) > 0 ? Convert.ToDecimal(item["RE_NaoLiberadas"]).ToString("###,###.##") : "0,00") + "</TD>";

                    naoliberadas += Convert.ToInt32(item["RE_NaoLiberadas"]);

                    Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["NotasFiscaisNasRENaoLiberadas"].ToString() + "</TD>";
                    //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["NotasFiscaisNasRENaoLiberadas"]) > 0 ? Convert.ToDecimal(item["NotasFiscaisNasRENaoLiberadas"]).ToString("###,###.##") : "0,00") + "</TD>";

                    nfs_naoLiberadas += Convert.ToInt32(item["NotasFiscaisNasRENaoLiberadas"]);

                    Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["SemDataDeEntrega"].ToString() + "</TD>";
                    //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["SemDataDeEntrega"]) > 0 ? Convert.ToDecimal(item["SemDataDeEntrega"]).ToString("###,###.##") : "0,00") + "</TD>";

                    semData += Convert.ToInt32(item["SemDataDeEntrega"]);

                }


                Tba += "</TR>";
            }

            Tba += "<TR style='background-image:url(http://www.grupologos.com.br/SistranWeb.NET/Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>";

            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'>TOTAL</TD>";
            Tba += "<TD class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;font-weight:bold'></TD>";


            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(NOTAS) > 0 ? Convert.ToDecimal(NOTAS).ToString("###,###.##") : "0,00") + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(ENTREGAS) > 0 ? Convert.ToDecimal(ENTREGAS).ToString("###,###.##") : "0,00") + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(VOLUMES) > 0 ? Convert.ToDecimal(VOLUMES).ToString("###,###.##") : "0,00") + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(PESO) > 0 ? Convert.ToDecimal(PESO).ToString("###,###.##") : "0,00") + "</TD>";
            Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(ValorDaNota) > 0 ? Convert.ToDecimal(ValorDaNota).ToString("###,###.##") : "0,00") + "</TD>";



            //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + ENTREGAS + "</TD>";
            //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + VOLUMES + "</TD>";
            //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + PESO + "</TD>";
            //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + Convert.ToDecimal(ValorDaNota).ToString("#0.00") + "</TD>";

            if (ATRASO == false)
            {
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(eitidas) > 0 ? Convert.ToDecimal(eitidas).ToString("###,###.##") : "0,00") + "</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(naoliberadas) > 0 ? Convert.ToDecimal(naoliberadas).ToString("###,###.##") : "0,00") + "</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(nfs_naoLiberadas) > 0 ? Convert.ToDecimal(nfs_naoLiberadas).ToString("###,###.##") : "0,00") + "</TD>";
                Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + (Convert.ToDecimal(semData) > 0 ? Convert.ToDecimal(semData).ToString("###,###.##") : "0,00") + "</TD>";

                //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + eitidas + "</TD>";
                //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + naoliberadas + "</TD>";
                //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + nfs_naoLiberadas + "</TD>";
                //Tba += "<TD class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;font-weight:bold'>" + semData + "</TD>";
            }


            Tba += "</TR>";

            Tba += "</TABLE>";
            Tba += "<BODY>";
            Tba += "</HTML>";
            EscreverLogHtml(Tba);

            return Tba;
            // Sistran.Library.EnviarEmails.EnviarEmail(emails, "moises@sistecno.com.br", Titulo.ToUpper(), Tba, "mail.sistecno.com.br", "mo2404");
            //enviarEmail(Titulo.ToUpper(), Tba, emails);

        }

        public static void EscreverLog(string menssagem)
        {
            //string nomeArquivo = ConfigurationSettings.AppSettings["ArquivoLog"].Replace("DDMMYYYY", DateTime.Now.ToShortDateString().Replace("\\","").Replace("/",""));
            //StreamWriter writer = new StreamWriter(nomeArquivo, true);
            //writer.WriteLine("DATA: " + DateTime.Now + " =>>" + menssagem.ToUpper());
            //writer.Close();
        }

        public static void EscreverLogHtml(string menssagem)
        {
            //string nomeArquivo = ConfigurationSettings.AppSettings["ArquivoLog"].Replace("DDMMYYYY", DateTime.Now.ToShortDateString().Replace("\\", "").Replace("/", "").Replace(".txt", ".htm"));
            //nomeArquivo = nomeArquivo.Replace(".txt", ".htm");
            //StreamWriter writer = new StreamWriter(nomeArquivo, false);
            //writer.WriteLine(menssagem.ToUpper());
            //writer.Close();
        }

        public void enviarEmail(string assunto, string corpo, string emails)
        {
            //create the mail message
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //set the addresses
            mail.From = new System.Net.Mail.MailAddress("moises@sistecno.com.br");
            mail.To.Add("moises@sistecno.com.br");
            mail.To.Add("moises@mrandrade.com");

            //set the content
            mail.Subject = assunto;
            mail.Body = corpo;
            mail.Priority = System.Net.Mail.MailPriority.High;
            mail.IsBodyHtml = true;

            //mail.Attachments.Add(new System.Net.Mail.Attachment("logos.jpg"));


            //send the message
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.grupologos.com.br");
            smtp.EnableSsl = false;

            System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("sistema@grupologos.com.br", "logos0902");
            smtp.Credentials = credenciais;
            smtp.Send(mail);
        }

        public static string RetornarStringBaseAntiga()
        {
            return BaseAntigaConfig.Default.BDAntigo;
        }

        public static DataTable RetornarEmails()
        {
            return Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM AVISOKPI ORDER BY NOME", BaseAntigaConfig.Default.BDAntigo);
        }

    }
}
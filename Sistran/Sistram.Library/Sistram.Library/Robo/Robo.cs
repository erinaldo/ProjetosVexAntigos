using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
//using AprovacaoRequisicao.Library;



namespace Sistran.Library.Robo
{
    public class Robo
    {
        public void Iniciar(string email)
        {
            Gerar(email);
        }

        public void IniciarBaseNova(string email)
        {
            GerarBaseNova(email);
        }

        public void Gerar(string email)
        {
            try
            {

                //SELECT PARA A BASE ANTIGA
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
                //strsql += " and RE.Emissao = CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "',103) ";
                strsql += " and RE.Emissao >=  CONVERT(datetime, '01/01/2014',103)  ";
                strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO', 'EM ENTREGA') ";
                strsql += " ) RE_Emitidas, ";

                strsql += "   ( ";
                strsql += " Select ";
                strsql += " Count(*) RE ";
                strsql += " From RelacaoDeEntrega RE ";

                strsql += " where  ";
                strsql += " RE.Filial = CEMB.FilialAtual ";
                strsql += " and RE.Situacao in ('DOCUMENTO IMPRESSO','AGUARDANDO IMPRESSAO') AND YEAR(RE.EMISSAO)>=YEAR(GETDATE()) ";
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
                strsql += " and CNH.DataDeEmissao >= '01/jan/2014' "; //fixo
                strsql += " and CNH.DataDeEntrega is null ";
                strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 AND CNH.SITUACAODAIMPRESSAODOCTRC<>'CANCELADO' ";
                strsql += " ) SemDataDeEntrega ";

                strsql += " From ConhecimentoAEmbarcar CEMB ";
                strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
                strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE')   ";
                strsql += " and CEMB.FilialAtual <>14 ";
                strsql += "  Group By CEMB.FilialAtual, FL.Sigla ";
                strsql += " Order By  ";
                strsql += " CEMB.FilialAtual  ";

       

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDAntigo);


                //PEGA AS NOTAS AGUARDANDO EMBARQUE SOMENTE DO CLIENTE 9
                strsql = " SELECT  ";
                strsql += " FIL.NUMERODAFILIAL FILIAL, ";
                strsql += " case FIL.NOME when 'TABOAO' then 'RICOY' ELSE FIL.NOME END nome, ";
                strsql += " COUNT(DOC.IDDOCUMENTO) NOTAS, ";
                strsql += " SUM(CASE WHEN DOC.DATADECONCLUSAO IS NOT NULL THEN 1 ELSE 0 END) ENTREGAS, ";
                strsql += " SUM(DOC.VOLUMES) VOLUMES, ";
                strsql += " Cast(SUM(ISNULL(DOC.PESOBRUTO,0)) As Numeric(12,0)) PESO, ";
                strsql += " SUM(DOC.VALORDANOTA) VALORDANOTA, ";
                strsql += " COUNT(DT.IDDT) RE_EMITIDAS,  ";
                strsql += " 0 RE_NAOLIBERADAS,  ";
                strsql += " 0 NOTASFISCAISNASRENAOLIBERADAS, ";
                strsql += " SUM(CASE WHEN DOC.DATADECONCLUSAO IS NULL THEN 1 ELSE 0 END) SEMDATADEENTREGA ";
                strsql += " FROM DOCUMENTO DOC WITH(NOLOCK)   ";
                strsql += " LEFT JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
                strsql += " LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = DOC.IDFILIAL ";
                strsql += " LEFT JOIN ROMANEIODOCUMENTO RD ON RD.IDDOCUMENTO = DOC.IDDOCUMENTO ";
                strsql += " LEFT JOIN DTROMANEIO DTROM ON DTROM.IDDTROMANEIO = RD.IDROMANEIO ";
                strsql += " LEFT JOIN DT ON DT.IDDT = DTROM.IDDT ";
                strsql += " WHERE  ";
                strsql += " DOC.IDCLIENTE =9 ";
                strsql += " AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')   ";
                strsql += " AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')    ";
                strsql += " AND DOC.DATADECONCLUSAO IS NULL   ";
                strsql += " AND NOT DOC.DATADEENTRADA IS NULL   ";
                strsql += " AND NOT DOC.CODIGODORECEXP IS NULL   ";
                strsql += " AND DOC.IDDOCUMENTOOCORRENCIA IS NULL  ";
                strsql += " AND DF.SITUACAO='AGUARDANDO EMBARQUE'  ";
                strsql += " AND DOC.ATIVO='SIM'  ";
                strsql += " AND FIL.NOME IS NOT NULL ";
                strsql += " AND DataDeEmissao >='2014-01-01' ";
                strsql += " GROUP BY  ";
                strsql += " FIL.NUMERODAFILIAL, ";
                strsql += " FIL.NOME ";

                //DataTable dtBdNovo = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDNovoLogos);



                //for (int i = 0; i < dtBdNovo.Rows.Count; i++)
                //{
                //    DataRow orw = dt.NewRow();
                //    orw["filial"] = dtBdNovo.Rows[i]["filial"];
                //    orw["nome"] = dtBdNovo.Rows[i]["nome"];
                //    orw["Notas"] = dtBdNovo.Rows[i]["Notas"];
                //    orw["Entregas"] = dtBdNovo.Rows[i]["Entregas"];
                //    orw["Volumes"] = dtBdNovo.Rows[i]["Volumes"];
                //    orw["Peso"] = dtBdNovo.Rows[i]["Peso"];
                //    orw["ValorDaNota"] = dtBdNovo.Rows[i]["ValorDaNota"];
                //    orw["RE_Emitidas"] = dtBdNovo.Rows[i]["RE_Emitidas"];
                //    orw["RE_NaoLiberadas"] = dtBdNovo.Rows[i]["RE_NaoLiberadas"];
                //    orw["NotasFiscaisNasRENaoLiberadas"] = dtBdNovo.Rows[i]["NotasFiscaisNasRENaoLiberadas"];
                //    orw["SemDataDeEntrega"] = dtBdNovo.Rows[i]["SemDataDeEntrega"];
                //    dt.Rows.Add(orw);
                //}




                strsql = " Select ";
                strsql += " CEMB.FilialAtual filial,  ";
                strsql += " case FL.Sigla when 'TABOAO' then 'RICOY' ELSE FL.Sigla END nome , ";
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
                strsql += " and CNH.DataDeEmissao >= '01/jan/2014' "; // fixa
                strsql += " and CNH.DataDeEntrega is null ";
                strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 AND CNH.SITUACAODAIMPRESSAODOCTRC<>'CANCELADO' ";
                strsql += " ) SemDataDeEntrega ";

                strsql += " From ConhecimentoAEmbarcar CEMB ";
                strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
                strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') ";
                strsql += " and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) @DIAS ";//  /*and CEMB.FilialAtual <> '14'  and CEMB.FilialAtual <> '00'*/ ";
                strsql += " Group By ";
                strsql += " CEMB.FilialAtual, FL.Sigla ";
                strsql += " Order By  ";
                strsql += " CEMB.FilialAtual  ";

                DataTable dt3dias = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", ">=3"), BaseAntigaConfig.Default.BDAntigo);
                dt3dias = RetornarBaseNova(3, dt3dias);


                DataTable dt2dias = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", "=2"), BaseAntigaConfig.Default.BDAntigo);
                dt2dias = RetornarBaseNova(2, dt2dias);


                DataTable dt1dia = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.Replace("@DIAS", "<=1"), BaseAntigaConfig.Default.BDAntigo);
                dt1dia = RetornarBaseNova(1, dt1dia);


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

                Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Aviso: Notas Fiscais Aguardando Embarque", juncao, "mail.grupologos.com.br", "logos0902", "Situacao de Entregas");
                //this.enviarEmail("Aviso: Notas Fiscais Aguardando Embarque", juncao, email);

                //new cEmail().enviarEmail("Aviso: Notas Fiscais Aguardando Embarque", juncao, email, "SituacaoDeEntregas");

            }
            catch (Exception ex)
            {

#if(debug)
throw ex;
#endif
            }

        }

        public void GerarBaseNova(string email)
        {


            #region G E R A L
            string strsqlGeral = "SELECT ";
            strsqlGeral += " DOC.IDFILIAL, ";
            strsqlGeral += " FIL.NOME FILIAL,";
            strsqlGeral += " COUNT(DISTINCT DOC.IDDOCUMENTO) NOTAS,";
            strsqlGeral += " SUM(CASE WHEN DOC.DATADECONCLUSAO IS NULL   THEN 0 ELSE 1 END) ENTREGAS,";
            strsqlGeral += " SUM(ISNULL(DOC.VOLUMES, 0)) VOLUMES,";
            strsqlGeral += " SUM(ISNULL(DOC.PESOBRUTO,0)) PESO,";
            strsqlGeral += " SUM(ISNULL(DOC.VALORDANOTA,0)) VALORNOTA,";
            strsqlGeral += " (";
            strsqlGeral += " SELECT ";
            strsqlGeral += " COUNT(DISTINCT DT.IDDT) ";
            strsqlGeral += " FROM DT  WITH (NOLOCK) ";
            strsqlGeral += " INNER JOIN DTROMANEIO DR ON DT.IDDT = DR.IDDT";
            strsqlGeral += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DR.IDROMANEIO";
            strsqlGeral += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO";
            strsqlGeral += " INNER JOIN DOCUMENTO DOC1 ON DOC1.IDDOCUMENTO = ROMDOC.IDDOCUMENTO";
            strsqlGeral += " INNER JOIN FILIAL FIL1 ON FIL1.IDFILIAL = DOC1.IDFILIAL";
            strsqlGeral += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsqlGeral += " AND DOC1.ATIVO = 'SIM'";
            strsqlGeral += " AND DOC1.DATADEEMISSAO = CONVERT(DATETIME, '@DATAEMISSAO', 103) ";
            strsqlGeral += " AND FIL1.IDFILIAL=DOC.IDFILIAL";
            strsqlGeral += " AND DT.ATIVO='SIM'";
            strsqlGeral += " ) ";
            strsqlGeral += " DTEMITIDA,";

            strsqlGeral += " (";
            strsqlGeral += " SELECT ";
            strsqlGeral += " COUNT(DISTINCT DT.IDDT) ";
            strsqlGeral += " FROM DT  WITH (NOLOCK) ";
            strsqlGeral += " INNER JOIN DTROMANEIO DR ON DT.IDDT = DR.IDDT";
            strsqlGeral += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DR.IDROMANEIO";
            strsqlGeral += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO";
            strsqlGeral += " INNER JOIN DOCUMENTO DOC1 ON DOC1.IDDOCUMENTO = ROMDOC.IDDOCUMENTO";
            strsqlGeral += " INNER JOIN FILIAL FIL1 ON FIL1.IDFILIAL = DOC1.IDFILIAL";
            strsqlGeral += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsqlGeral += " AND DOC1.ATIVO = 'SIM'";
            strsqlGeral += " AND DOC1.DATADEEMISSAO = CONVERT(DATETIME, '@DATAEMISSAO', 103) ";
            strsqlGeral += " AND FIL1.IDFILIAL=DOC.IDFILIAL";
            strsqlGeral += " AND DT.ATIVO='SIM'";
            strsqlGeral += " AND DT.IMPRESSO='SIM'";
            strsqlGeral += " ) ";
            strsqlGeral += " RELIBERADA,";
            strsqlGeral += " (SELECT ";
            strsqlGeral += " COUNT(DISTINCT DT.IDDT) ";
            strsqlGeral += " FROM DT  WITH (NOLOCK)";
            strsqlGeral += " INNER JOIN DTROMANEIO DR ON DT.IDDT = DR.IDDT";
            strsqlGeral += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DR.IDROMANEIO";
            strsqlGeral += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO";
            strsqlGeral += " INNER JOIN DOCUMENTO DOC1 ON DOC1.IDDOCUMENTO = ROMDOC.IDDOCUMENTO";
            strsqlGeral += " INNER JOIN FILIAL FIL1 ON FIL1.IDFILIAL = DOC1.IDFILIAL";
            strsqlGeral += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsqlGeral += " AND DOC1.ATIVO = 'SIM'";
            strsqlGeral += " AND DOC1.DATADEEMISSAO = CONVERT(DATETIME, '@DATAEMISSAO', 103) ";
            strsqlGeral += " AND FIL1.IDFILIAL=DOC.IDFILIAL";
            strsqlGeral += " AND DT.ATIVO='SIM'";
            strsqlGeral += " AND DT.IMPRESSO='NAO'";
            strsqlGeral += " ) ";
            strsqlGeral += " RENAOLIBERADA,";

            strsqlGeral += " (SELECT COUNT(DISTINCT DOC1.IDDOCUMENTO)";
            strsqlGeral += " FROM DOCUMENTO DOC1";
            strsqlGeral += " INNER JOIN FILIAL FIL1 ON FIL1.IDFILIAL = DOC1.IDFILIAL";
            strsqlGeral += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsqlGeral += " AND DOC1.ATIVO = 'SIM'";
            strsqlGeral += " AND DATADECONCLUSAO IS NULL";
            strsqlGeral += " AND DATEDIFF(DAY, DOC1.DATADEEMISSAO, GETDATE()) >=5";
            strsqlGeral += " AND DATADEEMISSAO = '2014-01-01'";
            strsqlGeral += " AND FIL1.IDFILIAL=DOC.IDFILIAL) NFSEMDATADEENTREGA";

            strsqlGeral += " FROM DOCUMENTO DOC";
            strsqlGeral += " INNER JOIN FILIAL FIL ON FIL.IDFILIAL = DOC.IDFILIAL";
            strsqlGeral += " WHERE TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsqlGeral += " AND DOC.ATIVO = 'SIM'";
            strsqlGeral += " AND DATADEEMISSAO = CONVERT(DATETIME, '@DATAEMISSAO', 103) ";
            strsqlGeral += " GROUP BY ";
            strsqlGeral += " DOC.IDFILIAL, ";
            strsqlGeral += " FIL.NOME";
            strsqlGeral += " ORDER BY FIL.NOME";

            strsqlGeral = strsqlGeral.Replace("@DATAEMISSAO", DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
            #endregion


            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(strsqlGeral, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            string S1 = MontarHtmlParaEmailBaseNova(dt, "Notas Fiscais Aguardando Embarque", false);
            //string S2 = MontarHtmlParaEmail(dt3dias, "NOTAS FISCAIS AGUARDANDO EMBARQUE 72 HS OU MAIS", true);
            //string S3 = MontarHtmlParaEmail(dt2dias, "NOTAS FISCAIS AGUARDANDO EMBARQUE 48 HS ", true);
            //string S4 = MontarHtmlParaEmail(dt1dia, "NOTAS FISCAIS AGUARDANDO EMBARQUE 24 HS ", true);

            string S2 = "";
            string S3 = "";
            string S4 = "";

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

            //Sistran.Library.EnviarEmails.EnviarEmail(email, "sistema@grupologos.com.br", "Aviso: Notas Fiscais Aguardando Embarque", juncao, "mail.grupologos.com.br", "logos0902", "Situacao de Entregas");

            //new cEmail().enviarEmail("Aviso: Notas Fiscais Aguardando Embarque", juncao, email, "SituacaoDeEntregas");
        }

        private DataTable RetornarBaseNova(int dias, DataTable dt)
        {


            string strsql = " SELECT ";
            strsql += " FIL.NUMERODAFILIAL FILIAL, ";
            strsql += " case FIL.NOME when 'TABOAO' then 'RICOY' ELSE FIL.NOME END nome, ";
            strsql += " COUNT(DOC.IDDOCUMENTO) NOTAS, ";
            strsql += " SUM(CASE WHEN DOC.DATADECONCLUSAO IS NOT NULL THEN 1 ELSE 0 END) ENTREGAS, ";
            strsql += " SUM(DOC.VOLUMES) VOLUMES, ";
            strsql += " Cast(SUM(ISNULL(DOC.PESOBRUTO,0)) As Numeric(12,0)) PESO, ";
            strsql += " SUM(DOC.VALORDANOTA) VALORDANOTA, ";
            strsql += " COUNT(DT.IDDT) RE_EMITIDAS,  ";
            strsql += " 0 RE_NAOLIBERADAS,  ";
            strsql += " 0 NOTASFISCAISNASRENAOLIBERADAS, ";
            strsql += " SUM(CASE WHEN DOC.DATADECONCLUSAO IS NULL THEN 1 ELSE 0 END) SEMDATADEENTREGA ";
            strsql += " FROM DOCUMENTO DOC WITH(NOLOCK)   ";
            strsql += " LEFT JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = DOC.IDDOCUMENTO)  ";
            strsql += " LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = DOC.IDFILIAL ";
            strsql += " LEFT JOIN ROMANEIODOCUMENTO RD ON RD.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            strsql += " LEFT JOIN DTROMANEIO DTROM ON DTROM.IDDTROMANEIO = RD.IDROMANEIO ";
            strsql += " LEFT JOIN DT ON DT.IDDT = DTROM.IDDT ";
            strsql += " WHERE  ";
            strsql += " DOC.IDCLIENTE IN(9)  ";
            strsql += " AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')   ";
            strsql += " AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')   ";
            strsql += " AND DOC.DATADECONCLUSAO IS NULL   ";
            strsql += " AND NOT DOC.DATADEENTRADA IS NULL   ";
            strsql += " AND NOT DOC.CODIGODORECEXP IS NULL   ";
            strsql += " AND DOC.IDDOCUMENTOOCORRENCIA IS NULL  ";
            strsql += " AND DF.SITUACAO='AGUARDANDO EMBARQUE'  ";
            strsql += " AND DOC.ATIVO='SIM'  ";
            strsql += " AND FIL.NOME IS NOT NULL ";
            strsql += " AND DataDeEmissao >='2011-08-01' ";
            strsql += " and DATEDIFF(day, DataDeEntrada, GETDATE()) @dias ";
            strsql += " GROUP BY  ";
            strsql += " FIL.NUMERODAFILIAL, ";
            strsql += " FIL.NOME ";

            strsql = strsql.Replace("@dias", ">=" + dias.ToString());
            //DataTable dtBdNovo = Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, BaseAntigaConfig.Default.BDNovoLogos);


            //for (int i = 0; i < dtBdNovo.Rows.Count; i++)
            //{
            //    DataRow orw = dt.NewRow();
            //    orw["filial"] = dtBdNovo.Rows[i]["filial"];
            //    orw["nome"] = dtBdNovo.Rows[i]["nome"];
            //    orw["Notas"] = dtBdNovo.Rows[i]["Notas"];
            //    orw["Entregas"] = dtBdNovo.Rows[i]["Entregas"];
            //    orw["Volumes"] = dtBdNovo.Rows[i]["Volumes"];
            //    orw["Peso"] = dtBdNovo.Rows[i]["Peso"];
            //    orw["ValorDaNota"] = dtBdNovo.Rows[i]["ValorDaNota"];
            //    orw["RE_Emitidas"] = dtBdNovo.Rows[i]["RE_Emitidas"];
            //    orw["RE_NaoLiberadas"] = dtBdNovo.Rows[i]["RE_NaoLiberadas"];
            //    orw["NotasFiscaisNasRENaoLiberadas"] = dtBdNovo.Rows[i]["NotasFiscaisNasRENaoLiberadas"];
            //    orw["SemDataDeEntrega"] = dtBdNovo.Rows[i]["SemDataDeEntrega"];
            //    dt.Rows.Add(orw);
            //}

            return dt;

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
            strsql += " and CNH.DataDeEmissao >= '01/jan/2014' ";
            strsql += " and CNH.DataDeEntrega is null ";
            strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 AND CNH.SITUACAODAIMPRESSAODOCTRC<>'CANCELADO' ";
            strsql += " ) SemDataDeEntrega ";

            strsql += " From ConhecimentoAEmbarcar CEMB ";
            strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
            strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') /*and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00'*/ Group By ";
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
            strsql += " and CNH.DataDeEmissao >= '01/jan/2014' ";
            strsql += " and CNH.DataDeEntrega is null ";
            strsql += " and DATEDIFF(day, CNH.DataDeEmissao, GETDATE()) > 5 AND CNH.SITUACAODAIMPRESSAODOCTRC<>'CANCELADO' ";
            strsql += " ) SemDataDeEntrega ";

            strsql += " From ConhecimentoAEmbarcar CEMB ";
            strsql += " Inner Join Conhecimento CNH on (CNH.FilialLancamento = CEMB.FilialLancamento and CNH.Controle = CEMB.Controle) ";
            strsql += " Inner Join Filial FL on (FL.Filial = CEMB.FilialAtual) where CEMB.Situacao IN ('AGUARDANDO ENTREGA', 'AGUARDANDO EMBARQUE') ";
            strsql += " and DATEDIFF(day, CNH.DataDeCadastro, GETDATE()) > 3  "; //and CEMB.FilialAtual <> '14' and CEMB.FilialAtual <> '00' ";
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

        public string MontarHtmlParaEmailBaseNova(DataTable dt, string Titulo, bool ATRASO)
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

            decimal PESO = 0;
            decimal ValorDaNota = Convert.ToDecimal(0);
            foreach (DataRow item in dt.Rows)
            {

                Tba += "<TR>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["IDFILIAL"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["FILIAL"].ToString() + "</TD>";


                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["notas"]) > 0 ? Convert.ToDecimal(item["notas"]).ToString("###,###.##") : "0,00") + "</TD>";
                NOTAS += Convert.ToInt32(item["notas"].ToString());

                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["entregas"]) > 0 ? Convert.ToDecimal(item["entregas"]).ToString("###,###.##") : "0,00") + "</TD>";

                ENTREGAS += Convert.ToInt32(item["entregas"].ToString());

                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["volumes"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["volumes"]) > 0 ? Convert.ToDecimal(item["volumes"]).ToString("###,###.##") : "0,00") + "</TD>";

                VOLUMES += Convert.ToInt32(item["volumes"].ToString());


                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["peso"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["peso"]) > 0 ? Convert.ToDecimal(item["peso"]).ToString("###,###.##") : "0,00") + "</TD>";

                PESO += Convert.ToDecimal(item["peso"].ToString());

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

            decimal PESO = 0;
            decimal ValorDaNota = Convert.ToDecimal(0);
            foreach (DataRow item in dt.Rows)
            {

                Tba += "<TR>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["FILIAL"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='left' style='font-size:7pt;height:10px;font-weight:normal'>" + item["nome"].ToString() + "</TD>";


                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["notas"]) > 0 ? Convert.ToDecimal(item["notas"]).ToString("###,###.##") : "0,00") + "</TD>";                
                NOTAS += Convert.ToInt32(item["notas"].ToString());

                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["entregas"]) > 0 ? Convert.ToDecimal(item["entregas"]).ToString("###,###.##") : "0,00") + "</TD>";

                ENTREGAS += Convert.ToInt32(item["entregas"].ToString());

                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["volumes"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["volumes"]) > 0 ? Convert.ToDecimal(item["volumes"]).ToString("###,###.##") : "0,00") + "</TD>";

                VOLUMES += Convert.ToInt32(item["volumes"].ToString());


                //Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + item["peso"].ToString() + "</TD>";
                Tba += "<TD class='tdpVerdana' nowrap=nowrap align='right' style='font-size:7pt;height:10px;font-weight:normal'>" + (Convert.ToDecimal(item["peso"]) > 0 ? Convert.ToDecimal(item["peso"]).ToString("###,###.##") : "0,00") + "</TD>";

                PESO += Convert.ToDecimal(item["peso"].ToString());

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


		//	dffgdfgfdg
        }

        public static void EscreverLogHtml(string menssagem)
        {
            //string nomeArquivo = ConfigurationSettings.AppSettings["ArquivoLog"].Replace("DDMMYYYY", DateTime.Now.ToShortDateString().Replace("\\", "").Replace("/", "").Replace(".txt", ".htm"));
            ////nomeArquivo = nomeArquivo.Replace(".txt", ".htm");
            ////StreamWriter writer = new StreamWriter(nomeArquivo, false);
            ////writer.WriteLine(menssagem.ToUpper());
            //writer.Close();
        }

        //public void enviarEmail(string assunto, string corpo, string emails)
        //{
        //    //create the mail message
        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        //    //set the addresses
        //    mail.From = new System.Net.Mail.MailAddress("moises@sistecno.com.br");
        //    mail.To.Add("moises@sistecno.com.br");
        //    mail.To.Add("moises@mrandrade.com");

        //    //set the content
        //    mail.Subject = assunto;
        //    mail.Body = corpo;
        //    mail.Priority = System.Net.Mail.MailPriority.High;
        //    mail.IsBodyHtml = true;

        //    //mail.Attachments.Add(new System.Net.Mail.Attachment("logos.jpg"));


        //    //send the message
        //    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.grupologos.com.br");
        //    smtp.EnableSsl = false;

        //    System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("sistema@grupologos.com.br", "logos0902");
        //    smtp.Credentials = credenciais;
        //    smtp.Send(mail);
        //}

        public static string RetornarStringBaseAntiga()
        {
            return BaseAntigaConfig.Default.BDAntigo;
        }

        public static string RetornarStringBaseNovaLogos()
        {
            return BaseAntigaConfig.Default.BDNovoLogos;
        }

        public static string RetornarStringBaseSTNNovo()
        {
            return BaseAntigaConfig.Default.BDStnNovo;
        }

        public static DataTable RetornarEmails()
        {
            DataTable dtemp = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM AVISOROBOEMAIL ORDER BY NOME", BaseAntigaConfig.Default.BDNovoLogos);

            for (int i = 0; i < dtemp.Rows.Count; i++)
            {
                dtemp.Rows[i]["reports"] = dtemp.Rows[i]["reports"].ToString().Replace(",", "<br>");
            }
            return dtemp;
        }

        public static DataTable RetornarEmails2()
        {
            DataTable dtemp = Sistran.Library.GetDataTables.RetornarDataTableWS("SELECT * FROM AVISOROBOEMAIL ORDER BY NOME", BaseAntigaConfig.Default.BDNovoLogos);
            return dtemp;
        }
    }
}
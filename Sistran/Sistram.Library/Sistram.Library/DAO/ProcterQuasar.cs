using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class ProcterQuasar
    {
        const string conexaoQuasar = "Data Source=192.168.10.3;Initial Catalog=quasar;User ID=site_ASP;Password=asp7998;";

        public DataTable RerotnarLista(DateTime dti, DateTime dtf)
        {
            string strsql = ""; //" EXEC RetornarMovProcter '" + dti + "', '" + dtf + "'";

            #region antigo


            dtf = dtf.AddHours(23);
            dtf = dtf.AddMinutes(59);

            //strsql += "  Select   ";
            //strsql += "  Rem.CnpjCpf CnpjRemetente, ";
            //strsql += "  Rem.RazaosocialNome NomeRemetente, ";
            //strsql += "  CUF.UF UFRemetente, ";
            //strsql += "  CRem.Nome CidadeRemetente, ";
            //strsql += "  Sum(CTO.Volumes) Volumes,  ";
            //strsql += "  Sum(CTO.PesoBruto) PesoBruto,    ";
            //strsql += "  Sum(CTO.ValorDaNota) ValorDaNota,   ";
            //strsql += "  Sum(DF.Frete) Frete,      ";
            //strsql += " SUM(DF.FRETE) FRETE, ";
            //strsql += " COUNT( CTO.IDDOCUMENTO) QUANTIDADEDENOTAS,   ";
            //strsql += "   SUM(ISNULL(TDUP.VALOR,0)) FATURADO,  ";
            ////strsql += " ABS(SUM(DF.FRETE)-SUM(ISNULL(TDUP.VALOR,0))) NAOFATURADO,    ";
            //strsql += " ABS(SUM(DF.FRETE)- SUM(ISNULL(CASE TDUP.SALDO  WHEN 0 THEN TDUP.VALOR ELSE 0  END,0))) NAOFATURADO,";
            //strsql += " ISNULL(SUM( CASE TDUP.Saldo  WHEN 0 THEN TDUP.VALOR else 0  END),0) PAGO, ";
            //strsql += " ISNULL(SUM(ISNULL(TDUP.VALOR,0)) - ISNULL(SUM(CASE TDUP.Saldo  WHEN 0 THEN TDUP.VALOR else 0  END),0),0) ABERTO      ";
            //strsql += "  From Documento NF  ";
            //strsql += "  Inner Join Cadastro Rem on (Rem.IdCadastro = NF.IdRemetente) ";
            //strsql += "  Inner Join Cadastro Dest on (Dest.IdCadastro = NF.IdDestinatario)  ";
            //strsql += "  Inner Join Cidade CRem on (CRem.IdCidade = Rem.IdCidade)  ";
            //strsql += "  Inner Join Estado CUF on (CUF.IdEstado = CRem.IdEstado) ";
            //strsql += "  Inner Join Cidade CDes on (CDes.IdCidade = Dest.IdCidade)  ";
            //strsql += "  Inner Join Estado DUF on (DUF.IdEstado = CDes.IdEstado)  ";
            //strsql += "  Left Join DocumentoRelacionado DR on (DR.IdDocumentoFilho = NF.IdDocumento) ";
            //strsql += "  Left Join Documento CTO on (CTO.IdDocumento = DR.IdDocumentoPai) ";
            //strsql += "  Left Join DocumentoFrete DF on (DF.IdDocumento = CTO.IdDocumento)  ";
            //strsql += " LEFT JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTO.IDDOCUMENTO ";
            //strsql += " left join Titulo t on t.IDTitulo = td.IdTitulo ";
            //strsql += " left join TituloDuplicata tdup on tdup.IDTitulo = t.IDTitulo ";
            //strsql += "  where Rem.RazaoSocialNome like 'PROCTER%'     ";
            //strsql += "  and NF.Ativo = 'SIM' ";
            //strsql += "  and CTO.Ativo = 'SIM' ";
            //strsql += "  and cto.DataDeEmissao between CONVERT(DATETIME, '" + dti + "', 103) and CONVERT(DATETIME, '" + dtf + "', 103) ";
            //strsql += "  Group By ";
            //strsql += "  Rem.CnpjCpf, ";
            //strsql += "  Rem.RazaosocialNome, ";
            //strsql += "  CUF.UF, ";
            //strsql += "  CRem.Nome ";
            #endregion

            #region errado


            //strsql += "  Select ";
            //strsql += " REM.CnpjCpf CnpjRemetente, ";
            //strsql += " Rem.RazaoSocialNome NomeRemetente, ";
            //strsql += " RemUF.UF UFRemetente, ";
            //strsql += " RemCidade.Nome CidadeRemetente, ";
            //strsql += " SUM(DF.Frete) Frete, ";
            //strsql += " SUM(CTR.VOLUMES) VOLUMES,     ";
            //strsql += " SUM(CTR.PESOBRUTO) PESOBRUTO,    ";
            //strsql += " SUM(CTR.VALORDANOTA) VALORDANOTA,   ";
            //strsql += " COUNT(CTR.IDDOCUMENTO) QUANTIDADEDENOTAS,    ";
            //strsql += " ISNULL(( ";
            //strsql += " SELECT  ";
            //strsql += " SUM(DISTINCT ISNULL(TDUP.VALOR,0))  ";
            //strsql += " FROM DOCUMENTO CTRI ";
            //strsql += " INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDREMETENTE)    ";
            //strsql += " INNER JOIN DOCUMENTOFRETE DFI ON (DFI.IDDOCUMENTO = CTRI.IDDOCUMENTO) ";
            //strsql += " INNER JOIN CIDADE REMCIDADEI ON (REMCIDADEI.IDCIDADE = REMI.IDCIDADE) ";
            //strsql += " INNER JOIN ESTADO REMUFI ON (REMUFI.IDESTADO = REMCIDADEI.IDESTADO)  ";
            //strsql += " INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO   ";
            //strsql += " INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO   ";
            //strsql += " INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO ";
            //strsql += " WHERE ";
            //strsql += " REMI.IDCadastro = CTR.IDCliente ";
            //strsql += " AND REMCIDADEI.IDCIDADE =REMCIDADE.IDCIDADE ";
            //strsql += " AND REMUFI.IDEstado = REMUF.IDEstado  ";
            //strsql += " AND CTRI.ATIVO = 'SIM'   ";
            //strsql += " AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103) ";

            //strsql += " ),0) FATURADO,    ";

            //strsql += "ISNULL((   ";
            //strsql += "SELECT SUM(VALOR) FROM TITULODUPLICATA ";
            //strsql += "WHERE IDTITULODUPLICATA IN( ";
            //strsql += "SELECT    ";
            //strsql += "DISTINCT TDUP.IDTITULODUPLICATA ";
            //strsql += "FROM DOCUMENTO CTRI   ";
            //strsql += "INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDREMETENTE)     		 ";
            //strsql += "INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO     ";
            //strsql += "INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO     ";
            //strsql += "INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO   ";
            //strsql += "WHERE   ";
            //strsql += "REMI.IDCADASTRO = ctr.IDCliente  		 ";
            //strsql += "AND CTRI.ATIVO = 'SIM'  AND T.PagarReceber='RECEBER'   ";
            //strsql += "AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103) ";
            //strsql += ")),0) 		 ";
            //strsql += "FATURADO,    ";


            //strsql += " ISNULL(( ";
            //strsql += " SELECT  ";
            //strsql += " SUM(DISTINCT ISNULL(TDUP.VALOR,0)) FATURADO ";
            //strsql += " FROM DOCUMENTO CTRI ";
            //strsql += " INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDREMETENTE)    ";
            //strsql += " INNER JOIN DOCUMENTOFRETE DFI ON (DFI.IDDOCUMENTO = CTRI.IDDOCUMENTO) ";
            //strsql += " INNER JOIN CIDADE REMCIDADEI ON (REMCIDADEI.IDCIDADE = REMI.IDCIDADE) ";
            //strsql += " INNER JOIN ESTADO REMUFI ON (REMUFI.IDESTADO = REMCIDADEI.IDESTADO)  ";
            //strsql += " INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO   ";
            //strsql += " INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO   ";
            //strsql += " INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO ";
            //strsql += " WHERE ";
            //strsql += " REMI.IDCadastro = CTR.IDCliente ";
            //strsql += " AND REMCIDADEI.IDCIDADE =REMCIDADE.IDCIDADE ";
            //strsql += " AND REMUFI.IDEstado = REMUF.IDEstado  ";
            //strsql += " AND CTRI.ATIVO = 'SIM'   ";
            //strsql += " AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103) ";
            //strsql += " AND Saldo=0 ";

            //strsql += "     ),0) PAGO, ";
            //strsql += " ISNULL(( ";
            //strsql += " SUM(ISNULL(DF.Frete,0)) - ";
            //strsql += " ( ";
            //strsql += " SELECT  ";
            //strsql += " ISNULL(SUM(DISTINCT ISNULL(TDUP.VALOR,0)),0) FATURADO ";
            //strsql += " FROM DOCUMENTO CTRI ";
            //strsql += " INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDREMETENTE)    ";
            //strsql += " INNER JOIN DOCUMENTOFRETE DFI ON (DFI.IDDOCUMENTO = CTRI.IDDOCUMENTO) ";
            //strsql += " INNER JOIN CIDADE REMCIDADEI ON (REMCIDADEI.IDCIDADE = REMI.IDCIDADE) ";
            //strsql += " INNER JOIN ESTADO REMUFI ON (REMUFI.IDESTADO = REMCIDADEI.IDESTADO)  ";
            //strsql += " INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO   ";
            //strsql += " INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO   ";
            //strsql += " INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO ";
            //strsql += " WHERE ";
            //strsql += " REMI.IDCadastro = CTR.IDCliente ";
            //strsql += " AND REMCIDADEI.IDCIDADE =REMCIDADE.IDCIDADE ";
            //strsql += " AND REMUFI.IDEstado = REMUF.IDEstado  ";
            //strsql += " AND CTRI.ATIVO = 'SIM'   ";
            //strsql += " AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103) ";
            //strsql += " AND Saldo=0)  ";
            //strsql += " ),0) ABERTO, ";

            //strsql += " abs(ISNULL(( ";
            //strsql += " SUM(ISNULL(DF.Frete,0)) - ";
            //strsql += " ( ";
            //strsql += " SELECT ISNULL( ";
            //strsql += " SUM(DISTINCT ISNULL(TDUP.VALOR,0)),0) FATURADO ";
            //strsql += " FROM DOCUMENTO CTRI ";
            //strsql += " INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDREMETENTE)    ";
            //strsql += " INNER JOIN DOCUMENTOFRETE DFI ON (DFI.IDDOCUMENTO = CTRI.IDDOCUMENTO) ";
            //strsql += " INNER JOIN CIDADE REMCIDADEI ON (REMCIDADEI.IDCIDADE = REMI.IDCIDADE) ";
            //strsql += " INNER JOIN ESTADO REMUFI ON (REMUFI.IDESTADO = REMCIDADEI.IDESTADO)  ";
            //strsql += " INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO   ";
            //strsql += " INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO   ";
            //strsql += " INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO ";
            //strsql += " WHERE ";
            //strsql += " REMI.IDCadastro = CTR.IDCliente ";
            //strsql += " AND REMCIDADEI.IDCIDADE =REMCIDADE.IDCIDADE ";
            //strsql += " AND REMUFI.IDEstado = REMUF.IDEstado  ";
            //strsql += " AND CTRI.ATIVO = 'SIM'   ";
            //strsql += " AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103) ) 		     ";
            //strsql += " ),0) ) NAOFATURADO ";


            //strsql += " From Documento CTR ";
            //strsql += " INNER JOIN CADASTRO REM ON (REM.IDCADASTRO = CTR.IDREMETENTE)    ";
            //strsql += " INNER JOIN DocumentoFrete DF on (DF.IDDocumento = CTR.IDDocumento) ";
            //strsql += " Inner Join Cidade RemCidade on (RemCIdade.IdCidade = REM.IdCidade) ";
            //strsql += " Inner Join Estado RemUF on (RemUF.IdEstado = RemCIdade.IdEstado)     ";
            //strsql += " where CTR.TipoDeDocumento = 'CONHECIMENTO'  ";
            //strsql += " and REM.RAZAOSOCIALNOME LIKE 'PROCTER%'      ";
            //strsql += " and CTR.ATIVO = 'SIM'   ";
            //strsql += " and CTR.DATADEEMISSAO BETWEEN CONVERT(DATETIME, @DATAINICIO, 103) AND CONVERT(DATETIME, @DATAFIM, 103)   ";
            //strsql += " Group By  ";
            //strsql += " REM.CnpjCpf, ";
            //strsql += " Rem.RazaoSocialNome, ";
            //strsql += " RemUF.UF, ";
            //strsql += " RemCidade.Nome, ";
            //strsql += " CTR.IDCliente, ";
            //strsql += " RemUF.IDEstado, ";
            //strsql += " RemCidade.IDCidade ";
            #endregion


            #region Conferido
            strsql += "  SELECT   ";
            strsql += "  REM.CNPJCPF CNPJREMETENTE,   ";
            strsql += "  REM.FANTASIAAPELIDO NOMEREMETENTE,   ";
            strsql += "  REMUF.UF UFREMETENTE,   ";
            strsql += "  REMCIDADE.NOME CIDADEREMETENTE,   ";
            strsql += "  SUM(DF.FRETE) FRETE,   ";
            strsql += "  SUM(CTR.VOLUMES) VOLUMES,       ";
            strsql += "  SUM(CTR.PESOBRUTO) PESOBRUTO,      ";
            strsql += "  SUM(CTR.VALORDANOTA) VALORDANOTA,     ";
            strsql += "  COUNT(CTR.IDDOCUMENTO) QUANTIDADEDENOTAS,      ";
            strsql += "  ISNULL((   ";
            strsql += "  SELECT SUM(ISNULL(VALOR,0)) FROM TITULODUPLICATA ";
            strsql += "  WHERE IDTITULODUPLICATA IN( ";
            strsql += "  SELECT    ";
            strsql += "  DISTINCT TDUP.IDTITULODUPLICATA ";
            strsql += "  FROM DOCUMENTO CTRI   ";
            strsql += "  INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDCliente)     		 ";
            strsql += "  INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO     ";
            strsql += "  INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO     ";
            strsql += "  INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO   ";
            strsql += "  WHERE   ";
            strsql += "  REMI.IDCADASTRO = ctr.IDCliente  		 ";
            strsql += "  AND CTRI.ATIVO = 'SIM'     ";
            strsql += "  AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + dti + "', 103)  ";
            strsql += "  AND CONVERT(DATETIME, '" + dtf + "', 103)    ";
            strsql += "  AND T.PagarReceber='RECEBER' ";
            strsql += "  )),0) 		 ";
            strsql += "  FATURADO,      ";
            strsql += "    ISNULL((  ";
            strsql += "  SELECT SUM(ISNULL(ValorPagoAcumulado,0)) FROM TITULODUPLICATA ";
            strsql += "  WHERE IDTITULODUPLICATA IN( ";
            strsql += "  SELECT    ";
            strsql += "  DISTINCT TDUP.IDTITULODUPLICATA ";
            strsql += "  FROM DOCUMENTO CTRI   ";
            strsql += "  INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDCliente)     		 ";
            strsql += "  INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO     ";
            strsql += "  INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO     ";
            strsql += "  INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO   ";
            strsql += "  WHERE   ";
            strsql += "  REMI.IDCADASTRO = ctr.IDCliente  		 ";
            strsql += "  AND CTRI.ATIVO = 'SIM'     ";
            strsql += "  AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + dti + "', 103)  ";
            strsql += "  AND CONVERT(DATETIME, '" + dtf + "', 103)    ";
            strsql += "  AND T.PagarReceber='RECEBER'  AND SALDO=0   ";
            strsql += "  		)),0) PAGO, ";
            strsql += "  	ISNULL((  SUM(ISNULL(DF.FRETE,0)) -   ";
            strsql += "  (	    ";
            strsql += "  SELECT SUM(ISNULL(ValorPagoAcumulado,0)) FROM TITULODUPLICATA ";
            strsql += "  WHERE IDTITULODUPLICATA IN( ";
            strsql += "  SELECT    ";
            strsql += "  DISTINCT TDUP.IDTITULODUPLICATA ";
            strsql += "  FROM DOCUMENTO CTRI   ";
            strsql += "  INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDCliente)     		 ";
            strsql += "  INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO     ";
            strsql += "  INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO     ";
            strsql += "  INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO   ";
            strsql += "  WHERE   ";
            strsql += "  REMI.IDCADASTRO = ctr.IDCliente  		 ";
            strsql += "  AND CTRI.ATIVO = 'SIM'     ";
            strsql += "  AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + dti + "', 103)  ";
            strsql += "  AND CONVERT(DATETIME, '" + dtf + "', 103)    ";
            strsql += "  AND T.PagarReceber='RECEBER'  AND SALDO=0)      	 ";
            strsql += "  ) ";
            strsql += "  ),0) ABERTO,   ";
            strsql += " ABS( ";
            strsql += " ISNULL( ";
            strsql += " ( SUM(ISNULL(DF.FRETE,0)) -   ";
            strsql += " ( ";
            strsql += " SELECT SUM(ISNULL(VALOR,0)) FROM TITULODUPLICATA ";
            strsql += " WHERE IDTITULODUPLICATA IN( ";
            strsql += " SELECT    ";
            strsql += " DISTINCT TDUP.IDTITULODUPLICATA ";
            strsql += " FROM DOCUMENTO CTRI   ";
            strsql += " INNER JOIN CADASTRO REMI ON (REMI.IDCADASTRO = CTRI.IDCliente)     		 ";
            strsql += " INNER JOIN TITULODOCUMENTO TD ON TD.IDDOCUMENTO =CTRI.IDDOCUMENTO     ";
            strsql += " INNER JOIN TITULO T ON T.IDTITULO = TD.IDTITULO     ";
            strsql += " INNER JOIN TITULODUPLICATA TDUP ON TDUP.IDTITULO = T.IDTITULO   ";
            strsql += " WHERE   ";
            strsql += " REMI.IDCADASTRO = ctr.IDCliente  		 ";
            strsql += " AND CTRI.ATIVO = 'SIM'     ";
            strsql += " AND CTRI.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + dti + "', 103)  ";
            strsql += " AND CONVERT(DATETIME, '" + dtf + "', 103)    ";
            strsql += " AND T.PagarReceber='RECEBER'  ";
            strsql += " ))),0)  ";
            strsql += " ) NAOFATURADO   ";
            strsql += "  		FROM DOCUMENTO CTR   ";
            strsql += "  INNER JOIN CADASTRO REM ON (REM.IDCADASTRO = CTR.IDREMETENTE)      ";
            strsql += "  INNER JOIN DOCUMENTOFRETE DF ON (DF.IDDOCUMENTO = CTR.IDDOCUMENTO)   ";
            strsql += "  INNER JOIN CIDADE REMCIDADE ON (REMCIDADE.IDCIDADE = REM.IDCIDADE)   ";
            strsql += "  INNER JOIN ESTADO REMUF ON (REMUF.IDESTADO = REMCIDADE.IDESTADO)       ";
            strsql += "  WHERE CTR.TIPODEDOCUMENTO = 'CONHECIMENTO'    ";
            strsql += "  AND REM.RAZAOSOCIALNOME LIKE 'PROCTER%'        ";
            strsql += "  AND CTR.ATIVO = 'SIM'     ";
            strsql += "  AND CTR.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + dti + "', 103)  ";
            strsql += "  AND CONVERT(DATETIME, '" + dtf + "', 103)     ";
            strsql += "  GROUP BY   REM.CNPJCPF,  REM.FANTASIAAPELIDO,  REMUF.UF,  REMCIDADE.NOME,  CTR.IDCLIENTE,  REMUF.IDESTADO,  REMCIDADE.IDCIDADE  ";
            #endregion

            strsql = strsql.Replace("@DATAINICIO", "'" + dti + "'");
            strsql = strsql.Replace("@DATAFIM", "'" + dtf + "'");


            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, conexaoQuasar);
        }


        public DataTable RerotnarLista(DateTime dti, DateTime dtf, string cnpj)
        {
            string strsql = "";
            dtf = dtf.AddHours(23);
            dtf = dtf.AddMinutes(59);

            strsql += "  Select   distinct";
            strsql += "  NF.DataDeEmissao, ";
            strsql += "  NF.Numero NotaFiscal,  ";
            strsql += " CTO.Numero CTRC, ";
            strsql += " CTO.Volumes, ";
            strsql += " Rem.CnpjCpf CnpjRemetente, ";
            strsql += " Rem.RazaosocialNome NomeRemetente, ";
            strsql += " CUF.UF UFRemetente, ";
            strsql += " CRem.Nome CidadeRemetente, ";
            strsql += " Dest.CnpjCpf CnpjDestinatario, ";
            strsql += " Dest.RazaoSocialNome NomeDestinatario, ";
            strsql += " DUF.UF UFDestinatario, ";
            strsql += " CDes.Nome CidadeDestinatario,  ";
            strsql += " CTO.PesoBruto, ";
            strsql += " CTO.ValorDaNota, ";
            strsql += " DF.Frete ";
            strsql += "  From Documento NF  ";
            strsql += "  Inner Join Cadastro Rem on (Rem.IdCadastro = NF.IdRemetente) ";
            strsql += "  Inner Join Cadastro Dest on (Dest.IdCadastro = NF.IdDestinatario)  ";
            strsql += "  Inner Join Cidade CRem on (CRem.IdCidade = Rem.IdCidade)  ";
            strsql += "  Inner Join Estado CUF on (CUF.IdEstado = CRem.IdEstado) ";
            strsql += "  Inner Join Cidade CDes on (CDes.IdCidade = Dest.IdCidade)  ";
            strsql += "  Inner Join Estado DUF on (DUF.IdEstado = CDes.IdEstado)  ";
            strsql += "  Left Join DocumentoRelacionado DR on (DR.IdDocumentoFilho = NF.IdDocumento) ";
            strsql += "  Left Join Documento CTO on (CTO.IdDocumento = DR.IdDocumentoPai) ";
            strsql += "  Left Join DocumentoFrete DF on (DF.IdDocumento = CTO.IdDocumento)  ";
            strsql += "  where Rem.RazaoSocialNome like 'PROCTER%'     ";
            strsql += "   and NF.Ativo = 'SIM' ";
            strsql += " and CTO.Ativo = 'SIM' ";
            strsql += "  and cto.DataDeEmissao between CONVERT(DATETIME, '" + dti + "', 103) and CONVERT(DATETIME, '" + dtf + "', 103) ";
            strsql += " and Rem.CnpjCpf = '"+cnpj+"'  ";
            strsql += " Order By Rem.CnpjCpf, NF.Numero ";

            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, conexaoQuasar);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;
using Sistran.Library;

namespace SistranDAO
{
    public sealed class NotasFiscais
    {
        public static List<SistranMODEL.NotasFiscais> RetornaNotasFiscaisSaida(int IdCliente, int? NotaFiscalId, DateTime DataEmissaoInicio, DateTime DataEmissaoFinal, DateTime DataEntregaInicio, DateTime DataEntregaFinal, string Cnpj, string RazaoSocial, string Fantasia, string tiponota, string Conn)
        {
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);

            DbCommand cmdNotasSaida = SistranDb.GetStoredProcCommand("stpBuscaNotaFiscalSaida");
            cmdNotasSaida.CommandType = CommandType.StoredProcedure;
            cmdNotasSaida.CommandTimeout = 300;

            SistranDb.AddInParameter(cmdNotasSaida, "IdCliente", DbType.Int32, IdCliente);
            if (NotaFiscalId != null)
            {
                SistranDb.AddInParameter(cmdNotasSaida, "NotaFiscalId", DbType.Int32, NotaFiscalId);
            }
            SistranDb.AddInParameter(cmdNotasSaida, "DataEmissaoInicio", DbType.DateTime, DataEmissaoInicio.ToString());
            SistranDb.AddInParameter(cmdNotasSaida, "DataEmissaoFinal", DbType.DateTime, DataEmissaoFinal.ToString());
            SistranDb.AddInParameter(cmdNotasSaida, "DataEntregaInicio", DbType.DateTime, DataEntregaInicio.ToString());
            SistranDb.AddInParameter(cmdNotasSaida, "DataEntregaFinal", DbType.DateTime, DataEntregaFinal.ToString());
            SistranDb.AddInParameter(cmdNotasSaida, "Cnpj", DbType.String, Cnpj);
            SistranDb.AddInParameter(cmdNotasSaida, "RazaoSocial", DbType.String, RazaoSocial);
            SistranDb.AddInParameter(cmdNotasSaida, "Fantasia", DbType.String, Fantasia);
            SistranDb.AddInParameter(cmdNotasSaida, "TipoNota", DbType.String, tiponota);


            List<SistranMODEL.NotasFiscais> ILNotasFiscais = new List<SistranMODEL.NotasFiscais>();

            using (IDataReader drNotasSaida = SistranDb.ExecuteReader(cmdNotasSaida))
                while (drNotasSaida.Read())
                {
                    ILNotasFiscais.Add(new SistranMODEL.NotasFiscais(Convert.ToInt32(drNotasSaida["IDDOCUMENTO"]),
                    Convert.ToInt32(drNotasSaida["NUMERO"]),
                    drNotasSaida["CNPJCPFDESTINATARIO"].ToString(),
                    drNotasSaida["RAZAONOMEDESTINATARIO"].ToString(),
                    drNotasSaida["FANTASIAAPELIDODESTINATARIO"].ToString(),
                    drNotasSaida["CNPJCPFREMETENTE"].ToString(),
                    drNotasSaida["RAZAONOMEREMETENTE"].ToString(),
                    drNotasSaida["ORIGEM"].ToString(),
                    Convert.ToDateTime(drNotasSaida["DATADEEMISSAO"]),
                    Convert.ToDateTime(drNotasSaida["DATADEENTRADA"]),
                    Convert.ToDateTime(drNotasSaida["DATADESAIDA"]),
                    Convert.ToDateTime(drNotasSaida["DATADECONCLUSAO"]),
                    drNotasSaida["SITUACAO"].ToString(),
                    drNotasSaida["OCORRENCIA"].ToString(),
                    Convert.ToInt32(drNotasSaida["CODIGODORECEXP"]),
                    drNotasSaida["CIDADEREMETENTE"].ToString(),
                    drNotasSaida["UFREMETENTE"].ToString(),
                    drNotasSaida["CIDADEDESTINATARIO"].ToString(),
                    drNotasSaida["UFDESTINATARIO"].ToString(),
                    drNotasSaida["ENDERECODESTINATARIO"].ToString(),
                    drNotasSaida["NUMERODESTINATARIO"].ToString(),
                    drNotasSaida["COMPLEMENTODESTINATARIO"].ToString(),
                    drNotasSaida["ATIVO"].ToString(),
                    drNotasSaida["VALORDANOTA"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDANOTA"]),
                    drNotasSaida["VALORDEDESCONTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDEDESCONTO"]),
                    drNotasSaida["BASEDOIPI"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["BASEDOIPI"]),
                    drNotasSaida["BASEDOICMS"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["BASEDOICMS"]),
                    drNotasSaida["VALORDOICMS"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDOICMS"]),
                    drNotasSaida["PESOLIQUIDO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["PESOLIQUIDO"]),
                    drNotasSaida["PESOBRUTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["PESOBRUTO"]),
                    drNotasSaida["VALORDEDESCONTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDEDESCONTO"])));

                }

            return ILNotasFiscais;
        }

        public static DataTable NotaFiscalSelConsultar(int DocId, string Conn)
        {
            // Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT DOCOCO.DESCRICAO DESCOCO , DOC.DOCUMENTOESPECIAL ESPECIAL, DOC.DATAPLANEJADA, DOC.DATADEAGENDAMENTO, ");
            strsql.Append(" DOC.TIPODEDOCUMENTO,");
            strsql.Append(" TIPODESERVICO,");
            strsql.Append(" ENTRADASAIDA,");
            strsql.Append(" DOC.NUMERO,");
            strsql.Append(" SERIE,");
            strsql.Append(" CLI.IDCLIENTE,");
            strsql.Append(" CADCLI.RAZAOSOCIALNOME RAZAOSOCIALCLIENTE, ");
            strsql.Append(" ESTCLI.UF AS UFCLIENTE,    ");
            strsql.Append(" CIDCLI.NOME AS CIDADECLIENTE, ");
            strsql.Append(" ESTCLI.UF AS UFCLIENTE,");
            strsql.Append(" ISNULL(CADCLI.ENDERECO, '') AS ENDERECOCLIENTE,    ");
            strsql.Append(" ISNULL(CADCLI.NUMERO,'') AS NUMEROCLIENTE,    ");
            strsql.Append(" ISNULL(CADCLI.COMPLEMENTO, '') AS COMPLEMENTOCLIENTE, ");
            strsql.Append(" CADREM.RAZAOSOCIALNOME AS RAZAONOMEREMETENTE, ");
            strsql.Append(" ESTREM.UF AS UFREMETENTE,    ");
            strsql.Append(" CIDDES.NOME AS CIDADEDESTINATARIO,    ");
            strsql.Append(" ESTDES.UF AS UFDESTINATARIO,");
            strsql.Append(" ISNULL(CADREM.ENDERECO, '') AS ENDERECOREMETENTE,    ");
            strsql.Append(" ISNULL(CADREM.NUMERO,'') AS NUMEROREMETENTE,    ");
            strsql.Append(" ISNULL(CADREM.COMPLEMENTO, '') AS COMPLEMENTOREMETENTE, ");
            strsql.Append(" CIDREM.NOME AS CIDADEREMETENTE,");
            strsql.Append(" CADDES.CNPJCPF AS CNPJCPFDESTINATARIO,    ");
            strsql.Append(" CADDES.RAZAOSOCIALNOME AS RAZAONOMEDESTINATARIO,    ");
            strsql.Append(" CADDES.FANTASIAAPELIDO AS FANTASIAAPELIDODESTINATARIO, ");
            strsql.Append(" CADREM.CNPJCPF AS CNPJCPFREMETENTE,  ");
            strsql.Append(" ISNULL(CADDES.ENDERECO, '') AS ENDERECODESTINATARIO,    ");
            strsql.Append(" ISNULL(CADDES.NUMERO,'') AS NUMERODESTINATARIO,    ");
            strsql.Append(" ISNULL(CADDES.COMPLEMENTO, '') AS COMPLEMENTODESTINATARIO,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADOMOVIMENTO, '01/01/1900'), 103))AS DATADOMOVIMENTO, ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADEENTRADA, '01/01/1900'), 103))AS DATADEENTRADA,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECANCELAMENTO, '01/01/1900'), 103))AS DATADECANCELAMENTO,");

            strsql.Append(" ISNULL(DOC.ATIVO, '') AS ATIVO,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADEEMISSAO, '01/01/1900'), 103))AS DATADEEMISSAO,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATAPLANEJADA, '01/01/1900'), 103))AS DATAPLANEJADA,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECONCLUSAO, '01/01/1900'), 103))AS DATADECONCLUSAO,");
            strsql.Append(" ISNULL(DOC.CODIGODORECEXP, 0) AS CODIGODORECEXP,");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADECONCLUSAO, '01/01/1900'), 103))AS DATADECONCLUSAO,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADORECEXP, '01/01/1900'), 103))AS DATADECONCLUSAORECEBIMENTO,");
            strsql.Append(" DOC.ENDERECO,");
            strsql.Append(" DOC.ENDERECONUMERO,");
            strsql.Append(" DOC.ENDERECOCOMPLEMENTO,");
            strsql.Append(" CID.NOME CIDADE,");
            strsql.Append(" EST.NOME ESTADO,");
            strsql.Append(" DOC.IDDOCUMENTO,");
            strsql.Append(" DOC.ORIGEM,    ");
            strsql.Append(" (CONVERT(VARCHAR(10), ISNULL(DOC.DATADESAIDA, '01/01/1900'), 103))AS DATADESAIDA,    ");
            strsql.Append(" DOCFIL.SITUACAO,    ");
            strsql.Append(" OCO.NOME AS OCORRENCIA,");
            strsql.Append(" DOCOCO.DATAOCORRENCIA,   ");
            strsql.Append(" OCO.Nome,  ");
            strsql.Append(" doc.VALORDANOTA,");
            strsql.Append(" VALORDEDESCONTO,");
            strsql.Append(" BASEDOIPI,");
            strsql.Append(" BASEDOICMS,");
            strsql.Append(" VALORDOICMS,");
            strsql.Append(" doc.PESOLIQUIDO,");
            strsql.Append(" doc.PESOBRUTO,");
            strsql.Append(" VALORDEDESCONTO,");
            strsql.Append(" FIL.Nome AS NOMEFILIAL,");
            strsql.Append(" FIL.IDFILIAL,");
            strsql.Append(" doc.IDDOCUMENTOOCORRENCIA, DOC.VOLUMES, DOC.METRAGEMCUBICA,");
            strsql.Append(" cdaMot.RazaoSocialNome Motorista ");
            strsql.Append(" FROM DOCUMENTO DOC   WITH (NOLOCK)  ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO  ON(DOCOCO.IDDOCUMENTO = DOC.IDDOCUMENTO)  ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO  ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)  ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DOCFIL  ON(DOCFIL.IDDOCUMENTO = DOC.IDDOCUMENTO)  ");
            strsql.Append(" LEFT JOIN CADASTRO CADDES  ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
            strsql.Append(" LEFT JOIN CIDADE CIDDES  ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTDES  ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
            strsql.Append(" LEFT JOIN CADASTRO CADREM  ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
            strsql.Append(" LEFT JOIN CIDADE CIDREM  ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTREM  ON(ESTREM.IDESTADO = CIDREM.IDESTADO)  ");
            strsql.Append(" LEFT JOIN CLIENTE CLI ON DOC.IDCLIENTE = CLI.IDCLIENTE ");
            strsql.Append(" LEFT JOIN CADASTRO CADCLI  ON(CADCLI.IDCADASTRO = DOC.IDCLIENTE)  ");
            strsql.Append(" LEFT JOIN CIDADE CIDCLI  ON(CIDCLI.IDCIDADE = CLI.IDCLIENTE)  ");
            strsql.Append(" LEFT JOIN ESTADO ESTCLI  ON(ESTCLI.IDESTADO = CLI.IDCLIENTE)");
            strsql.Append(" LEFT JOIN CIDADE CID  ON(CID.IDCIDADE = DOC.IDENDERECOBAIRRO)  ");
            strsql.Append(" LEFT JOIN ESTADO EST  ON(EST.IDESTADO = CID.IDESTADO)");
            strsql.Append(" INNER JOIN Filial FIL ON FIL.IDFilial = DOC.IDFilial");
            strsql.Append(" LEFT JOIN DTROMANEIO DTROM ON DTROM.IDROMANEIO = DOC.IDROMANEIO");
            strsql.Append(" LEFT JOIN DT ON DT.IDDT = DTROM.IDDT");
            strsql.Append(" LEFT JOIN CADASTRO CDAMOT ON CDAMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA");
            strsql.Append(" WHERE  ");
            strsql.Append(" DOC.IDDOCUMENTO = @IDDOCUMENTO");

            strsql.Replace("@IDDOCUMENTO", DocId.ToString());

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

            //return SistranDb.ExecuteDataSet(CommandType.Text, strsql.ToString()).Tables[0];

        }

        public static DataTable ListarOcorrenciaSelConsultar(int DocId, string Conn)
        {
            //Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" Select DocOco.*, DTR.IdDt, DT.Numero, Oco.Nome As NomeDaOcorrencia, Oco.Codigo, upper(Usu.Login) AS NOMEUSUARIO,  ");
            strsql.Append(" Case when DocArq.IdDocumentoOcorrenciaArquivo > 0 then 'SIM' else null end Foto    ");
            strsql.Append(" From DocumentoOcorrencia DocOco    WITH (NOLOCK) ");
            strsql.Append(" Left Join Ocorrencia Oco on (Oco.IdOcorrencia=DocOco.IdOcorrencia)    ");
            strsql.Append(" Left Join Usuario Usu on (Usu.IdUsuario=DocOco.IdUsuario)    ");
            strsql.Append(" Left Join DtRomaneio DTR on (DTR.IdRomaneio=DocOco.IdRomaneio)   ");
            strsql.Append(" Left Join DT DT on (DT.IdDt=DTR.IdDt)    ");
            strsql.Append(" Left Join DocumentoOcorrenciaArquivo DocArq on (DocArq.IdDocumentoOcorrencia = DocOco.IdDocumentoOcorrencia)    ");
            strsql.Append(" where IdDocumento = @IdDocumento   ");
            strsql.Append(" Order By DocOco.DataOcorrencia Desc ");
            strsql.Replace("@IdDocumento", DocId.ToString());

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static SistranMODEL.NotasFiscais RetornaNotaFiscalSaida(int NotaFiscalId, string Conn)
        {
            SistranMODEL.NotasFiscais NotasFiscais = new SistranMODEL.NotasFiscais();
            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT DISTINCT DOC.IDDOCUMENTO, DOC.NUMERO, CADDES.CNPJCPF AS CNPJCPFDESTINATARIO, CADDES.RAZAOSOCIALNOME AS RAZAONOMEDESTINATARIO, ");
            strsql.Append(" CADDES.FANTASIAAPELIDO AS FANTASIAAPELIDODESTINATARIO,  ");
            strsql.Append(" CADREM.CNPJCPF AS CNPJCPFREMETENTE,     ");
            strsql.Append(" CADREM.RAZAOSOCIALNOME AS RAZAONOMEREMETENTE,     ");
            strsql.Append(" DOC.ORIGEM,    (CONVERT(VARCHAR(10), isnull(DOC.DATADEEMISSAO, '01/01/1900'), 103))AS DATADEEMISSAO,     ");
            strsql.Append(" (CONVERT(VARCHAR(10), isnull(DOC.DATADEENTRADA, '01/01/1900'), 103))AS DATADEENTRADA,     ");
            strsql.Append(" (CONVERT(VARCHAR(10), isnull(DOC.DATADESAIDA, '01/01/1900'), 103)    )AS DATADESAIDA,  (  CONVERT(VARCHAR(10), isnull(DOC.DATADECONCLUSAO, '01/01/1900'), 103) )AS DATADECONCLUSAO,     ");
            strsql.Append(" DOCFIL.SITUACAO,    OCO.NOME AS OCORRENCIA,      isnull(DOC.CODIGODORECEXP, 0) AS CODIGODORECEXP,     CIDREM.NOME AS CIDADEREMETENTE,     ");
            strsql.Append(" ESTREM.UF AS UFREMETENTE,     CIDDES.NOME AS CIDADEDESTINATARIO,     ESTDES.UF AS UFDESTINATARIO,     isnull(CADDES.Endereco, '') AS ENDERECODESTINATARIO,     ");
            strsql.Append(" isnull(CADDES.Numero,'') AS NUMERODESTINATARIO,    isnull(CADDES.Complemento, '') AS COMPLEMENTODESTINATARIO,  isnull(DOC.Ativo, '') AS ATIVO ,   ");
            strsql.Append(" ValorDaNota,ValorDeDesconto,BaseDoIPI,BaseDoICMS,ValorDoICMS,PesoLiquido,PesoBruto,ValorDeDesconto");
            strsql.Append(" FROM DOCUMENTO DOC   WITH (NOLOCK)   ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO  ON(DOCOCO.IDDOCUMENTO = DOC.IDDOCUMENTO)   ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO  ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)   ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DOCFIL  ON(DOCFIL.IDDOCUMENTO = DOC.IDDOCUMENTO) ");
            strsql.Append(" INNER JOIN CADASTRO CADDES  ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)   ");
            strsql.Append(" INNER JOIN CIDADE CIDDES  ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)   ");
            strsql.Append(" INNER JOIN ESTADO ESTDES  ON(ESTDES.IDESTADO = CIDDES.IDESTADO)   ");
            strsql.Append(" INNER JOIN CADASTRO CADREM  ON(CADREM.IDCADASTRO = DOC.IDREMETENTE)   ");
            strsql.Append(" INNER JOIN CIDADE CIDREM  ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)   ");
            strsql.Append(" INNER JOIN ESTADO ESTREM  ON(ESTREM.IDESTADO = CIDREM.IDESTADO)   ");
            strsql.Append(" WHERE   ");
            strsql.Append(" DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA') ");

            strsql.Append(" AND DOC.IDDOCUMENTO = @IDDOCUMENTO   AND DOC.ATIVO='SIM' ");

            strsql.Replace("@IDDOCUMENTO", NotaFiscalId.ToString());
            // Database SistranDb = DatabaseFactory.CreateDatabase(Conn);

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            IDbConnection cn = factory.CreateConnection();
            IDbCommand cd = factory.CreateCommand();


            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.Connection = cn;
            cd.CommandText = strsql.ToString();


            cn.Open();
            IDataReader drNotasSaida = cd.ExecuteReader();
            if (drNotasSaida.Read())
            {
                NotasFiscais = new SistranMODEL.NotasFiscais(Convert.ToInt32(drNotasSaida["IDDOCUMENTO"]),
                Convert.ToInt32(drNotasSaida["NUMERO"]),
                drNotasSaida["CNPJCPFDESTINATARIO"].ToString(),
                drNotasSaida["RAZAONOMEDESTINATARIO"].ToString(),
                drNotasSaida["FANTASIAAPELIDODESTINATARIO"].ToString(),
                drNotasSaida["CNPJCPFREMETENTE"].ToString(),
                drNotasSaida["RAZAONOMEREMETENTE"].ToString(),
                drNotasSaida["ORIGEM"].ToString(),
                Convert.ToDateTime(drNotasSaida["DATADEEMISSAO"]),
                Convert.ToDateTime(drNotasSaida["DATADEENTRADA"]),
                Convert.ToDateTime(drNotasSaida["DATADESAIDA"]),
                Convert.ToDateTime(drNotasSaida["DATADECONCLUSAO"]),
                drNotasSaida["SITUACAO"].ToString(),
                drNotasSaida["OCORRENCIA"].ToString(),
                Convert.ToInt32(drNotasSaida["CODIGODORECEXP"]),
                drNotasSaida["CIDADEREMETENTE"].ToString(),
                drNotasSaida["UFREMETENTE"].ToString(),
                drNotasSaida["CIDADEDESTINATARIO"].ToString(),
                drNotasSaida["UFDESTINATARIO"].ToString(),
                drNotasSaida["ENDERECODESTINATARIO"].ToString(),
                drNotasSaida["NUMERODESTINATARIO"].ToString(),
                drNotasSaida["COMPLEMENTODESTINATARIO"].ToString(),
                drNotasSaida["ATIVO"].ToString(),
                drNotasSaida["VALORDANOTA"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDANOTA"]),
                drNotasSaida["VALORDEDESCONTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDEDESCONTO"]),
                drNotasSaida["BASEDOIPI"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["BASEDOIPI"]),
                drNotasSaida["BASEDOICMS"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["BASEDOICMS"]),
                drNotasSaida["VALORDOICMS"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDOICMS"]),
                drNotasSaida["PESOLIQUIDO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["PESOLIQUIDO"]),
                drNotasSaida["PESOBRUTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["PESOBRUTO"]),
                drNotasSaida["VALORDEDESCONTO"] == DBNull.Value ? 0 : Convert.ToDecimal(drNotasSaida["VALORDEDESCONTO"]));
            }
            drNotasSaida.Close();
            cn.Close();
            return NotasFiscais;
        }

        public static DataTable ListarDesempenhoEntregaCidade(DateTime Ini, DateTime Fim, string clientes, string ordem, string ordem2, string Conn, bool incluirTransportadora)
        {

            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT    ");
            strsql.Append(" DESTCID.NOME AS DESTINATARIOCIDADE,    ");
            strsql.Append(" DESTEST.UF DESTINATARIOUF,  ");
            strsql.Append(" FL.NOME FILIAL,   ");

            if (incluirTransportadora)
            {
                strsql.Append("nf.IdTransportadora,    ");
                strsql.Append("catrans.FantasiaApelido, ");
            }
            strsql.Append(" ISNULL(AVG(CASE WHEN NF.DATADECONCLUSAO IS NOT NULL THEN ISNULL(NF.PRAZOUTILIZADO,0) - ISNULL(NF.DIASOCORRENCIACLIENTE,0) END),0.00)  AS MEDIA , ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) NOTASFISCAIS_ENTREGUE,   ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)   THEN 1 ELSE 0 END) NOTASFISCAIS_NAO_ENTREGUE  ");
            strsql.Append(" FROM DOCUMENTO NF    WITH (NOLOCK)");
            strsql.Append(" left JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)    ");
            strsql.Append(" left JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)     ");
            strsql.Append(" LEFT JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)    ");
            strsql.Append(" LEFT JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)    ");
            strsql.Append(" left JOIN FILIAL FL ON(FL.IDFILIAL = NF.IDFILIALATUAL) ");

            if (incluirTransportadora)
                strsql.Append("left join Transportadora trans on trans.IDTransportadora = nf.IdTransportadora left join Cadastro catrans on catrans.IDCadastro = trans.IDTransportadora");


            strsql.Append(" WHERE NF.ATIVO='SIM'  AND NF.SERIE<>'UNI' AND NF.SERIE<>'DEV' ");
            strsql.Append(" AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + Ini + "', 103)  ");
            strsql.Append(" AND CONVERT(DATETIME, '" + Fim + "', 103)   ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  AND NF.ATIVO='SIM' ");

            if (clientes != "" && clientes != "0")
            {
                // strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ")  OR NF.IDCLIENTE IN (" + clientes + ") )   ");
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }
            
            strsql.Append(" GROUP BY  DESTCID.NOME , DESTEST.UF, FL.NOME ");

            if (incluirTransportadora)
                strsql.Append(", nf.IdTransportadora, catrans.FantasiaApelido ");

            if (ordem.ToUpper().Contains("TRANSIT"))
            {
                strsql.Append(" ORDER BY  ISNULL(AVG(CASE WHEN NF.DATADECONCLUSAO IS NOT NULL THEN ISNULL(NF.PRAZOUTILIZADO,0) - ISNULL(NF.DIASOCORRENCIACLIENTE,0) END),0.00)  ");
            }

            if (ordem.ToUpper().Contains("FILIAL"))
            {
                strsql.Append(" ORDER BY   FL.NOME  ");
            }

            if (ordem.ToUpper().Contains("CIDADE"))
            {
                strsql.Append(" ORDER BY   DESTCID.NOME  ");
            }

            if (ordem.ToUpper().Contains("N.F ENTREGUES"))
            {
                strsql.Append(" ORDER BY  6  ");
            }

            if (ordem.ToUpper().Contains("N.F. NÃO ENTREGUES"))
            {
                strsql.Append(" ORDER BY  7 ");
            }

            strsql.Append(" " + ordem2);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarDesempenhoEntregaDiaNaoEntregue(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            StringBuilder strsql = new StringBuilder();

            //Fim = Fim.AddMinutes(59);
            //Fim = Fim.AddSeconds(59);
            //Fim = Fim.AddHours(59);

            strsql.Append(" SELECT DF.SITUACAO, ");
            strsql.Append(" COUNT(*) NOTAS  FROM DOCUMENTO NF  WITH (NOLOCK)");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DF ON DF.IDDOCUMENTO = NF.IDDOCUMENTO ");
            strsql.Append(" WHERE 0=0  ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");

            strsql.Append(" AND NF.ATIVO='SIM' AND NF.SERIE <>'UNI'  AND NF.SERIE<>'DEV'  AND NF.ATIVO='SIM'  ");
            strsql.Append(" AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + Ini + "', 103)   ");
            strsql.Append(" AND CONVERT(DATETIME, '" + Fim + "', 103)   ");

            if (clientes.Length > 1)
            {
                //strsql.Append(" AND  ( NF.IDREMETENTE IN (" + clientes + ")  OR NF.IDCLIENTE IN (" + clientes + "))   ");
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }

            strsql.Append(" AND NF.DATADECONCLUSAO IS NULL ");
            strsql.Append(" GROUP BY DF.SITUACAO ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarDesempenhoEntregaDia(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
          
            strsql.Append(" SELECT  ");
            strsql.Append(" ISNULL(NF.PRAZOUTILIZADO,1) PRAZOUTILIZADO, ");
            //strsql.Append(" isnull(AVG(CASE WHEN NF.DATADECONCLUSAO IS NOT NULL THEN ISNULL(NF.PRAZOUTILIZADO,0) - ISNULL(NF.DIASOCORRENCIACLIENTE,0) END),0.00)  PRAZOUTILIZADO, ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) NOTASFISCAISENTREGUE,   ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) NOTASNAOFISCAISENTREGUE,   ");
            strsql.Append(" 0.00 'PERC',");
            strsql.Append(" 0.00 'ACUMUL'  ");
            strsql.Append(" FROM DOCUMENTO NF  WITH (NOLOCK) INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)  ");
            strsql.Append(" WHERE  0=0 ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");

            strsql.Append(" AND NF.ATIVO='SIM'");
            strsql.Append(" AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + Ini + "', 103) ");
            strsql.Append(" AND CONVERT(DATETIME, '" + Fim + "', 103) ");
            strsql.Append(" AND NF.SERIE<>'UNI' AND NF.SERIE<>'DEV'  AND NF.ATIVO='SIM' ");

            if (clientes != "" && clientes != "0")
            {
                //strsql.Append(" AND ( NF.IDREMETENTE IN (" + clientes + ") ");
                //strsql.Append(" OR NF.IDCLIENTE IN (" + clientes + ")  ) ");
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }
            strsql.Append(" GROUP BY ISNULL(NF.PRAZOUTILIZADO,1) ");
            //strsql.Append(" ORDER BY NF.PRAZOUTILIZADO");
            strsql.Append(" ORDER BY ISNULL(NF.PRAZOUTILIZADO,1) ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarDesempenhoEntregaFilial(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT ");
            strsql.Append(" FL.IDFILIAL,");
            strsql.Append(" FL.NOME NOMEDAFILIAL, ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) NOTASFISCAIS_ENTREGUE , ");
            strsql.Append(" ISNULL(AVG(CASE WHEN NF.DATADECONCLUSAO IS NOT NULL THEN ISNULL(CAST(DATEDIFF(DAY,  NF.DATADEEMISSAO, NF.DATADECONCLUSAO)AS DECIMAL),0.00) END ),0) LEADTIME, ");
            strsql.Append(" ISNULL(AVG(CASE WHEN NF.DATADECONCLUSAO IS NOT NULL THEN ISNULL(NF.PRAZOUTILIZADO,0) - ISNULL(NF.DIASOCORRENCIACLIENTE,0) END),0) TRANSITTIME, ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) NOTASFISCAIS_NAO_ENTREGUE, ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL) AND (DATEDIFF(D, NF.DATAPLANEJADA,GETDATE())>1)  THEN 1 ELSE 0 END )NOTASFISCAIS_NAO_ENTREGUE_ATRASO, ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL) AND (DATEDIFF(D, NF.DATAPLANEJADA,GETDATE())<=1)  THEN 1 ELSE 0 END) NOTASFISCAIS_NAO_ENTREGUE_NO_PRAZO,  ");
            strsql.Append(" ABS(AVG(CAST((CASE WHEN (NF.DATADECONCLUSAO IS NULL) THEN  ");
            strsql.Append(" ISNULL(DATEDIFF(D,  NF.DATAPLANEJADA, GETDATE()),0) ELSE   ISNULL(DATEDIFF(D,  NF.DATAPLANEJADA, NF.DATADECONCLUSAO),0)   END) AS NUMERIC(10,2))))  MEDIA_DOS_DIAS_NAO_ENTREGUE, ");
            strsql.Append(" ABS(AVG(CAST((CASE WHEN (NF.DATADECONCLUSAO IS NULL) AND DATEDIFF(D,  NF.DATAPLANEJADA, GETDATE())>1 THEN   ISNULL(DATEDIFF(D,  NF.DATAPLANEJADA, GETDATE()),0) ELSE   ISNULL(DATEDIFF(D,  NF.DATAPLANEJADA, NF.DATADECONCLUSAO),0)   END) AS NUMERIC(10,2)))) MEDIA_DOS_DIAS_NAO_ENTREGUE_ATRASO,  ");
            strsql.Append(" ABS(AVG(CAST((CASE WHEN (NF.DATADECONCLUSAO IS NULL) AND DATEDIFF(D,  NF.DATAPLANEJADA, GETDATE())<=1 THEN  ISNULL(DATEDIFF(D,  GETDATE(), NF.DATAPLANEJADA),0) ELSE 0  /*ISNULL(DATEDIFF(D,  NF.DATAPLANEJADA, NF.DATADECONCLUSAO),0)*/   END) AS NUMERIC(10,2)))) MEDIA_DOS_DIAS_NAO_ENTREGUE_NO_PRAZO ");
            strsql.Append(" FROM DOCUMENTO NF  WITH (NOLOCK) ");
            strsql.Append(" left JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE) ");
            strsql.Append(" left JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO) ");
            strsql.Append(" LEFT JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE) ");
            strsql.Append(" LEFT JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO) ");
            strsql.Append(" LEFT JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIALAtual) ");
            strsql.Append(" WHERE  NF.ATIVO='SIM' AND  NF.SERIE <>'UNI' AND SERIE<>'DEV'  ");
            strsql.Append(" AND NF.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim + "', 103) ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO  IN('TRANSPORTE', 'COLETA')  ");


            if (clientes != "" && clientes != "0")
            {
                //strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ")   OR NF.IDCLIENTE IN (" + clientes + ") ) ");
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }
            
            strsql.Append(" GROUP BY FL.IDFILIAL   , FL.NOME    ");
            strsql.Append(" ORDER BY AVG(NF.PRAZOUTILIZADO) ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }


        public static DataTable ListarResumoPorFilial(DateTime Ini, DateTime Fim, string clientes, string Conn, Enuns.tipoReportResumoFilial tipo, bool OutrasSeries, bool Dev, bool Ret)
        {
            StringBuilder strsql = new StringBuilder();

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.filial)
                strsql.Append(" SELECT FL.NOME NOMEDAFILIAL, ");

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.cidadeDestinatario)
                strsql.Append(" SELECT ISNULL(DESTCID.NOME , 'N/A') NOMEDAFILIAL, ");

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.destinatario)
                strsql.Append(" SELECT replace( ISNULL(ISNULL(DEST.FANTASIAAPELIDO, DEST.RAZAOSOCIALNOME),'N/A'),'''','') NOMEDAFILIAL, ");

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.estado)
                strsql.Append(" SELECT ISNULL(DESTEST.NOME , 'N/A') NOMEDAFILIAL, ");


            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) NOTASFISCAIS_ENTREGUE, ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) NOTASFISCAISNAOENTREGUE,   ");
            strsql.Append(" ISNULL(NF.PRAZOUTILIZADO,1) PRAZOUTILIZADO,    ");
            strsql.Append(" isnull(AVG(isnull(NF.PRAZOUTILIZADO,0)),0) AS MEDIA, ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) + SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) TOTALDENOTAS  ");
            strsql.Append(" FROM DOCUMENTO NF     WITH (NOLOCK) ");
            strsql.Append(" INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)    ");
            strsql.Append(" LEFT JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)    ");
            strsql.Append(" LEFT JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)    ");
            strsql.Append(" LEFT JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)    ");
            strsql.Append(" INNER JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIALATUAL)  AND (FL.ATIVO ='SIM')    ");
            strsql.Append(" WHERE   NF.SERIE <>'UNI' AND SERIE<>'DEV' ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')   AND NF.ATIVO='SIM' ");
            strsql.Append(" AND NF.ATIVO='SIM'   ");
            strsql.Append(" AND NF.DATADEENTRADA BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim + "', 103)  ");



            if (clientes != "")
            {
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if(HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if(HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }

            //if (series != "")
            //    strsql.Append("AND  NF.SERIE IN (" + series + ")");



            if (OutrasSeries && Dev && Ret)
            {
                strsql.Append("");
            }
            else
            {
                if (OutrasSeries && Dev == false && Ret == false)
                {
                    strsql.Append(" AND NF.SERIE NOT IN('RET', 'DEV') ");
                }
                else
                {
                    if (OutrasSeries && (Dev == false || Ret == false))
                    {
                        if (Dev)
                            strsql.Append(" AND NF.SERIE <> 'RET' ");

                        if (Ret)
                            strsql.Append(" AND NF.SERIE <> 'DEV' ");

                    }
                    else
                    {
                        string sel = "";
                        if (Dev && Ret)
                            sel += "'DEV', 'RET'";
                        else
                        {
                            if (Dev)
                                sel += "'DEV'";

                            if (Ret)
                                sel += "'RET'";
                        }

                        strsql.Append(" AND NF.SERIE  IN(" + sel + ") ");
                    }
                }
            }


            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.filial)
            {
                strsql.Append(" GROUP BY FL.NOME, ISNULL( NF.PRAZOUTILIZADO,1)    ");
                strsql.Append(" ORDER BY FL.NOME , ISNULL( NF.PRAZOUTILIZADO,1)    ");
            }

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.cidadeDestinatario)
            {
                strsql.Append(" GROUP BY ISNULL(DESTCID.NOME , 'N/A') , ISNULL( NF.PRAZOUTILIZADO,1)    ");
                strsql.Append(" ORDER BY ISNULL(DESTCID.NOME , 'N/A') , ISNULL( NF.PRAZOUTILIZADO,1)    ");
            }

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.destinatario)
            {
                strsql.Append(" GROUP BY replace( ISNULL(ISNULL(DEST.FANTASIAAPELIDO, DEST.RAZAOSOCIALNOME),'N/A'),'''',''), ISNULL( NF.PRAZOUTILIZADO,1)    ");
                strsql.Append(" ORDER BY replace( ISNULL(ISNULL(DEST.FANTASIAAPELIDO, DEST.RAZAOSOCIALNOME),'N/A'),'''','') , ISNULL( NF.PRAZOUTILIZADO,1)    ");
            }

            if (tipo == Sistran.Library.Enuns.tipoReportResumoFilial.estado)
            {
                strsql.Append(" GROUP BY DESTEST.NOME, ISNULL( NF.PRAZOUTILIZADO,1)    ");
                strsql.Append(" ORDER BY DESTEST.NOME , ISNULL( NF.PRAZOUTILIZADO,1)    ");
            }

            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql.ToString(), Conn);
        }


        public static DataTable ListarOcorrenciasPorNota(DateTime Ini, DateTime Fim, string clientes, string Conn, Enuns.tipoReportResumoFilial tipo, string series)
        {
            string strsql = " "; 
                strsql += " SELECT    "; 
                strsql += " DOCOCO.IDDOCUMENTO, "; 
                strsql += " DOCOCO.DATAOCORRENCIA,  "; 
                strsql += " OCO.NOME ";
                strsql += " FROM DOCUMENTO NF     WITH (NOLOCK)  "; 
                strsql += " INNER JOIN DOCUMENTOOCORRENCIA DOCOCO ON DOCOCO.IDDOCUMENTO = NF.IDDOCUMENTO "; 
                strsql += " INNER JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DOCOCO.IDOCORRENCIA "; 
                strsql += " WHERE    ";
                strsql += " NF.SERIE <>'UNI'   AND SERIE<>'DEV' "; 
                strsql += " AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')   ";
                strsql += " AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')    "; 
                strsql += " AND NF.ATIVO='SIM' ";
                strsql += "  AND NF.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim + "', 103)  ";

                if (clientes != "")
                {
                    //strsql += "  AND (NF.IDREMETENTE IN (" + clientes + ")  OR NF.IDCLIENTE IN (" + clientes + "))  ";
                    if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                        strsql += " AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ";
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                        strsql += " AND NF.IDREMETENTE IN (" + clientes + ")";
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                        strsql += " AND NF.IDCLINTE IN (" + clientes + ")";
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                        strsql += " AND NF.IDDESTINATARIO IN (" + clientes + ")";
                }

                if (series != "")
                    strsql += " AND  NF.SERIE IN (" + series + ")";
                
                strsql += " ORDER BY  NF.NUMERO ";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

        }

        public static DataTable ListarDadosResumoPorFilial(DateTime Ini, DateTime Fim, string clientes, string Conn, Enuns.tipoReportResumoFilial tipo,  bool OutrasSeries, bool Ret, bool Dev)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  NF.IDDOCUMENTO,");
            strsql.Append(" NF.NUMERO,    ");
            strsql.Append(" NF.SERIE,   ");
            strsql.Append(" FL.NOME FILIAL, ");
            strsql.Append(" REME.CNPJCPF [CNPJ REMETENTE],   ");
            strsql.Append(" ISNULL(REME.FANTASIAAPELIDO,  REME.RAZAOSOCIALNOME) REMETENTE,   ");
            strsql.Append(" DEST.CNPJCPF [CNPJ DESTINATARIO],   ");
            strsql.Append(" DEST.RAZAOSOCIALNOME DESTINATARIO,    DESTCID.NOME + '/' +DESTEST.UF [CIDADE / UF] ,");
            strsql.Append(" CAST(DAY(NF.DATADEENTRADA) AS VARCHAR(2)) + '/'+  CAST(MONTH(NF.DATADEENTRADA) AS VARCHAR(2)) + '/'+  CAST(YEAR(NF.DATADEENTRADA) AS VARCHAR(4))  DATADEENTRADA,    ");
            strsql.Append(" PESOBRUTO, PESOLIQUIDO, ");

            strsql.Append("SUBSTRING(CONVERT(VARCHAR,DATADEENTRADA,20),12,8)  HORADEENTRADA,    ");

            strsql.Append(" CAST(DAY(NF.DATADEEMISSAO) AS VARCHAR(2)) + '/'+  CAST(MONTH(NF.DATADEEMISSAO) AS VARCHAR(2)) + '/'+  CAST(YEAR(NF.DATADEEMISSAO) AS VARCHAR(4))  DATADEEMISSAO,    ");

            strsql.Append("SUBSTRING(CONVERT(VARCHAR,DATADEEMISSAO,20),12,8)  HORADEEMISSAO,    ");

            strsql.Append(" CAST(DAY(NF.DATADECONCLUSAO) AS VARCHAR(2)) + '/'+  CAST(MONTH(NF.DATADECONCLUSAO) AS VARCHAR(2)) + '/'+  CAST(YEAR(NF.DATADECONCLUSAO) AS VARCHAR(4))  DATADECONCLUSAO,    ");

            strsql.Append(" ( ");
            strsql.Append(" SELECT TOP 1 CAST(DAY(DATAOCORRENCIA ) AS VARCHAR(2)) + '/'+  CAST(MONTH(DATAOCORRENCIA ) AS VARCHAR(2)) + '/'+  CAST(YEAR(DATAOCORRENCIA ) AS VARCHAR(4)) FROM DOCUMENTOOCORRENCIA DOCOCO WHERE DOCOCO.IDDOCUMENTO = NF.IDDOCUMENTO ORDER BY IDDOCUMENTOOCORRENCIA DESC	 ");

            strsql.Append(" ) [DATA STATUS], ");

            strsql.Append(" ( ");
            strsql.Append(" SELECT TOP 1 OCO.NOME FROM DOCUMENTOOCORRENCIA DOCOCO  ");
            strsql.Append(" INNER JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DOCOCO.IDOCORRENCIA ");
            strsql.Append(" WHERE DOCOCO.IDDOCUMENTO = NF.IDDOCUMENTO ORDER BY IDDOCUMENTOOCORRENCIA	DESC ");
            strsql.Append(" ) [STATUS], ");
            strsql.Append(" (SELECT SITUACAO FROM DOCUMENTOFILIAL DF WHERE DF.IDDOCUMENTO = NF.IDDOCUMENTO) [STATUS DA FILIAL], ");

            strsql.Append(" NF.PrazoUtilizadoCorrido [DIAS CORRIDOS DE ENTREGA], ");
            strsql.Append(" NF.QuantidadeSabDom [DIAS FINAL DE SEMANA], ");
            strsql.Append(" NF.QuantidadeFeriados [DIAS FERIADOS], ");
            strsql.Append(" ISNULL(NF.QuantidadeSabDom,0) + ISNULL(NF.QuantidadeFeriados,0) [TOTAL DE DIAS NAO UTEIS], ");
            strsql.Append(" CAST(NF.PRAZOUTILIZADO  AS VARCHAR(20)) [DIAS DE ENTREGA],  ");
            strsql.Append(" '' HISTORICO");

            strsql.Append(" FROM DOCUMENTO NF    WITH (NOLOCK) ");
            strsql.Append(" INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)    ");
            strsql.Append(" LEFT JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)    ");
            strsql.Append(" LEFT JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)    ");
            strsql.Append(" LEFT JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)    ");
            strsql.Append(" INNER JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIALATUAL)  AND (FL.ATIVO ='SIM')    ");
            strsql.Append(" WHERE   NF.SERIE <>'UNI' AND SERIE<>'DEV'  AND NF.ATIVO='SIM' ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");
            strsql.Append(" AND NF.ATIVO='SIM'   ");
            strsql.Append(" AND NF.DATADEENTRADA BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim + "', 103)  ");

            if (clientes != "")
            {
                //strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ")  OR NF.IDCLIENTE IN (" + clientes + "))  ");
                if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                    strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                    strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                    strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
                else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                    strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");
            }

            //if (series != "")
            //    strsql.Append("AND  NF.SERIE IN (" + series + ")");

            if (OutrasSeries && Dev && Ret)
            {
                strsql.Append("");
            }
            else
            {
                if (OutrasSeries && Dev == false && Ret == false)
                {
                    strsql.Append(" and NF.SERIE NOT IN('RET', 'DEV') ");
                }
                else
                {
                    if (OutrasSeries && (Dev == false || Ret == false))
                    {
                        if (Dev)
                            strsql.Append(" and NF.SERIE <> 'RET' ");

                        if (Ret)
                            strsql.Append(" and NF.SERIE <> 'DEV' ");

                    }
                    else
                    {
                        string sel = "";
                        if (Dev && Ret)
                            sel += "'DEV', 'RET'";
                        else
                        {
                            if (Dev)
                                sel += "'DEV'";

                            if (Ret)
                                sel += "'RET'";
                        }

                        strsql.Append(" and NF.SERIE  IN(" + sel + ") ");
                    }
                }
            }



            strsql.Append(" ORDER BY FL.NOME, NF.NUMERO");
            DataTable dt= Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

            DataTable dtOco = ListarOcorrenciasPorNota(Ini, Fim, clientes, Conn, tipo, "");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow[] row = dtOco.Select("IDDOCUMENTO=" + dt.Rows[i]["IDDOCUMENTO"].ToString(), "");

                string ocorrs = "";
                for (int ioco = 0; ioco < row.Length; ioco++)
                {
                    ocorrs += DateTime.Parse(row[ioco]["DATAOCORRENCIA"].ToString()).Day + "/" + DateTime.Parse(row[ioco]["DATAOCORRENCIA"].ToString()).Month + "/" + DateTime.Parse(row[ioco]["DATAOCORRENCIA"].ToString()).Year + " : " + row[ioco]["NOME"].ToString() + "\r\n";
                }

                dt.Rows[i]["HISTORICO"] = ocorrs;                
            }

            //dt.Columns.RemoveAt(0);

            return dt;
        }


        public static DataTable ListarResumoPorFilialPrazoNovo(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT FL.NOME NOMEDAFILIAL,    ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) NOTASFISCAIS_ENTREGUE, ");
            strsql.Append(" SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) NOTASFISCAISNAOENTREGUE,   ");
            strsql.Append(" ISNULL(NF.PRAZOUTILIZADONOVO,0) PRAZOUTILIZADO,    ");
            strsql.Append(" CONVERT(DECIMAL(5,2) ,AVG(isnull(NF.PRAZOUTILIZADONOVO,0))) AS MEDIA,    ");
            strsql.Append(" SUM(CASE WHEN  (NF.DATADECONCLUSAO IS NOT NULL)  THEN 1 ELSE 0 END) + SUM(CASE WHEN (NF.DATADECONCLUSAO IS NULL)  THEN 1 ELSE 0 END) TOTALDENOTAS  ");
            strsql.Append(" FROM DOCUMENTO NF     WITH (NOLOCK) ");
            strsql.Append(" INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)    ");
            strsql.Append(" INNER JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)    ");
            strsql.Append(" INNER JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)    ");
            strsql.Append(" INNER JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)    ");
            strsql.Append(" INNER JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIAL)    ");
            strsql.Append(" WHERE   DOC.SERIE <> 'UNI' AND SERIE<>'DEV'  AND DOC.ATIVO='SIM'  ");
            strsql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");

            strsql.Append(" AND NF.ATIVO='SIM'   ");
            strsql.Append(" AND NF.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + Ini + "', 103) AND CONVERT(DATETIME, '" + Fim + "', 103)  ");
            //strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ")   OR NF.IDCLIENTE IN (" + clientes + ") )  ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (NF.IDREMETENTE IN (" + clientes + ") OR NF.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND NF.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND NF.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND NF.IDDESTINATARIO IN (" + clientes + ")");

            strsql.Append(" GROUP BY FL.NOME, ISNULL( NF.P RAZOUTILIZADONOVO,0)    ");
            strsql.Append(" ORDER BY FL.NOME , ISNULL( NF.PRAZOUTILIZADONOVO,0)    ");


            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarHomeAguardandoEmbarque(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  Convert(Varchar(10),DOC.DATADEENTRADA,103) DataDeEntrada, COUNT(*) Notas   ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)    ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = DOC.IDDOCUMENTO)     ");
            strsql.Append(" WHERE DOC.DATADECONCLUSAO IS NULL  AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV'  ");
            //strsql.Append(" WHERE (DOC.IDREMETENTE in(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + "))   ");
            strsql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL  AND NOT DOC.CODIGODORECEXP IS NULL  AND DOC.ATIVO='SIM'  ");
            //strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL   ");
            strsql.Append(" AND DF.SITUACAO='" + situacao + "' AND YEAR(DOC.DATADEENTRADA) > 2005  ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");


            if (ini != null && fim != null)
            {
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + ini + "', 103) AND CONVERT(DATETIME, '" + fim + "', 103)");
            }

            strsql.Append(" Group By Convert(Varchar(10),DOC.DATADEENTRADA,103)   ");            
            strsql.Append(" ORDER BY SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 7,4) + SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 4,2) + SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 1,2) asc");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

        }

        public static DataTable ListarHomeAguardandoEmbarqueFilial(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  FL.IDFILIAL,FL.NOME,   ");
            strsql.Append(" COUNT(*) Notas    FROM DOCUMENTO DOC WITH(NOLOCK)   ");
            strsql.Append(" left JOIN Filial FL  ON FL.IDFilial=DOC.IDFilial     ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = DOC.IDDOCUMENTO)       ");            
            strsql.Append(" WHERE DOC.DATADECONCLUSAO IS NULL ");
            strsql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL AND NOT DOC.CODIGODORECEXP IS NULL    AND DOC.ATIVO='SIM'    ");
            strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL AND DF.SITUACAO='AGUARDANDO EMBARQUE' AND YEAR(DOC.DATADEENTRADA) > 2005 AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV' ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");
            
            if (ini != null && fim != null)
            {
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + ini + "', 103) AND CONVERT(DATETIME, '" + fim + "', 103)");
            }

            strsql.Append(" GROUP BY  FL.IDFILIAL, FL.NOME ");
            strsql.Append(" ORDER BY FL.NOME ASC ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }


        public static DataTable ListarDataFilialSituacao(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim, bool OutrasSeries, bool Ret, bool Dev, bool Especial)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  DISTINCT  CAST(DATADEENTRADA AS DATE) DATA, FL.IDFILIAL,FL.NOME,   ");
            strsql.Append(" COUNT(DISTINCT DOC.IDDOCUMENTO) Notas , ");
            strsql.Append(" SUM(CASE DOCUMENTOESPECIAL WHEN 'SIM' THEN 1 ELSE 0 END) ESPECIAL");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)");
            strsql.Append(" left JOIN Filial FL  WITH(NOLOCK) ON FL.IDFilial=DOC.IDFilialAtual     ");
//            strsql.Append(" left JOIN Filial FL  WITH(NOLOCK) ON FL.IDFilial=DOC.IDFilial     ");
            strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DF WITH(NOLOCK) ON (DF.IDDOCUMENTO = DOC.IDDOCUMENTO)       ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DC WITH(NOLOCK) ON DC.IDDOCUMENTO = DOC.IDDOCUMENTO");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON OCO.IDOCORRENCIA = DC.IDOCORRENCIA");            
            strsql.Append(" WHERE 0=0  ");            
            strsql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL    AND DOC.ATIVO='SIM'    ");

            if (situacao == "EM DEVOLUCAO" || situacao == "EM ENTREGA" || situacao == "AGUARDANDO EMBARQUE" || situacao == "AGUARDANDO DEVOLUCAO" || situacao == "MERCADORIA EMBARCADA")
            {
                if (!situacao.Contains("DEVOLUCAO"))
                {
                    strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL ");
                    strsql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");

                }
                else
                {
                    strsql.Append(" AND DOC.DATADECONCLUSAO IS not  NULL ");

                }

                strsql.Append(" AND DF.SITUACAO='" + situacao + "' ");
            }
            else
            {
                //ocorrencias
                strsql.Append(" AND OCO.IDOCORRENCIA = " + situacao);
                strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT  NULL ");
                strsql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
            }

            strsql.Append(" AND YEAR(DOC.DATADEENTRADA) > 2013 AND  DOC.SERIE <> 'UNI' AND FL.ATIVO='SIM' ");

            if (Especial)
                strsql.Append(" AND DOCUMENTOESPECIAL='SIM' ");



            //if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            //else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
            //    strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            //else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
            //    strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            //else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
            //    strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");



            if (OutrasSeries && Dev && Ret)
            {
                strsql.Append("");
            }
            else
            {
                if (OutrasSeries && Dev == false && Ret == false)
                {
                    strsql.Append(" AND DOC.SERIE NOT IN('RET', 'DEV') ");
                }
                else
                {
                    if (OutrasSeries && (Dev == false || Ret == false))
                    {
                        if (Dev)
                            strsql.Append(" AND DOC.SERIE <> 'RET' ");

                        if (Ret)
                            strsql.Append(" AND DOC.SERIE <> 'DEV' ");

                    }
                    else
                    {
                        string sel = "";
                        if (Dev && Ret)
                            sel += "'DEV', 'RET'";
                        else
                        {
                            if (Dev)
                                sel += "'DEV'";

                            if (Ret)
                                sel += "'RET'";
                        }

                        strsql.Append(" AND DOC.SERIE  IN(" + sel + ") ");
                    }
                }
            }



       
            strsql.Append("group by CAST(DATADEENTRADA AS DATE) , FL.IDFILIAL, FL.NOME ");

            strsql.Append(" ORDER BY FL.NOME ASC ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable RetornarSituacoes(string clientes, string dataIni, string dataFim, string Conn)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT OCO.NOME, oco.IDOCORRENCIA ");
            str.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)   ");
            str.Append(" INNER JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)   ");
            str.Append(" INNER JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)   ");

            str.Append(" WHERE (DOC.IDREMETENTE IN(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") )  ");
            str.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')   ");
            str.Append(" AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  AND DOC.SERIE NOT IN('UNI', 'DEV')   AND DOC.ATIVO='SIM' ");
            str.Append(" AND OCO.CODIGO <> 1  ");
            str.Append(" AND OCO.IDOCORRENCIA IS NOT NULL AND YEAR(DOC.DATADEEMISSAO)>=2013");
            str.Append(" AND DOC.DATADECONCLUSAO IS NULL ORDER BY 1 ");

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(str.ToString(), Conn);


            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "NOME", "IDOCORRENCIA");

            return distinctValues;
        }

        public static DataTable ListarHomeNotasFiscaisEmbarcadas(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT Convert(Varchar(10),DOC.DATADEENTRADA,103) DataDeEntrada, Count(*) Notas  ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)   ");
            strsql.Append(" LEFT JOIN DocumentoFilial DF WITH (NOLOCK) ON (DF.IDDocumento = DOC.IDDocumento)    ");
            //strsql.Append(" WHERE (DOC.IDREMETENTE in(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") )  ");
            strsql.Append(" WHERE DOC.TIPODEDOCUMENTO = 'NOTA FISCAL'   ");
            strsql.Append("   AND DOC.DATADECONCLUSAO IS NULL   ");
            strsql.Append("   AND NOT DOC.DATADEENTRADA IS NULL   ");
            strsql.Append("   AND NOT DOC.CODIGODORECEXP IS NULL   ");
            //strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL  ");
            strsql.Append("   AND DF.SITUACAO='" + situacao + "' AND DOC.SERIE <> 'UNI' AND SERIE<>'DEV'  AND DOC.ATIVO='SIM'  ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");

            if (ini != null && fim != null)
            {
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + ini + "', 103) AND CONVERT(DATETIME, '" + fim + "', 103)");
            }

            strsql.Append(" GROUP BY Convert(Varchar(10),DOC.DATADEENTRADA,103)  ");
            //strsql.Append(" ORDER BY Convert(Varchar(10),DOC.DATADEENTRADA,103) desc");
            strsql.Append(" ORDER BY SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 7,4) + SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 4,2) + SUBSTRING(Convert(Varchar(10),DOC.DATADEENTRADA,103), 1,2) asc");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrencias(string clientes, string Conn, DateTime? INI, DateTime? FIM, bool datarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT OCO.CODIGO, LTRIM(RTRIM(OCO.NOME)) NOME , OCO.FINALIZADOR , COUNT(*) Notas      ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)       ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)       ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)       ");
            //strsql.Append(" WHERE (DOC.IDREMETENTE in(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") )    ");
            strsql.Append(" WHERE NOT DOC.DATADEENTRADA IS NULL     AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV' ");
            strsql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO  IN('TRANSPORTE', 'COLETA')  ");
            strsql.Append(" AND DOC.CODIGODORECEXP IS NOT NULL ");
            strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT NULL  ");
            // strsql.Append(" AND OCO.NOME IS NOT NULL AND OCO.CODIGO <>'01' ");
            strsql.Append(" AND OCO.NOME IS NOT NULL AND OCO.IDOCORRENCIA <>1  AND DOC.ATIVO='SIM' ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");

            if (datarPorOcorrencia)
            {
                if (INI != null && FIM != null)
                {
                    strsql.Append(" AND  DOCOCO.DataOcorrencia BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
                }
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == false)
            {

                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL      ");
                if (INI != null && FIM != null)
                {
                    strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
                }
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == true)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO=CONVERT(DATETIME, '" + INI + "', 103) AND DOCOCO.DATAOCORRENCIA=CONVERT(DATETIME, '" + FIM + "', 103)");
            }


            strsql.Append(" GROUP BY OCO.CODIGO, OCO.NOME, OCO.FINALIZADOR ");
            strsql.Append(" ORDER by COUNT(*) DESC  ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrenciasResponsabilidade(string clientes, string Conn, DateTime INI, DateTime FIM, bool datarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT OCO.CODIGO,  ");
            strsql.Append("   LTRIM(RTRIM(OCO.NOME)) NOME, OCO.FINALIZADOR,   ");
            strsql.Append(" COUNT(*) NOTAS,  ");
            strsql.Append(" OCO.RESPONSABILIDADE        ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)   ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)          ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)          ");
            //strsql.Append(" WHERE (DOC.IDREMETENTE IN(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") )       ");
            strsql.Append(" WHERE DOC.TIPODEDOCUMENTO = 'NOTA FISCAL'    ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL        ");
            strsql.Append(" AND DOC.CODIGODORECEXP IS NOT NULL    AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV'  AND DOC.ATIVO='SIM' ");

            strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT NULL    ");
            strsql.Append(" AND OCO.NOME IS NOT NULL   ");
            strsql.Append(" AND OCO.CODIGO <>'01'          ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");

            //if (DatarPorOcorrencia)
            //{
            //    strsql.Append(" AND  DOCOCO.DataOcorrencia BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");

            //}
            //else
            //{

            //    strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL         ");
            //    strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            //}

            if (datarPorOcorrencia)
            {
                //strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOCOCO.DATAOCORRENCIA BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == false)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == true)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO=CONVERT(DATETIME, '" + INI + "', 103) AND DOCOCO.DATAOCORRENCIA=CONVERT(DATETIME, '" + FIM + "', 103)");
            }



            strsql.Append(" GROUP BY OCO.CODIGO, OCO.NOME, OCO.FINALIZADOR, OCO.RESPONSABILIDADE  ");
            strsql.Append(" ORDER BY OCO.RESPONSABILIDADE, COUNT(*) DESC     ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrenciasFilial(string clientes, string Conn, DateTime INI, DateTime FIM, bool datarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT   ");
            strsql.Append(" FL.IDFILIAL,");
            strsql.Append(" FL.NOME, ");
            strsql.Append(" COUNT(*) NOTAS  ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)        ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)        ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)        ");
            strsql.Append(" INNER JOIN FILIAL FL ON FL.IDFILIAL=DOC.IDFILIAL");
            //strsql.Append(" WHERE  DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' AND (DOC.IDREMETENTE IN(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") )");
            strsql.Append(" WHERE  DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL  AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV' ");
            strsql.Append(" AND DOC.CODIGODORECEXP IS NOT NULL ");

            strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT NULL ");
            strsql.Append(" AND OCO.NOME IS NOT NULL ");
            strsql.Append(" AND OCO.CODIGO <>'01'  AND DOC.ATIVO='SIM' ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");

            //if (DatarPorOcorrencia)
            //{
            //    strsql.Append(" AND  DOCOCO.DataOcorrencia BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");

            //}
            //else
            //{
            //    strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
            //    strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            //}


            if (datarPorOcorrencia)
            {
                //strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOCOCO.DATAOCORRENCIA BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == false)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == true)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO=CONVERT(DATETIME, '" + INI + "', 103) AND DOCOCO.DATAOCORRENCIA=CONVERT(DATETIME, '" + FIM + "', 103)");
            }


            strsql.Append(" GROUP BY FL.IDFILIAL,  FL.NOME ");
            strsql.Append(" ORDER BY COUNT(*) DESC ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(string clientes, string Conn, DateTime INI, DateTime FIM, bool datarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT    FL.IDFILIAL,  ");
            strsql.Append("  OCO.RESPONSABILIDADE, ");
            strsql.Append(" FL.NOME,   ");
            strsql.Append(" COUNT(*) NOTAS    ");
            strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)          ");
            strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)          ");
            strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)          ");
            strsql.Append(" INNER JOIN FILIAL FL ON FL.IDFILIAL=DOC.IDFILIAL  ");
            strsql.Append(" WHERE DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' AND DOC.ATIVO = 'SIM'  ");
            //strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL   AND (DOC.IDREMETENTE IN(" + clientes + ") OR DOC.IDCLIENTE IN(" + clientes + ") ) ");
            strsql.Append(" AND NOT DOC.DATADEENTRADA IS NULL ");
            strsql.Append(" AND DOC.CODIGODORECEXP IS NOT NULL  AND  DOC.SERIE <> 'UNI' AND SERIE<>'DEV'   AND DOC.ATIVO='SIM' ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (DOC.IDREMETENTE IN (" + clientes + ") OR DOC.IDCLIENTE IN (" + clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND DOC.IDREMETENTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND DOC.IDCLINTE IN (" + clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND DOC.IDDESTINATARIO IN (" + clientes + ")");

            if (datarPorOcorrencia)
            {
                //strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOCOCO.DATAOCORRENCIA BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == false)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '" + INI + "', 103) AND CONVERT(DATETIME, '" + FIM + "', 103)");
            }
            else if (datarPorOcorrencia == false && dtEmissaoOcorrencia == true)
            {
                strsql.Append(" AND DOC.DATADECONCLUSAO IS NULL   ");
                strsql.Append(" AND  DOC.DATADEEMISSAO=CONVERT(DATETIME, '" + INI + "', 103) AND DOCOCO.DATAOCORRENCIA=CONVERT(DATETIME, '" + FIM + "', 103)");
            }


            strsql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT NULL   ");
            strsql.Append(" AND OCO.NOME IS NOT NULL  AND OCO.CODIGO <>'01' ");

            strsql.Append(" GROUP BY  ");
            strsql.Append(" OCO.RESPONSABILIDADE,  ");
            strsql.Append(" FL.IDFILIAL,   ");
            strsql.Append(" FL.NOME   ");
            strsql.Append(" ORDER BY OCO.RESPONSABILIDADE, FL.IDFILIAL");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable RetornarImagemByDocumentoOcorrencia(int IdDocumentoOcorrencia, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT ARQUIVO FROM DOCUMENTOOCORRENCIAARQUIVO WHERE IDDOCUMENTOOCORRENCIA=" + IdDocumentoOcorrencia.ToString());
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static DataTable RetornarImagemByDocumentoOcorrenciaMultiplas(string IdDocumentoOcorrencia, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT ARQUIVO FROM DOCUMENTOOCORRENCIAARQUIVO WHERE IDDOCUMENTOOCORRENCIA IN(" + IdDocumentoOcorrencia + ")");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public static int RetornarTotalNotasFiscaisEmitidas(string Clientes, string DataInicio, string DataFim)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT  ");
            strsql.Append("  COUNT(NF.iddocumento) FOTASFICAISEMITIDAS  ");
            strsql.Append("  FROM DOCUMENTO NF    WITH (NOLOCK) INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)       ");
            strsql.Append("  INNER JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)        ");
            strsql.Append("  INNER JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)       ");
            strsql.Append("  INNER JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)       ");
            strsql.Append("  LEFT JOIN FILIALCIDADESETOR  FCS ON (FCS.IDCIDADE= DEST.IDCIDADE)    ");
            strsql.Append("  LEFT JOIN FILIAL FL ON(FL.IDFILIAL = FCS.IDFILIAL)    ");
            strsql.Append("  LEFT JOIN SETOR ST ON(ST.IDSETOR=FCS.IDSETOR)    ");
            strsql.Append("  WHERE     ");
            strsql.Append("  NF.SERIE <> 'UNI' AND SERIE<>'DEV'   AND NF.ATIVO='SIM' ");
            strsql.Append(" AND NF.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  ");

            strsql.Append("  AND NF.ATIVO='SIM'     ");
            strsql.Append("  AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103)     ");
            strsql.Append("  AND CONVERT(DATETIME, '" + DataFim + "', 103)      ");
            // strsql.Append("  AND (NF.IDREMETENTE IN (" + Clientes + ")   OR NF.IDCLIENTE IN (" + Clientes + ") )      ");

            if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                strsql.Append(" AND (NF.IDREMETENTE IN (" + Clientes + ") OR NF.IDCLIENTE IN (" + Clientes + "))  ");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                strsql.Append(" AND NF.IDREMETENTE IN (" + Clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                strsql.Append(" AND NF.IDCLINTE IN (" + Clientes + ")");
            else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                strsql.Append(" AND NF.IDDESTINATARIO IN (" + Clientes + ")");

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");

        }

        public sealed class Ocorrencia
        {
            public static DataTable OcorrenciaAtualListar(int DocId, int IDDocumentoOcorrencia, string Conn)
            {

                System.Text.StringBuilder strsql = new StringBuilder();

                if (IDDocumentoOcorrencia > 0)
                {
                    strsql.Append(" SELECT  ");
                    strsql.Append(" 	FIL.NUMERODAFILIAL, FIL.NOME, ");
                    strsql.Append(" OCO.CODIGO,  OCO.NOME AS DESCRICAO,  ");
                    strsql.Append(" COALESCE(DOC.DATADECONCLUSAO,DOC.DATADAULTIMAOCORRENCIA) DATADECONCLUSAO, ");
                    //strsql.Append(" DOCOCO.SENHA  ");
                    strsql.Append(" '' SENHA  ");
                    strsql.Append(" FROM DOCUMENTO DOC  ");
                    strsql.Append(" INNER JOIN DOCUMENTOOCORRENCIA DOCOCO ON (DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)  ");
                    strsql.Append(" INNER JOIN OCORRENCIA OCO ON (OCO.IDOCORRENCIA = DOCOCO.IDOCORRENCIA)  ");
                    strsql.Append(" LEFT JOIN FILIAL FIL ON (FIL.IDFILIAL=DOC.IDFILIALATUAL)  ");
                    strsql.Append(" WHERE DOC.IDDOCUMENTO = @IDDOCUMENTO ");
                }
                else
                {
                    strsql.Append(" SELECT  ");
                    strsql.Append(" FIL.NUMERODAFILIAL, FIL.NOME,  ");
                    strsql.Append(" CAST('' AS CHAR(10)) CODIGO, CAST(DOCFIL.SITUACAO AS VARCHAR(60)) DESCRICAO, DOCFIL.DATA AS DATADECONCLUSAO, ");
                    strsql.Append(" CAST('' AS VARCHAR(50)) AS SENHA  ");
                    strsql.Append(" FROM DOCUMENTO DOC  ");
                    strsql.Append(" LEFT JOIN DOCUMENTOFILIAL DOCFIL ON (DOCFIL.IDDOCUMENTO=DOC.IDDOCUMENTO)  ");
                    strsql.Append(" LEFT JOIN FILIAL FIL ON (FIL.IDFILIAL=DOC.IDFILIALATUAL)  ");
                    strsql.Append(" WHERE DOC.IDDOCUMENTO = @IDDOCUMENTO /*AND DOC.IDFILIALATUAL=DOCFIL.IDFILIAL*/ ");
                }

                strsql.Replace("@IDDOCUMENTO", DocId.ToString());

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

            }

            public static DataTable OcorrenciaListar(string Conn, string IDCliente)
            {
                try
                {

                    System.Text.StringBuilder strsql = new StringBuilder();
                    strsql.Append(" SELECT DISTINCT  ltrim(rtrim(OCO.Codigo)) Codigo, ltrim(rtrim(OCO.Nome)) Nome ");
                    strsql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK)   ");
                    strsql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA)   ");
                    strsql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA)   ");
                    //strsql.Append(" WHERE (DOC.IDREMETENTE in( " + IDCliente + ") OR DOC.IDCLIENTE in (" + IDCliente + ") )");
                    strsql.Append(" WHERE DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' ");
                    strsql.Append("   AND NOT DOC.DATADEENTRADA IS NULL ");
                    strsql.Append("   AND DOC.CodigoDoRecExp is Not Null ");
                    strsql.Append("   AND DOC.IDDOCUMENTOOCORRENCIA IS not NULL   ");
                    strsql.Append("   AND OCO.NOME IS NOT NULL AND OCO.CODIGO <>'01'   ");

                    if (HttpContext.Current.Session["campos_reports"].ToString() == "")
                        strsql.Append(" AND (DOC.IDREMETENTE IN (" + IDCliente + ") OR DOC.IDCLIENTE IN (" + IDCliente + "))  ");
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDREMETENTE"))
                        strsql.Append(" AND DOC.IDREMETENTE IN (" + IDCliente + ")");
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDCLIENTE"))
                        strsql.Append(" AND DOC.IDCLINTE IN (" + IDCliente + ")");
                    else if (HttpContext.Current.Session["campos_reports"].ToString().Contains("IDDESTINATARIO"))
                        strsql.Append(" AND DOC.IDDESTINATARIO IN (" + IDCliente + ")");

                    return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

                }
                catch (Exception)
                {
                    throw new Exception("Não Existem Ocorrências.");
                }
            }
        }
    }
}
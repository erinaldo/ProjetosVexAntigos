using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SistranDAO
{
    class NF
    {
        public DataSet ExcSQL(string Conn, string ssql)
        {
           return Sistran.Library.GetDataTables.RetornarDataSet(ssql, Conn);           
        }
       
        public DataSet ListarNF_Entregas(
                                            string notafiscalid,
                                            string serie,
                                            DateTime? DataEmissaoI,
                                            DateTime? DataEmissaoF,
                                            DateTime? DataEntradaI,
                                            DateTime? DataEntradaF,
                                            DateTime? PrevisaoSaidaI,
                                            DateTime? PrevisaoSaidaF,
                                            DateTime? DataSaidaI,
                                            DateTime? DataSaidaF,
                                            DateTime? DataConclusaoI,
                                            DateTime? DataConclusaoF,
                                            DateTime? DataPlanejadaI,
                                            DateTime? DataPlanejadaF,
                                            string CnpjRem,
                                            string FantasiaRem,
                                            string RazaoRem,
                                            string CnpjDest,
                                            string FantasiaDest,
                                            string RazaoDest,
                                            int IdCliente,
                                            int? IdOcorrencia,
                                            string Conn)
{
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            DbCommand cmdCliente = SistranDb.GetStoredProcCommand("NotaFiscalEntregaConsultar");
            cmdCliente.CommandType = CommandType.StoredProcedure;
            
            SistranDb.AddInParameter(cmdCliente, "NOTAFISCALID", DbType.String, notafiscalid);
            SistranDb.AddInParameter(cmdCliente, "SERIE", DbType.String, serie);
            SistranDb.AddInParameter(cmdCliente, "DATAEMISSAOI", DbType.DateTime, DataEmissaoI);
            SistranDb.AddInParameter(cmdCliente, "DATAEMISSAOF", DbType.DateTime, DataEntradaF);
            SistranDb.AddInParameter(cmdCliente, "DATAENTRADAI", DbType.DateTime, DataEntradaI);
            SistranDb.AddInParameter(cmdCliente, "DATAENTRADAF", DbType.DateTime, DataEntradaF);
            SistranDb.AddInParameter(cmdCliente, "PREVISAOSAIDAI", DbType.DateTime, PrevisaoSaidaI);
            SistranDb.AddInParameter(cmdCliente, "PREVISAOSAIDAF", DbType.DateTime, PrevisaoSaidaF);
            SistranDb.AddInParameter(cmdCliente, "DATASAIDAI", DbType.DateTime, DataSaidaI);
            SistranDb.AddInParameter(cmdCliente, "DATASAIDAF", DbType.DateTime, DataSaidaF);
            SistranDb.AddInParameter(cmdCliente, "DATACONCLUSAOI", DbType.DateTime, DataConclusaoI);
            SistranDb.AddInParameter(cmdCliente, "DATACONCLUSAOF", DbType.DateTime, DataConclusaoF);
            SistranDb.AddInParameter(cmdCliente, "DATAPLANEJADAI", DbType.DateTime, DataPlanejadaI);
            SistranDb.AddInParameter(cmdCliente, "DATAPLANEJADAF", DbType.DateTime, DataPlanejadaF);
            SistranDb.AddInParameter(cmdCliente, "CNPJREM", DbType.String, CnpjRem);
            SistranDb.AddInParameter(cmdCliente, "FANTASIAREM", DbType.String, FantasiaRem);
            SistranDb.AddInParameter(cmdCliente, "RAZAOREM", DbType.String, RazaoRem);
            SistranDb.AddInParameter(cmdCliente, "CNPJDEST", DbType.String, CnpjDest);
            SistranDb.AddInParameter(cmdCliente, "FANTASIADEST", DbType.String, FantasiaDest);
            SistranDb.AddInParameter(cmdCliente, "RAZAODEST", DbType.String, RazaoDest);
            SistranDb.AddInParameter(cmdCliente, "IDOCORRENCIA", DbType.String, IdOcorrencia);
            SistranDb.AddInParameter(cmdCliente, "IDCLIENTE", DbType.String, IdCliente);       
            DataSet ds = SistranDb.ExecuteDataSet(cmdCliente);
            return ds;
        }

        public DataSet ListarNF_Abertas(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime DataEmissaoI, DateTime DataEmissaoF, DateTime DataEntregaI, DateTime DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            DbCommand cmdCliente = SistranDb.GetStoredProcCommand("NotaFiscalAbertaConsultar");
            cmdCliente.CommandType = CommandType.StoredProcedure;

            SistranDb.AddInParameter(cmdCliente, "notafiscalid", DbType.String, notafiscalid);
            SistranDb.AddInParameter(cmdCliente, "Cnpj", DbType.String, Cnpj);
            SistranDb.AddInParameter(cmdCliente, "Razao", DbType.String, Razao);
            SistranDb.AddInParameter(cmdCliente, "Fantasia", DbType.String, Fantasia);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoI", DbType.DateTime, DataEmissaoI);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoF", DbType.DateTime, DataEmissaoF);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaI", DbType.DateTime, DataEntregaI);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaF", DbType.DateTime, DataEntregaF);
            SistranDb.AddInParameter(cmdCliente, "IDCliente", DbType.Int32, IdCliente);
            SistranDb.AddInParameter(cmdCliente, "IDocorrencia", DbType.Int32, IDOcorrencia);
            DataSet ds = SistranDb.ExecuteDataSet(cmdCliente);
            return ds;
        }

        public DataSet ListarNF_Entregues(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime DataEmissaoI, DateTime DataEmissaoF, DateTime DataEntregaI, DateTime DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            DbCommand cmdCliente = SistranDb.GetStoredProcCommand("NotaFiscalEntreguesConsultar");
            cmdCliente.CommandType = CommandType.StoredProcedure;

            SistranDb.AddInParameter(cmdCliente, "notafiscalid", DbType.String, notafiscalid);
            SistranDb.AddInParameter(cmdCliente, "Cnpj", DbType.String, Cnpj);
            SistranDb.AddInParameter(cmdCliente, "Razao", DbType.String, Razao);
            SistranDb.AddInParameter(cmdCliente, "Fantasia", DbType.String, Fantasia);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoI", DbType.DateTime, DataEmissaoI);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoF", DbType.DateTime, DataEmissaoF);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaI", DbType.DateTime, DataEntregaI);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaF", DbType.DateTime, DataEntregaF);
            SistranDb.AddInParameter(cmdCliente, "IDCliente", DbType.Int32, IdCliente);
            SistranDb.AddInParameter(cmdCliente, "IDocorrencia", DbType.Int32, IDOcorrencia);
            DataSet ds = SistranDb.ExecuteDataSet(cmdCliente);
            return ds;
        }
        
        public DataSet ListarNF_Ocorrencia(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime DataEmissaoI, DateTime DataEmissaoF, DateTime DataEntregaI, DateTime DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            DbCommand cmdCliente = SistranDb.GetStoredProcCommand("NotaFiscalOcorrenciaConsultar");
            cmdCliente.CommandType = CommandType.StoredProcedure;

            SistranDb.AddInParameter(cmdCliente, "notafiscalid", DbType.String, notafiscalid);
            SistranDb.AddInParameter(cmdCliente, "Cnpj", DbType.String, Cnpj);
            SistranDb.AddInParameter(cmdCliente, "Razao", DbType.String, Razao);
            SistranDb.AddInParameter(cmdCliente, "Fantasia", DbType.String, Fantasia);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoI", DbType.DateTime, DataEmissaoI);
            SistranDb.AddInParameter(cmdCliente, "DataEmissaoF", DbType.DateTime, DataEmissaoF);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaI", DbType.DateTime, DataEntregaI);
            SistranDb.AddInParameter(cmdCliente, "DataEntregaF", DbType.DateTime, DataEntregaF);
            SistranDb.AddInParameter(cmdCliente, "IDCliente", DbType.Int32, IdCliente);
            SistranDb.AddInParameter(cmdCliente, "IDocorrencia", DbType.Int32, IDOcorrencia);
            DataSet ds = SistranDb.ExecuteDataSet(cmdCliente);
            return ds;
        }
                
        public DataTable Consultar(string Conn, string idDocumento)
        {
            Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            DbCommand cmdCliente = SistranDb.GetStoredProcCommand("NotaFiscalConsultar");
            cmdCliente.CommandType = CommandType.StoredProcedure;
            SistranDb.AddInParameter(cmdCliente, "IDDOCUMENTO", DbType.String, idDocumento);
            DataSet ds = SistranDb.ExecuteDataSet(cmdCliente);
            return ds.Tables[0];
        }
    }
}

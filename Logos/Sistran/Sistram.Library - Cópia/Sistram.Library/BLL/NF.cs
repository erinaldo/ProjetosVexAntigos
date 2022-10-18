using System;
using System.Data;

namespace SistranBLL
{
    public class NF
    {

        public DataSet ExcSQL(string Conn, string ssql)
        {

            return new SistranDAO.NF().ExcSQL(Conn, ssql);
        }

        #region Methos Olds
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

            return new SistranDAO.NF().ListarNF_Entregas(notafiscalid,
                                           serie,
                                           DataEmissaoI,
                                           DataEmissaoF,
                                           DataEntradaI,
                                           DataEntradaF,
                                           PrevisaoSaidaI,
                                           PrevisaoSaidaF,
                                           DataSaidaI,
                                           DataSaidaF,
                                           DataConclusaoI,
                                           DataConclusaoF,
                                           DataPlanejadaI,
                                           DataPlanejadaF,
                                           CnpjRem,
                                           FantasiaRem,
                                           RazaoRem,
                                           CnpjDest,
                                           FantasiaDest,
                                           RazaoDest,
                                           IdCliente,
                                           IdOcorrencia,
                                           Conn);
        }

        public DataSet ListarNF_Abertas(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime? DataEmissaoI, DateTime? DataEmissaoF, DateTime? DataEntregaI, DateTime? DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            {
                if (DataEmissaoI == null)
                {
                    DataEmissaoI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEmissaoF == null)
                {
                    DataEmissaoF = Convert.ToDateTime("31/12/2050");
                }

                if (DataEntregaI == null)
                {
                    DataEntregaI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEntregaF == null)
                {
                    DataEntregaF = Convert.ToDateTime("31/12/2050");
                }


                return new SistranDAO.NF().ListarNF_Abertas(Conn, notafiscalid, Cnpj, Razao, Fantasia, Convert.ToDateTime(DataEmissaoI), Convert.ToDateTime(DataEmissaoF), Convert.ToDateTime(DataEntregaI), Convert.ToDateTime(DataEntregaF), IdCliente, IDOcorrencia);
            }
        }
        public DataSet ListarNF_Entregues(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime? DataEmissaoI, DateTime? DataEmissaoF, DateTime? DataEntregaI, DateTime? DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            {
                if (DataEmissaoI == null)
                {
                    DataEmissaoI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEmissaoF == null)
                {
                    DataEmissaoF = Convert.ToDateTime("31/12/2050");
                }

                if (DataEntregaI == null)
                {
                    DataEntregaI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEntregaF == null)
                {
                    DataEntregaF = Convert.ToDateTime("31/12/2050");
                }


                return new SistranDAO.NF().ListarNF_Entregues(Conn, notafiscalid, Cnpj, Razao, Fantasia, Convert.ToDateTime(DataEmissaoI), Convert.ToDateTime(DataEmissaoF), Convert.ToDateTime(DataEntregaI), Convert.ToDateTime(DataEntregaF), IdCliente, IDOcorrencia);
            }
        }

        
        public DataTable Consultar(string conn, string idDocumento)
        {
            return new SistranDAO.NF().Consultar(conn, idDocumento);
        }

        public DataSet ListarNF_Ocorrencia(string Conn, string notafiscalid, string Cnpj, string Razao, string Fantasia, DateTime? DataEmissaoI, DateTime? DataEmissaoF, DateTime? DataEntregaI, DateTime? DataEntregaF, int IdCliente, int IDOcorrencia)
        {
            {
                if (DataEmissaoI == null)
                {
                    DataEmissaoI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEmissaoF == null)
                {
                    DataEmissaoF = Convert.ToDateTime("31/12/2050");
                }

                if (DataEntregaI == null)
                {
                    DataEntregaI = Convert.ToDateTime("01/01/1980");
                }

                if (DataEntregaF == null)
                {
                    DataEntregaF = Convert.ToDateTime("31/12/2050");
                }


                return new SistranDAO.NF().ListarNF_Ocorrencia(Conn, notafiscalid, Cnpj, Razao, Fantasia, Convert.ToDateTime(DataEmissaoI), Convert.ToDateTime(DataEmissaoF), Convert.ToDateTime(DataEntregaI), Convert.ToDateTime(DataEntregaF), IdCliente, IDOcorrencia);
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Sistran.Library;

namespace SistranBLL
{
    public sealed class NotasFiscais
    {
        public static List<SistranMODEL.NotasFiscais> RetornaNotasFiscaisSaida(int IdCliente, int? NotaFiscalId, DateTime DataEmissaoInicio, DateTime DataEmissaoFinal, DateTime DataEntregaInicio,
                DateTime DataEntregaFinal, string Cnpj, string RazaoSocial, string Fantasia, string tiponota, string Conn)
        {

            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(IdCliente)))
                {
                    throw new Exception("Cliente Inválido");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    List<SistranMODEL.NotasFiscais> LnotasFiscaisSaida = SistranDAO.NotasFiscais.RetornaNotasFiscaisSaida(IdCliente, NotaFiscalId, DataEmissaoInicio, DataEmissaoFinal, DataEntregaInicio,
                        DataEntregaFinal, Cnpj, RazaoSocial, Fantasia, tiponota, Conn);


                    return LnotasFiscaisSaida;

                }

            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }

        }

        public static DataTable RetornarSituacoes(string clientes, string dataIni, string dataFim, string Conn)
        {
            return SistranDAO.NotasFiscais.RetornarSituacoes(clientes, dataIni, dataFim, Conn);
        
        }

        public static SistranMODEL.NotasFiscais RetornaNotaFiscalSaidaDescricao(int NotaFiscalId, string Conn)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(NotaFiscalId)))
                {
                    throw new Exception("Nota fiscal inválida. Por favor entre em contato com o adminstrador do sistema");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    SistranMODEL.NotasFiscais notasFiscaisSaida = SistranDAO.NotasFiscais.RetornaNotaFiscalSaida(NotaFiscalId, Conn);


                    return notasFiscaisSaida;

                }

            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public static DataTable ListarOcorrenciaSelConsultar(int DocId, string Conn)
        {
            return SistranDAO.NotasFiscais.ListarOcorrenciaSelConsultar( DocId,  Conn);
        }

        public static DataTable NotaFiscalSelConsultar(int DocId, string Conn)
        {
            return SistranDAO.NotasFiscais.NotaFiscalSelConsultar(DocId, Conn);
        }

        public static DataTable ListarDesempenhoEntregaDiaNaoEntregue(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            return SistranDAO.NotasFiscais.ListarDesempenhoEntregaDiaNaoEntregue(Ini, Fim, clientes, Conn);
        }
        public static DataTable ListarDesempenhoEntregaDia(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            return SistranDAO.NotasFiscais.ListarDesempenhoEntregaDia(Ini, Fim, clientes, Conn);
        }

        public static DataTable ListarDesempenhoEntregaFilial(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            return SistranDAO.NotasFiscais.ListarDesempenhoEntregaFilial(Ini, Fim, clientes, Conn);
        }

        public static DataTable ListarDesempenhoEntregaCidade(DateTime Ini, DateTime Fim, string clientes, string ordem, string ordem2, string Conn, bool incluirTransportadora)
        {
            return SistranDAO.NotasFiscais.ListarDesempenhoEntregaCidade(Ini, Fim, clientes, ordem,ordem2, Conn, incluirTransportadora);
        }

        public static DataTable ListarDadosResumoPorFilial(DateTime Ini, DateTime Fim, string clientes, string Conn, Enuns.tipoReportResumoFilial tipo, bool os, bool ret, bool dev )
        {
            return SistranDAO.NotasFiscais.ListarDadosResumoPorFilial(Ini, Fim, clientes, Conn, tipo, os, ret, dev);
        }

        public static DataTable ListarResumoPorFilial(DateTime Ini, DateTime Fim, string clientes, string Conn, Sistran.Library.Enuns.tipoReportResumoFilial tipo, bool OutrasSeries, bool Dev, bool Ret)
        {
            DataTable dttemp = SistranDAO.NotasFiscais.ListarResumoPorFilial(Ini, Fim, clientes, Conn, tipo, OutrasSeries, Dev,  Ret);
            
            dttemp.Columns.Add("ValorOrdem");

            DataTable distinctTable = dttemp.DefaultView.ToTable(true, "NOMEDAFILIAL");
            distinctTable.Columns.Add("Valor");

            for (int i = 0; i < distinctTable.Rows.Count; i++)
            {
                int valorIndividual = 0;
                int totalNF = 0;
                try
                {
                    totalNF = Convert.ToInt32(dttemp.Compute("SUM(TOTALDENOTAS)", "NOMEDAFILIAL='" + distinctTable.Rows[i]["NOMEDAFILIAL"].ToString() + "'"));
                    valorIndividual = Convert.ToInt32(dttemp.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "NOMEDAFILIAL='" + distinctTable.Rows[i]["NOMEDAFILIAL"].ToString() + "' AND PRAZOUTILIZADO=1"));
                }
                catch (Exception)
                {
                }

                decimal valor = Convert.ToDecimal(0);

                if(Convert.ToDecimal(totalNF) > Convert.ToDecimal(0))
                    valor = Convert.ToDecimal(valorIndividual) / Convert.ToDecimal(totalNF);
                
                distinctTable.Rows[0][1] = valor;

                for (int t = 0; t < dttemp.Rows.Count; t++)
                {
                    try
                    {
                        if (dttemp.Rows[t][0].ToString() == distinctTable.Rows[i]["NOMEDAFILIAL"].ToString())
                        {
                            dttemp.Rows[t]["ValorOrdem"] = valor.ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            DataView dv = new DataView(dttemp);
            dv.Sort = "ValorOrdem desc";

            DataTable df = (DataTable)dv.ToTable();

            return df;
        }

        public static DataTable ListarResumoPorFilialPrazoNovo(DateTime Ini, DateTime Fim, string clientes, string Conn)
        {
            return SistranDAO.NotasFiscais.ListarResumoPorFilialPrazoNovo(Ini, Fim, clientes, Conn);
        }

        public static DataTable ListarHomeAguardandoEmbarque(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            return SistranDAO.NotasFiscais.ListarHomeAguardandoEmbarque(clientes, Conn, situacao, ini , fim);
        }

        public static DataTable ListarHomeAguardandoEmbarqueFilial(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            return SistranDAO.NotasFiscais.ListarHomeAguardandoEmbarqueFilial(clientes, Conn, situacao, ini, fim);
        }


        public static DataTable ListarDataFilialSituacao(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim, bool OutrasSeries, bool Ret, bool Dev, bool especial)
        {
            return SistranDAO.NotasFiscais.ListarDataFilialSituacao(clientes, Conn, situacao, ini, fim, OutrasSeries, Ret, Dev, especial);
        }

        public static DataTable ListarHomeNotasFiscaisEmbarcadas(string clientes, string Conn, string situacao, DateTime? ini, DateTime? fim)
        {
            return SistranDAO.NotasFiscais.ListarHomeNotasFiscaisEmbarcadas(clientes, Conn, situacao, ini, fim );
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrencias(string clientes, string Conn, DateTime? INI, DateTime? FIM, bool DatarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            return SistranDAO.NotasFiscais.ListarHomeNotasFiscaisComOcorrencias(clientes, Conn, INI, FIM, DatarPorOcorrencia, dtEmissaoOcorrencia);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrenciasResponsabilidade(string clientes, string Conn, DateTime INI, DateTime FIM, bool DatarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            return SistranDAO.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasResponsabilidade(clientes, Conn, INI, FIM, DatarPorOcorrencia, dtEmissaoOcorrencia);
        }

        public static DataTable ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(string clientes, string Conn, DateTime INI, DateTime FIM, bool DatarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            return SistranDAO.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilialResponsaveis(clientes, Conn, INI, FIM, DatarPorOcorrencia, dtEmissaoOcorrencia);
        }
        public static DataTable ListarHomeNotasFiscaisComOcorrenciasFilial(string clientes, string Conn, DateTime INI, DateTime FIM, bool DatarPorOcorrencia, bool dtEmissaoOcorrencia)
        {
            return SistranDAO.NotasFiscais.ListarHomeNotasFiscaisComOcorrenciasFilial(clientes, Conn, INI, FIM, DatarPorOcorrencia, dtEmissaoOcorrencia);
        }

        public static DataTable RetornarImagemByDocumentoOcorrencia(int IdDocumentoOcorrencia, string Conn)
        {
            return SistranDAO.NotasFiscais.RetornarImagemByDocumentoOcorrencia(IdDocumentoOcorrencia, Conn);
        }


        public static DataTable RetornarImagemByDocumentoOcorrenciaMultiplas(string IdDocumentoOcorrencia, string Conn)
        {
            return SistranDAO.NotasFiscais.RetornarImagemByDocumentoOcorrenciaMultiplas(IdDocumentoOcorrencia, Conn);
         
        }

        public static int RetornarTotalNotasFiscaisEmitidas(string Clientes, string DataInicio, string DataFim)
        {
            return SistranDAO.NotasFiscais.RetornarTotalNotasFiscaisEmitidas(Clientes, DataInicio, DataFim);
        }

        public sealed class Ocorrencia
        {
            public static DataTable OcorrenciaAtualListar(int DocId, int IDDocumentoOcorrencia, string Conn)
            {
                return SistranDAO.NotasFiscais.Ocorrencia.OcorrenciaAtualListar(DocId, IDDocumentoOcorrencia, Conn);
            }

            public static DataTable OcorrenciaListar(string Conn, string IdCliente)
            {
                return SistranDAO.NotasFiscais.Ocorrencia.OcorrenciaListar(Conn, IdCliente);
            }
        }
    }
}
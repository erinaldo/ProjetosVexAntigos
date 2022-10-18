using System.Data;
using System;

namespace SistranBLL
{
    public sealed class Projeto
    {
        public DataTable Filtrar(int? IDPROJETO)
        {
            return new SistranDAO.Projeto().Filtrar(IDPROJETO);
        }

        public int InserirAlterar(
                                   int IDPROJETO,
                                   int IDFILIAL,
                                   string NOME,
                                   string CONTATOCLIENTE,
                                   string CONTATOCONTRATADO,
                                   string UTILIZAAREACLIMATIZADA,
                                   DateTime INICIODAPRODUCAO,
                                   DateTime FINALDAPRODUCAO,
                                   DateTime INICIODAENTREGA,
                                   DateTime FINALDAENTREGA,
                                   int TOTALDEKITS,
                                   int FATORPORCAIXA,
                                   int FATORPORPALLET,
                                   decimal PESOPORKIT,
                                   decimal FRETEPORKIT,
                                   decimal TEMPODEPRODUCAO,
                                   int TURNOS,
                                   int PESSOASPORTURNO,
                                   int MAODEOBRA,
                                   string STATUS,
                                   DataTable items, DataTable Producao,
                                    string Edi,
                                    string DigitalizarComprovante,
                                    string EmitirCTRC,
                                    string EmitirNotaFiscalServico,
                                    string FormaFaturamento,
                                    string Vencimento,
                                    string OrigemDaColeta,
                                    string ValorPorColeta,
                                    string PlanejamentoDeTransferencia,
                                    string Observacao
                           )
        {
            return new SistranDAO.Projeto().InserirAlterar(
                                                            IDPROJETO,
                                                            IDFILIAL,
                                                            NOME,
                                                            CONTATOCLIENTE,
                                                            CONTATOCONTRATADO,
                                                            UTILIZAAREACLIMATIZADA,
                                                            INICIODAPRODUCAO,
                                                            FINALDAPRODUCAO,
                                                            INICIODAENTREGA,
                                                            FINALDAENTREGA,
                                                            TOTALDEKITS,
                                                            FATORPORCAIXA,
                                                            FATORPORPALLET,
                                                            PESOPORKIT,
                                                            FRETEPORKIT,
                                                            TEMPODEPRODUCAO,
                                                            TURNOS,
                                                            PESSOASPORTURNO,
                                                            MAODEOBRA,
                                                            STATUS,
                                                            items, Producao,
                                                            Edi,
                                                            DigitalizarComprovante,
                                                            EmitirCTRC,
                                                            EmitirNotaFiscalServico,
                                                            FormaFaturamento,
                                                            Vencimento,
                                                            OrigemDaColeta,
                                                            ValorPorColeta,
                                                            PlanejamentoDeTransferencia,
                                                            Observacao
                                    );
        }
    }
}
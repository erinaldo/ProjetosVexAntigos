using System.Data;
using System;

namespace SistranBLL
{
    public sealed class Sobra
    {
        public DataTable Filtrar(int? idSobra, int? idFilial, Boolean exibirFinalizados, DateTime Ini, DateTime Fim)
        {
            return new SistranDAO.Sobra().Filtrar(idSobra, idFilial, exibirFinalizados, Ini, Fim);
        }

        /*public int InserirAlterar(
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
                                   DataTable items, DataTable Producao
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
                                                            items, Producao
                                    );
        }*/
    }
}
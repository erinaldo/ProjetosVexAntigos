using System;

namespace SistranMODEL
{
    public sealed class NotasFiscaisHistorico
    {

        public int Ocorrencia { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Historico { get; set; }
        public string Descicao { get; set; }
        public string Foto { get; set; }
        public string Usuario { get; set; }

        public NotasFiscaisHistorico()
        { }


        public NotasFiscaisHistorico(int ocorrencia, DateTime dataOcorrencia, string historico, string descricao, string foto, string usuario)
        {
            Ocorrencia = ocorrencia;
            DataOcorrencia = dataOcorrencia;
            Historico = historico;
            Descicao = descricao;
            Foto = foto;
            Usuario = usuario;
        }

    }
}

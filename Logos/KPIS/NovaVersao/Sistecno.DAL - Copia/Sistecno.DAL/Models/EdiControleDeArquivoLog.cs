using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EdiControleDeArquivoLog
    {
        public int IdEdiControleDeArquivoLog { get; set; }
        public int IdEdiControleDeArquivo { get; set; }
        public string TipoDeRegistro { get; set; }
        public string Chave { get; set; }
        public Nullable<int> Linha { get; set; }
        public string Ocorrencia { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string NomeDoArquivo { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LancamentoOcorrencia
    {
        public int IdLancamentoOcorrencia { get; set; }
        public int IdLancamento { get; set; }
        public string Ocorrencia { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime DataDaOcorrencia { get; set; }
        public string ProgramaOrigem { get; set; }
    }
}

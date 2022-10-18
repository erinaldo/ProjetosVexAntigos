using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class AgendamentoDocumento
    {
        public int IdAgendamentoDocumento { get; set; }
        public int IdAgendamento { get; set; }
        public int IdDocumento { get; set; }
        public string StatusDoDocumento { get; set; }
        public string SituacaoDoDocumento { get; set; }
        public string NomeDoArquivo { get; set; }
        public Nullable<System.DateTime> DataGeracaoDoArquivo { get; set; }
        public virtual Agendamento Agendamento { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

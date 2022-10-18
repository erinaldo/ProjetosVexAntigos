using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Lancamento
    {
        public int IdLancamento { get; set; }
        public Nullable<int> IdDocumentoOrigem { get; set; }
        public string Tabela { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<System.DateTime> DataLancamento { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeDeArmazenagemMov
    {
        public int IdUnidadeDeArmazenagemMov { get; set; }
        public int IdUnidadeDeArmazenagem { get; set; }
        public Nullable<int> IdEnderecoOrigem { get; set; }
        public Nullable<int> IdEnderecoDestino { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
    }
}

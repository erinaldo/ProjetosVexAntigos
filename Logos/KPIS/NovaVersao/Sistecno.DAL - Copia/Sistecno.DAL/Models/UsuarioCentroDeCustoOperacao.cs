using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCentroDeCustoOperacao
    {
        public int IdUsuarioCentroDeCustoOperacao { get; set; }
        public int IdUsuarioCentroDeCusto { get; set; }
        public int IdOperacao { get; set; }
        public Nullable<decimal> Valor { get; set; }
    }
}

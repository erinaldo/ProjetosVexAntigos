using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LancamentoContabilCC
    {
        public int IdLancamentoContabilCC { get; set; }
        public int IdCentroDeCustoFilial { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<int> IdLancamento { get; set; }
        public Nullable<int> IdLancamentoContabil { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesaItem
    {
        public int IdContaDespesaItem { get; set; }
        public int IdContaDespesa { get; set; }
        public Nullable<decimal> PercentualRateioCentroDeCusto { get; set; }
        public Nullable<decimal> PercentualRateioContaContabil { get; set; }
        public Nullable<int> IdContaContabilFilial { get; set; }
        public Nullable<int> IdCentroDeCustoFilial { get; set; }
        public Nullable<decimal> ValorRateioCentroDeCusto { get; set; }
        public Nullable<decimal> ValorRateioContaContabil { get; set; }
        public virtual ContaDespesa ContaDespesa { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoItem
    {
        public ContratoItem()
        {
            this.ContratoItemCCustoes = new List<ContratoItemCCusto>();
        }

        public int IdContratoItem { get; set; }
        public int IdContrato { get; set; }
        public Nullable<decimal> PercentualRateioCentroDeCusto { get; set; }
        public Nullable<decimal> PercentualRateioContaContabil { get; set; }
        public Nullable<int> IdContaContabilFilial { get; set; }
        public Nullable<int> IdCentroDeCustoFilial { get; set; }
        public Nullable<decimal> ValorRateioCentroDeCusto { get; set; }
        public Nullable<decimal> ValorRateioContaContabil { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual ICollection<ContratoItemCCusto> ContratoItemCCustoes { get; set; }
    }
}

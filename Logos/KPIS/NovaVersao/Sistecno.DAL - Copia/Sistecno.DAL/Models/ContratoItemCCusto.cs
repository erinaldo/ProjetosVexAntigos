using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoItemCCusto
    {
        public int IdContratoItemCCusto { get; set; }
        public int IdContratoItem { get; set; }
        public int IdCentroDeCustoFilial { get; set; }
        public Nullable<decimal> PercentualRateioCentroDeCusto { get; set; }
        public Nullable<decimal> ValorRateioCentroDeCusto { get; set; }
        public virtual ContratoItem ContratoItem { get; set; }
    }
}

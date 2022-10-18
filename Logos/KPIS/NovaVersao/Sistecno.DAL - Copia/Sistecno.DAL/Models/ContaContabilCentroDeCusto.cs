using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaContabilCentroDeCusto
    {
        public int IdContaContabilCentroDeCusto { get; set; }
        public int IdContaContabil { get; set; }
        public int IdCentroDeCusto { get; set; }
        public string Operacao { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
    }
}

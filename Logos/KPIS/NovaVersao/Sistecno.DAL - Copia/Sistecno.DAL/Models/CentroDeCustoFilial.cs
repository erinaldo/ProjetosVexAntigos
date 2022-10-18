using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CentroDeCustoFilial
    {
        public int IDCentroDeCustoFilial { get; set; }
        public int IDCentroDeCusto { get; set; }
        public int IDFilial { get; set; }
        public Nullable<decimal> SaldoInicial { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

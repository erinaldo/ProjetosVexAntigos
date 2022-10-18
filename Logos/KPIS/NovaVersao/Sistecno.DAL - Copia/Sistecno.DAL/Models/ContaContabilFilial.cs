using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaContabilFilial
    {
        public int IDContaContabilFilial { get; set; }
        public int IDContaContabil { get; set; }
        public int IDFilial { get; set; }
        public Nullable<decimal> SaldoInicial { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<System.DateTime> DataSaldoInicial { get; set; }
        public Nullable<System.DateTime> DataDeFechamento { get; set; }
        public Nullable<int> IdContaContabilFilialRelacionada { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesaCapa
    {
        public int IdContaDespesaCapa { get; set; }
        public int IdContaDespesa { get; set; }
        public int IdContaDespesaEvento { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Percentual { get; set; }
        public string Descritivo { get; set; }
        public virtual ContaDespesa ContaDespesa { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Irrf
    {
        public int IdIrrf { get; set; }
        public Nullable<decimal> AcimaDe { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> Deduzir { get; set; }
        public Nullable<decimal> ValorDependente { get; set; }
        public Nullable<decimal> BaseDeCalculo { get; set; }
    }
}

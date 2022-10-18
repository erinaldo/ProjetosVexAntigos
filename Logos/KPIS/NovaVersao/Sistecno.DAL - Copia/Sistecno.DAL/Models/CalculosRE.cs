using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CalculosRE
    {
        public int IDDOCUMENTO { get; set; }
        public decimal VOLUMES { get; set; }
        public Nullable<decimal> VALORDANOTA { get; set; }
        public Nullable<decimal> PESOBRUTO { get; set; }
        public Nullable<decimal> PESOLIQUIDO { get; set; }
        public int IDDT { get; set; }
    }
}

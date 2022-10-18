using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BASE
    {
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public string CDA { get; set; }
        public Nullable<double> IDCDA { get; set; }
        public Nullable<double> SALDOATUAL { get; set; }
        public Nullable<double> SALDOCORRETO { get; set; }
        public bool F7 { get; set; }
    }
}

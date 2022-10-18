using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VW_REENTREGA
    {
        public int IDDESTINATARIO { get; set; }
        public int ROMIDDT { get; set; }
        public int IDREGIAO { get; set; }
        public string PROPRIETARIO { get; set; }
        public string ENTREGAEFETUADA { get; set; }
        public Nullable<int> FRETEIDDDT { get; set; }
        public Nullable<decimal> FRETE { get; set; }
    }
}

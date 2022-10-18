using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DtCheque
    {
        public int IdDtCheque { get; set; }
        public int IdDt { get; set; }
        public int IdCheque { get; set; }
        public virtual Cheque Cheque { get; set; }
        public virtual DT DT { get; set; }
    }
}

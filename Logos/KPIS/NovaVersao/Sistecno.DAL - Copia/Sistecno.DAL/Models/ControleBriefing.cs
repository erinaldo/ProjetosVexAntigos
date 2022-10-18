using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ControleBriefing
    {
        public int IdControleBriefing { get; set; }
        public int Briefing { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Saldo { get; set; }
    }
}

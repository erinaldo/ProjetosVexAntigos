using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioPrevisaoRegiao
    {
        public int IdRomaneioPrevisaoRegiao { get; set; }
        public int IdRomaneioPrevisao { get; set; }
        public int IdRegiao { get; set; }
        public Nullable<int> OrdemDeCarregamento { get; set; }
        public Nullable<System.DateTime> data { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual RomaneioPrevisao RomaneioPrevisao { get; set; }
    }
}

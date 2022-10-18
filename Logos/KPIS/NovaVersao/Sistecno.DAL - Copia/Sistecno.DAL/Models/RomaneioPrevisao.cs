using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioPrevisao
    {
        public RomaneioPrevisao()
        {
            this.RomaneioPrevisaoRegiaos = new List<RomaneioPrevisaoRegiao>();
        }

        public int IdRomaneioPrevisao { get; set; }
        public System.DateTime Data { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public Nullable<int> IdRegiao { get; set; }
        public virtual ICollection<RomaneioPrevisaoRegiao> RomaneioPrevisaoRegiaos { get; set; }
    }
}

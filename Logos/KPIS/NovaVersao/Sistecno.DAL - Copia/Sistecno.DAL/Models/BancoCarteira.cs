using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoCarteira
    {
        public BancoCarteira()
        {
            this.BancoContaBloquetoes = new List<BancoContaBloqueto>();
        }

        public int IDBancoCarteira { get; set; }
        public int IDBanco { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual ICollection<BancoContaBloqueto> BancoContaBloquetoes { get; set; }
    }
}

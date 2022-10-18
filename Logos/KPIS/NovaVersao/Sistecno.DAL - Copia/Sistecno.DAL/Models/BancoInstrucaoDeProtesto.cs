using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoInstrucaoDeProtesto
    {
        public BancoInstrucaoDeProtesto()
        {
            this.BancoContaBloquetoes = new List<BancoContaBloqueto>();
        }

        public int IdBancoInstrucaoDeProtesto { get; set; }
        public int IdBanco { get; set; }
        public string Codigo { get; set; }
        public int Dias { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual ICollection<BancoContaBloqueto> BancoContaBloquetoes { get; set; }
    }
}

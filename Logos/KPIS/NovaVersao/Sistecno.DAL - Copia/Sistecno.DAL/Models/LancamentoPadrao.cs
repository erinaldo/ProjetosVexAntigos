using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LancamentoPadrao
    {
        public LancamentoPadrao()
        {
            this.LancamentoPadraoConfiguracaos = new List<LancamentoPadraoConfiguracao>();
        }

        public int IDLancamentoPadrao { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<LancamentoPadraoConfiguracao> LancamentoPadraoConfiguracaos { get; set; }
    }
}

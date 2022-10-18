using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Agrupamento
    {
        public Agrupamento()
        {
            this.AgrupamentoRegiaos = new List<AgrupamentoRegiao>();
        }

        public int IdAgrupamento { get; set; }
        public string Nome { get; set; }
        public string Ordem { get; set; }
        public virtual ICollection<AgrupamentoRegiao> AgrupamentoRegiaos { get; set; }
    }
}

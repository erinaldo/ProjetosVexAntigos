using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class AgrupamentoRegiao
    {
        public int IdAgrupamentoRegiao { get; set; }
        public int IdAgrupamento { get; set; }
        public int IdRegiao { get; set; }
        public virtual Agrupamento Agrupamento { get; set; }
        public virtual Regiao Regiao { get; set; }
    }
}

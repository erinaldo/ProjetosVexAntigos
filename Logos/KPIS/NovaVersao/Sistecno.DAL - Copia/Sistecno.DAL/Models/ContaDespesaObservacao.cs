using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesaObservacao
    {
        public int IdContaDespesaObservacao { get; set; }
        public int IdContaDespesa { get; set; }
        public string Observacao { get; set; }
        public virtual ContaDespesa ContaDespesa { get; set; }
    }
}

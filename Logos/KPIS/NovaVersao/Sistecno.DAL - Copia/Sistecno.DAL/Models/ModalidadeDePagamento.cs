using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModalidadeDePagamento
    {
        public int IdModalidadeDePagamento { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public virtual FinalidadeDocTed FinalidadeDocTed { get; set; }
    }
}

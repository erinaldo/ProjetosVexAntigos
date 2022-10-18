using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FinalidadeDocTed
    {
        public int IdfinalidadeDocTed { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Ativo { get; set; }
        public virtual ModalidadeDePagamento ModalidadeDePagamento { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModalidadeFinalidadeDocTed
    {
        public int IdModalidadeFinalidadeDocTed { get; set; }
        public Nullable<int> IdModalidadeDocTed { get; set; }
        public Nullable<int> IdFinalidadeDocTed { get; set; }
        public Nullable<int> IdModalidadeDePagamento { get; set; }
    }
}

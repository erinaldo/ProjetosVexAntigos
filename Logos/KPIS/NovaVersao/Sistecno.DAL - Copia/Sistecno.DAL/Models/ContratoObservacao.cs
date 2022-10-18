using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoObservacao
    {
        public int IdContratoObservacao { get; set; }
        public int IdContrato { get; set; }
        public string Observacao { get; set; }
    }
}

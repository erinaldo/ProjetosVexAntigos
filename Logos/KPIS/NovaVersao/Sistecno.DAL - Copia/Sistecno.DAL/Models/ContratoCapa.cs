using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoCapa
    {
        public int IdContratoCapa { get; set; }
        public int IdContrato { get; set; }
        public int IdContratoEvento { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Percentual { get; set; }
        public string Descritivo { get; set; }
        public virtual Contrato Contrato { get; set; }
    }
}

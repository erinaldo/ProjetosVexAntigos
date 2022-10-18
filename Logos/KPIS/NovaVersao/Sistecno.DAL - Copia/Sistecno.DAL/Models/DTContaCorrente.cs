using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTContaCorrente
    {
        public int IdDtContaCorrente { get; set; }
        public int IdDT { get; set; }
        public Nullable<System.DateTime> DataDoLancamento { get; set; }
        public Nullable<System.DateTime> DataDoEvento { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdOcorrencia { get; set; }
        public string Historico { get; set; }
        public Nullable<decimal> Debito { get; set; }
        public Nullable<decimal> Credito { get; set; }
        public string Status { get; set; }
        public Nullable<int> IdDTLancamento { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public virtual DT DT { get; set; }
    }
}

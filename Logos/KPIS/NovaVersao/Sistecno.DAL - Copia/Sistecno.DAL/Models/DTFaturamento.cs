using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTFaturamento
    {
        public DTFaturamento()
        {
            this.DTFaturamentoClientes = new List<DTFaturamentoCliente>();
        }

        public int IdDtFaturamento { get; set; }
        public int IdDt { get; set; }
        public Nullable<System.DateTime> DataFaturamento { get; set; }
        public virtual DT DT { get; set; }
        public virtual ICollection<DTFaturamentoCliente> DTFaturamentoClientes { get; set; }
    }
}

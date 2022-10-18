using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteRegra
    {
        public int IdClienteRegra { get; set; }
        public Nullable<int> PrazoDeEntrega { get; set; }
        public Nullable<int> PrazoDeEntregaTolerancia { get; set; }
        public Nullable<int> NumeroDoRecebimento { get; set; }
        public Nullable<decimal> ValorPorCaixa { get; set; }
        public Nullable<System.DateTime> DataDoRecebimento { get; set; }
        public Nullable<decimal> KM { get; set; }
        public Nullable<int> Faixa { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}

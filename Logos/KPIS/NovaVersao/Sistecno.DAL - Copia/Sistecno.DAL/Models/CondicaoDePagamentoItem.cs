using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CondicaoDePagamentoItem
    {
        public int IdCondicaoDePagamentoItem { get; set; }
        public int IdCondicaoDePagamento { get; set; }
        public int QtdeDias { get; set; }
        public decimal Percentual { get; set; }
    }
}

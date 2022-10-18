using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoClienteDivisao
    {
        public System.DateTime Data { get; set; }
        public int IdProdutoCliente { get; set; }
        public int IdClienteDivisao { get; set; }
        public decimal Saldo { get; set; }
        public Nullable<int> IdFilial { get; set; }
    }
}

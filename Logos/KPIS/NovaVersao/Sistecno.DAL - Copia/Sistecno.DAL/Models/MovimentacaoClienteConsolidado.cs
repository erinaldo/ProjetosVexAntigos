using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoClienteConsolidado
    {
        public int IdMovimentacaoClienteConsolidado { get; set; }
        public int IdCliente { get; set; }
        public System.DateTime Data { get; set; }
        public Nullable<int> PalletsArmazenagem { get; set; }
        public Nullable<int> PalletsEntrada { get; set; }
        public Nullable<int> PalletsSaida { get; set; }
        public Nullable<decimal> M3Armazenagem { get; set; }
        public Nullable<decimal> M3Entrada { get; set; }
        public Nullable<decimal> M3Saida { get; set; }
    }
}

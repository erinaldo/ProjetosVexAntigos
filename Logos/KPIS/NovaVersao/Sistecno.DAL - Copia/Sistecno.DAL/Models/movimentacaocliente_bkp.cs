using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class movimentacaocliente_bkp
    {
        public int IdMovimentoCliente { get; set; }
        public System.DateTime Data { get; set; }
        public int IdCliente { get; set; }
        public int IdProdutoCliente { get; set; }
        public int IdUnidadeDeArmazenagem { get; set; }
        public int IdUnidadeDeArmazenagemLote { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public decimal SaldoDeEntrada { get; set; }
        public decimal Saldo { get; set; }
        public string Tipo { get; set; }
        public string UsouSaldoMinimo { get; set; }
        public Nullable<int> OcupaQtosPallets { get; set; }
        public Nullable<decimal> ValorEmEstoque { get; set; }
        public Nullable<decimal> Valorunitario { get; set; }
    }
}

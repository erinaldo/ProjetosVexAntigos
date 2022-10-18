using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoEstrutura
    {
        public int IdProdutoEstrutura { get; set; }
        public int IDProdutoClientePai { get; set; }
        public int IdProdutoClienteFilho { get; set; }
        public decimal Quantidade { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public Nullable<int> IdProdutoEmbalagem { get; set; }
        public Nullable<int> IdProdutoCliente { get; set; }
        public Nullable<decimal> Perda { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ProdutoCliente ProdutoCliente1 { get; set; }
    }
}

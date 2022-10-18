using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Estoque
    {
        public Estoque()
        {
            this.EstoqueComprasMovs = new List<EstoqueComprasMov>();
            this.EstoqueDivisaos = new List<EstoqueDivisao>();
            this.Lotes = new List<Lote>();
        }

        public int IDEstoque { get; set; }
        public int IDProdutoCliente { get; set; }
        public int IDFilial { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> ValorEmEstoque { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ICollection<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public virtual ICollection<EstoqueDivisao> EstoqueDivisaos { get; set; }
        public virtual ICollection<Lote> Lotes { get; set; }
    }
}

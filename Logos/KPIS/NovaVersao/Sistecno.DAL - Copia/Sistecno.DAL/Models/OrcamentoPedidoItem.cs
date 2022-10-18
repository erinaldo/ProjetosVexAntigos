using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OrcamentoPedidoItem
    {
        public int IdOrcamentoPedidoItem { get; set; }
        public int IdOrcamentoPedido { get; set; }
        public int IdProdutoCliente { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<decimal> TotalDoItem { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public virtual OrcamentoPedido OrcamentoPedido { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Lote
    {
        public Lote()
        {
            this.EstoqueComprasMovs = new List<EstoqueComprasMov>();
        }

        public int IDLote { get; set; }
        public Nullable<int> IDEstoque { get; set; }
        public Nullable<int> IDProdutoCliente { get; set; }
        public Nullable<int> IDProdutoEmbalagem { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IdRomaneio { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public Nullable<System.DateTime> DataDeEntrada { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public string Referencia { get; set; }
        public string Ativo { get; set; }
        public string Observacao { get; set; }
        public Nullable<decimal> SaldoNFEntrada { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Estoque Estoque { get; set; }
        public virtual ICollection<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem { get; set; }
        public virtual Romaneio Romaneio { get; set; }
    }
}

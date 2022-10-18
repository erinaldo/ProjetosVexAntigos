using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class InventarioContagemProduto
    {
        public int IdInventarioContagemProduto { get; set; }
        public int IdInventarioContagem { get; set; }
        public Nullable<int> IdProdutoCliente { get; set; }
        public Nullable<int> IdProdutoEmbalagem { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IdUnidadeDeArmazenagem { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public string Referencia { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public Nullable<int> Lastro { get; set; }
        public Nullable<int> Altura { get; set; }
        public Nullable<int> Excedente { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual InventarioContagem InventarioContagem { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

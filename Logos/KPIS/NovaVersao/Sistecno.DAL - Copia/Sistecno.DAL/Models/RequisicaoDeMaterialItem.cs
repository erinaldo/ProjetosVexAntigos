using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RequisicaoDeMaterialItem
    {
        public RequisicaoDeMaterialItem()
        {
            this.CotacaoDeCompraItems = new List<CotacaoDeCompraItem>();
            this.EstoqueComprasMovs = new List<EstoqueComprasMov>();
        }

        public int IdRequisicaoDeMaterialItem { get; set; }
        public int IdRequisicaoDeMaterial { get; set; }
        public int IdProdutoCliente { get; set; }
        public decimal QuantidadeSolicitada { get; set; }
        public decimal QuantidadeAtendida { get; set; }
        public string Andamento { get; set; }
        public string Observacao { get; set; }
        public Nullable<int> IdCentroDeCusto { get; set; }
        public virtual ICollection<CotacaoDeCompraItem> CotacaoDeCompraItems { get; set; }
        public virtual ICollection<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public virtual RequisicaoDeMaterial RequisicaoDeMaterial { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstoqueComprasMov
    {
        public int IDEstoqueComprasMov { get; set; }
        public int IDRequisicaoDeMaterial { get; set; }
        public int IDRequisicaoDeMaterialItem { get; set; }
        public int IDEstoque { get; set; }
        public int IDLote { get; set; }
        public int IDUnidadeDeArmazenagemLote { get; set; }
        public System.DateTime DataDeMovimentacao { get; set; }
        public string EntradaSaida { get; set; }
        public decimal Quantidade { get; set; }
        public virtual Estoque Estoque { get; set; }
        public virtual Lote Lote { get; set; }
        public virtual RequisicaoDeMaterial RequisicaoDeMaterial { get; set; }
        public virtual RequisicaoDeMaterialItem RequisicaoDeMaterialItem { get; set; }
        public virtual UnidadeDeArmazenagemLote UnidadeDeArmazenagemLote { get; set; }
    }
}

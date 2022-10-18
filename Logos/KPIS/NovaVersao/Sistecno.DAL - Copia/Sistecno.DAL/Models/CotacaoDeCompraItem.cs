using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CotacaoDeCompraItem
    {
        public int IdCotacaoDeCompraItem { get; set; }
        public int IdCotacaoFornecedor { get; set; }
        public Nullable<int> IdRequisicaoDeMaterialItem { get; set; }
        public int IdProdutoCliente { get; set; }
        public int Quantidade { get; set; }
        public int Saldo { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalDoItem { get; set; }
        public int IdUnidadeDeMedida { get; set; }
        public Nullable<decimal> AliquotaDeIcms { get; set; }
        public Nullable<decimal> ValorDeIcms { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<decimal> Acrescimo { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<decimal> AliquotaDeIpi { get; set; }
        public Nullable<decimal> ValorDeIpi { get; set; }
        public string Observacao { get; set; }
        public Nullable<int> IdCentroDeCusto { get; set; }
        public virtual RequisicaoDeMaterialItem RequisicaoDeMaterialItem { get; set; }
    }
}

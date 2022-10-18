using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstoqueMov
    {
        public int IDEstoqueMov { get; set; }
        public int IDEstoque { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagemLote { get; set; }
        public Nullable<int> IDProdutoCliente { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacaoOrigem { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacaoDestino { get; set; }
        public int IDEstoqueOperacao { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IdMovimentacaoItem { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataHora { get; set; }
        public Nullable<decimal> QuantidadeSolicitada { get; set; }
        public Nullable<decimal> UnidadeDoCliente { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> ValorEmEstoque { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> ValorIcms { get; set; }
        public Nullable<decimal> ValorIcmsAcumulado { get; set; }
        public Nullable<decimal> ValorEmEstoqueAcumulado { get; set; }
    }
}

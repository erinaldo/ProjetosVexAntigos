using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoItem
    {
        public MovimentacaoItem()
        {
            this.EstoqueDivisaoMovs = new List<EstoqueDivisaoMov>();
        }

        public int IDMovimentacaoItem { get; set; }
        public int IDMovimentacao { get; set; }
        public Nullable<int> IDRomaneio { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagem { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagemLote { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagemDestino { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacaoOrigem { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacaoDestino { get; set; }
        public Nullable<int> IDProdutoEmbalagem { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDDocumentoItem { get; set; }
        public Nullable<int> IDMapa { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<decimal> QuantidadeBaixada { get; set; }
        public Nullable<System.DateTime> DataDeExecucao { get; set; }
        public Nullable<decimal> QuantidadeUnidadeEstoque { get; set; }
        public string PedidoNotaFiscal { get; set; }
        public Nullable<System.DateTime> DataHoraPedidoNotaFiscal { get; set; }
        public string TipoMovto { get; set; }
        public string obs { get; set; }
        public Nullable<int> IdProdutoCliente { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs { get; set; }
    }
}

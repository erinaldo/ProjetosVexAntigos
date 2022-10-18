using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoRomaneioItem
    {
        public int IDMovimentacaoRomaneioItem { get; set; }
        public int IDMovimentacaoRomaneio { get; set; }
        public int IDUnidadeDeArmazenagemLote { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagemDestino { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacaoDestino { get; set; }
        public Nullable<int> IDProdutoEmbalagem { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public int IDDocumento { get; set; }
        public Nullable<int> IDDocumentoItem { get; set; }
        public decimal Quantidade { get; set; }
        public Nullable<decimal> QuantidadeBaixada { get; set; }
        public Nullable<System.DateTime> DataDeExecucao { get; set; }
        public Nullable<decimal> QuantidadeUnidadeEstoque { get; set; }
        public Nullable<int> IDMapa { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
        public virtual Mapa Mapa { get; set; }
        public virtual MovimentacaoRomaneio MovimentacaoRomaneio { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
        public virtual UnidadeDeArmazenagemLote UnidadeDeArmazenagemLote { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

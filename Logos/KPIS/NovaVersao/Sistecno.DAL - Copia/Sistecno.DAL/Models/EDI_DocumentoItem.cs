using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoItem
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDDocumentoItem { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDProdutoEmbalagem { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<int> IDClienteDivisao { get; set; }
        public Nullable<int> IDDocumentoOrigem { get; set; }
        public Nullable<int> IDDocumentoItemOrigem { get; set; }
        public Nullable<int> IDCFOP { get; set; }
        public Nullable<int> IDTES { get; set; }
        public Nullable<int> IDLote { get; set; }
        public Nullable<int> IDOcorrencia { get; set; }
        public string Referencia { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> QuantidadeUnidadeEstoque { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> UnidadeLogistica { get; set; }
        public string StatusSeparacao { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<decimal> ValorTotalDoItem { get; set; }
        public Nullable<decimal> ValorDoDesconto { get; set; }
        public Nullable<decimal> PercentualDeDesconto { get; set; }
        public Nullable<decimal> AliquotaDeIcms { get; set; }
        public Nullable<decimal> ValorIcms { get; set; }
        public Nullable<decimal> AliquotaDeIPI { get; set; }
        public Nullable<decimal> ValorDeIPI { get; set; }
        public Nullable<decimal> AliquotaDeICMSSubst { get; set; }
        public Nullable<decimal> ValorDeICMSSubst { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public string EstoqueProcessado { get; set; }
        public string StatusDoItem { get; set; }
        public Nullable<decimal> UnidadeDoCLiente { get; set; }
        public Nullable<decimal> RedBaseIcms { get; set; }
        public Nullable<decimal> QuantidadeEDI { get; set; }
        public Nullable<decimal> ValorUnitarioEDI { get; set; }
        public Nullable<decimal> QuantidadeRecebida { get; set; }
        public Nullable<int> IdCda { get; set; }
        public Nullable<int> Item { get; set; }
        public string CfopOrigem { get; set; }
        public string TipoDeItem { get; set; }
        public string SituacaoTributariaICMS { get; set; }
        public string SituacaoTributariaIPI { get; set; }
        public string SituacaoTributariaPIS { get; set; }
        public Nullable<decimal> BaseDoIPI { get; set; }
        public Nullable<decimal> ValorBaseCalculoPis { get; set; }
        public Nullable<decimal> AliquotaPis { get; set; }
        public Nullable<decimal> ValorDoPis { get; set; }
        public Nullable<decimal> AliquotaCofins { get; set; }
        public Nullable<decimal> ValorCofins { get; set; }
        public Nullable<decimal> BaseDoIcms { get; set; }
        public string SituacaoTributariaCofins { get; set; }
        public Nullable<decimal> BaseCalculoCofins { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConciliacaoLoteDetalhe
    {
        public int IdConciliacaoLoteDeTalhe { get; set; }
        public int IdConciliacaoLote { get; set; }
        public Nullable<decimal> DControleBanco { get; set; }
        public Nullable<decimal> DControleLote { get; set; }
        public Nullable<decimal> DControleRegistro { get; set; }
        public Nullable<decimal> DServicoRegistro { get; set; }
        public string DServicoSegmento { get; set; }
        public Nullable<decimal> DEmpresaInscricaoTipo { get; set; }
        public Nullable<decimal> DEmpresaInscricaoNumero { get; set; }
        public string DEmpresaConvenio { get; set; }
        public Nullable<decimal> DEmpresaContaCorrenteAgenciaCodigo { get; set; }
        public string DEmpresaContaCorrenteAgenciaDigito { get; set; }
        public Nullable<decimal> DEmpresaContaCorrenteContaNumero { get; set; }
        public string DEmpresaContaCorrenteContaDigito { get; set; }
        public string DEmpresaContaCorrenteDV { get; set; }
        public string DEmpresaNome { get; set; }
        public string DNatureza { get; set; }
        public Nullable<decimal> DTipoComplemento { get; set; }
        public string DComplemento { get; set; }
        public string DCpmf { get; set; }
        public Nullable<decimal> DData { get; set; }
        public Nullable<decimal> DLancamentoData { get; set; }
        public Nullable<decimal> DLancamentoValor { get; set; }
        public string DLancamentoTipo { get; set; }
        public Nullable<decimal> DLancamentoCategoria { get; set; }
        public string DLancamentoHistoricoCodigo { get; set; }
        public string DLancamentoHistorico { get; set; }
        public string DLancamentoDocumento { get; set; }
        public virtual ConciliacaoLote ConciliacaoLote { get; set; }
    }
}

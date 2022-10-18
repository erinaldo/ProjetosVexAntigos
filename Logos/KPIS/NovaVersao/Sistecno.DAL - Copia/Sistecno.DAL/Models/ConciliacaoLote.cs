using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConciliacaoLote
    {
        public ConciliacaoLote()
        {
            this.ConciliacaoLoteDetalhes = new List<ConciliacaoLoteDetalhe>();
        }

        public int IdConciliacaoLote { get; set; }
        public int IdConciliacao { get; set; }
        public Nullable<decimal> HControleBanco { get; set; }
        public Nullable<decimal> HControleLote { get; set; }
        public Nullable<decimal> HControleRegistro { get; set; }
        public string HServicoOperacao { get; set; }
        public Nullable<decimal> HServicoServico { get; set; }
        public Nullable<decimal> HServicoFormaLancamento { get; set; }
        public Nullable<decimal> HServicoLayOutDoLote { get; set; }
        public Nullable<decimal> HEmpresaIncricaoTipo { get; set; }
        public Nullable<decimal> HEmpresaIncricaoNumero { get; set; }
        public string HEmpresaConvenio { get; set; }
        public Nullable<decimal> HEmpresaContaCorrenteAgenciaCodigo { get; set; }
        public string HEmpresaContaCorrenteAgenciaDigito { get; set; }
        public Nullable<decimal> HEmpresaContaCorrenteContaNumero { get; set; }
        public string HEmpresaContaCorrenteContaDigito { get; set; }
        public string HEmpresaContaCorrenteDV { get; set; }
        public string HEmpresaNome { get; set; }
        public Nullable<decimal> HEmpresaSaldoInicialData { get; set; }
        public Nullable<decimal> HEmpresaSaldoInicialValor { get; set; }
        public string HEmpresaSaldoInicialSituacao { get; set; }
        public string HEmpresaSaldoInicialStatus { get; set; }
        public string HEmpresaSaldoInicialTipoDeMoeda { get; set; }
        public Nullable<decimal> HEmpresaSaldoInicialSequncia { get; set; }
        public Nullable<decimal> TControleBanco { get; set; }
        public Nullable<decimal> TControleLote { get; set; }
        public Nullable<decimal> TControleRegistro { get; set; }
        public Nullable<decimal> TEmpresaInscricaoTipo { get; set; }
        public Nullable<decimal> TEmpresaInscricaoNumero { get; set; }
        public string TEmpresaConvenio { get; set; }
        public Nullable<decimal> TEmpresaContaCorrenteAgenciaCodigo { get; set; }
        public string TEmpresaContaCorrenteAgenciaDigito { get; set; }
        public Nullable<decimal> TEmpresaContaCorrenteContaNumero { get; set; }
        public string TEmpresaContaCorrenteContaDigito { get; set; }
        public string TEmpresaContaCorrenteDV { get; set; }
        public Nullable<decimal> TValoresBloqueadoDiaAnterior { get; set; }
        public Nullable<decimal> TValoresLimite { get; set; }
        public Nullable<decimal> TValoresLoqueadoDia { get; set; }
        public Nullable<decimal> TSaldoFinalData { get; set; }
        public Nullable<decimal> TSaldoFinalValor { get; set; }
        public string TSaldoFinalSituacao { get; set; }
        public string TSaldoFinalStatus { get; set; }
        public Nullable<decimal> TTotaisQuantidadeDeRegistros { get; set; }
        public Nullable<decimal> TTotaisValorDebitos { get; set; }
        public Nullable<decimal> TTotaisValorCreditos { get; set; }
        public virtual Conciliacao Conciliacao { get; set; }
        public virtual ICollection<ConciliacaoLoteDetalhe> ConciliacaoLoteDetalhes { get; set; }
    }
}

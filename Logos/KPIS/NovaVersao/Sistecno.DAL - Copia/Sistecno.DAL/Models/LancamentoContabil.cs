using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LancamentoContabil
    {
        public int IDLancamentoContabil { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IDDocumentoOrigem { get; set; }
        public System.DateTime DataDeLancamento { get; set; }
        public Nullable<System.DateTime> DataDeCompetencia { get; set; }
        public string Historico { get; set; }
        public Nullable<decimal> ValorCredito { get; set; }
        public Nullable<decimal> ValorDebito { get; set; }
        public string Tabela { get; set; }
        public Nullable<int> Lote { get; set; }
        public Nullable<decimal> Ano { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public Nullable<int> IdContaContabilFilial { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<int> IdLancamento { get; set; }
        public string DebitoCredito { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<int> ID { get; set; }
        public string LoteParaAgrupamento { get; set; }
        public string Conciliado { get; set; }
        public Nullable<int> CodigoDeConciliacao { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

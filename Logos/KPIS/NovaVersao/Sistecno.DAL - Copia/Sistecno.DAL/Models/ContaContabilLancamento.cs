using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaContabilLancamento
    {
        public int IDContaContabilLancamento { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IDDocumentoOrigem { get; set; }
        public System.DateTime DataDeLancamento { get; set; }
        public System.DateTime DataDeCompetencia { get; set; }
        public string Historico { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public string DebitoCredito { get; set; }
        public string Tabela { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ContaContabil ContaContabil1 { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LancamentoPadraoConfiguracao
    {
        public int IDLancamentoPadraoConfiguracao { get; set; }
        public int IDLancamentoPadrao { get; set; }
        public int Sequencia { get; set; }
        public string TipoDeLancamento { get; set; }
        public string ContaContabilCredito { get; set; }
        public string ContaContabilDebito { get; set; }
        public string Valor { get; set; }
        public string Historico { get; set; }
        public string CentroDeCustoDebito { get; set; }
        public string CentroDeCustoCredito { get; set; }
        public string OrigemDoLancamento { get; set; }
        public string OutrasInformacoesCredito { get; set; }
        public string OutrasInformacoesDebito { get; set; }
        public string TipoDoSaldo { get; set; }
        public Nullable<int> IdContaContabilDebito { get; set; }
        public Nullable<int> IdContaContabilCredito { get; set; }
        public virtual LancamentoPadrao LancamentoPadrao { get; set; }
    }
}

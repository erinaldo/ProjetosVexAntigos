//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
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

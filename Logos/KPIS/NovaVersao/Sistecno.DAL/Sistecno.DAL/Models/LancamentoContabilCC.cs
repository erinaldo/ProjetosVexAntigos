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
    
    public partial class LancamentoContabilCC
    {
        public int IdLancamentoContabilCC { get; set; }
        public int IdCentroDeCustoFilial { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<int> IdLancamento { get; set; }
        public Nullable<int> IdLancamentoContabil { get; set; }
    }
}
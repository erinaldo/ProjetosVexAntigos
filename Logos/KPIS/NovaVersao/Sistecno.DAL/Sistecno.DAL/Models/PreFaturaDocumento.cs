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
    
    public partial class PreFaturaDocumento
    {
        public int IdPreFaturaDocumento { get; set; }
        public int IdPreFatura { get; set; }
        public string SerieDaNotaFiscal { get; set; }
        public Nullable<int> IdNotaFiscal { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<int> IdCtrc { get; set; }
        public Nullable<int> Ctrc { get; set; }
        public Nullable<decimal> ValorFrete { get; set; }
    
        public virtual PreFatura PreFatura { get; set; }
    }
}
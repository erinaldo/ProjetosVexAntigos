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
    
    public partial class AgrupamentoRegiao
    {
        public int IdAgrupamentoRegiao { get; set; }
        public int IdAgrupamento { get; set; }
        public int IdRegiao { get; set; }
    
        public virtual Agrupamento Agrupamento { get; set; }
        public virtual Regiao Regiao { get; set; }
    }
}

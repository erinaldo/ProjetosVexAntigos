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
    
    public partial class TipoDeMonitoramento
    {
        public TipoDeMonitoramento()
        {
            this.DT = new HashSet<DT>();
        }
    
        public int IdTipoDeMonitoramento { get; set; }
        public string Nome { get; set; }
    
        public virtual ICollection<DT> DT { get; set; }
    }
}
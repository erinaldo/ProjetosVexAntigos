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
    
    public partial class VeiculoLicenciamento
    {
        public int IdVeiculoLicenciamento { get; set; }
        public int IdVeiculoTipo { get; set; }
        public string FinalDaPlaca { get; set; }
        public System.DateTime DataLimite { get; set; }
    
        public virtual VeiculoTipo VeiculoTipo { get; set; }
    }
}

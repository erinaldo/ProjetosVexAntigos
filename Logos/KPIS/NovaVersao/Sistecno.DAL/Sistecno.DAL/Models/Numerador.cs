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
    
    public partial class Numerador
    {
        public int IDNumerador { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public string Nome { get; set; }
        public string Serie { get; set; }
        public int ProximoNumero { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
    }
}

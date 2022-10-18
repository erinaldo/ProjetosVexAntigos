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
    
    public partial class Transportadora
    {
        public Transportadora()
        {
            this.DT = new HashSet<DT>();
            this.Regiao = new HashSet<Regiao>();
            this.UsuarioDeTabelaDeFrete = new HashSet<UsuarioDeTabelaDeFrete>();
        }
    
        public int IDTransportadora { get; set; }
        public int IDContaContabil { get; set; }
    
        public virtual Cadastro Cadastro { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ICollection<DT> DT { get; set; }
        public virtual ICollection<Regiao> Regiao { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFrete { get; set; }
    }
}

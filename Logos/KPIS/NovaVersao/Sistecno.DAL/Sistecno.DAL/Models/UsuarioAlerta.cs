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
    
    public partial class UsuarioAlerta
    {
        public int IdUsuarioAlerta { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlerta { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public string Ativo { get; set; }
    
        public virtual Alerta Alerta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

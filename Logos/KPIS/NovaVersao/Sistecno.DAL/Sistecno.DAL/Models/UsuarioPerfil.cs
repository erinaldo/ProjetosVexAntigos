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
    
    public partial class UsuarioPerfil
    {
        public int IDUsuarioPerfil { get; set; }
        public int IDUsuario { get; set; }
        public int IDPerfil { get; set; }
        public int IDFilial { get; set; }
    
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}

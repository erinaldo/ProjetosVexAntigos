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
    
    public partial class UsuarioFavoritos
    {
        public int IDUsuarioFavoritos { get; set; }
        public int IDUsuario { get; set; }
        public int IDModuloOpcao { get; set; }
        public int Ordem { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}

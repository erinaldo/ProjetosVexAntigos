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
    
    public partial class UsuarioGradeCampo
    {
        public int IDUsuarioGradeCampo { get; set; }
        public int IDUsuarioGrade { get; set; }
        public string Campo { get; set; }
        public Nullable<int> Posicao { get; set; }
        public string Visivel { get; set; }
        public Nullable<int> Largura { get; set; }
    
        public virtual UsuarioGrade UsuarioGrade { get; set; }
    }
}

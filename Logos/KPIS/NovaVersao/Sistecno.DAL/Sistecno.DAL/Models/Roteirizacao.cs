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
    
    public partial class Roteirizacao
    {
        public Roteirizacao()
        {
            this.Roteirizacao1 = new HashSet<Roteirizacao>();
            this.Roteirizacao11 = new HashSet<Roteirizacao>();
        }
    
        public int IDRoteirizacao { get; set; }
        public int IDFilial { get; set; }
        public int IDRoteirizacaoTipo { get; set; }
        public int IDParent { get; set; }
    
        public virtual Filial Filial { get; set; }
        public virtual ICollection<Roteirizacao> Roteirizacao1 { get; set; }
        public virtual Roteirizacao Roteirizacao2 { get; set; }
        public virtual ICollection<Roteirizacao> Roteirizacao11 { get; set; }
        public virtual Roteirizacao Roteirizacao3 { get; set; }
    }
}
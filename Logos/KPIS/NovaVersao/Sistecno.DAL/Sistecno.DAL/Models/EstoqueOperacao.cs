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
    
    public partial class EstoqueOperacao
    {
        public EstoqueOperacao()
        {
            this.EstoqueDivisaoMov = new HashSet<EstoqueDivisaoMov>();
        }
    
        public int IDEstoqueOperacao { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string Sistema { get; set; }
    
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMov { get; set; }
    }
}
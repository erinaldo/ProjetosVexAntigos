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
    
    public partial class UnidadeDeMedida
    {
        public UnidadeDeMedida()
        {
            this.ProdutoCliente = new HashSet<ProdutoCliente>();
        }
    
        public int IDUnidadeDeMedida { get; set; }
        public string Unidade { get; set; }
        public string Nome { get; set; }
        public Nullable<int> Decimais { get; set; }
    
        public virtual ICollection<ProdutoCliente> ProdutoCliente { get; set; }
    }
}
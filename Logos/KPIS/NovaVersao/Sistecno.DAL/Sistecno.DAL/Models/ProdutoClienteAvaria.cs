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
    
    public partial class ProdutoClienteAvaria
    {
        public int IdProdutoClienteAvaria { get; set; }
        public int IdProdutoCliente { get; set; }
        public Nullable<decimal> Unidades { get; set; }
        public System.DateTime Data { get; set; }
        public string TipoDeAvaria { get; set; }
    
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}
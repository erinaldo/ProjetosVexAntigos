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
    
    public partial class ClienteSetorFilial
    {
        public int IdClienteSetorFilial { get; set; }
        public int IdCliente { get; set; }
        public Nullable<int> IdSetor { get; set; }
        public int IdFilial { get; set; }
        public string Roteiro { get; set; }
        public Nullable<int> NumeroDoRoteiro { get; set; }
        public Nullable<int> CodigoDoCliente { get; set; }
        public Nullable<int> CodigoDoClienteFilial { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
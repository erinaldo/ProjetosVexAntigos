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
    
    public partial class RPCIImagem
    {
        public int IdRpciImagem { get; set; }
        public int IdRpci { get; set; }
        public string Titulo { get; set; }
        public byte[] Imagem { get; set; }
    
        public virtual RPCI RPCI { get; set; }
    }
}
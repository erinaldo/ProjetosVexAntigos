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
    
    public partial class ControleBriefingItem
    {
        public int IdControleBriefingItem { get; set; }
        public int IdControleBriefing { get; set; }
        public int IdDocumento { get; set; }
        public int IdProdutoEmbalagem { get; set; }
        public string Operacao { get; set; }
        public Nullable<decimal> Valor { get; set; }
    }
}

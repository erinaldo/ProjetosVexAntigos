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
    
    public partial class ColetorConferenciaCDEAN
    {
        public int IdColetorConferenciaCDEAN { get; set; }
        public string CodigoBarras { get; set; }
        public string UnidadeVenda { get; set; }
        public string DescricaoUnidadeVenda { get; set; }
        public int QuantidadeUnidadeVenda { get; set; }
        public Nullable<System.DateTime> UltimaAtualizacao { get; set; }
    }
}
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
    
    public partial class PalletDocumentoItemCarregamento
    {
        public int IdPalletDocumentoItemCarregamento { get; set; }
        public int IdPalletDocumento { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
    }
}

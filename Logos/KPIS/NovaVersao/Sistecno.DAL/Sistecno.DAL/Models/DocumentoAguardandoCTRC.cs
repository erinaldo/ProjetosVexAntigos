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
    
    public partial class DocumentoAguardandoCTRC
    {
        public int IdDocumentoAguardandoCTRC { get; set; }
        public int IdFilial { get; set; }
        public int IdDocumento { get; set; }
        public Nullable<int> IdDocumentoFrete { get; set; }
    
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
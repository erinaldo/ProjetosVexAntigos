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
    
    public partial class DocumentoReclamacao
    {
        public int IdDocumentoReclamacao { get; set; }
        public int IdDocumento { get; set; }
        public int IdReclamacao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<System.DateTime> DataDaReclamacao { get; set; }
    
        public virtual Documento Documento { get; set; }
    }
}

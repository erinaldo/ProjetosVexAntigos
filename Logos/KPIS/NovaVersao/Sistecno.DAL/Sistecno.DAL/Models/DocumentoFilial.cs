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
    
    public partial class DocumentoFilial
    {
        public int IDDocumentoFilial { get; set; }
        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public int IDRegiaoItem { get; set; }
        public Nullable<int> IdRegiaoItemFilial { get; set; }
        public Nullable<int> IdRegiaoItemCliente { get; set; }
        public Nullable<int> IdRegiaoItemTransportador { get; set; }
        public string Situacao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdSetor { get; set; }
    
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
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
    
    public partial class DocumentoImposto
    {
        public int IdDocumentoImposto { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<decimal> Pis { get; set; }
        public Nullable<decimal> Cofins { get; set; }
        public Nullable<decimal> Inss { get; set; }
        public Nullable<decimal> IRRF { get; set; }
        public Nullable<decimal> CSLL { get; set; }
        public Nullable<int> IdCodigoDoServico { get; set; }
        public Nullable<decimal> AliquotaServicos { get; set; }
        public string IssRetido { get; set; }
    }
}
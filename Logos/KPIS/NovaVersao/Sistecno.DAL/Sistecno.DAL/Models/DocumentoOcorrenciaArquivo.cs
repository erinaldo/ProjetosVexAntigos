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
    
    public partial class DocumentoOcorrenciaArquivo
    {
        public int IDDocumentoOcorrenciaArquivo { get; set; }
        public int IDDocumentoOcorrencia { get; set; }
        public byte[] Arquivo { get; set; }
    
        public virtual DocumentoOcorrencia DocumentoOcorrencia { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoComprovante
    {
        public int IdDocumentoComprovante { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdDocumentoNotaFiscal { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Documento Documento1 { get; set; }
    }
}

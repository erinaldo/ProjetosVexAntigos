using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoNotaFiscal
    {
        public int IDDocumentoNotaFiscal { get; set; }
        public int IDDocumentoOrigem { get; set; }
        public int IDNotaFiscal { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Documento Documento1 { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoRelacionado
    {
        public int IdDocumentoRelacionado { get; set; }
        public int IdDocumentoPai { get; set; }
        public Nullable<int> IdDocumentoFilho { get; set; }
        public Nullable<int> IdAgrupamento { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Documento Documento1 { get; set; }
    }
}

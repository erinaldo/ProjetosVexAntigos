using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoObservacao
    {
        public int IDDocumentoObservacao { get; set; }
        public int IDDocumento { get; set; }
        public string Observacao { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

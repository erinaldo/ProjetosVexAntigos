using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
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

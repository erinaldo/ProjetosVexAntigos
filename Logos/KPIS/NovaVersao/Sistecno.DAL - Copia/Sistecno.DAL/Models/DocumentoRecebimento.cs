using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoRecebimento
    {
        public int IdDocumentoRecebimento { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public string Transferido { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<System.DateTime> DataDoRecebimento { get; set; }
        public string Status { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

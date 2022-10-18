using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoItemLabelPicking
    {
        public int IdDocumentoItemLabelPicking { get; set; }
        public int IdDocumentoItem { get; set; }
        public string Ordem { get; set; }
        public Nullable<System.DateTime> Impresso { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
    }
}

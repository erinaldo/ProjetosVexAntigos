using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoCDA
    {
        public int IdDocumentoCDA { get; set; }
        public int IdDocumento { get; set; }
        public string Situacao { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

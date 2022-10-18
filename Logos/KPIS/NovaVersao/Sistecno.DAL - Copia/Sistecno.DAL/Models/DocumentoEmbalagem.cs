using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoEmbalagem
    {
        public int IdDocumentoEmbalagem { get; set; }
        public int IdDocumento { get; set; }
        public int Ordem { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

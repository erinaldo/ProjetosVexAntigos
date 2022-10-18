using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoItemComplemento
    {
        public int IdDocumentoItemComplemento { get; set; }
        public int IdDocumentoItem { get; set; }
        public string Complemento { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
    }
}

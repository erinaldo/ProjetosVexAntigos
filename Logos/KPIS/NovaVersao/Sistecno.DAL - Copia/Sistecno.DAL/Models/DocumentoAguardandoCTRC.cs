using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoAguardandoCTRC
    {
        public int IdDocumentoAguardandoCTRC { get; set; }
        public int IdFilial { get; set; }
        public int IdDocumento { get; set; }
        public Nullable<int> IdDocumentoFrete { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

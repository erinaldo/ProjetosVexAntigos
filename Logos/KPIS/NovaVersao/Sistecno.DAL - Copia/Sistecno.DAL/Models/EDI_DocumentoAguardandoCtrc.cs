using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoAguardandoCtrc
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IdDocumentoAguardandoCTRC { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public Nullable<int> IdDocumento { get; set; }
    }
}

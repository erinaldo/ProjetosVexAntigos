using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoNotaFiscal
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDDocumentoNotaFiscal { get; set; }
        public Nullable<int> IDDocumentoOrigem { get; set; }
        public Nullable<int> IDNotaFiscal { get; set; }
    }
}

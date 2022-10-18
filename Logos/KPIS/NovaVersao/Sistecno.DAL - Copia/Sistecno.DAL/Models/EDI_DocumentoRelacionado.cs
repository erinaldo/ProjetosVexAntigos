using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoRelacionado
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IdDocumentoRelacionado { get; set; }
        public Nullable<int> IdDocumentoPai { get; set; }
        public Nullable<int> IdDocumentoFilho { get; set; }
        public Nullable<int> IdAgrupamento { get; set; }
        public Nullable<int> IdEDI_DocumentoRelacionado { get; set; }
    }
}

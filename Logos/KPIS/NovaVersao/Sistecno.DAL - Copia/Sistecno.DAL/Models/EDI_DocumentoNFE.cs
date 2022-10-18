using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoNFE
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IdDocumentoNfe { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdDocumento { get; set; }
    }
}

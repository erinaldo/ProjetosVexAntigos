using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoFilial
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDDocumentoFilial { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public Nullable<int> IDRegiaoItem { get; set; }
        public Nullable<int> IdRegiaoItemFilial { get; set; }
        public Nullable<int> IdRegiaoItemCliente { get; set; }
        public Nullable<int> IdRegiaoItemTransportador { get; set; }
        public string Situacao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    }
}

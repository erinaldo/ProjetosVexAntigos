using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class edi_gerado_antigo
    {
        public int IdEdi_Gerado { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
    }
}

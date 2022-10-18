using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoRemessa
    {
        public int IdDocumentoRemessa { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdUsuarioEnvio { get; set; }
        public Nullable<System.DateTime> DataEnvio { get; set; }
        public Nullable<int> IdUsuarioRecebeu { get; set; }
        public Nullable<System.DateTime> DataRecebimento { get; set; }
        public string Situacao { get; set; }
    }
}

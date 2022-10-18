using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoMigrado
    {
        public int IdDocumentoMigrado { get; set; }
        public string Conhecimento { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
    }
}

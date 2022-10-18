using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoEdi
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public int IdDocumentoEdi { get; set; }
        public Nullable<int> IdEdi { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string NomeDoArquivo { get; set; }
    }
}

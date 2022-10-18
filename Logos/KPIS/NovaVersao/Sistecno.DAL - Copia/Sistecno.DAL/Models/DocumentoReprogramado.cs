using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoReprogramado
    {
        public int IdDocumentoReprogramado { get; set; }
        public int IdDocumento { get; set; }
        public System.DateTime DataParaEntrega { get; set; }
        public System.DateTime DataSugerida { get; set; }
        public Nullable<System.DateTime> DataGeracaoDoArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoEdi
    {
        public int IdDocumentoEdi { get; set; }
        public Nullable<int> IdEdi { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string NomeDoArquivo { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual EDI EDI { get; set; }
        public virtual Titulo Titulo { get; set; }
    }
}

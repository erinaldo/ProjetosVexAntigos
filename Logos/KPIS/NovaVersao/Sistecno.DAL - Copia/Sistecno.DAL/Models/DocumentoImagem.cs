using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoImagem
    {
        public int IDDocumentoImagem { get; set; }
        public int IDDocumento { get; set; }
        public string Titulo { get; set; }
        public byte[] Imagem { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

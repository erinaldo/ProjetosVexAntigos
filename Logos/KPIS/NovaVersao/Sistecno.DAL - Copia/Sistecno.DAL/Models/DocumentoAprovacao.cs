using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoAprovacao
    {
        public int IdDocumentoAprovacao { get; set; }
        public int IdDocumento { get; set; }
        public int UltimaSequenciaAprovacao { get; set; }
        public int UltimoIdUsuario { get; set; }
    }
}

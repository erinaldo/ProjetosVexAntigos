using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoOcorrenciaArquivo
    {
        public int IDDocumentoOcorrenciaArquivo { get; set; }
        public int IDDocumentoOcorrencia { get; set; }
        public byte[] Arquivo { get; set; }
        public virtual DocumentoOcorrencia DocumentoOcorrencia { get; set; }
    }
}

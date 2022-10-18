using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoAFaturar
    {
        public int IdDocumentoAFaturar { get; set; }
        public int IdDocumento { get; set; }
        public int IdFilial { get; set; }
        public string TipoDeFatura { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

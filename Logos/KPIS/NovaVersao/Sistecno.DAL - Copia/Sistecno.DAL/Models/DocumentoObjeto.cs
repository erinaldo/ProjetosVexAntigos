using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoObjeto
    {
        public DocumentoObjeto()
        {
            this.DocumentoObjetoOcorrencias = new List<DocumentoObjetoOcorrencia>();
        }

        public int IdDocumentoObjeto { get; set; }
        public int IdDocumento { get; set; }
        public string Objeto { get; set; }
        public string Situacao { get; set; }
        public Nullable<int> Ordem { get; set; }
        public Nullable<int> Volumes { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual ICollection<DocumentoObjetoOcorrencia> DocumentoObjetoOcorrencias { get; set; }
    }
}

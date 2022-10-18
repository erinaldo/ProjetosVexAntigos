using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoOcorrenciaItem
    {
        public int IdDocumentoOcorrenciaItem { get; set; }
        public int IdDocumentoOcorrencia { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<decimal> TotalDoItem { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
        public virtual DocumentoOcorrencia DocumentoOcorrencia { get; set; }
    }
}

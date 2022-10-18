using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoItemEmpenhado
    {
        public int IdDocumentoItemEmpenhado { get; set; }
        public int IdDocumentoItem { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
    }
}

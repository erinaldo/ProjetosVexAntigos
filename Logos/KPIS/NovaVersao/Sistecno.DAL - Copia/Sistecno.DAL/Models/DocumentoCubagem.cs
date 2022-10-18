using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoCubagem
    {
        public int IDDocumentoCubagem { get; set; }
        public int IDDocumento { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Profundidade { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> VolumeTotal { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

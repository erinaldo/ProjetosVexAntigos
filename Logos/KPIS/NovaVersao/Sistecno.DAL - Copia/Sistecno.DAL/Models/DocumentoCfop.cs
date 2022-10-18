using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoCfop
    {
        public int IDDocumentoCfop { get; set; }
        public int IDDocumento { get; set; }
        public int IDCfop { get; set; }
        public decimal Valor { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public virtual Cfop Cfop { get; set; }
        public virtual Documento Documento { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioDocumentoItem
    {
        public int IDRomaneioDocumentoItem { get; set; }
        public int IDRomaneioDocumento { get; set; }
        public int IDDocumentoItem { get; set; }
        public decimal Quantidade { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> QuantidadeUnidadeEstoque { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
    }
}

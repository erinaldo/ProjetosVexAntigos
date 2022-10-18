using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BorderoTituloDuplicata
    {
        public int IDBorderoTituloDuplicata { get; set; }
        public int IDBordero { get; set; }
        public int IDTituloDuplicata { get; set; }
        public virtual Bordero Bordero { get; set; }
        public virtual TituloDuplicata TituloDuplicata { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PreFaturaDocumento
    {
        public int IdPreFaturaDocumento { get; set; }
        public int IdPreFatura { get; set; }
        public string SerieDaNotaFiscal { get; set; }
        public Nullable<int> IdNotaFiscal { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<int> IdCtrc { get; set; }
        public Nullable<int> Ctrc { get; set; }
        public Nullable<decimal> ValorFrete { get; set; }
        public virtual PreFatura PreFatura { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TipoDeTituloDuplicata
    {
        public int IdTipoDeTituloDuplicata { get; set; }
        public Nullable<int> IdTipoDeTitulo { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public virtual TipoDeTitulo TipoDeTitulo { get; set; }
    }
}

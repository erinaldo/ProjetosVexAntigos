using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TipoDeTitulo
    {
        public TipoDeTitulo()
        {
            this.TipoDeTituloDuplicatas = new List<TipoDeTituloDuplicata>();
        }

        public int IdTipoDeTitulo { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public string Tipo { get; set; }
        public virtual ICollection<TipoDeTituloDuplicata> TipoDeTituloDuplicatas { get; set; }
    }
}

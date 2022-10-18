using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RoteirizacaoTipo
    {
        public RoteirizacaoTipo()
        {
            this.RegiaoItems = new List<RegiaoItem>();
        }

        public int IDRoteirizacaoTipo { get; set; }
        public decimal Ordem { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
    }
}

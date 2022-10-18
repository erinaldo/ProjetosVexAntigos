using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoRomaneio
    {
        public MovimentacaoRomaneio()
        {
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
        }

        public int IDMovimentacaoRomaneio { get; set; }
        public Nullable<int> IDMovimentacao { get; set; }
        public Nullable<int> IDRomaneio { get; set; }
        public virtual Movimentacao Movimentacao { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
    }
}

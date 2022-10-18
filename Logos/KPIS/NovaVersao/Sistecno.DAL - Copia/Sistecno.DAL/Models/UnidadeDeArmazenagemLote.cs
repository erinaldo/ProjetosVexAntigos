using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeDeArmazenagemLote
    {
        public UnidadeDeArmazenagemLote()
        {
            this.EstoqueComprasMovs = new List<EstoqueComprasMov>();
            this.InventarioUas = new List<InventarioUa>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
        }

        public int IDUnidadeDeArmazenagemLote { get; set; }
        public int IDUnidadeDeArmazenagem { get; set; }
        public int IDLote { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> Lastro { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public string Divisao { get; set; }
        public Nullable<decimal> SaldoPicking { get; set; }
        public string obs { get; set; }
        public virtual ICollection<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public virtual ICollection<InventarioUa> InventarioUas { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
    }
}

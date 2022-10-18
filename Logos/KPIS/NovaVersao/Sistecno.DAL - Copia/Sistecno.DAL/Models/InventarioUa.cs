using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class InventarioUa
    {
        public int IdInventarioUa { get; set; }
        public int IdInventarioContagem { get; set; }
        public int IdUnidadeDeArmazenagemLote { get; set; }
        public string Status { get; set; }
        public virtual InventarioContagem InventarioContagem { get; set; }
        public virtual UnidadeDeArmazenagemLote UnidadeDeArmazenagemLote { get; set; }
    }
}

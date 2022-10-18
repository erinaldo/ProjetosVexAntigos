using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ControleBriefingItem
    {
        public int IdControleBriefingItem { get; set; }
        public int IdControleBriefing { get; set; }
        public int IdDocumento { get; set; }
        public int IdProdutoEmbalagem { get; set; }
        public string Operacao { get; set; }
        public Nullable<decimal> Valor { get; set; }
    }
}

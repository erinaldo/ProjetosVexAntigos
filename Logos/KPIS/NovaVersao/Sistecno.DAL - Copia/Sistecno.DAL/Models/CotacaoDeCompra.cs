using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CotacaoDeCompra
    {
        public int IdCotacaoDeCompra { get; set; }
        public int IdFilial { get; set; }
        public string Status { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IdUsuarioCompra { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public string Adiantamento { get; set; }
    }
}

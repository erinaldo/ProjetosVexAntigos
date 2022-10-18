using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioOperacao
    {
        public int IdUsuarioOperacao { get; set; }
        public int IdUsuario { get; set; }
        public int IdOperacao { get; set; }
        public Nullable<int> IdCentroDeCusto { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

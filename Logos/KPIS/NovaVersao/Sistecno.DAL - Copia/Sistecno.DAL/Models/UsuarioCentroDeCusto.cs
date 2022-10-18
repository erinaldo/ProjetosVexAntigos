using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCentroDeCusto
    {
        public int IdUsuarioCentroDeCusto { get; set; }
        public int IdUsuario { get; set; }
        public int IdCentroDeCusto { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

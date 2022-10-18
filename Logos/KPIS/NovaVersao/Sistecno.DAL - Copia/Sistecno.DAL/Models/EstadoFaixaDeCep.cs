using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstadoFaixaDeCep
    {
        public int IDEstadoFaixaDeCep { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public string CepInicial { get; set; }
        public string CepFinal { get; set; }
        public virtual Estado Estado { get; set; }
    }
}

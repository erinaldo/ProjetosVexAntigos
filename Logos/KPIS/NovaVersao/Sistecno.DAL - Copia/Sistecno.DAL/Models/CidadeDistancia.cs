using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CidadeDistancia
    {
        public int IdCidadeDistancia { get; set; }
        public int IdFilialOrigem { get; set; }
        public int IdCidadeDestino { get; set; }
        public Nullable<decimal> Distancia { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

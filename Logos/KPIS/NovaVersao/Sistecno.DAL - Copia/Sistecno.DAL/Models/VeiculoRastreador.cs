using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoRastreador
    {
        public VeiculoRastreador()
        {
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDVeiculoRastreador { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
    }
}

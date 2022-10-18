using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoMarca
    {
        public VeiculoMarca()
        {
            this.VeiculoModeloes = new List<VeiculoModelo>();
        }

        public int IDVeiculoMarca { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<VeiculoModelo> VeiculoModeloes { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoModelo
    {
        public VeiculoModelo()
        {
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDVeiculoModelo { get; set; }
        public int IDVeiculoMarca { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
        public virtual VeiculoMarca VeiculoMarca { get; set; }
    }
}

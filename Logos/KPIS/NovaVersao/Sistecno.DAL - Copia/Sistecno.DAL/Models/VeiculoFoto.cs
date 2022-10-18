using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoFoto
    {
        public int IDVeiculoFoto { get; set; }
        public int IDVeiculo { get; set; }
        public byte[] Foto { get; set; }
        public string Descricao { get; set; }
        public virtual Veiculo Veiculo { get; set; }
    }
}

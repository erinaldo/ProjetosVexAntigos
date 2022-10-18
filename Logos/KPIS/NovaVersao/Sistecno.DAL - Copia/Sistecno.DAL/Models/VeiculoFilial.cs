using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoFilial
    {
        public VeiculoFilial()
        {
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
        }

        public int IDVeiculoFilial { get; set; }
        public int IDVeiculo { get; set; }
        public int IDFilial { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public virtual Veiculo Veiculo { get; set; }
    }
}

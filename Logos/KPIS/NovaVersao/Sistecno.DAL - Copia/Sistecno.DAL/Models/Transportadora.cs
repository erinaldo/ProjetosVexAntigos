using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Transportadora
    {
        public Transportadora()
        {
            this.DTs = new List<DT>();
            this.Regiaos = new List<Regiao>();
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
        }

        public int IDTransportadora { get; set; }
        public int IDContaContabil { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<Regiao> Regiaos { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
    }
}

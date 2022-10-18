using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Proprietario
    {
        public Proprietario()
        {
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDProprietario { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Agendum
    {
        public int IdAgenda { get; set; }
        public int IdCadastro { get; set; }
        public int IdCadastroContato { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}

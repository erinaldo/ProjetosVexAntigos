using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Agregado
    {
        public int IdAgregado { get; set; }
        public int IdContaContabil { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}

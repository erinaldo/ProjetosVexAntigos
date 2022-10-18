using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PortariaVisitante
    {
        public int IdPortariaVisitante { get; set; }
        public int IdPortaria { get; set; }
        public int IdCadastro { get; set; }
        public string Tipo { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Portaria Portaria { get; set; }
    }
}

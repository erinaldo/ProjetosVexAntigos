using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            this.Funcionarios = new List<Funcionario>();
        }

        public int IDDepartamento { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}

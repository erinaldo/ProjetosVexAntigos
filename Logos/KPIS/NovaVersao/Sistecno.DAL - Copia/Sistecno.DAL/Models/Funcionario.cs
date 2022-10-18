using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Funcionario
    {
        public int IdFuncionario { get; set; }
        public int IdFilial { get; set; }
        public int IdDepartamento { get; set; }
        public string Matricula { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Filial Filial { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PlanoDeContas_Ant
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Conta { get; set; }
        public string CodigoReduzido { get; set; }
        public string Nome { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IdParent { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
    }
}

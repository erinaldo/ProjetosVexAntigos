using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Apolouse
    {
        public int IdApolice { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IdCadastroSeguradora { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public string Ativo { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
    }
}

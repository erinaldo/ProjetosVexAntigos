using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroEntrega
    {
        public int IdCadastroEntrega { get; set; }
        public int IdCadastro { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdUsuarioAprovou { get; set; }
        public string Observacoes { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioAlerta
    {
        public int IdUsuarioAlerta { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlerta { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public string Ativo { get; set; }
        public virtual Alerta Alerta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

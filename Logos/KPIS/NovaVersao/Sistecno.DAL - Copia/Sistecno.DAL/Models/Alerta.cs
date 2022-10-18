using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Alerta
    {
        public Alerta()
        {
            this.CadastroContatoAlertas = new List<CadastroContatoAlerta>();
            this.UsuarioAlertas = new List<UsuarioAlerta>();
        }

        public int IDAlerta { get; set; }
        public string Descricao { get; set; }
        public string Rotina { get; set; }
        public Nullable<int> ExecutarEmMinutos { get; set; }
        public virtual ICollection<CadastroContatoAlerta> CadastroContatoAlertas { get; set; }
        public virtual ICollection<UsuarioAlerta> UsuarioAlertas { get; set; }
    }
}

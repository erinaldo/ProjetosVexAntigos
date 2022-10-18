using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioFilial
    {
        public int idUsuarioFilial { get; set; }
        public int IdFilial { get; set; }
        public string Permissao { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual UsuarioFilial UsuarioFilial1 { get; set; }
        public virtual UsuarioFilial UsuarioFilial2 { get; set; }
    }
}

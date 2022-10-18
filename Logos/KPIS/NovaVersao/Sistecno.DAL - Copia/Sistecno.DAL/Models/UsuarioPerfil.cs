using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioPerfil
    {
        public int IDUsuarioPerfil { get; set; }
        public int IDUsuario { get; set; }
        public int IDPerfil { get; set; }
        public int IDFilial { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}

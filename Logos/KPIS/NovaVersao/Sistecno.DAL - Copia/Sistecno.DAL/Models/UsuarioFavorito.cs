using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioFavorito
    {
        public int IDUsuarioFavoritos { get; set; }
        public int IDUsuario { get; set; }
        public int IDModuloOpcao { get; set; }
        public int Ordem { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

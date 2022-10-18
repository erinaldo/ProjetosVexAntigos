using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCliente
    {
        public int IDUsuarioCliente { get; set; }
        public int IDUsuario { get; set; }
        public int IDCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

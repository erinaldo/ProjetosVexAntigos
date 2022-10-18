using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioOpcaoAcesso
    {
        public int IDUsuarioOpcaoAcesso { get; set; }
        public int IDUsuarioOpcao { get; set; }
        public string Objeto { get; set; }
        public string Acesso { get; set; }
        public virtual UsuarioOpcao UsuarioOpcao { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LoginPerfilPermissao
    {
        public int IDLoginPerfilPermissao { get; set; }
        public int IDLoginPerfil { get; set; }
        public int IDModulo { get; set; }
        public string Programa { get; set; }
        public virtual LoginPerfil LoginPerfil { get; set; }
    }
}

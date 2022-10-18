using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LoginPerfil
    {
        public LoginPerfil()
        {
            this.LoginPerfilPermissaos = new List<LoginPerfilPermissao>();
        }

        public int IDLoginPerfil { get; set; }
        public int IDGrupo { get; set; }
        public string Descricao { get; set; }
        public string CriaUsuario { get; set; }
        public string Administrador { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<LoginPerfilPermissao> LoginPerfilPermissaos { get; set; }
    }
}

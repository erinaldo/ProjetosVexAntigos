using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            this.Empresas = new List<Empresa>();
            this.LoginPerfils = new List<LoginPerfil>();
            this.Usuarios = new List<Usuario>();
        }

        public int IDGrupo { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<LoginPerfil> LoginPerfils { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}

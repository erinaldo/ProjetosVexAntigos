using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioOpcao
    {
        public UsuarioOpcao()
        {
            this.UsuarioOpcaoAcessoes = new List<UsuarioOpcaoAcesso>();
        }

        public int IDUsuarioOpcao { get; set; }
        public int IDUsuario { get; set; }
        public int IDModuloOpcao { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public string Permissao { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
        public virtual ICollection<UsuarioOpcaoAcesso> UsuarioOpcaoAcessoes { get; set; }
    }
}

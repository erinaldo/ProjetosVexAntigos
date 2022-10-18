using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioGrade
    {
        public UsuarioGrade()
        {
            this.UsuarioGradeCampoes = new List<UsuarioGradeCampo>();
        }

        public int IDUsuarioGrade { get; set; }
        public int IDUsuario { get; set; }
        public string Formulario { get; set; }
        public string Grade { get; set; }
        public string Descricao { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<UsuarioGradeCampo> UsuarioGradeCampoes { get; set; }
    }
}

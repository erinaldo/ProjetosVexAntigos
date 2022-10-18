using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioGradeCampo
    {
        public int IDUsuarioGradeCampo { get; set; }
        public int IDUsuarioGrade { get; set; }
        public string Campo { get; set; }
        public Nullable<int> Posicao { get; set; }
        public string Visivel { get; set; }
        public Nullable<int> Largura { get; set; }
        public virtual UsuarioGrade UsuarioGrade { get; set; }
    }
}

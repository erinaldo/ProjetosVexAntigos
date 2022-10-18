using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloHistorico
    {
        public int IDTituloHistorico { get; set; }
        public int IDTitulo { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public virtual Titulo Titulo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

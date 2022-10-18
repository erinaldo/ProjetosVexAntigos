using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Resposta
    {
        public int IDUsuario { get; set; }
        public string Formulario { get; set; }
        public string Componente { get; set; }
        public string Propriedade { get; set; }
        public Nullable<byte> Linha { get; set; }
        public string Valor { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

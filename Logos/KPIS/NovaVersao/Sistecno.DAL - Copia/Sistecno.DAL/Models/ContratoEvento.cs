using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoEvento
    {
        public int IdContratoEvento { get; set; }
        public string Descricao { get; set; }
        public string TipoDeDado { get; set; }
        public string Comentario { get; set; }
    }
}

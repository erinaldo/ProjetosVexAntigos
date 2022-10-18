using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesaEvento
    {
        public int IdContaDespesaEvento { get; set; }
        public string Descricao { get; set; }
        public string TipoDeDado { get; set; }
        public string Comentario { get; set; }
    }
}

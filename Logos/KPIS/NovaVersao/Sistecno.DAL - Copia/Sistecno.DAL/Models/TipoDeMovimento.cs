using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TipoDeMovimento
    {
        public int IdTipoDeMovimento { get; set; }
        public string Descricao { get; set; }
        public string EntradaSaida { get; set; }
        public string TipoDeDocumento { get; set; }
    }
}

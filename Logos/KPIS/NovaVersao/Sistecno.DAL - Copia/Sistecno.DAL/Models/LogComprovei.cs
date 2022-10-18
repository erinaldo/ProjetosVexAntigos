using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LogComprovei
    {
        public int IdDocumento { get; set; }
        public string Chave { get; set; }
        public System.DateTime DataHora { get; set; }
        public string Resultado { get; set; }
        public string XML { get; set; }
    }
}

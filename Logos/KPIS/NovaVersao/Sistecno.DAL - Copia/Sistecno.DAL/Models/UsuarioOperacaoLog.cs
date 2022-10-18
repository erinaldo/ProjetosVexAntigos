using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioOperacaoLog
    {
        public int IdUsuarioOperacaoLog { get; set; }
        public int IdUsuarioOperacao { get; set; }
        public System.DateTime DataHora { get; set; }
        public string Historico { get; set; }
    }
}

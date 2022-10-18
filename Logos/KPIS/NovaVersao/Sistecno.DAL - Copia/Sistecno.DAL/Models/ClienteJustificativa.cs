using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteJustificativa
    {
        public int IdClienteJustificativa { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
    }
}

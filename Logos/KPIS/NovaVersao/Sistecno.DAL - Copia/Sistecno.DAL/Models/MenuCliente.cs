using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MenuCliente
    {
        public int IdMenuCliente { get; set; }
        public int IdCliente { get; set; }
        public int IdModuloOpcao { get; set; }
        public string Nome { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
    }
}

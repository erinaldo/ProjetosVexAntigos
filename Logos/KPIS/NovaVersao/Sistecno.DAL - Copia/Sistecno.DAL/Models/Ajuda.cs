using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Ajuda
    {
        public int IDModuloOpcao { get; set; }
        public string Campo { get; set; }
        public string Ajuda1 { get; set; }
        public string AjudaUsuario { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
    }
}

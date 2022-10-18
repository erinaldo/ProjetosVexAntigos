using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModuloOpcaoAcao
    {
        public int IDModuloOpcaoAcao { get; set; }
        public int IDModuloOpcao { get; set; }
        public string Descricao { get; set; }
        public string Componente { get; set; }
        public string Ativo { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
    }
}

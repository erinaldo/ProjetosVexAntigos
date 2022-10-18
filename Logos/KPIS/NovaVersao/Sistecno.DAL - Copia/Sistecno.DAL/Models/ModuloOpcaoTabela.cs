using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModuloOpcaoTabela
    {
        public int IDModuloOpcaoTabela { get; set; }
        public int IDModuloOpcao { get; set; }
        public string Tabela { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
    }
}

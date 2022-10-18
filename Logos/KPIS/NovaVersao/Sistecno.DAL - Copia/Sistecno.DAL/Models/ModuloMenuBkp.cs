using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModuloMenuBkp
    {
        public int IDModuloMenu { get; set; }
        public Nullable<int> IDModulo { get; set; }
        public Nullable<int> IDModuloOpcao { get; set; }
        public string Parametro { get; set; }
        public int Ordem { get; set; }
        public Nullable<int> IDParente { get; set; }
        public string Ativo { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MenuSite
    {
        public int IdMenuSite { get; set; }
        public Nullable<int> IdModuloOpcao { get; set; }
        public Nullable<int> IdParente { get; set; }
        public string Ativo { get; set; }
        public string Visao { get; set; }
        public string ICONE { get; set; }
    }
}

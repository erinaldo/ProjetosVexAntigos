using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TESControle
    {
        public int IDTESControle { get; set; }
        public Nullable<int> IDTES { get; set; }
        public Nullable<int> IDModuloOpcao { get; set; }
        public Nullable<int> PROGRAMA { get; set; }
        public string Ativo { get; set; }
        public virtual ModuloOpcao ModuloOpcao { get; set; }
        public virtual TE TE { get; set; }
    }
}

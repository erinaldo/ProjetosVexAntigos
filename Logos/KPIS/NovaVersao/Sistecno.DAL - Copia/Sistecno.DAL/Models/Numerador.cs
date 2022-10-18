using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Numerador
    {
        public int IDNumerador { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public string Nome { get; set; }
        public string Serie { get; set; }
        public int ProximoNumero { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
    }
}

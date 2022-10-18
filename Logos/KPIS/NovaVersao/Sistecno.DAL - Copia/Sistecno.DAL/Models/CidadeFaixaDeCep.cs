using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CidadeFaixaDeCep
    {
        public int IDCidadeFaixaDeCep { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public string CepInicial { get; set; }
        public string CepFinal { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoLicenciamento
    {
        public int IdVeiculoLicenciamento { get; set; }
        public int IdVeiculoTipo { get; set; }
        public string FinalDaPlaca { get; set; }
        public System.DateTime DataLimite { get; set; }
        public virtual VeiculoTipo VeiculoTipo { get; set; }
    }
}

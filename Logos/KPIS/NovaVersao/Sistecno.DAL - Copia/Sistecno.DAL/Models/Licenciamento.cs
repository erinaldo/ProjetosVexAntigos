using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Licenciamento
    {
        public Licenciamento()
        {
            this.LicenciamentoMes = new List<LicenciamentoMe>();
        }

        public int IdLicenciamento { get; set; }
        public string Tipo { get; set; }
        public virtual ICollection<LicenciamentoMe> LicenciamentoMes { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LicenciamentoMe
    {
        public int IdLicenciamentoMes { get; set; }
        public int IdLicenciamento { get; set; }
        public string FinalDaPlaca { get; set; }
        public string Mes { get; set; }
        public virtual Licenciamento Licenciamento { get; set; }
    }
}

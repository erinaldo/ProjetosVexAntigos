using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_FAIXA_LOCALIDADE
    {
        public int LOC_NU { get; set; }
        public string LOC_CEP_INI { get; set; }
        public string LOC_CEP_FIM { get; set; }
        public Nullable<System.DateTime> Incluido { get; set; }
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
    }
}

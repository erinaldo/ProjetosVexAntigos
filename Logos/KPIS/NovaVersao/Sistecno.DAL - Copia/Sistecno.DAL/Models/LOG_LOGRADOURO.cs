using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_LOGRADOURO
    {
        public int LOG_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public Nullable<int> BAI_NU_INI { get; set; }
        public Nullable<int> BAI_NU_FIM { get; set; }
        public string LOG_NO { get; set; }
        public string LOG_COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string TLO_TX { get; set; }
        public string LOG_STA_TLO { get; set; }
        public string LOG_NO_ABREV { get; set; }
        public Nullable<System.DateTime> Incluido { get; set; }
    }
}

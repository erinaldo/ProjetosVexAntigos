using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_BAIRRO
    {
        public int BAI_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public string BAI_NO { get; set; }
        public string BAI_NO_ABREV { get; set; }
        public Nullable<System.DateTime> Incluido { get; set; }
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
    }
}

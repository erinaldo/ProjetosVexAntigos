using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_GRANDE_USUARIO
    {
        public int GRU_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public int BAI_NU { get; set; }
        public Nullable<int> LOG_NU { get; set; }
        public string GRU_NO { get; set; }
        public string GRU_ENDERECO { get; set; }
        public string CEP { get; set; }
        public string GRU_NO_ABREV { get; set; }
    }
}

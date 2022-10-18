using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_UNID_OPER
    {
        public int UOP_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public int BAI_NU { get; set; }
        public Nullable<int> LOG_NU { get; set; }
        public string UOP_NO { get; set; }
        public string UOP_ENDERECO { get; set; }
        public string CEP { get; set; }
        public string UOP_IN_CP { get; set; }
        public string UOP_NO_ABREV { get; set; }
    }
}

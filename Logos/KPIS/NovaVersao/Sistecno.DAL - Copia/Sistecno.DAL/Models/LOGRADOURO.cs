using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOGRADOURO
    {
        public int CD_LOGRADOURO { get; set; }
        public int CD_BAIRRO { get; set; }
        public string CD_TIPO_LOGRADOUROS { get; set; }
        public string DS_LOGRADOURO_NOME { get; set; }
        public string NO_LOGRADOURO_CEP { get; set; }
        public Nullable<int> CD_ID_CIDADE { get; set; }
    }
}

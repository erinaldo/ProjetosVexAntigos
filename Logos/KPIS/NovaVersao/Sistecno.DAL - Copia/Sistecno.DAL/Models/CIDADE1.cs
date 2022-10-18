using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CIDADE1
    {
        public Nullable<int> CD_UF { get; set; }
        public int CD_CIDADE { get; set; }
        public string DS_CIDADE_NOME { get; set; }
        public Nullable<double> CD_AREA { get; set; }
        public string CODIGO_IBGE { get; set; }
        public string UF { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Bairro
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public int IDBairro { get; set; }
        public int IDCidade { get; set; }
        public string Nome { get; set; }
    }
}

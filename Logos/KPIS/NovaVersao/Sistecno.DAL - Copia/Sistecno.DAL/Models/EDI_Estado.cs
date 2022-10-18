using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Estado
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public int IDEstado { get; set; }
        public int IDPais { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
    }
}

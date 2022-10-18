using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Cidade
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public int IDCidade { get; set; }
        public int IDEstado { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Tipo { get; set; }
        public string CodificarPor { get; set; }
    }
}

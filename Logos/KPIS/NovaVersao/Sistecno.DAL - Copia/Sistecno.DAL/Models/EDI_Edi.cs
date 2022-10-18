using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Edi
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDEDI { get; set; }
        public string Descricao { get; set; }
        public string Metodo { get; set; }
        public string TabelasEnvolvidas { get; set; }
        public string EntradaSaida { get; set; }
        public string Sistema { get; set; }
    }
}

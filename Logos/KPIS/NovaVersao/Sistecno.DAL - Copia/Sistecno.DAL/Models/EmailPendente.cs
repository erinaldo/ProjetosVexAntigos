using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EmailPendente
    {
        public string Email { get; set; }
        public int IdEmailPendente { get; set; }
        public string Conteudo { get; set; }
        public Nullable<System.DateTime> DataHoraEnvio { get; set; }
        public string Assunto { get; set; }
        public string CC { get; set; }
    }
}

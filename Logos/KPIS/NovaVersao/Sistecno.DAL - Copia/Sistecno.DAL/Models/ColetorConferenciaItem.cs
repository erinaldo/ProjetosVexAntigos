using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ColetorConferenciaItem
    {
        public int IdColetorConferenciaItem { get; set; }
        public int IdColetorConferencia { get; set; }
        public string CodigoDeBarras { get; set; }
        public int Quantidade { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public virtual ColetorConferencia ColetorConferencia { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ColetorConferenciaVolume
    {
        public int IdColetorConferenciaVolume { get; set; }
        public int IdColetorConferencia { get; set; }
        public string CodigoDeBarras { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public virtual ColetorConferencia ColetorConferencia { get; set; }
    }
}

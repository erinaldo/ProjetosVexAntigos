using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioConferencia
    {
        public RomaneioConferencia()
        {
            this.RomaneioConferenciaItems = new List<RomaneioConferenciaItem>();
        }

        public int IdRomaneioConferencia { get; set; }
        public int IdRomaneio { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> Abertura { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Final { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItems { get; set; }
    }
}

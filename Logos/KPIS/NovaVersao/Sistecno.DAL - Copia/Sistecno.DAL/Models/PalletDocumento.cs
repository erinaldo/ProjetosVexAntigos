using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PalletDocumento
    {
        public PalletDocumento()
        {
            this.PalletDocumentoItems = new List<PalletDocumentoItem>();
        }

        public int IdPalletDocumento { get; set; }
        public int IdPallet { get; set; }
        public int IdUADocumento { get; set; }
        public int IdDocumento { get; set; }
        public Nullable<int> IdRomaneio { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Final { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Pallet Pallet { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
        public virtual ICollection<PalletDocumentoItem> PalletDocumentoItems { get; set; }
        public virtual Romaneio Romaneio { get; set; }
    }
}

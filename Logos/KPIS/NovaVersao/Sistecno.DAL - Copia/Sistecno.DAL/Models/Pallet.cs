using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Pallet
    {
        public Pallet()
        {
            this.PalletDocumentoes = new List<PalletDocumento>();
        }

        public int IdPallet { get; set; }
        public Nullable<int> IdRomaneio { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<int> IdUsuarioCarregamento { get; set; }
        public Nullable<System.DateTime> DataDeAbertura { get; set; }
        public Nullable<System.DateTime> DataDeFechamento { get; set; }
        public Nullable<System.DateTime> DataDeAlteracao { get; set; }
        public Nullable<System.DateTime> InicioCarregamento { get; set; }
        public Nullable<System.DateTime> FinalCarregamento { get; set; }
        public Nullable<System.DateTime> ReAberturaDoPallet { get; set; }
        public Nullable<int> ReaberturaDoPalletIdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        public virtual ICollection<PalletDocumento> PalletDocumentoes { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
    }
}

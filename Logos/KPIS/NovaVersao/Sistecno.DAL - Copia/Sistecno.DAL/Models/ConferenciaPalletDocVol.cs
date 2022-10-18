using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPalletDocVol
    {
        public ConferenciaPalletDocVol()
        {
            this.ConferenciaPalletDocVolItems = new List<ConferenciaPalletDocVolItem>();
        }

        public int IdConferenciaPalletDocVol { get; set; }
        public int IdConferenciaPalletDoc { get; set; }
        public Nullable<int> IdVolume { get; set; }
        public virtual ConferenciaPalletDoc ConferenciaPalletDoc { get; set; }
        public virtual ICollection<ConferenciaPalletDocVolItem> ConferenciaPalletDocVolItems { get; set; }
    }
}

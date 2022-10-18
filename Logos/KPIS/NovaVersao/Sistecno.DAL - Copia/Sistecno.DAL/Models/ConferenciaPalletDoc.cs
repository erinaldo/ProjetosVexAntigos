using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPalletDoc
    {
        public ConferenciaPalletDoc()
        {
            this.ConferenciaPalletDocVols = new List<ConferenciaPalletDocVol>();
        }

        public int IdConferenciaPalletDoc { get; set; }
        public int IdConferenciaPallet { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public string Situacao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string pedidonotafiscal { get; set; }
        public virtual ConferenciaPallet ConferenciaPallet { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual ICollection<ConferenciaPalletDocVol> ConferenciaPalletDocVols { get; set; }
    }
}

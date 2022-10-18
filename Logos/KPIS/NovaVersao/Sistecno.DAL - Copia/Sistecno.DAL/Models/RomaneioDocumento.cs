using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioDocumento
    {
        public RomaneioDocumento()
        {
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
        }

        public int IDRomaneioDocumento { get; set; }
        public int IDRomaneio { get; set; }
        public int IDDocumento { get; set; }
        public Nullable<decimal> Volumes { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> Cubagem { get; set; }
        public Nullable<decimal> ValorDoDocumento { get; set; }
        public string Status { get; set; }
        public Nullable<int> IdDocumentoVerificado { get; set; }
        public Nullable<decimal> ValorDoFrete { get; set; }
        public Nullable<decimal> ValorDoFreteIcmsIss { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Documento Documento1 { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
    }
}

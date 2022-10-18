using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Gaiola
    {
        public Gaiola()
        {
            this.GaiolaConferencias = new List<GaiolaConferencia>();
        }

        public int IdGaiola { get; set; }
        public string Gaiola1 { get; set; }
        public string Filial { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime Data { get; set; }
        public string Impresso { get; set; }
        public Nullable<int> IdUsuarioRecebeu { get; set; }
        public Nullable<int> IdGaiolaLida { get; set; }
        public Nullable<System.DateTime> DataRecebimento { get; set; }
        public string PertenceAFilial { get; set; }
        public string VolumeInicial { get; set; }
        public Nullable<System.DateTime> DataFechamento { get; set; }
        public string Situacao { get; set; }
        public string NumeroColetor { get; set; }
        public string EMEI { get; set; }
        public Nullable<int> QtdVolumesLidos { get; set; }
        public virtual ICollection<GaiolaConferencia> GaiolaConferencias { get; set; }
    }
}

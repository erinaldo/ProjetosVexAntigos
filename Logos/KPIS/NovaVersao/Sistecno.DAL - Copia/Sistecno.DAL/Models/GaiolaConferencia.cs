using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class GaiolaConferencia
    {
        public int IdGaiolaConferencia { get; set; }
        public int IdGaiola { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Roteiro { get; set; }
        public string PertenceAFilial { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime Data { get; set; }
        public string Situacao { get; set; }
        public string NumeroColetor { get; set; }
        public string Emei { get; set; }
        public string Ativo { get; set; }
        public virtual Gaiola Gaiola { get; set; }
    }
}

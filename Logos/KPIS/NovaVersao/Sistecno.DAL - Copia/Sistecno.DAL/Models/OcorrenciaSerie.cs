using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OcorrenciaSerie
    {
        public OcorrenciaSerie()
        {
            this.OcorrenciaCodigoes = new List<OcorrenciaCodigo>();
        }

        public int IDOcorrenciaSerie { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<OcorrenciaCodigo> OcorrenciaCodigoes { get; set; }
    }
}

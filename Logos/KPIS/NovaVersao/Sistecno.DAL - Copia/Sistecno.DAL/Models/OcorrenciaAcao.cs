using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OcorrenciaAcao
    {
        public OcorrenciaAcao()
        {
            this.Ocorrencias = new List<Ocorrencia>();
        }

        public int IDOcorrenciaAcao { get; set; }
        public string Acao { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencias { get; set; }
    }
}

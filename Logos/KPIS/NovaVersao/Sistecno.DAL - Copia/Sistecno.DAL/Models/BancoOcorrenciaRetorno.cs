using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoOcorrenciaRetorno
    {
        public BancoOcorrenciaRetorno()
        {
            this.BancoOcorrenciaRejeicaos = new List<BancoOcorrenciaRejeicao>();
        }

        public int IDBancoOcorrenciaRetorno { get; set; }
        public int IDBanco { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string TipoRetorno { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual ICollection<BancoOcorrenciaRejeicao> BancoOcorrenciaRejeicaos { get; set; }
    }
}

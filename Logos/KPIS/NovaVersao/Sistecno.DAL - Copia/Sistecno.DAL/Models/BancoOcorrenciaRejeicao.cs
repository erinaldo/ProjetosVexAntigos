using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoOcorrenciaRejeicao
    {
        public int IDBancoOcorrenciaRejeicao { get; set; }
        public int IDBancoOcorrenciaRetorno { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public virtual BancoOcorrenciaRetorno BancoOcorrenciaRetorno { get; set; }
    }
}

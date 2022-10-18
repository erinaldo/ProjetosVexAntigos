using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoOcorrenciaRemessa
    {
        public BancoOcorrenciaRemessa()
        {
            this.TituloDuplicataRemessas = new List<TituloDuplicataRemessa>();
        }

        public int IDBancoOcorrenciaRemessa { get; set; }
        public int IDBanco { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Sistema { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual ICollection<TituloDuplicataRemessa> TituloDuplicataRemessas { get; set; }
    }
}

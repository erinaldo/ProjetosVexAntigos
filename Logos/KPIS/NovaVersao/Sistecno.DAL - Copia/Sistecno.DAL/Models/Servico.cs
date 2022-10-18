using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Servico
    {
        public Servico()
        {
            this.RomaneioDocumentoFretes = new List<RomaneioDocumentoFrete>();
        }

        public int IDServico { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<RomaneioDocumentoFrete> RomaneioDocumentoFretes { get; set; }
    }
}

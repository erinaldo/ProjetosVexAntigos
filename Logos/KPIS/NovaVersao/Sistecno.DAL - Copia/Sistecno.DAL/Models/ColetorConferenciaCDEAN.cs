using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ColetorConferenciaCDEAN
    {
        public int IdColetorConferenciaCDEAN { get; set; }
        public string CodigoBarras { get; set; }
        public string UnidadeVenda { get; set; }
        public string DescricaoUnidadeVenda { get; set; }
        public int QuantidadeUnidadeVenda { get; set; }
        public Nullable<System.DateTime> UltimaAtualizacao { get; set; }
    }
}

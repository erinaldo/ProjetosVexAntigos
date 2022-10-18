using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTLancamento
    {
        public int IdDtLancamento { get; set; }
        public string Nome { get; set; }
        public string DebitoCredito { get; set; }
        public string SolicitarDocumento { get; set; }
        public string TipoLancamento { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTFaturamentoClienteDocumento
    {
        public int IdDtFaturamentoClienteDocumento { get; set; }
        public int IdDtFaturamentoCliente { get; set; }
        public int IdDocumento { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual DTFaturamentoCliente DTFaturamentoCliente { get; set; }
    }
}

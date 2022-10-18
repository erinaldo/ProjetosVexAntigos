using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaItemNaoCadastrado
    {
        public int IdConferenciaItemNaoCadastrado { get; set; }
        public int IdConferencia { get; set; }
        public string CodigoDeBarras { get; set; }
        public decimal Quantidade { get; set; }
        public virtual Conferencia Conferencia { get; set; }
    }
}

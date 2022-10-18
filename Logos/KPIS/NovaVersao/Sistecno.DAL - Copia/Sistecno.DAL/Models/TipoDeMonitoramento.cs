using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TipoDeMonitoramento
    {
        public TipoDeMonitoramento()
        {
            this.DTs = new List<DT>();
        }

        public int IdTipoDeMonitoramento { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
    }
}

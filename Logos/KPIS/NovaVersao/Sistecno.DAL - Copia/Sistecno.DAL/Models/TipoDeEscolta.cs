using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TipoDeEscolta
    {
        public TipoDeEscolta()
        {
            this.DTs = new List<DT>();
        }

        public int IdTipoDeEscolta { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
    }
}

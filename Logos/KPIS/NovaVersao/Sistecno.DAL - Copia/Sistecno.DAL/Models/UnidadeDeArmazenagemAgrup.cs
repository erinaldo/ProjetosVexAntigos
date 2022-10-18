using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeDeArmazenagemAgrup
    {
        public int IDUnidadeDeArmazenagemAgrup { get; set; }
        public int IDUnidadeDeArmazenagem { get; set; }
        public int Agrupamento { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
    }
}

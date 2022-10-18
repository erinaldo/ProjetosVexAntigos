using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTTipo
    {
        public DTTipo()
        {
            this.DTs = new List<DT>();
        }

        public int IDDTTipo { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BairroFaixaDeCep
    {
        public int IDBairroFaixaDeCep { get; set; }
        public Nullable<int> IDBairro { get; set; }
        public string Inicial { get; set; }
        public string Final { get; set; }
        public virtual Bairro Bairro { get; set; }
    }
}

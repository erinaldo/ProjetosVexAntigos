using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CidadeSedex
    {
        public int IdCidadeSedex { get; set; }
        public Nullable<int> IdModal { get; set; }
        public Nullable<int> IdCidade { get; set; }
    }
}

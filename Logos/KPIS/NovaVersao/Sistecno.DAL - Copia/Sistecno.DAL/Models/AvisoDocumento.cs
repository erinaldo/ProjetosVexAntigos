using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class AvisoDocumento
    {
        public int IdAvisoDocumento { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdUsuario { get; set; }
    }
}

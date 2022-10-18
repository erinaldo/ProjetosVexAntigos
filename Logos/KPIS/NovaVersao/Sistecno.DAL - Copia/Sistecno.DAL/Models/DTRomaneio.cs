using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTRomaneio
    {
        public int IDDTRomaneio { get; set; }
        public int IDDT { get; set; }
        public int IDRomaneio { get; set; }
        public virtual DT DT { get; set; }
        public virtual Romaneio Romaneio { get; set; }
    }
}

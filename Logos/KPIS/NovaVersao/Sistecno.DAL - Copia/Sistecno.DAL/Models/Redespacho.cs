using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Redespacho
    {
        public int IDRedespacho { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}

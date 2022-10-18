using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MotoristaFilial
    {
        public int IDMotoristaFilial { get; set; }
        public int IDMotorista { get; set; }
        public int IDFilial { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Motorista Motorista { get; set; }
    }
}

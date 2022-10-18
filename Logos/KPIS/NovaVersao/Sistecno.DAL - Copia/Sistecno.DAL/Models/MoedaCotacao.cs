using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MoedaCotacao
    {
        public int IDMoedaCotacao { get; set; }
        public int IDMoeda { get; set; }
        public System.DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public virtual Moeda Moeda { get; set; }
    }
}

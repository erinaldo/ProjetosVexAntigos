using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FilialPortaria
    {
        public int IdFilialPortaria { get; set; }
        public int IdFilialPai { get; set; }
        public int IdFilialFilho { get; set; }
    }
}

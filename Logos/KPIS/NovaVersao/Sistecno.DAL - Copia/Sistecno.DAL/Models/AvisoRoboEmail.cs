using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class AvisoRoboEmail
    {
        public int IDAvisoKPI { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Horas { get; set; }
        public string Reports { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Operacao
    {
        public int IdOperacao { get; set; }
        public string Operacao1 { get; set; }
        public string CentroDeCusto { get; set; }
        public string Habilitado { get; set; }
    }
}

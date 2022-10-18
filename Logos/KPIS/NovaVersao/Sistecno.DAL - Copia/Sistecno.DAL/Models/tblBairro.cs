using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class tblBairro
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string BairroAbreviado { get; set; }
        public string UF { get; set; }
        public int CodigoCidade { get; set; }
    }
}

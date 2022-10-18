using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Dicionario
    {
        public string TABELA { get; set; }
        public string CAMPO { get; set; }
        public string OBRIGATORIO { get; set; }
        public string CONTROLADO { get; set; }
        public string Descricao { get; set; }
    }
}

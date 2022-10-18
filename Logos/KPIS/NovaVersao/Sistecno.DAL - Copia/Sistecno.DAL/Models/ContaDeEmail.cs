using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDeEmail
    {
        public int IdContaDeEmail { get; set; }
        public string Operacao { get; set; }
        public string De { get; set; }
        public string DeApelido { get; set; }
        public string Senha { get; set; }
        public string SMTP { get; set; }
        public Nullable<int> Porta { get; set; }
        public string Para { get; set; }
        public string CCopia { get; set; }
    }
}

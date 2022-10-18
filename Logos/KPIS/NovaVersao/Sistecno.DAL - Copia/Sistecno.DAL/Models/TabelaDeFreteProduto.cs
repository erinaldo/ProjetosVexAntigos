using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TabelaDeFreteProduto
    {
        public int IdTabelaDeFreteProduto { get; set; }
        public int IdTabelaDeFrete { get; set; }
        public string Acesso { get; set; }
        public string Nome { get; set; }
        public virtual TabelaDeFrete TabelaDeFrete { get; set; }
    }
}

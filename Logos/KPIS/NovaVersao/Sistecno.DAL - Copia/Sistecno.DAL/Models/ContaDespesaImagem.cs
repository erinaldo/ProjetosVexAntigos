using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesaImagem
    {
        public int IdContaDespesaImagem { get; set; }
        public int IdContaDespesa { get; set; }
        public string Nome { get; set; }
        public byte[] Imagem { get; set; }
        public virtual ContaDespesa ContaDespesa { get; set; }
    }
}

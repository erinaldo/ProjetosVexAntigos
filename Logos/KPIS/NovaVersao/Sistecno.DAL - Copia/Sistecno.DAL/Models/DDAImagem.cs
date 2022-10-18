using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DDAImagem
    {
        public int IdDDAImagem { get; set; }
        public int IdDDA { get; set; }
        public byte[] Imagem { get; set; }
        public virtual DDA DDA { get; set; }
    }
}

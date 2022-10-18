using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RPCIImagem
    {
        public int IdRpciImagem { get; set; }
        public int IdRpci { get; set; }
        public string Titulo { get; set; }
        public byte[] Imagem { get; set; }
        public virtual RPCI RPCI { get; set; }
    }
}

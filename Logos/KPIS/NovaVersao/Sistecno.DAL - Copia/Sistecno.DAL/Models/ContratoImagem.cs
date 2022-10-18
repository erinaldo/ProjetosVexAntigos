using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContratoImagem
    {
        public int IdContratoImagem { get; set; }
        public int IdContrato { get; set; }
        public string Nome { get; set; }
        public byte[] Imagem { get; set; }
        public virtual Contrato Contrato { get; set; }
    }
}

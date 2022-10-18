using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloImagem
    {
        public int IdTituloImagem { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public string Nome { get; set; }
        public byte[] Imagem { get; set; }
        public virtual Titulo Titulo { get; set; }
    }
}

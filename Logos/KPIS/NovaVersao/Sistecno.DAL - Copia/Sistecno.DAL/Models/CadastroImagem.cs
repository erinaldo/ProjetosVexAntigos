using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroImagem
    {
        public int IDCadastroImagem { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public byte[] Imagem { get; set; }
        public string Nome { get; set; }
        public string TipoImagem { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}

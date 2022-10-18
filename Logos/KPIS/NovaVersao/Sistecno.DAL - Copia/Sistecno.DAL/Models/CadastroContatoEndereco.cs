using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroContatoEndereco
    {
        public int IDCadastroContatoEndereco { get; set; }
        public int IDCadastro { get; set; }
        public int IDCadastroTipoDeContato { get; set; }
        public string Endereco { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual CadastroTipoDeContato CadastroTipoDeContato { get; set; }
    }
}
